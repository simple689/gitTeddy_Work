using System;
using System.Net.Sockets;
using System.Text;
using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;
using UnityEngine;

class ClientSDK : EngineBase_Socket {
    #region Controller
    LogController_Unity _logController_Unity = null;
    RequestSocketController _requestSocketController = null;
    ListenSocketController _listenSocketController = null;
    #endregion

    int _listenPort;

    public override void init(EngineBase controller) {
        initLog_Unity();
        base.init(controller);
        initResPath_Unity();

        _host = "127.0.0.1";
        _hostPort = 6989;
        initCommonConfig();
        initHostType();
        initServerConfigBase();
        initRequestSocket();
        initListenSocket();
    }

    public override void start() {
        base.start();

        _requestSocketController.start();
    }

    public override void update() {
        base.update();
    }

    public override void stop() {
        base.stop();
        _requestSocketController.stop();
        _listenSocketController.stop();
    }

    public override void socketReceiveCmd(SocketAsyncEventArgs ioContext, SocketCmdType socketCmdType, string socketCmdStr) {
        try {
            switch (socketCmdType) {
                case SocketCmdType.ConnectSuccess: {
                    string sendStr = "请求监听端口";
                    socketSend(ioContext, SocketCmdType.ListenPort, sendStr);
                }
                break;
                case SocketCmdType.ListenPort: {
                    string sendStr = "6990";
                    _listenPort = Convert.ToInt32(sendStr);
                    initListenSocket();
                }
                break;
                default:
                break;
            }
            //Data_SocketCmd data_SocketCmd = _controllerManagerr._jsonController.deserializeStrToObject<Data_SocketCmd>(socketCmdStr);
            //_controllerManagerr._dataController._dataDict.Add("Data_SocketCmd", data_SocketCmd);
            //_controllerManagerr.callBackLogPrint(data_SocketCmd._socketCmdType.ToString());
        } catch (Exception e) {
            callBackLogPrint(e.Message);
        }
    }

    #region init
    void initLog_Unity() {
        Debug.Log("/* 初始化Log_Unity */");
        try {
            _logController_Unity = new LogController_Unity();
            _logController_Unity.init(this);
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }

    void initResPath_Unity() {
        callBackLogPrint("/* 初始化资源路径_Unity */");
        try {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(_resController._runPath)
                      .Append("/")
                      .Append(ResSubDir.Assets)
                      .Append("/")
                      .Append(ResSubDir.Product_ClientSDK);
            _resController._resPath = strBuilder.ToString();
            callBackLogPrint("资源路径 = " + _resController._resPath);
        } catch (Exception e) {
            callBackLogPrint(e.Message);
        }
    }

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

    void initServerConfigBase() {
        try {
            string path = _resController.getResPathAbsolute(_resController._resPath,
                                                            ResSubDir.Config,
                                                            ResNamePrefix.SocketConfig,
                                                            SocketConfigType.ClientSDK.ToString(),
                                                            ResNamePostfix.None,
                                                            ResType.json);
            callBackLogPrint(path);
            string file = _fileController.readFile(path);
            callBackLogPrint(file);
            var data = _jsonController.deserializeStrToObject<DataFile_SocketConfig_ClientSDK>(file);
            _dataFileController.addData<DataFile_SocketConfig_ClientSDK>(data);
        } catch (Exception e) {
            callBackLogPrint(e.Message);
        }
    }

    public void initRequestSocket() {
        try {
            var data = _dataFileController.getData<DataFile_SocketConfig_ClientSDK>();
            _requestSocketController = new RequestSocketController();
            _requestSocketController.init(this,
                                          _host,
                                          _hostPort,
                                          data._requestSocket._bufferSize);
        } catch (Exception e) {
            callBackLogPrint(e.Message);
        }
    }

    public void initListenSocket() {
        try {
            var data = _dataFileController.getData<DataFile_SocketConfig_ClientSDK>();
            _listenSocketController = new ListenSocketController();
            _listenSocketController.init(this,
                                         _listenPort,
                                         data._listenSocket._maxConnectionNum,
                                         data._listenSocket._bufferSize);
        } catch (Exception e) {
            callBackLogPrint(e.Message);
        }
    }
    #endregion
}