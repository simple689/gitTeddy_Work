using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Teddy {
    public class Log : Singleton<Log> { // 封装日志模块
        public LogLevel _uiLogLevel = LogLevel.Info; // UI输出日志等级，只要大于等于这个级别的日志，都会输出到屏幕
        public LogLevel _fileLogLevel = LogLevel.Max; // 文本输出日志等级，只要大于等于这个级别的日志，都会输出到文本

        public Dictionary<LogType, LogLevel> _logTypeLevelDict = null; // 日志和日志输出等级的映射
        private List<ILogOutput> _logOutputList = null; // 日志输出列表
        private int _mainThreadID = -1;

        public delegate void OnGUICallback(); // OnGUI回调
        public OnGUICallback _onGUICallback = null; // OnGUI回调

        private Log() { // Debug.logger.Log or delegate registered with Application.RegisterLogCallback.
            Application.logMessageReceived += logCallback;
            Application.logMessageReceivedThreaded += logMultiThreadCallback;

            _uiLogLevel = LogLevel.Info;
            _fileLogLevel = LogLevel.Max;
            _logTypeLevelDict = new Dictionary<LogType, LogLevel> {
                { LogType.Log, LogLevel.Info },
                { LogType.Warning, LogLevel.Warning },
                { LogType.Assert, LogLevel.Assert },
                { LogType.Error, LogLevel.Error },
                { LogType.Exception, LogLevel.Exception },
            };
            _logOutputList = new List<ILogOutput> {
                new LogOutputFile(),
                new LogOutputConsole(),
            };
            _mainThreadID = Thread.CurrentThread.ManagedThreadId;

            App.instance._onGUI += onGUI;
            App.instance._onDestroy += onDestroy;
        }

        void onDestroy() {
            Application.logMessageReceived -= logCallback;
            Application.logMessageReceivedThreaded -= logMultiThreadCallback;
        }

        void onGUI() {
            if (_onGUICallback != null) {
                _onGUICallback();
            }
        }

        void logCallback(string condition, string stackTrace, LogType type) { // 日志调用回调，主线程和其他线程都会回调这个函数，在其中根据配置输出日志。condition日志，stackTrace堆栈追踪，type日志类型
            if (_mainThreadID == Thread.CurrentThread.ManagedThreadId)
                output(condition, stackTrace, type);
        }

        void logMultiThreadCallback(string condition, string stackTrace, LogType type) {
            if (_mainThreadID != Thread.CurrentThread.ManagedThreadId)
                output(condition, stackTrace, type);
        }

        void output(string condition, string stackTrace, LogType type) {
            LogLevel level = _logTypeLevelDict[type];
            LogData logData = new LogData {
                _log = condition,
                _stackTrace = stackTrace,
                _level = level,
            };
            for (int i = 0; i < _logOutputList.Count; ++i) {
                _logOutputList[i].print(logData);
            }
        }

        public void assert(bool condition, string info) { // Unity的Debug.Assert()在发布版本有问题。condition条件，info输出信息
            if (condition) {
                return;
            }
            print(info, LogLevel.Assert);
        }

        public void print(string condition, LogLevel logLevel = LogLevel.Info) {
            switch (logLevel) {
                case LogLevel.Info: {
                    Debug.Log(condition);
                    break;
                }
                case LogLevel.Warning: {
                    Debug.LogWarning(condition);
                    break;
                }
                case LogLevel.Assert: {
                    Debug.LogAssertion(condition);
                    break;
                }
                case LogLevel.Error: {
                    Debug.LogError(condition);
                    break;
                }
                case LogLevel.Exception: {
                    //Debug.LogException(condition);
                    Debug.Log(condition);
                    break;
                }
                default: {
                    Debug.Log(condition);
                    break;
                }
            }
        }
    }
}