using System;
using System.Net.Sockets;
using System.Text;
using TeddyNetCore_EngineEnum;

namespace TeddyNetCore_EngineCore {
    public abstract class EngineBase_Socket : EngineBase {
        public string _host;
        public int _hostPort;

        public override void init(EngineBase controller) {
            base.init(controller);
            _mainCmdDict = controller._mainCmdDict;

            initCallBack();
        }

        void initCallBack() {
            callBackSocketReceive += socketReceive;
            callBackSocketSend += socketSend;
        }

        public virtual void socketReceiveCmd(SocketAsyncEventArgs ioContext, SocketCmdType socketCmdType, string socketCmdStr) {
        }

        #region callBack
        public bool socketReceive(SocketAsyncEventArgs ioContext) {
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
                callBackLogPrint(e.Message);
            }
            return result;
        }

        public bool socketSend(SocketAsyncEventArgs ioContext, SocketCmdType socketCmdType, string socketCmdStr) {
            bool result = true;
            try {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append(Convert.ToString(socketCmdType.ToString().Length))
                          .Append("|")
                          .Append(socketCmdType.ToString())
                          .Append("|")
                          .Append(socketCmdStr);
                string msgSend = strBuilder.ToString();
                callBackLogPrint("发送消息 = " + msgSend);

                byte[] msgSendByte = Encoding.UTF8.GetBytes(msgSend);
                ioContext.SetBuffer(msgSendByte, 0, msgSendByte.Length);
                Socket socket = (Socket)ioContext.UserToken;
                socket.SendAsync(ioContext); // 投递发送请求，这个函数有可能同步发送出去，这时返回false，并且不会引发SocketAsyncEventArgs.Completed事件
            } catch (Exception e) {
                result = false;
                callBackLogPrint(e.Message);
            }
            return result;
        }
        #endregion
    }
}
