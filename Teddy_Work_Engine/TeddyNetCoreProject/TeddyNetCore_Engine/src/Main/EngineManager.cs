using System;
using System.Text;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;

namespace TeddyNetCore_Engine {
    public class EngineManager : EngineBase {
        #region DLL
        string _dllTypeStr;
        string _dllDir;
        string _dllName;
        #endregion

        public void init(string[] args) {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // 必须写在第一次 Console.WriteLine 的前面
            Console.WriteLine(Encoding.GetEncoding("GB2312"));
            Console.WriteLine(Encoding.GetEncoding("GBK"));
            Console.WriteLine("中文");

            initMain(args);
            init(this);
        }

        public override void init(EngineBase controller) {
            base.init(this);

            initMainDLL(); // 一定要在最后
            loadMainDLL();
            callBackLogPrint("/* 调用主DLL方法 = init */");
            _dllController.invoke(_dllName, _dllTypeStr, "init", this);
        }

        public override void start() {
            callBackLogPrint("/* 调用主DLL方法 = start */");
            _dllController.invoke(_dllName, _dllTypeStr, "start", null);
        }

        public override void update() {
            while (true) {
                _dllController.invoke(_dllName, _dllTypeStr, "update", null);
            }
        }

        public override void stop() {
            callBackLogPrint("/* 调用主DLL方法 = stop */");
            _dllController.invoke(_dllName, _dllTypeStr, "stop", null);
            Console.ReadLine();
            Environment.Exit(0);
        }

        void addMainCmd(string cmdType, string cmdStr) {
            try {
                MainCmdType mainCmdType = (MainCmdType)Enum.Parse(typeof(MainCmdType), cmdType, true);
                _mainCmdDict.Add(mainCmdType, cmdStr);
            } catch (Exception e) {
                Console.WriteLine("[Error]" + e.Message);
                stop();
            }
        }

        #region initMain
        void initMain(string[] args) {
            Console.WriteLine("==============================================================");
            Console.WriteLine("/* 传入参数 */");
            foreach (var arg in args) {
                Console.Write(arg + " ");
            }
            Console.Write("\n");
            Console.WriteLine("==============================================================");
            Console.WriteLine("/* 解析参数 */");
            for (int index = 0; index < args.Length;) {
                Console.WriteLine(args[index]);
                int readNum = 1;
                if (args[index].StartsWith("-")) {
                    StringBuilder strBuilder = new StringBuilder();
                    for (int readIndex = index + readNum; readIndex < args.Length; readIndex++) {
                        if (args[readIndex].StartsWith("-")) {
                            break;
                        }
                        strBuilder.Append(args[readIndex])
                                  .Append(" ");
                        readNum++;
                    }
                    string cmdStr = strBuilder.ToString();
                    Console.WriteLine(cmdStr);
                    addMainCmd(args[index].Substring(1), cmdStr.Substring(0, cmdStr.Length - 1));
                }
                index += readNum;
            }
            Console.WriteLine("==============================================================");
        }
        #endregion

        #region initMainDLL
        void initMainDLL() {
            callBackLogPrint("/* 初始化主DLL */");
            try {
                string path = _resController.getResPathAbsolute(_resController._runPath,
                                                                ResSubDir.Config,
                                                                ResNamePrefix.DLLConfig,
                                                                ResType.json);
                callBackLogPrint("DLLConfig路径 = " + path);
                string file = _fileController.readFile(path);
                callBackLogPrint("DLLConfig内容 = ");
                callBackLogPrint(file);

                var data = _jsonController.deserializeStrToObject<DataFile_DLLConfig>(file);
                _dataFileController.addData<DataFile_DLLConfig>(data);

                _dllTypeStr = _mainCmdDict[MainCmdType.DLLType];
                _dllName = data._dllAry[_dllTypeStr]._name;
                _dllDir = _resController._runPath;

                string configStr = _mainCmdDict[MainCmdType.ConfigType];
                ConfigType configType = (ConfigType)Enum.Parse(typeof(ConfigType), configStr, true);
                switch (configType) {
                    case ConfigType.Debug:
                        _dllDir += data._dllAry[_dllTypeStr]._dirDebug;
                        break;
                    default:
                        _dllDir += data._dllAry[_dllTypeStr]._dir;
                        break;
                }
                callBackLogPrint("_dllTypeStr = " + _dllTypeStr);
                callBackLogPrint("_dllName = " + _dllName);
                callBackLogPrint("_dllDir = " + _dllDir);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }

        void loadMainDLL() {
            callBackLogPrint("/* 加载主DLL */");
            try {
                _dllController.loadAssembly(_dllDir, _dllName);
            } catch (Exception e) {
                callBackLogPrint(e.Message);
            }
        }
        #endregion
    }
}
