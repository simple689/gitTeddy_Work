namespace Teddy {
    public class FSMTranslation { // 跳转类
        public FSMState _fromState;
        public ushort _eventName;
        public FSMState _toState;

        public FSMTranslation(FSMState fromState, ushort eventName, FSMState toState) {
            _fromState = fromState;
            _eventName = eventName;
            _toState = toState;
        }
    }
}