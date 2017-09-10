//using System.Collections.Generic;

//namespace TeddyNetCore_EngineCore {
//    public class TeddyDictionary<TKey, TValue> : Dictionary<TKey, TValue> {
//        LinkedList<TKey> _linkedList = new LinkedList<TKey>();

//        public new void Add(TKey key, TValue value) {
//            base.Add(key, value);
//            _linkedList.AddLast(key);
//        }

//        public new void Clear() {
//            base.Clear();
//            _linkedList.Clear();
//        }

//        public new bool Remove(TKey key) {
//            bool result = base.Remove(key);
//            if (result) {
//                _linkedList.Remove(key);
//            }
//            return result;
//        }
//    }
//}
