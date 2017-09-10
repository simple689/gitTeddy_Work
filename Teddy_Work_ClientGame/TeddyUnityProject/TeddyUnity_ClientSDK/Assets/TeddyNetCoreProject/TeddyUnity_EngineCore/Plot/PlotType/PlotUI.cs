using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;

namespace TeddyUnity_EngineCore {
    public class PlotUI : Plot {
        DataBase_PlotUI _plotUIData;

        public override void init(EngineBase controller, PlotStep plotStep, DataBase_Plot plot) {
            base.init(controller, plotStep, plot);
            _plotUIData = plot as DataBase_PlotUI;
        }

        public override void start() {
            base.start();
        }

        public override void update() {
            base.update();
        }

        public override void stop() {
            base.stop();
        }
    }
}
