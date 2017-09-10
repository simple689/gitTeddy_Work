using System;
using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;

namespace TeddyNetCore_EngineDemo {
    class EngineDemo : EngineBase {
        public override void init(EngineBase controller, string resSubDir) {
            base.init(controller, resSubDir);
        }

        public override void start() {
            base.start();
            test();
        }

        public override void update() {
            base.update();
        }

        public override void stop() {
            base.stop();
        }

        #region test
        void test() {
            try {
                string path = _resController._resPath;
                path += _resController.getResPathRelative(ResSubDir.Config,
                                                          ResNamePrefix.CommonConfig,
                                                          ResType.json);
                _fileController.readFileThread(new ReadFileParameter(path, readFileComplete));

                DataFile_PlotSection a = new DataFile_PlotSection();

                DataBase_PlotStep b = new DataBase_PlotStep();
                DataBase_PlotStep c = new DataBase_PlotStep();
                DataBase_PlotStep d = new DataBase_PlotStep();
                DataBase_PlotStep e = new DataBase_PlotStep();
                DataBase_PlotStep f = new DataBase_PlotStep();

                DataBase_Plot aa = new DataBase_Plot();
                DataBase_PlotUI bb = new DataBase_PlotUI();
                DataBase_PlotSound cc = new DataBase_PlotSound();

                a._plotStep._plotList.Add(aa);
                a._plotStep._plotList.Add(bb);
                a._plotStep._plotList.Add(cc);

                a._plotStep._plotStepDict.Add("_plotStep_0", b);
                a._plotStep._plotStepDict.Add("_plotStep_1", c);

                b._plotStepDict.Add("_plotStep_0", d);
                d._plotStepDict.Add("_plotStep_0", e);
                e._plotStepDict.Add("_plotStep_0", f);

                string str = _jsonController.serializeObject(a);
                callBackLogPrint("--------------------------------------------------");
                callBackLogPrint(str);
                callBackLogPrint("--------------------------------------------------");
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        bool readFileComplete(string path, string readStr) {
            try {
                callBackLogPrint(path);
                callBackLogPrint(readStr);
                var data = _jsonController.deserializeObject<DataFile_CommonConfig>(readStr);
                _dataFileController.addData<DataFile_CommonConfig>(data);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
            return true;
        }
        #endregion
    }
}
