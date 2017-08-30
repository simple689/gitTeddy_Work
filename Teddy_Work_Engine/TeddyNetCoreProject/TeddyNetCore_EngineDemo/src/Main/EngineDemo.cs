using System;
using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;

namespace TeddyNetCore_EngineDemo {
    class EngineDemo : EngineBase {
        public override void init(EngineBase controller) {
            base.init(controller);
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
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        bool readFileComplete(string path, string readStr) {
            try {
                callBackLogPrint(path);
                callBackLogPrint(readStr);
                var data = _jsonController.deserializeStrToObject<DataFile_CommonConfig>(readStr);
                _dataFileController.addData<DataFile_CommonConfig>(data);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
            return true;
        }
        #endregion
    }
}
