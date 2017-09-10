using System;
using System.Net.Sockets;
using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;
using TeddyUnity_EngineCore;
using UnityEngine;

class ClientSDK : EngineBase_Socket {
    #region Controller
    LogController_Unity _logController_Unity = null;
    UIController _uiController = null;
    PlotController _plotController = null;
    RequestSocketController _requestSocketController = null;
    ListenSocketController _listenSocketController = null;
    #endregion

    int _listenPort;

    public override void init(EngineBase controller, string resSubDir) {
        initLog_Unity();
        base.init(controller, resSubDir);

        initSocketConfig();

        initUI();
        initPlot();
        initRequestSocket();
        initListenSocket();
    }

    public override void start() {
        base.start();

        _requestSocketController.start();
        _plotController.start();
    }

    public override void update() {
        base.update();
        _plotController.update();
        _uiController.update();
    }

    public override void stop() {
        base.stop();
        _plotController.stop();
        _uiController.stop();
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

    void initSocketConfig() {
        callBackLogPrint("/* 初始化SocketConfig */");
        try {
            string path = _resController.getResPathAbsolute(_resController._resPath,
                                                            ResSubDir.Config,
                                                            ResNamePrefix.SocketConfig,
                                                            SocketConfigType.ClientSDK.ToString(),
                                                            ResNamePostfix.None,
                                                            ResType.json);
            callBackLogPrint("SocketConfig路径 = " + path);
            string file = _fileController.readFile(path);
            callBackLogPrint("SocketConfig内容 = \n" + file);
            var data = _jsonController.deserializeObject<DataFile_SocketConfig_ClientSDK>(file);
            _dataFileController.addData<DataFile_SocketConfig_ClientSDK>(data);
        } catch (Exception e) {
            callBackLogPrint(e.Message);
        }
    }

    void initUI() {
        try {
            _uiController = new UIController();
            _uiController.init(this);
        } catch (Exception e) {
            callBackLogPrint(e.Message);
        }
    }

    void initPlot() {
        try {
            _plotController = new PlotController();
            _plotController.init(this);
        } catch (Exception e) {
            callBackLogPrint(e.Message);
        }
    }

    void initRequestSocket() {
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

    void initListenSocket() {
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