using System;
using System.Net.Sockets;
using TeddyNetCore_EngineEnum;
using TeddyNetCore_Engine;
using TeddyNetCore_EngineData;

namespace TeddyNetCore_ServerPassport {
    class ServerPassport : EngineBaseServer {
        #region Controller
        RequestSocketController _requestSocketController = null;
        ListenSocketController _listenSocketController = null;
        #endregion

        int _listenPort;

        public override void init(EngineBase controller) {
            base.init(controller);

            initServerConfigCommon();
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
        void initServerConfigBase() {
            try {
                string path = _resController.getResPathAbsolute(_resController._runPath,
                                                                ResSubDir.Config,
                                                                ResNamePrefix.ServerConfig,
                                                                ServerType.ServerPassport.ToString(),
                                                                ResNamePostfix.None,
                                                                ResType.json);
                callBackLogPrint(path);
                string file = _fileController.readFile(path);
                callBackLogPrint(file);
                var data = _jsonController.deserializeStrToObject<DataFile_ServerConfig_ServerPassport>(file);
                _dataFileController.addData<DataFile_ServerConfig_ServerPassport>(data);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        public void initRequestSocket() {
            try {
                var data = _dataFileController.getData<DataFile_ServerConfig_ServerPassport>();
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
                var data = _dataFileController.getData<DataFile_ServerConfig_ServerPassport>();
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
}
