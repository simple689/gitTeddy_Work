using System;
using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;

namespace TeddyNetCore_Engine {
    public abstract class EngineBase_Server : EngineBase_Socket {
        public override void init(EngineBase controller) {
            base.init(controller);

            initCommonConfig();
            initHostType();
        }

        #region init
        void initCommonConfig() {
            callBackLogPrint("/* 初始化CommonConfig */");
            try {
                string path = _resController.getResPathAbsolute(_resController._resPath,
                                                                ResSubDir.Config,
                                                                ResNamePrefix.CommonConfig,
                                                                ResType.json);
                callBackLogPrint("CommonConfig路径 = " + path);
                string file = _fileController.readFile(path);
                callBackLogPrint("CommonConfig内容 = ");
                callBackLogPrint(file);
                var data = _jsonController.deserializeStrToObject<DataFile_CommonConfig>(file);
                _dataFileController.addData<DataFile_CommonConfig>(data);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        void initHostType() {
            callBackLogPrint("/* 初始化HostType */");
            try {
                var data = _dataFileController.getData<DataFile_CommonConfig>();
                _host = data._server._hostLocal;
                _hostPort = data._server._hostPort;
                string cmdStr = _mainCmdDict[MainCmdType.HostType];
                HostType hostType = (HostType)Enum.Parse(typeof(HostType), cmdStr, true);
                switch (hostType) {
                    case HostType.Lan:
                        _host = data._server._hostLan;
                        break;
                    case HostType.Wan:
                        _host = data._server._hostWan;
                        break;
                    case HostType.Local:
                        _host = data._server._hostLocal;
                        break;
                    default:
                        callBackLogPrint("HostType is error.");
                        break;

                }
                callBackLogPrint("_host = " + _host);
                callBackLogPrint("_hostPort = " + _hostPort);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }
        #endregion
    }
}
