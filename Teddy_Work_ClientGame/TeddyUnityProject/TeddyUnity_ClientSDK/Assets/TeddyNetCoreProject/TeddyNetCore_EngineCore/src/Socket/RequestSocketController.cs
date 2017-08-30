using System;
using System.Net;
using System.Net.Sockets;

namespace TeddyNetCore_EngineCore {
    public class RequestSocketController : CommonSocketController {
        string _host;
        int _port;
        SocketAsyncEventArgs _ioContext;

        public void init(EngineBase controller, string host, int port, int bufferSize) {
            _controller = controller;

            try {
                _host = host;
                _port = port;
                _bufferSize = bufferSize;
                _ioContext = new SocketAsyncEventArgs();
                _ioContext.Completed += new EventHandler<SocketAsyncEventArgs>(onIOCompleted);
                _ioContext.SetBuffer(new byte[_bufferSize], 0, _bufferSize);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        public void start() {
            try {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                SocketAsyncEventArgs connectContext = new SocketAsyncEventArgs();
                connectContext.Completed += new EventHandler<SocketAsyncEventArgs>(onConnectCompleted);
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(_host), _port);
                connectContext.RemoteEndPoint = ipEndPoint;
                connectContext.UserToken = _socket;

                _controller.callBackLogPrint("/* [开始连接服务器] */");
                _socket.ConnectAsync(connectContext);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        public override void stop() {
            base.stop();
        }

        #region connect
        void onConnectCompleted(object sender, SocketAsyncEventArgs connectContext) {
            try {
                if (connectContext == null) {
                    return;
                }
                if (connectContext.SocketError == SocketError.Success) {
                    processConnect(connectContext);
                } else {
                    _socket.ConnectAsync(connectContext);
                }
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        void processConnect(SocketAsyncEventArgs connectContext) {
            try {
                if (connectContext == null) {
                    return;
                }
                string printStr = string.Format("请求中心服完成 = {0}", _socket.RemoteEndPoint);
                _controller.callBackLogPrint(printStr);

                _ioContext.UserToken = _socket;
                _socket.ReceiveAsync(_ioContext);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }
        #endregion

        #region error
        public override bool closeClientSocket(SocketAsyncEventArgs context) {
            bool result = base.closeClientSocket(context);
            if (result) {
                try {
                } catch (Exception e) {
                    _controller.callBackLogPrint(e.Message);
                }
            }
            return result;
        }
        #endregion
    }
}
