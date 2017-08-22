using System.Collections.Generic;
using System.Net.Sockets;

namespace TeddyNetCore_Engine {
    public class SocketContextPool { // 与每个客户Socket相关联，进行Send和Receive投递时所需要的参数
        List<SocketAsyncEventArgs> _ioContextPool; // 为每一个Socket客户端分配一个SocketAsyncEventArgs，用一个List管理，在程序启动时建立。
        int _capacity; // pool对象池的容量
        int _boundary; // 已分配和未分配的对象边界，大的是已经分配的，小的是未分配的

        public SocketContextPool(int capacity) {
            _ioContextPool = new List<SocketAsyncEventArgs>(capacity);
            _capacity = capacity;
            _boundary = 0;
        }

        public bool add(SocketAsyncEventArgs arg) {
            if (arg != null) {
                lock (_ioContextPool) {
                    if (_ioContextPool.Count < _capacity) {
                        _ioContextPool.Add(arg);
                        _boundary++;
                        return true;
                    }
                }
            }
            return false;
        }

        public SocketAsyncEventArgs get(int index) {
            if (index >= 0 && index < _capacity) {
                return _ioContextPool[index];
            }
            return null;
        }

        public SocketAsyncEventArgs pop() {
            lock (_ioContextPool) {
                if (_boundary > 0) {
                    --_boundary;
                    return _ioContextPool[_boundary];
                }
            }
            return null;
        }

        public bool push(SocketAsyncEventArgs arg) { // 一个socket客户断开，与其相关的IoContext被释放，重新投入Pool中，备用。
            if (arg != null) {
                lock (_ioContextPool) {
                    int index = _ioContextPool.IndexOf(arg, _boundary); // 找出被断开的客户，此处一定能查到，因此index不可能为-1，必定要大于0。
                    if (index == _boundary) { // 正好是边界元素
                        _boundary++;
                    } else {
                        _ioContextPool[index] = _ioContextPool[_boundary]; //将断开客户移到边界上，边界右移
                        _ioContextPool[_boundary++] = arg;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
