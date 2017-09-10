using System.Collections.Generic;

namespace Teddy {
    public class QFSM { // 有限状态机
        FSMState _curState;
        Dictionary<ushort, FSMState> _stateDict = new Dictionary<ushort, FSMState>(); // 状态字典

        public FSMState curState {
            get {
                return _curState;
            }
        }

        public void addState(FSMState state) { // 添加状态
            _stateDict.Add(state._name, state);
        }

        public void addTranslation(FSMTranslation translation) { // 添加跳转
            _stateDict[translation._fromState._name]._translationDict.Add(translation._eventName, translation);
        }

        public void addTranslation(FSMState fromState, ushort eventName, FSMState toState) { // 添加跳转
            _stateDict[fromState._name]._translationDict.Add(eventName, new FSMTranslation(fromState, eventName, toState));
        }

        public void start(FSMState startState) { // 启动状态机
            _curState = startState;
            _curState.OnEnter();
        }

        public void handleEvent(ushort eventName) { // 处理事件
            if (_curState != null && _stateDict[_curState._name]._translationDict.ContainsKey(eventName)) {
                FSMTranslation tempTranslation = _stateDict[_curState._name]._translationDict[eventName];
                tempTranslation._fromState.OnExit();
                _curState = tempTranslation._toState;
                tempTranslation._toState.OnEnter();
            }
        }

        public void clear() { // 清空
            _stateDict.Clear();
        }
    }
}