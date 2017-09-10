using System;

namespace TeddyNetCore_EngineCore {
    public struct ReadFileParameter {
        public string _path;
        public Func<string, string, bool> _callBackReadFile;

        public ReadFileParameter(string path, Func<string, string, bool> callBackReadFile) {
            _path = path;
            _callBackReadFile = callBackReadFile;
        }
    }

    public struct WriteFileParameter {
        public string _path;
        public string _writeStr;
        public Func<string, bool, bool> _callBackWriteFile;

        public WriteFileParameter(string path, string writeStr, Func<string, bool, bool> callBackWriteFile) {
            _path = path;
            _writeStr = writeStr;
            _callBackWriteFile = callBackWriteFile;
        }
    }
}
