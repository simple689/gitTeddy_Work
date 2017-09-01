namespace Teddy {
    public class MsgEvent {
        public const int _span = 3000;
    }

    public enum MgrID {
        Game = 0,
        UI = MsgEvent._span, // 3000
        Sound = MsgEvent._span * 2, // 6000
        NPCManager = MsgEvent._span * 3, // 9000
        CharactorManager = MsgEvent._span * 4, // 12000
        AB = MsgEvent._span * 5, // 15000
        NetManager = MsgEvent._span * 6, // 18000
        Info = MsgEvent._span * 7 // 21000
    }

    public enum UIEventScene { // 对外通信
        Load = MgrID.UI,
        Register,
        MaxValue
    }

    public enum UIEventNPC {
        NPCAttack = UIEventScene.MaxValue,
        NPCBlood,
        MaxValue
    }
}