using UnityEngine;
using System.Collections.Generic;

namespace Teddy {
    public enum MsgChannel {
        Global, // 全局
        UI,
        Logic,
    }

    public class MsgDispatcher : Singleton<MsgDispatcher> { // 消息分发器
        Dictionary<MsgChannel, Dictionary<string, List<MsgHandler>>> _msgHandlerDict = new Dictionary<MsgChannel, Dictionary<string, List<MsgHandler>>>(); // 每个消息名字维护一组消息捕捉器。

        private MsgDispatcher() { }

        public void registerMsgGlobal(IMsgReceiver receiver, string msgName, VoidDelegate.withParams callback) { // 注册消息
            registerMsgByChannel(receiver, MsgChannel.Global, msgName, callback);
        }

        public void registerMsgByChannel(IMsgReceiver receiver, MsgChannel channel, string msgName, VoidDelegate.withParams callback) { // 注册消息
            if (string.IsNullOrEmpty(msgName)) {
                Log.instance.print("registerMsg: " + msgName + " is Null or Empty.", LogLevel.Error);
                return;
            }
            if (null == callback) {
                Log.instance.print("registerMsg: " + msgName + " callback is Null.", LogLevel.Error);
                return;
            }
            if (!_msgHandlerDict.ContainsKey(channel)) { // 添加消息通道
                _msgHandlerDict[channel] = new Dictionary<string, List<MsgHandler>>();
            }
            if (!_msgHandlerDict[channel].ContainsKey(msgName)) {
                _msgHandlerDict[channel][msgName] = new List<MsgHandler>();
            }
            var handlers = _msgHandlerDict[channel][msgName];
            foreach (var handler in handlers) { // 防止重复注册
                if (handler._receiver == receiver && handler._callback == callback) {
                    Log.instance.print("registerMsg: " + msgName + " already register.", LogLevel.Warning);
                    return;
                }
            }
            handlers.Add(new MsgHandler(receiver, callback));
        }

        public void unRegisterMsgGlobal(IMsgReceiver receiver, string msgName) { // 注销消息不需要callback
            unRegisterMsgByChannel(receiver, MsgChannel.Global, msgName);
        }

        public void unRegisterMsgByChannel(IMsgReceiver receiver, MsgChannel channel, string msgName) { // 注销消息不需要callback
            if (string.IsNullOrEmpty(msgName)) {
                Log.instance.print("unRegisterMsg: " + msgName + " is Null or Empty.", LogLevel.Error);
                return;
            }
            if (!_msgHandlerDict.ContainsKey(channel)) {
                Debug.LogError("unRegisterMsg Channel: " + channel.ToString() + " doesn't exist.");
                return;
            }
            var handlers = _msgHandlerDict[channel][msgName];
            int handlerCount = handlers.Count;
            for (int index = handlerCount - 1; index >= 0; index--) { // 删除List需要从后向前遍历
                var handler = handlers[index];
                if (handler._receiver == receiver) {
                    handlers.Remove(handler);
                    break;
                }
            }
        }

        public void sendMsgGlobal(IMsgSender sender, string msgName, params object[] paramList) { // 发送消息
            sendMsgByChannel(sender, MsgChannel.Global, msgName, paramList);
        }

        public void sendMsgByChannel(IMsgSender sender, MsgChannel channel, string msgName, params object[] paramList) {
            if (string.IsNullOrEmpty(msgName)) {
                Log.instance.print("sendMsg: " + msgName + " is Null or Empty.", LogLevel.Error);
                return;
            }
            if (!_msgHandlerDict.ContainsKey(channel)) {
                Log.instance.print("sendMsg Channel: " + channel.ToString() + " doesn't exist.", LogLevel.Error);
                return;
            }
            if (!_msgHandlerDict[channel].ContainsKey(msgName)) {
                Log.instance.print("sendMsg is unRegister.", LogLevel.Warning);
                return;
            }
            var handlers = _msgHandlerDict[channel][msgName];
            var handlerCount = handlers.Count;
            for (int index = handlerCount - 1; index >= 0; index--) { // 之所以是从后向前遍历,是因为  从前向后遍历删除后索引值会不断变化，参考文章,http://www.2cto.com/kf/201312/266723.html
                var handler = handlers[index];
                if (handler._receiver != null) {
                    Log.instance.print("sendMsg succeed: [" + channel.ToString() + "] " + msgName);
                    handler._callback(paramList);
                } else {
                    handlers.Remove(handler);
                }
            }
        }
    }
}