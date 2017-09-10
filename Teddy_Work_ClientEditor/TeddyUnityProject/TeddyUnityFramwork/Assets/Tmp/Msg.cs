using UnityEngine;

namespace Teddy {
    public class Msg { // 消息体
        public ushort msgId; // 表示 65535个消息 占两个字节

        public MgrID GetMgrID() {
            int tmpId = msgId / MsgEvent._span;

            return (MgrID)(tmpId * MsgEvent._span);
        }

        public Msg() { }

        public Msg(ushort msg) {
            msgId = msg;
        }

    }

    public class SoundMsg : Msg {
        public bool soundOn;

        public SoundMsg(ushort msgId, bool soundOn) : base(msgId) {
            this.soundOn = soundOn;
        }
    }

    public class StrMsg : Msg {
        public string strMsg;

        public StrMsg(ushort msgId, string strMsg) : base(msgId) {
            this.strMsg = strMsg;
        }
    }


    public class MsgTransform : Msg {
        public Transform value;

        public MsgTransform(ushort msgId, Transform tmpTrans) : base(msgId) {
            this.value = tmpTrans;
        }
    }
}