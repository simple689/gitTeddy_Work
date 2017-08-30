using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TeddyNetCore_Engine;
using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;

namespace TeddyNetCore_ServerManager {
    class ServerManager : EngineBase_Server {
        #region Controller
        RequestSocketController _requestSocketController = null;
        ListenSocketController _listenSocketController = null;
        #endregion

        int _listenPort;
        Thread threadRequestSocket;
        Thread threadListenSocket;
        Thread thread;

        public override void init(EngineBase controller) {
            base.init(controller);

            initServerConfigBase();
            initRequestSocket();
            initListenSocket();

            //thread = new Thread(consoleCmd);
            //thread.Start();
            //consoleCmd();
        }

        public override void start() {
            base.start();

            threadRequestSocket = new Thread(_requestSocketController.start);
            threadRequestSocket.Start();
            //_requestSocketController.start();
        }

        public override void update() {
            base.update();

            Console.Write("->");
            string cmd = Console.ReadLine();
            Console.WriteLine("->" + cmd);

            Console.WriteLine("==============================================================");
            Console.WriteLine("/* 解析命令 */");
            String[] strAry = cmd.Split(' ');
            for (int index = 0; index < strAry.Length;) {
                Console.WriteLine(strAry[index]);
                int readNum = 1;
                if (strAry[index].StartsWith("-")) {
                    StringBuilder strBuilder = new StringBuilder();
                    for (int readIndex = index + readNum; readIndex < strAry.Length; readIndex++) {
                        if (strAry[readIndex].StartsWith("-")) {
                            break;
                        }
                        strBuilder.Append(strAry[readIndex])
                                  .Append(" ");
                        readNum++;
                    }
                    string cmdStr = strBuilder.ToString();
                    Console.WriteLine(cmdStr);
                    addMainCmd(strAry[index].Substring(1), cmdStr.Substring(0, cmdStr.Length - 1));
                }
                index += readNum;
            }
            Console.WriteLine("==============================================================");
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
                //_dataController._dataDict.Add("Data_SocketCmd", data_SocketCmd);
                //callBackLogPrint(data_SocketCmd._socketCmdType.ToString());
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        #region init
        void initServerConfigBase() {
            callBackLogPrint("/* 初始化ServerConfig_ServerCenter */");
            try {
                string path = _resController.getResPathAbsolute(_resController._resPath,
                                                                ResSubDir.Config,
                                                                ResNamePrefix.SocketConfig,
                                                                TeddyNetCore_EngineEnum.SocketConfigType.ServerManager.ToString(),
                                                                ResNamePostfix.None,
                                                                ResType.json);
                callBackLogPrint("ServerConfig_ServerCenter路径 = " + path);
                callBackLogPrint(path);
                string file = _fileController.readFile(path);
                callBackLogPrint("ServerConfig_ServerCenter内容 = ");
                callBackLogPrint(file);
                var data = _jsonController.deserializeStrToObject<DataFile_SocketConfig_ServerManager>(file);
                _dataFileController.addData<DataFile_SocketConfig_ServerManager>(data);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        public void initRequestSocket() {
            try {
                var data = _dataFileController.getData<DataFile_SocketConfig_ServerManager>();
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
                var data = _dataFileController.getData<DataFile_SocketConfig_ServerManager>();
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

        #region consoleCmd
        void consoleCmd() {
            while(true) {
                string cmd = Console.ReadLine();
                Console.WriteLine(cmd);
            }
        }
        #endregion

        void addMainCmd(string cmdType, string cmdStr) {
            try {
                MainCmdType mainCmdType = (MainCmdType)Enum.Parse(typeof(MainCmdType), cmdType, true);
                _mainCmdDict.Add(mainCmdType, cmdStr);
            } catch (Exception e) {
                Console.WriteLine("[Error]" + e.Message);
                stop();
            }
        }
    }
}
