using UnityEngine;

namespace Teddy {
    public class VoidDelegate { // 返回空类型的回调定义
        public delegate void withVoid();
        public delegate void withBool(bool value);
        public delegate void withParams(params object[] paramList);
        public delegate void withObject(Object obj);
        public delegate void withGameObject(GameObject go);
    }
}