using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TeddyNetCore_EngineEnum;

namespace TeddyNetCore_EngineCore {
    public class ListenSocketController : CommonSocketController { // 基于SocketAsyncEventArgs 实现 IOCP 服务器
        int _listenPort; // 监听端口
        int _connectedSocketNum; // 服务器上连接的客户端总数
        int _maxConnectionNum; // 服务器能接受的最大连接数量
        SocketContextPool _socketContextPool; // 完成端口上进行投递所用的IOContext对象池
        static Mutex _mutex = new Mutex(); // 用于服务器执行的互斥同步对象

        public void init(EngineBase controller, int listenPort, int maxConnectionNum, int bufferSize) {
            _controller = controller;

            try {
                _listenPort = listenPort;
                _connectedSocketNum = 0;
                _maxConnectionNum = maxConnectionNum;
                _bufferSize = bufferSize;

                _socketContextPool = new SocketContextPool(_maxConnectionNum);
                for (int i = 0; i < _maxConnectionNum; i++) { // 为IoContextPool预分配SocketAsyncEventArgs对象
                    SocketAsyncEventArgs ioContext = new SocketAsyncEventArgs();
                    ioContext.Completed += new EventHandler<SocketAsyncEventArgs>(onIOCompleted);
                    ioContext.SetBuffer(new byte[_bufferSize], 0, _bufferSize);
                    _socketContextPool.add(ioContext); // 将预分配的对象加入SocketAsyncEventArgs对象池中
                }
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        public void start() { // 启动服务，开始监听
            try {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.ReceiveBufferSize = _bufferSize;
                _socket.SendBufferSize = _bufferSize;
                _socket.Bind(new IPEndPoint(IPAddress.Any, _listenPort));
                _socket.Listen(_maxConnectionNum);

                _controller.callBackLogPrint("/* 开始监听 */");
                startAccept(null); // 在监听Socket上投递一个接受请求。

                _mutex.WaitOne(); // Blocks the current thread to receive incoming messages.
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        public override void stop() { // 停止服务
            base.stop();
            try {
                _mutex.ReleaseMutex();
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        #region accept
        void startAccept(SocketAsyncEventArgs acceptContext) { // 从客户端开始接受一个连接操作
            try {
                if (acceptContext == null) {
                    acceptContext = new SocketAsyncEventArgs();
                    acceptContext.Completed += new EventHandler<SocketAsyncEventArgs>(onAcceptCompleted);
                } else {
                    acceptContext.AcceptSocket = null; // 重用前进行对象清理
                }
                _socket.AcceptAsync(acceptContext);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        void onAcceptCompleted(object sender, SocketAsyncEventArgs acceptContext) { // accept操作完成时回调函数 "sender"=Object who raised the event. "acceptContext"=SocketAsyncEventArg associated with the completed accept operation.
            processAccept(acceptContext);
        }

        void processAccept(SocketAsyncEventArgs acceptContext) { // 监听Socket接受处理 "acceptContext"=SocketAsyncEventArg associated with the completed accept operation.
            try {
                if (acceptContext == null) {
                    return;
                }
                Socket socket = acceptContext.AcceptSocket;
                if (socket == null) {
                    return;
                }
                if (socket.Connected) {
                    SocketAsyncEventArgs ioContext = _socketContextPool.pop();
                    if (ioContext != null) { // 从接受的客户端连接中取数据配置ioContext
                        ioContext.UserToken = socket;

                        Interlocked.Increment(ref _connectedSocketNum);

                        string printStr = string.Format("连入 {0} ，共有 {1} 个连接。", socket.RemoteEndPoint, _connectedSocketNum);
                        _controller.callBackLogPrint(printStr);

                        _controller.callBackSocketSend(ioContext, SocketCmdType.ConnectSuccess, "");
                    } else { // 已经达到最大客户连接数量，在这接受连接，发送“连接已经达到最大数”，然后断开连接
                        string printStr = string.Format("连接已满，拒绝连接 {0} 。", socket.RemoteEndPoint);
                        _controller.callBackLogPrint(printStr);

                        _controller.callBackSocketSend(ioContext, SocketCmdType.ConnectFail, "连接已经达到最大数!");
#if NETCOREAPP2_0
                        socket.Dispose();
#else
                        socket.Close();
#endif
                    }
                    startAccept(acceptContext); // 投递下一个接受请求
                }
            } catch (SocketException e) {
                Socket token = (Socket)acceptContext.UserToken;

                string printStr = string.Format("接收客户 {0} 数据出错，异常信息 = {1}", token.RemoteEndPoint, e.Message);
                _controller.callBackLogPrint(printStr);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }
        #endregion

        #region error
        public override bool closeClientSocket(SocketAsyncEventArgs context) { // 关闭socket连接 "context"=SocketAsyncEventArg associated with the completed send/receive operation.
            bool result = base.closeClientSocket(context);
            if (result) {
                try {
                    Interlocked.Decrement(ref _maxConnectionNum);
                    _socketContextPool.push(context); // SocketAsyncEventArg 对象被释放，压入可重用队列。
                    string printStr = string.Format("服务器共有 {1} 个连接。", _connectedSocketNum);
                    _controller.callBackLogPrint(printStr);
                } catch (Exception e) {
                    _controller.callBackLogPrint(e.Message);
                }
            }
            return result;
        }
        #endregion
    }
}
