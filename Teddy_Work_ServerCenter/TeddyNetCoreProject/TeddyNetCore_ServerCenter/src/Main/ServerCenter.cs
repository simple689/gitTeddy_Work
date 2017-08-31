using System.Net.Sockets;
using TeddyNetCore_EngineEnum;
using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;
using System;
using TeddyNetCore_Engine;

namespace TeddyNetCore_ServerCenter {
    class ServerCenter : EngineBase_Socket {
        #region Controller
        MySqlController _mySqlController = null;
        ListenSocketController _listenSocketController = null;
        #endregion

        public override void init(EngineBase controller) {
            base.init(controller);

            initSocketConfig();
            initMySql();
            initListenSocket();
        }

        public override void start() {
            base.start();

            startMySql();
            startListenSocket();
        }

        public override void update() {
            base.update();
        }

        public override void stop() {
            base.stop();

            stopListenSocket();
        }

        public override void socketReceiveCmd(SocketAsyncEventArgs ioContext, SocketCmdType socketCmdType, string socketCmdStr) {
            try {
                switch (socketCmdType) {
                    case SocketCmdType.ListenPort: {
                            string sendStr = "返回监听端口";
                            socketSend(ioContext, SocketCmdType.ListenPort, sendStr);
                        }
                        break;
                    default:
                        break;
                }
                //Data_SocketCmd data_SocketCmd = _controllerManager._jsonController.deserializeStrToObject<Data_SocketCmd>(socketCmdStr);
                //_controllerManager._dataController._dataDict.Add("Data_SocketCmd", data_SocketCmd);
                //_controllerManager.callBackLogPrint(data_SocketCmd._socketCmdType.ToString());
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        #region init
        void initSocketConfig() {
            callBackLogPrint("/* 初始化ServerConfig_ServerCenter */");
            try {
                string path = _resController.getResPathAbsolute(_resController._resPath,
                                                                ResSubDir.Config,
                                                                ResNamePrefix.SocketConfig,
                                                                SocketConfigType.ServerCenter.ToString(),
                                                                ResNamePostfix.None,
                                                                ResType.json);
                callBackLogPrint("ServerConfig_ServerCenter路径 = " + path);
                string file = _fileController.readFile(path);
                callBackLogPrint("ServerConfig_ServerCenter内容 = ");
                callBackLogPrint(file);
                var data = _jsonController.deserializeStrToObject<DataFile_SocketConfig_ServerCenter>(file);
                _dataFileController.addData<DataFile_SocketConfig_ServerCenter>(data);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        void initMySql() {
            callBackLogPrint("/* 初始化MySql */");
            try {
                _mySqlController = new MySqlController();
                _mySqlController.init(this);
                var data = _dataFileController.getData<DataFile_SocketConfig_ServerCenter>();
                _mySqlController.setConnectionStr(_host,
                                                  data._mySql._port,
                                                  data._mySql._user,
                                                  data._mySql._password,
                                                  data._mySql._dataBase,
                                                  data._mySql._other);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        void initListenSocket() {
            callBackLogPrint("/* 初始化ListenSocket */");
            try {
                var data = _dataFileController.getData<DataFile_SocketConfig_ServerCenter>();
                _listenSocketController = new ListenSocketController();
                _listenSocketController.init(this,
                                             _hostPort,
                                             data._listenSocket._maxConnectionNum,
                                             data._listenSocket._bufferSize);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }
        #endregion

        #region start
        void startMySql() {
            callBackLogPrint("/* 查询MySql */");
            try {
                string sqlStr = "SELECT * FROM Teddy_Work_Project_201706.User;";
                _mySqlController.ExecuteNonQuery(sqlStr);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        void startListenSocket() {
            callBackLogPrint("/* 开启ListenSocket */");
            try {
                _listenSocketController.start();
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }
        #endregion

        #region stop
        void stopListenSocket() {
            callBackLogPrint("/* 停止ListenSocket */");
            try {
                _listenSocketController.stop();
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }
        #endregion
    }
}
