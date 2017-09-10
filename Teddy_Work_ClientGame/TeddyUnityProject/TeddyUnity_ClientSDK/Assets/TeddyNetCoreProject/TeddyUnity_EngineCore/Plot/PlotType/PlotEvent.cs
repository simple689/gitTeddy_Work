using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;

namespace TeddyUnity_EngineCore {
    public class PlotEvent : Plot {
        DataBase_PlotEvent _plotEventData;

        public override void init(EngineBase controller, PlotStep plotStep, DataBase_Plot plot) {
            base.init(controller, plotStep, plot);
            _plotEventData = plot as DataBase_PlotEvent;
        }

        public override void start() {
            base.start();
            //if (_plotStepData._plotStepDict != null) {
            //    foreach (var item in _plotStepData._plotStepDict) {
            //        PlotStep plotStep = new PlotStep();
            //        plotStep.init(_controller, _plotSection, this, item.Value);
            //        _plotStepDict.Add(item.Key, plotStep);
            //        plotStep.start();
            //    }
            //}
        }

        public override void update() {
            base.update();
        }

        public override void stop() {
            base.stop();
        }
    }
}
