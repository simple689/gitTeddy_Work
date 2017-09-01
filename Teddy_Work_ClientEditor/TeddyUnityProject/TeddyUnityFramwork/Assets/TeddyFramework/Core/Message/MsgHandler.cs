namespace Teddy {
    class MsgHandler { // 消息捕捉器
        public IMsgReceiver _receiver;
        public VoidDelegate.withParams _callback; // 是一种委托

        public MsgHandler(IMsgReceiver receiver, VoidDelegate.withParams callback) {
            _receiver = receiver;
            _callback = callback;
        }
    }
}