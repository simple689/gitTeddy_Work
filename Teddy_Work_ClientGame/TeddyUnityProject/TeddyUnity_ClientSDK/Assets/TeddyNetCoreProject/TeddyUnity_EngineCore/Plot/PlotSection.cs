using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;

namespace TeddyUnity_EngineCore {
    public class PlotSection {
        EngineBase _controller;

        string _plotSectionKey;
        public DataFile_PlotSection _plotSectionData;

        PlotStep _plotStep = new PlotStep();

        public void init(EngineBase controller, string key, DataFile_PlotSection data) {
            _controller = controller;
            _plotSectionKey = key;
            _plotSectionData = data;

            _plotStep.init(_controller, this, null, _plotSectionKey, _plotSectionData._plotStep);
        }

        public void start(bool reloadFile) {
            _plotStep.start(reloadFile);
        }

        public void update() {
            _plotStep.update();
        }

        public void stop() {
            _plotStep.stop();
        }
    }
}
