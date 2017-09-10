using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;
using UnityEngine;

namespace TeddyUnity_EngineCore {
    public class Plot {
        EngineBase _controller;

        PlotStep _plotStep;
        public DataBase_Plot _plotData;

        PlotStatus _plotStatus = PlotStatus.None;
        int _plotFrameStart = 0;

        public virtual void init(EngineBase controller, PlotStep plotStep, DataBase_Plot data) {
            _controller = controller;
            _plotStep = plotStep;
            _plotData = data;

            _plotStatus = PlotStatus.Init;
        }

        public virtual void start() {
            _plotStatus = PlotStatus.Start;
        }

        public virtual void update() {
            if (_plotStatus == PlotStatus.Start) {
                _plotFrameStart = Time.frameCount;
                _plotStatus = PlotStatus.Update;
            }
            _controller.callBackLogPrint("Plot 更新：" + _plotData._name);
        }

        public virtual void stop() {
            _plotStatus = PlotStatus.Stop;
        }

        public virtual bool isUpdate() {
            bool result = true;
            if (_plotStatus == PlotStatus.Stop) {
                result = false;
            }
            return result;
        }
    }
}
