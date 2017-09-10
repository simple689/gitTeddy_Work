using UnityEngine;
using UnityEditor;
using System.IO;

namespace Teddy {
    public class AssetBundleEditor : EditorWindow {
        private Vector2 _scrollPos;

        private int _buildPlatformIndex = 0;
        private string[] _platformLabels = new string[] { "Windows", "IOS", "Android" };

        public static bool _isEnableGenerateClass = false;
        public static string _resVersion = "100";
        private static string _projectTag = "teddy";

        private const string KEY_PTAssetBundleBuilder_RESVERSION = "KEY_PTAssetBundleBuilder_RESVERSION";
        private const string KEY_AUTOGENERATE_CLASS = "KEY_AUTOGENERATE_CLASS";
        private const string KEY_ProjectTag = "KEY_ProjectTag";
        private const string KEY_ZipFramework = "KEY_ZipFramework";
        //public static bool isUseFramework = true;

        [MenuItem("TeddyTool/AssetBundle/Build")]
        public static void abBuild() {
            AssetBundleEditor window = (AssetBundleEditor)GetWindow(typeof(AssetBundleEditor), true);
            window.position = new Rect(200, 100, 500, 400);
            window.Show();
        }

        void OnEnable() {
            switch (EditorUserBuildSettings.activeBuildTarget) {
                case BuildTarget.StandaloneWindows: {
                    _buildPlatformIndex = 0;
                    break;
                }
                case BuildTarget.iOS: {
                    _buildPlatformIndex = 1;
                    break;
                }
                case BuildTarget.Android: {
                    _buildPlatformIndex = 2;
                    break;
                }
                default: {
                    _buildPlatformIndex = 0;
                    break;
                }
            }

            _resVersion = EditorPrefs.GetString(KEY_PTAssetBundleBuilder_RESVERSION, "100");
            _isEnableGenerateClass = EditorPrefs.GetBool(KEY_AUTOGENERATE_CLASS, true);
            _projectTag = EditorPrefs.GetString(KEY_ProjectTag, "");
            //isUseFramework = EditorPrefs.GetBool (KEY_ZipFramework,true);
        }

        void OnGUI() {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Width(500), GUILayout.Height(400));
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            GUILayout.Label("PersistentPath:");
            GUILayout.TextField(Application.persistentDataPath);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Go To Persistance")) {
                EditorUtility.RevealInFinder(Application.persistentDataPath);
            }
            DrawMenu();
            DrawAssetBundleList();
            _isEnableGenerateClass = GUILayout.Toggle(_isEnableGenerateClass, "auto generate class");

            GUILayout.BeginHorizontal();
            GUILayout.Label("ResVersion:");
            _resVersion = GUILayout.TextField(_resVersion);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Project Tag:");
            _projectTag = GUILayout.TextField(_projectTag);
            //isUseFramework = GUILayout.Toggle (isUseFramework, "zip the framework");
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Build")) {
                BuildWithTarget(EditorUserBuildSettings.activeBuildTarget);
            }
            if (GUILayout.Button("ForceClear")) {
                ForceClear();
            }
            GUILayout.EndVertical();
            GUILayout.Space(50);
            EditorGUILayout.EndScrollView();
        }

        void DrawMenu() {
            GUILayout.Toolbar(_buildPlatformIndex, _platformLabels);
        }

        void DrawAssetBundleList() {
            //			GUILayout.BeginVertical ();
            //
            //			List<MarkItem> nodelist = PTConfigManager.Instance.markItems;
            //			if (nodelist != null) {
            //				for (int i = 0; i < nodelist.Count; i++) {
            //					EditorGUILayout.LabelField (nodelist [i].path, new GUIStyle (EditorStyles.helpBox){ fontSize = 13 }, GUILayout.Width (400), GUILayout.Height (30));
            //				}
            //			}
            //
            //			GUILayout.EndVertical ();
        }

        //		public void OnFocus ()
        //		{
        //			PTConfigManager.Instance.CheckItems ();
        //		}

        public static void ForceClear() {
            if (Directory.Exists(AssetBundleTool.AssetBundlesOutputPath)) {
                Directory.Delete(AssetBundleTool.AssetBundlesOutputPath, true);
            }
            if (Directory.Exists(Application.streamingAssetsPath + "/AssetBundles")) {
                Directory.Delete(Application.streamingAssetsPath + "/AssetBundles", true);
            }
            AssetDatabase.Refresh();
        }

        public void OnDisable() {
            EditorPrefs.SetBool(KEY_AUTOGENERATE_CLASS, _isEnableGenerateClass);
            EditorPrefs.SetString(KEY_PTAssetBundleBuilder_RESVERSION, _resVersion);
            EditorPrefs.SetString(KEY_ProjectTag, _projectTag);
            //EditorPrefs.SetBool (KEY_ZipFramework,isUseFramework);
            //			PTConfigManager.Instance.Dispose ();
        }

        void BuildWithTarget(BuildTarget buildTarget) {
            //			List<MarkItem> nodelist = PTConfigManager.Instance.markItems;
            //			for (int i = 0; i < nodelist.Count; i++) {
            //				AssetImporter ai = AssetImporter.GetAtPath (nodelist [i].path);
            //				ai.assetBundleName = nodelist [i].name;
            //				Debug.Log (ai.assetBundleName);
            //			}
            //			AssetDatabase.Refresh ();

            //			PTConfigManager.Instance.SaveConfigFile ();
            //PTAssetBundleTool.SetProjectTag();
            AssetDatabase.RemoveUnusedAssetBundleNames();
            AssetDatabase.Refresh();
            BuildScript.BuildAssetBundles(buildTarget, _projectTag);
        }
    }
}