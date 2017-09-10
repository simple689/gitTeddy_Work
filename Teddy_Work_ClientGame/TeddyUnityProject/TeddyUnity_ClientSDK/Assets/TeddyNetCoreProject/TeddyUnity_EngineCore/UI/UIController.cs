using System;
using TeddyNetCore_EngineCore;
using UnityEngine;

namespace TeddyUnity_EngineCore {
    public class UIController {
        EngineBase _controller;

        UIRoot nguiRoot;

        public GameObject scenePrefab;
        //private GameObject scenePrefabObj;

        public void init(EngineBase controller) {
            _controller = controller;

            nguiRoot = GameObject.FindObjectOfType<UIRoot>();             initUIConfig();         }

        public void start() {
            //if (scenePrefabObj == null) {
            //    scenePrefabObj = Instantiate(scenePrefab);
            //    Vector3 localScale = scenePrefabObj.transform.localScale;
            //    scenePrefabObj.transform.parent = nguiRoot.transform;
            //    scenePrefabObj.transform.localScale = localScale;
            //}
        }

        public void update() {
        }

        public void stop() {
        }

        #region init
        void initUIConfig() {
            try {
                //string path = _controller._resController.getResPathAbsolute(_controller._resController._resPath,
                //                                                        ResSubDir.Config,
                //                                                ResNamePrefix.SocketConfig,
                //                                                SocketConfigType.ClientSDK.ToString(),
                //                                                ResNamePostfix.None,
                //                                                ResType.json);
                //_controller.callBackLogPrint(path);
                //string file = _controller._fileController.readFile(path);
                //_controller.callBackLogPrint(file);
                //var data = _controller._jsonController.deserializeStrToObject<DataFile_UIConfig>(file);
                //_controller._dataFileController.addData<DataFile_UIConfig>(data);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }
        #endregion
    }
}
