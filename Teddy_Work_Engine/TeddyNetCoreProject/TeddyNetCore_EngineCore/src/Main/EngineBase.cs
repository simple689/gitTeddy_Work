using System;
using System.Collections.Generic;
using System.Net.Sockets;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;

namespace TeddyNetCore_EngineCore {
    public class EngineBase {
        public Dictionary<MainCmdType, string> _mainCmdDict = new Dictionary<MainCmdType, string>();
        public EngineBase _engineManager;

        #region Controller
        public FileController _fileController;
        public ResController _resController;
        public LogController _logController;
        public JsonController _jsonController;
        public DataController<DataFile> _dataFileController;
        #endregion

        #region 委托
        public Action<string> callBackLogPrint;
        public Func<SocketAsyncEventArgs, bool> callBackSocketReceive;
        public Func<SocketAsyncEventArgs, SocketCmdType, string, bool> callBackSocketSend;
        #endregion

        public virtual void init(EngineBase engineManager, string resSubDir) {
            initLog();
            _engineManager = engineManager;

            initFileAndRes(resSubDir);
            initJson();
            initData();
        }

        public virtual void start() {
        }

        public virtual void update() {
        }

        public virtual void stop() {
        }

        #region init
        void initLog() {
            Console.WriteLine("/* 初始化Log */");
            try {
                _logController = new LogController();
                _logController.init(this);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        void initFileAndRes(string resSubDir) {
            callBackLogPrint("/* 初始化文件和资源 */");
            try {
                _fileController = new FileController();
                _fileController.init(this);

                _resController = new ResController();
                _resController.init(this);
                _resController._dllPath = _fileController.getPath(FilePathType.DLL);
                _resController._runPath = _fileController.getPath(FilePathType.Run);
                _resController._resPath = _resController._runPath + resSubDir;
                callBackLogPrint("dll路径 = " + _resController._dllPath);
                callBackLogPrint("运行路径 = " + _resController._runPath);
                callBackLogPrint("资源路径 = " + _resController._resPath);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        void initJson() {
            callBackLogPrint("/* 初始化Json */");
            try {
                _jsonController = new JsonController();
                _jsonController.init(this);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        void initData() {
            callBackLogPrint("/* 初始化数据 */");
            try {
                _dataFileController = new DataController<DataFile>();
                _dataFileController.init(this);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }
        #endregion
    }
}
