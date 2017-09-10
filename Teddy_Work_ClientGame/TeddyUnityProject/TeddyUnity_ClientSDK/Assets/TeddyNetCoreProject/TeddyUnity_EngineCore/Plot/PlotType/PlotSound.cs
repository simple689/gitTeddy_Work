using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;

namespace TeddyUnity_EngineCore {
    public class PlotSound : Plot {
        DataBase_PlotSound _plotSoundData;

        public override void init(EngineBase controller, PlotStep plotStep, DataBase_Plot plot) {
            base.init(controller, plotStep, plot);
            _plotSoundData = plot as DataBase_PlotSound;
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
