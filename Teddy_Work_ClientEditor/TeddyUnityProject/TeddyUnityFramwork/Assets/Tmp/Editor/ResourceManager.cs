using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Teddy {
    public class ResourceManager : Singleton<ResourceManager> {
        private const string _defaultProjectTag = "TeddyFramework";
        private string _defaultGameObjectName = "AssetBundleManager";
        private Dictionary<string, AssetBundleManager> _assetBundleManagerDic = new Dictionary<string, AssetBundleManager>();

        private ResourceManager() { }

        public void Init(bool isAsync = false, string projectTag = null, Action<bool> action = null) { // 初始化加载manifest的同步方式
            projectTag = projectTag == null ? _defaultProjectTag : projectTag;
            string objectName = _defaultGameObjectName + "_" + projectTag;
            if (!_assetBundleManagerDic.ContainsKey(projectTag)) {
                GameObject gameObject = new GameObject(objectName, typeof(AssetBundleManager));
                //gameObject.transform.parent = transform; // hh
                _assetBundleManagerDic.Add(projectTag, gameObject.transform.GetComponent<AssetBundleManager>());
                if (isAsync) {
                    //StartCoroutine(InitAsync(action, projectTag, _assetBundleManagerDic[projectTag])); // hh
                } else {
                    _assetBundleManagerDic[projectTag].InitializeSync(projectTag);
                }
            }
        }

        protected IEnumerator InitAsync(Action<bool> action, string projectTag, AssetBundleManager manager) {
            var request = manager.InitializeAsync(projectTag);
            if (request != null) {
                //yield return StartCoroutine(request); // hh
                if (action != null) {
                    action(true);
                }
            } else {
#if UNITY_EDITOR
                if (action != null) {
                    action(true);
                }
#endif
            }
            return null;
        }

        private AssetBundleManager GetManager(string projectTag) {
            if (projectTag == null) {
                return _assetBundleManagerDic[_defaultProjectTag];
            }
            if (_assetBundleManagerDic.ContainsKey(projectTag))
                return _assetBundleManagerDic[projectTag];
            return null;
        }

        /// <summary>
        /// 加载AssetBundle 同步方式
        /// </summary>
        /// <returns>The asset.</returns>
        /// <param name="assetBundleName">Asset bundle name.</param>
        /// <param name="assetName">Asset name.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T LoadAsset<T>(string assetBundleName, string assetName, string projectTag = null) where T : UnityEngine.Object {
            assetBundleName = assetBundleName.ToLower();
            if (string.IsNullOrEmpty(projectTag)) {

                projectTag = _defaultProjectTag;
            }
            assetBundleName = RemapAssetBundleName(assetBundleName, projectTag);
            return GetManager(projectTag).LoadAsset<T>(assetBundleName, assetName, typeof(T));
        }

        /// <summary>
        /// 加载Asset 异步方式
        /// </summary>
        /// <param name="assetBundleName">Asset bundle name.</param>
        /// <param name="assetName">Asset name.</param>
        /// <param name="action">Action.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void LoadAssetAsync<T>(string assetBundleName, string assetName, Action<bool, T> action, string projectTag = null) where T : UnityEngine.Object {
            if (string.IsNullOrEmpty(projectTag)) {

                projectTag = _defaultProjectTag;
            }
            assetBundleName = assetBundleName.ToLower();
            assetBundleName = RemapAssetBundleName(assetBundleName, projectTag);
            //StartCoroutine(LoadFromAssetBundleAsync(assetBundleName, assetName, action, projectTag)); // hh
        }

        /// <summary>
        /// 异步加载AssetBundle
        /// </summary>
        /// <param name="assetBundleName">Asset bundle name.</param>
        /// <param name="action">Action.</param>
        public void LoadAssetBundleAsync(string assetBundleName, Action<bool, AssetBundle> action, string projectTag = null) {
            if (string.IsNullOrEmpty(projectTag)) {

                projectTag = _defaultProjectTag;
            }
            assetBundleName = assetBundleName.ToLower();
            assetBundleName = RemapAssetBundleName(assetBundleName, projectTag);

            if (GetManager(projectTag).IsEditorSimulate()) {

                AssetBundle ab = GetManager(projectTag).LoadAssetBundleSync(assetBundleName, false);
                if (action != null) {
                    action(ab == null ? false : true, ab);
                }
            } else {
                //StartCoroutine(LoadFromAssetBundleAsync<AssetBundle>(assetBundleName, null, action, projectTag)); // hh
            }
        }

        public void LoadAB(string assetBundleName, string projectTag = null) {
            LoadAssetBundle(assetBundleName);
        }
        /// <summary>
        /// 同步加载AssetBundle
        /// </summary>
        /// <param name="assetBundleName">Asset bundle name.</param>
        public void LoadAssetBundle(string assetBundleName, string projectTag = null) {
            if (string.IsNullOrEmpty(projectTag)) {

                projectTag = _defaultProjectTag;
            }
            assetBundleName = assetBundleName.ToLower();
            assetBundleName = RemapAssetBundleName(assetBundleName, projectTag);
            GetManager(projectTag).LoadAssetBundleSync(assetBundleName, false);
        }

        private IEnumerator LoadFromAssetBundleAsync<T>(string assetBundleName, string assetName, Action<bool, T> action, string projectTag = null) where T : UnityEngine.Object {
            assetBundleName = assetBundleName.ToLower();

            float startTime = Time.realtimeSinceStartup;
            AssetBundleLoadAssetOperation request = GetManager(projectTag).LoadAssetAsync(assetBundleName, assetName, typeof(T));
            if (request == null) {
                yield break;
            }
            //yield return StartCoroutine(request); // hh

            T prefab = request.GetAsset<T>();
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            Debug.Log(assetName + (prefab == null ? " was not" : " was") + " loaded successfully in " + elapsedTime + " seconds");
            if (action != null) {
                action(prefab == null ? false : true, prefab);
            }
        }

        /// <summary>
        /// 卸载AssetBundle
        /// </summary>
        /// <param name="assetBundleName">Asset bundle name.</param>
        /// <param name="force">If set to <c>true</c> force.</param>
        public void UnloadAssetBundle(string assetBundleName, bool force, string projectTag = null) {
            if (string.IsNullOrEmpty(projectTag)) {

                projectTag = _defaultProjectTag;
            }
            assetBundleName = assetBundleName.ToLower();
            assetBundleName = RemapAssetBundleName(assetBundleName, projectTag);

            GetManager(projectTag).UnloadAssetBundle(assetBundleName, force);
        }

        /// <summary>
        /// Gets the AB download progress.
        /// </summary>
        /// <returns>The AB download progress.</returns>
        /// <param name="assetBundleName">Asset bundle name.</param>
        public float GetABDownloadProgress(string assetBundleName, string projectTag = null) {
            if (string.IsNullOrEmpty(projectTag)) {

                projectTag = _defaultProjectTag;
            }
            assetBundleName = assetBundleName.ToLower();
            assetBundleName = RemapAssetBundleName(assetBundleName, projectTag);

            return GetManager(projectTag).GetDownLoadProgress(assetBundleName);
        }
        /// <summary>
        /// 强制卸载所有assetbundle
        /// </summary>
        public void ForceUnloadAllAssetBundle(string projectTag = null) {
            if (string.IsNullOrEmpty(projectTag)) {

                projectTag = _defaultProjectTag;
            }
            var manager = GetManager(projectTag);
            if (manager == null) {
                return;
            }

            manager.ForceUnloadAll();

            //			string objectName = default_projecttag;
            //			if (projectTag != null) {
            //				objectName =  projectTag;
            //			}
            //Destroy(_assetBundleManagerDic[projectTag].gameObject); // hh
            _assetBundleManagerDic.Remove(projectTag);

        }

        private string RemapAssetBundleName(string abName, string projectTag) {
            if (!string.IsNullOrEmpty(projectTag) && projectTag != abName) {
                return abName + "_project_" + projectTag;
            }
            return abName;
        }

        void Update() {

        }
    }
}