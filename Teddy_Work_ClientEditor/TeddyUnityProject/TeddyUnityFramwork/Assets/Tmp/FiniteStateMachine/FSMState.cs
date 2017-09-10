using System.Collections.Generic;

namespace Teddy {
    public class FSMState { // 有限状态机基类
        public ushort _name; // 字符串
        public Dictionary<ushort, FSMTranslation> _translationDict = new Dictionary<ushort, FSMTranslation>(); // 存储事件对应的条转

        public FSMState(ushort name) {
            _name = name;
        }

        public virtual void OnEnter() { // 进入状态(逻辑)
        }

        public virtual void OnExit() { // 离开状态(逻辑)
        }           
    }
}
