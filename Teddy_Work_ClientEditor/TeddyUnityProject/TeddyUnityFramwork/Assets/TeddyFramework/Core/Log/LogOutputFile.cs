using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Teddy {
    public class LogOutputFile : ILogOutput { // 文本日志输出
#if UNITY_EDITOR
        string _devicePersonalPath = Application.dataPath + "/.." + UtilPath._personalPath;
#elif UNITY_STANDALONE_WIN
		string _devicePersonalPath = Application.dataPath + UtilPath._personalPath;
#elif UNITY_STANDALONE_OSX
		string _devicePersonalPath = Application.dataPath + UtilPath._personalPath;
#else
		string _devicePersonalPath = Application.personaDataPath;
#endif

        static string _logPath = "Log";
        private StreamWriter _logWriter = null;
        private Queue<LogData> _logWritingQueue = null;
        private Queue<LogData> _logWaitingQueue = null;
        private object _logLock = null;
        private bool _isRunning = false;
        private Thread _logFileThread = null;

        public LogOutputFile() {
            App.instance._onApplicationQuit += close;

            System.DateTime now = System.DateTime.Now;
            string logName = string.Format("Teddy{0}_{1}_{2}_{3}_{4}_{5}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            string logPath = string.Format("{0}/{1}/{2}.txt", _devicePersonalPath, _logPath, logName);
            if (File.Exists(logPath)) {
                File.Delete(logPath);
            }
            string logDir = Path.GetDirectoryName(logPath);
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            _logWriter = new StreamWriter(logPath);
            _logWriter.AutoFlush = true;

            _logWritingQueue = new Queue<LogData>();
            _logWaitingQueue = new Queue<LogData>();
            _logLock = new object();
            _isRunning = true;

            _logFileThread = new Thread(new ThreadStart(writeLog));
            _logFileThread.Start();
        }

        public void print(LogData logData) {
            lock (_logLock) {
                _logWaitingQueue.Enqueue(logData);
                Monitor.Pulse(_logLock);
            }
        }

        public void close() {
            _isRunning = false;
            _logWriter.Close();
        }

        void writeLog() {
            while (_isRunning) {
                if (_logWritingQueue.Count == 0) {
                    lock (_logLock) {
                        while (_logWaitingQueue.Count == 0) {
                            Monitor.Wait(_logLock);
                        }
                        Queue<LogData> tmpQueue = _logWritingQueue;
                        _logWritingQueue = _logWaitingQueue;
                        _logWaitingQueue = tmpQueue;
                    }
                } else {
                    while (_logWritingQueue.Count > 0) {
                        LogData log = _logWritingQueue.Dequeue();
                        if (log._level == LogLevel.Error) {
                            _logWriter.WriteLine("---------------------------------------------------------------------------------------------------------------------");
                            _logWriter.WriteLine(System.DateTime.Now.ToString() + "\t" + log._log + "\n");
                            _logWriter.WriteLine(log._stackTrace);
                            _logWriter.WriteLine("---------------------------------------------------------------------------------------------------------------------");
                        } else {
                            _logWriter.WriteLine(System.DateTime.Now.ToString() + "\t" + log._log);
                        }
                    }
                }
            }
        }
    }
}