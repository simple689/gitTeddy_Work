using System;
using System.Reflection;
using UnityEngine;

namespace Teddy {
    public abstract class Singleton<T> where T : Singleton<T> {
        protected static T _instance = null;

        protected Singleton() {
        }

        public static T instance {
            get {
                if (_instance == null) {
                    ConstructorInfo[] constructorInfoAry = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic); // 获取所有非public的构造方法
                    ConstructorInfo constructorInfo = Array.Find(constructorInfoAry, obj => obj.GetParameters().Length == 0); // 获取无参的构造方法
                    if (constructorInfo == null) {
                        Debug.LogError("Non-public constructorInfo() not found.");
                    } else {
                        _instance = constructorInfo.Invoke(null) as T; // 调用构造方法
                    }
                }
                return _instance;
            }
        }

        public void destroy() {
            _instance = null;
        }
    }
}