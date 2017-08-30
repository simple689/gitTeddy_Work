using System;
using System.Net.Sockets;
using System.Text;

namespace TeddyNetCore_EngineCore {
    public class CommonSocketController {
        public EngineBase _controller = null;

        public int _bufferSize;  // 用于每个I/O Socket操作的缓冲区大小
        public Socket _socket;

        public virtual void stop() {
            try {
#if NETCOREAPP2_0
                _socket.Dispose();
#else
                _socket.Close();
#endif
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

#region IO
        public void onIOCompleted(object sender, SocketAsyncEventArgs ioContext) { // 当Socket上的发送或接收请求被完成时，调用此函数 "sender"=激发事件的对象 "ioContext"=与发送或接收完成操作相关联的SocketAsyncEventArg对象
            try {
                if (ioContext == null) {
                    return;
                }
                switch (ioContext.LastOperation) {
                    case SocketAsyncOperation.Receive:
                        processReceive(ioContext);
                        break;
                    case SocketAsyncOperation.Send:
                        processSend(ioContext);
                        break;
                    default:
                        throw new ArgumentException("The last operation completed on the socket was not a receive or send");
                }
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        void processReceive(SocketAsyncEventArgs ioContext) {
            try {
                if (ioContext == null) {
                    return;
                }
                if (ioContext.BytesTransferred > 0) { // 检查远程主机是否关闭连接
                    if (ioContext.SocketError == SocketError.Success) {
                        Socket socket = (Socket)ioContext.UserToken;
                        if (socket.Available == 0) { // 判断所有需接收的数据是否已经完成
                            string msgReceive = Encoding.UTF8.GetString(ioContext.Buffer, 0, ioContext.BytesTransferred);
                            string printStr = string.Format("接收消息 {0} = {1}", socket.RemoteEndPoint, msgReceive);
                            _controller.callBackLogPrint(printStr);

                            _controller.callBackSocketReceive(ioContext);
                        } else { // 为接收下一段数据，投递接收请求，这个函数有可能同步完成，这时返回false，并且不会引发SocketAsyncEventArgs.Completed事件
                            socket.ReceiveAsync(ioContext); // 同步接收时处理接收完成事件
                        }
                    } else {
                        processError(ioContext);
                    }
                } else {
                    closeClientSocket(ioContext);
                }
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        void processSend(SocketAsyncEventArgs ioContext) { // 发送完成时处理函数 "ioContext"=与发送完成操作相关联的SocketAsyncEventArg对象
            try {
                if (ioContext == null) {
                    return;
                }
                if (ioContext.SocketError == SocketError.Success) {
                    Socket socket = (Socket)ioContext.UserToken; // 接收时根据接收的字节数收缩了缓冲区的大小，因此投递接收请求时，恢复缓冲区大小
                    ioContext.SetBuffer(new byte[_bufferSize], 0, _bufferSize);
                    socket.ReceiveAsync(ioContext); // 投递接收请求
                } else {
                    processError(ioContext);
                }
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }
#endregion

#region error
        void processError(SocketAsyncEventArgs context) { // 处理socket错误
            try {
                if (context == null) {
                    return;
                }
                Socket socket = (Socket)context.UserToken;
                string printStr = string.Format("Socket error {0} on endpoint {1} during {2}.", (int)context.SocketError, socket.LocalEndPoint, context.LastOperation);
                _controller.callBackLogPrint(printStr);
                closeClientSocket(context);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        public virtual bool closeClientSocket(SocketAsyncEventArgs context) { // 关闭socket连接 "context"=SocketAsyncEventArg associated with the completed send/receive operation.
            bool result = true;
            try {
                if (context == null) {
                    result = false;
                }
                Socket socket = (Socket)context.UserToken;
                try {
                    socket.Shutdown(SocketShutdown.Send);
                    string printStr = string.Format("连接断开 {0} ", socket.RemoteEndPoint);
                    _controller.callBackLogPrint(printStr);
                } catch (Exception) { // Throw if client has closed, so it is not necessary to catch.
                    result = false;
                } finally {
#if NETCOREAPP2_0
                    socket.Dispose();
#else
                    socket.Close();
#endif
                }
            } catch (Exception e) {
                result = false;
                _controller.callBackLogPrint(e.Message);
            }
            return result;
        }
#endregion
    }
}
