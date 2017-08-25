using System;

namespace TeddyNetCore_Engine {
    public class LogController {
        EngineBase _controller;

        public void init(EngineBase controller) {
            _controller = controller;
            initCallBack();
        }

        void initCallBack() {
            _controller.callBackLogPrint += println;
        }

        void println(string str) {
            try {
                Console.WriteLine(str);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
