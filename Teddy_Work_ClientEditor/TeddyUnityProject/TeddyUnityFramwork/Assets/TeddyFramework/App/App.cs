using UnityEngine;
using System.Collections;

namespace Teddy {
    public enum AppMode {
        Developing, // 开发版本,为了快速快发,而写的测试入口。
        Release // 发布版本,跑整个游戏
    }

    public class App : SingletonMono<App> { // 全局唯一继承于MonoBehaviour的单例类，保证其他公共模块都以App的生命周期为准
        public AppMode _appMode = AppMode.Developing;

        public delegate void LifeCircleCallback(); // 全局生命周期回调
        public LifeCircleCallback _onUpdate = delegate { };
        public LifeCircleCallback _onFixedUpdate = delegate { };
        public LifeCircleCallback _onLatedUpdate = delegate { };
        public LifeCircleCallback _onGUI = delegate { };
        public LifeCircleCallback _onDestroy = delegate { };
        public LifeCircleCallback _onApplicationQuit = delegate { };

        private App() { }

        IEnumerator Start() {
            //yield return Framework.init();

            switch (App.instance._appMode) {
                case AppMode.Developing: {
                    //yield return GetComponent<IUnitTest>().launch();
                    foreach (var obj in GetComponents<IUnitTest>()) {
                        yield return obj.launch();
                    }
                    break;
                }
                case AppMode.Release: {
                    yield return GameManager.instance.launch();
                    break;
                }
            }
        }

        void Update() {
            if (_onUpdate != null) {
                _onUpdate();
            }
        }

        void FixedUpdate() {
            if (_onFixedUpdate != null) {
                _onFixedUpdate();
            }
        }

        void LatedUpdate() {
            if (_onLatedUpdate != null) {
                _onLatedUpdate();
            }
        }

        void OnGUI() {
            if (_onGUI != null) {
                _onGUI();
            }
        }

        protected void OnDestroy() {
            if (_onDestroy != null) {
                _onDestroy();
            }
        }

        void OnApplicationQuit() {
            if (_onApplicationQuit != null) {
                _onApplicationQuit();
            }
        }

        void Awake() {
            DontDestroyOnLoad(gameObject); // 确保不被销毁
            Application.targetFrameRate = 60; // 进入欢迎界面
        }
    }
}