using Newtonsoft.Json;
using System.Collections.Generic;

namespace TeddyNetCore_EngineCore {
    public class JsonController {
        EngineBase _controller;

        public void init(EngineBase controller) {
            _controller = controller;
        }

        public string serializeObject(object obj) { // 将对象序列化为JSON格式
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public T deserializeObject<T>(string jsonStr) { // 解析JSON字符串生成对象实体 "T"=对象类型
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        public T deserializeObject<T>(string jsonStr, params JsonConverter[] converters) {
            return JsonConvert.DeserializeObject<T>(jsonStr, converters);
        }

        public List<T> deserializeObjectList<T>(string jsonStr) { // 解析JSON数组生成对象实体集合 "T"=对象类型 returns=对象实体集合
            object obj = JsonConvert.DeserializeObject(jsonStr, typeof(List<T>));
            List<T> list = obj as List<T>;
            return list;
        }
    }
}
