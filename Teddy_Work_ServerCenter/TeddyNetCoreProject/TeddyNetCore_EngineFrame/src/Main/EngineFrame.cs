using TeddyNetCore_Engine;

namespace TeddyNetCore_EngineFrame {
    class EngineFrame {
        static void Main(string[] args) {
            EngineManager engineManager = new EngineManager();
            engineManager.init(args);
            engineManager.start();
            engineManager.update();
            engineManager.stop();
        }
    }
}
