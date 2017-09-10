using System;
using System.Collections.Generic;

namespace TeddyNetCore_EngineCore {
    public class DataController<T> {
        EngineBase _controller = null;

        Dictionary<string, T> _dataDict = new Dictionary<string, T>();

        public void init(EngineBase controller) {
            _controller = controller;
        }

        public bool addData<TData>(T data) where TData : class {
            bool result = true;
            try {
                string key = typeof(TData).Name;
                _dataDict.Add(key, data);
            } catch (Exception e) {
                result = false;
                _controller.callBackLogPrint(e.Message);
            }
            return result;
        }

        public TData getData<TData>() where TData : class {
            TData data = null;
            try {
                string key = typeof(TData).Name;
                data = _dataDict[key] as TData;
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
            return data;
        }
    }
}
