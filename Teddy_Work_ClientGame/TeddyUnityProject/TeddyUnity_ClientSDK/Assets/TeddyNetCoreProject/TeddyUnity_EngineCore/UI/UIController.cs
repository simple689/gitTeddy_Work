using TeddyNetCore_EngineCore;

namespace TeddyUnity_EngineCore {
    public class UIController {
        EngineBase _controller;

        public void init(EngineBase controller) {
            _controller = controller;
            initCallBack();
        }

        void initCallBack() {
        }
    }
}
