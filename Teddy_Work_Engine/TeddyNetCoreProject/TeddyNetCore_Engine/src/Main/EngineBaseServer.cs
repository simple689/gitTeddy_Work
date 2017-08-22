using System;
using System.Net.Sockets;
using System.Text;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;

namespace TeddyNetCore_Engine {
    public abstract class EngineBaseServer : EngineBase {
        public string _host;
        public int _hostPort;

        public override void init(EngineBase controller) {
            base.init(controller);
            _mainCmdDict = controller._mainCmdDict;

            initCallBack();
        }

        public override void start() {
            base.start();
        }

        public override void update() {
            base.update();
        }

        public override void stop() {
            base.stop();
        }

        public void initCallBack() {
            callBackSocketReceive += socketReceive;
            callBackSocketSend += socketSend;
        }

        public virtual void socketReceiveCmd(SocketAsyncEventArgs ioContext, SocketCmdType socketCmdType, string socketCmdStr) {
        }

        #region callBack
        public bool socketReceive(SocketAsyncEventArgs ioContext) {
            callBackLogPrint("/* 接收消息 */");
            bool result = true;
            try {
                string msgReceive = Encoding.UTF8.GetString(ioContext.Buffer, 0, ioContext.BytesTransferred);

                int index = msgReceive.IndexOf("|");
                int socketCmdTypeLen = Convert.ToInt32(msgReceive.Substring(0, index));
                string socketCmdTypeStr = msgReceive.Substring(index + 1, socketCmdTypeLen);
                SocketCmdType socketCmdType = (SocketCmdType)Enum.Parse(typeof(SocketCmdType), socketCmdTypeStr, true);

                int socketCmdStrBegin = index + socketCmdTypeLen + 2; // index + 1 + socketCmdTypeLen + 1
                string socketCmdStr = msgReceive.Substring(socketCmdStrBegin, msgReceive.Length - socketCmdStrBegin);

                socketReceiveCmd(ioContext, socketCmdType, socketCmdStr);
            } catch (Exception e) {
                result = false;
                _controller.callBackLogPrint(e.Message);
            }
            return result;
        }

        public bool socketSend(SocketAsyncEventArgs ioContext, SocketCmdType socketCmdType, string socketCmdStr) {
            callBackLogPrint("/* 发送消息 */");
            bool result = true;
            try {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append(Convert.ToString(socketCmdType.ToString().Length))
                          .Append("|")
                          .Append(socketCmdType.ToString())
                          .Append("|")
                          .Append(socketCmdStr);
                string msgSend = strBuilder.ToString();
                _controller.callBackLogPrint(msgSend);

                byte[] msgSendByte = Encoding.UTF8.GetBytes(msgSend);
                ioContext.SetBuffer(msgSendByte, 0, msgSendByte.Length);
                Socket socket = (Socket)ioContext.UserToken;
                socket.SendAsync(ioContext); // 投递发送请求，这个函数有可能同步发送出去，这时返回false，并且不会引发SocketAsyncEventArgs.Completed事件
            } catch (Exception e) {
                result = false;
                _controller.callBackLogPrint(e.Message);
            }
            return result;
        }
        #endregion

        #region init
        public void initCommonConfig() {
            callBackLogPrint("/* 初始化CommonConfig */");
            try {
                string path = _resController.getResPathAbsolute(_resController._runPath,
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

        public void initHostType() {
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
