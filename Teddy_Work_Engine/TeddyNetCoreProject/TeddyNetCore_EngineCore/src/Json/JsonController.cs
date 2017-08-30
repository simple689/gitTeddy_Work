using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace TeddyNetCore_EngineCore {
    public class JsonController {
        EngineBase _controller;

        JsonSerializer _jsonSerializer = new JsonSerializer();

        public void init(EngineBase controller) {
            _controller = controller;
        }

        public string serializeObjectToStr(object obj) { // 将对象序列化为JSON格式
            string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return jsonStr;
        }

        public T deserializeStrToObject<T>(string jsonStr) where T : class { // 解析JSON字符串生成对象实体 "T"=对象类型
            StringReader strReader = new StringReader(jsonStr);
            object obj = _jsonSerializer.Deserialize(new JsonTextReader(strReader), typeof(T));
            T t = obj as T;
            return t;
        }

        public List<T> deserializeStrToObjectList<T>(string jsonStr) where T : class { // 解析JSON数组生成对象实体集合 "T"=对象类型 returns=对象实体集合
            StringReader strReader = new StringReader(jsonStr);
            object obj = _jsonSerializer.Deserialize(new JsonTextReader(strReader), typeof(List<T>));
            List<T> list = obj as List<T>;
            return list;
        }

        public T deserializeStrToAnonymousType<T>(string jsonStr, T anonymousTypeObject) { // 反序列化JSON到给定的匿名对象. "T"=匿名对象类型 "anonymousTypeObject"=匿名对象 returns=匿名对象
            T t = JsonConvert.DeserializeAnonymousType(jsonStr, anonymousTypeObject);
            return t;
        }
    }
}
