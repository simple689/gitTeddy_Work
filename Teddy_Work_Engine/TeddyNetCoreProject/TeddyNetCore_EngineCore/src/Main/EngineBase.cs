using System;
using System.Collections.Generic;
using System.Net.Sockets;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;

namespace TeddyNetCore_EngineCore {
    public abstract class EngineBase {
        public Dictionary<MainCmdType, string> _mainCmdDict = new Dictionary<MainCmdType, string>();
        public EngineBase _controller;

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

        public virtual void init(EngineBase controller) {
            _controller = controller;

            initFileAndRes();
            initLog();
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
        void initFileAndRes() {
            Console.WriteLine("/* 初始化文件和资源 */");
            try {
                _fileController = new FileController();
                _fileController.init(this);

                _resController = new ResController();
                _resController.init(this);
                _resController._dllPath = _fileController.getPath(PathType.DLL);
                _resController._runPath = _fileController.getPath(PathType.Run);
                Console.WriteLine("dll路径 = " + _resController._dllPath);
                Console.WriteLine("运行路径 = " + _resController._runPath);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        void initLog() {
            Console.WriteLine("/* 初始化Log */");
            try {
                _logController = new LogController();
                _logController.init(this);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        void initJson() {
            Console.WriteLine("/* 初始化Json */");
            try {
                _jsonController = new JsonController();
                _jsonController.init(this);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        void initData() {
            Console.WriteLine("/* 初始化数据 */");
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
