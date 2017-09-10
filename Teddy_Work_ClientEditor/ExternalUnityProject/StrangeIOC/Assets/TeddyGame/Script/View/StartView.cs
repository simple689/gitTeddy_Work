using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace TeddyGame {
    // 所有使用Strange context的View层类都必须继承这个Strange的View类
    public class StartView : View {
        public Signal buttonClicked = new Signal();

        private Rect buttonRect = new Rect(0, 0, 200, 50);

        public void OnGUI() {
            if (GUI.Button(buttonRect, "Manage")) {
                buttonClicked.Dispatch();

                //Signal startSignal = injectionBinder.GetInstance<StartSignal>();
                //startSignal.Dispatch();
            }
        }
    }
}