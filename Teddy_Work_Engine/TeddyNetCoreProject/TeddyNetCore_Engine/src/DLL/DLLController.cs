using System;
using System.Collections.Generic;
using System.Reflection;

namespace TeddyNetCore_EngineCore {
    public class DLLController {
        EngineBase _controller;

        public Dictionary<string, Assembly> _assemblyDict = new Dictionary<string, Assembly>();
        public Dictionary<string, Object> _dllObjDict = new Dictionary<string, Object>();

        public void init(EngineBase controller) {
            _controller = controller;
        }

        public void loadAssembly(string dllDir, string dllName) {
            Assembly assembly = Assembly.LoadFile(dllDir + "/" + dllName + ".dll");
            _assemblyDict.Add(dllName, assembly);
        }

        public bool invoke(string dllName, string className, string methodName, params Object[] args) {
            bool result = true;
            try {
                Assembly assembly = _assemblyDict[dllName];
                Type type = assembly.GetType(dllName + "." + className);
                MethodInfo method = type.GetMethod(methodName);
                Object obj = null;
                if (_dllObjDict.ContainsKey(type.ToString())) {
                    obj = _dllObjDict[type.ToString()];
                } else {
                    obj = Activator.CreateInstance(type);
                    _dllObjDict.Add(type.ToString(), obj);
                }
                method.Invoke(obj, args);
            } catch (Exception e) {
                result = false;
                _controller.callBackLogPrint(e.Message);
            }
            return result;
        }
    }
}
