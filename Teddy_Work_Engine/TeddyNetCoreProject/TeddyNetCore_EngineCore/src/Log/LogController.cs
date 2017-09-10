using System;

namespace TeddyNetCore_EngineCore {
    public class LogController {
        EngineBase _controller;

        public void init(EngineBase controller) {
            _controller = controller;
            initCallBack();
        }

        void initCallBack() {
            _controller.callBackLogPrint += println;
        }

        public virtual void println(string str) {
            try {
                Console.WriteLine(str);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
