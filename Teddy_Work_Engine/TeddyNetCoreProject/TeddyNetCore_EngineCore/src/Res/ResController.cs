using System;
using System.Text;
using TeddyNetCore_EngineEnum;

namespace TeddyNetCore_EngineCore {
    public class ResController {
        EngineBase _controller;

        public string _dllPath { get; set; }
        public string _runPath { get; set; }
        public string _resPath { get; set; }

        public void init(EngineBase controller) {
            _controller = controller;
        }

        public string getResPathRelative(string resSubDir, string resNamePrefix, string custom, string resNamePostfix, string resType) {
            StringBuilder strBuilder = new StringBuilder();
            try {
                strBuilder.Append("/");
                if (Util.isValidString(resSubDir)) {
                    strBuilder.Append(resSubDir);
                    strBuilder.Append("/");
                }
                if (Util.isValidString(resNamePrefix)) {
                    strBuilder.Append(resNamePrefix);
                    if (Util.isValidString(custom)) {
                        strBuilder.Append("_");
                    } else if (Util.isValidString(resNamePostfix)) {
                        strBuilder.Append("_");
                    }
                }
                if (Util.isValidString(custom)) {
                    strBuilder.Append(custom);
                    if (Util.isValidString(resNamePostfix)) {
                        strBuilder.Append("_");
                    }
                }
                if (Util.isValidString(resNamePostfix)) {
                    strBuilder.Append(resNamePostfix);
                }
                if (Util.isValidString(resType)) {
                    strBuilder.Append(".");
                    strBuilder.Append(resType);
                }
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
            return strBuilder.ToString();
        }

        public string getResPathRelative(ResSubDir resSubDir, ResNamePrefix resNamePrefix, string custom, ResNamePostfix resNamePostfix, ResType resType) {
            return getResPathRelative(EnumUtil<ResSubDir>.getDisplayValue(resSubDir), resNamePrefix.ToString(), custom, resNamePostfix.ToString(), resType.ToString());
        }

        public string getResPathAbsolute(string rootPath, ResSubDir resSubDir, ResNamePrefix resNamePrefix, string custom, ResNamePostfix resNamePostfix, ResType resType) {
            StringBuilder strBuilder = new StringBuilder();
            try {
                strBuilder.Append(rootPath);
                strBuilder.Append(getResPathRelative(resSubDir, resNamePrefix, custom, resNamePostfix, resType));
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
            return strBuilder.ToString();
        }

        public string getResPathRelative(ResSubDir resSubDir, ResNamePrefix resNamePrefix, string custom, ResType resType) {
            return getResPathRelative(resSubDir, resNamePrefix, custom, ResNamePostfix.None, resType);
        }

        public string getResPathAbsolute(string rootPath, ResSubDir resSubDir, ResNamePrefix resNamePrefix, string custom, ResType resType) {
            StringBuilder strBuilder = new StringBuilder();
            try {
                strBuilder.Append(rootPath);
                strBuilder.Append(getResPathRelative(resSubDir, resNamePrefix, custom, ResNamePostfix.None, resType));
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
            return strBuilder.ToString();
        }

        public string getResPathRelative(ResSubDir resSubDir, ResNamePrefix resNamePrefix, ResType resType) {
            return getResPathRelative(resSubDir, resNamePrefix, "", ResNamePostfix.None, resType);
        }

        public string getResPathAbsolute(string rootPath, ResSubDir resSubDir, ResNamePrefix resNamePrefix, ResType resType) {
            StringBuilder strBuilder = new StringBuilder();
            try {
                strBuilder.Append(rootPath);
                strBuilder.Append(getResPathRelative(resSubDir, resNamePrefix, "", ResNamePostfix.None, resType));
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
            return strBuilder.ToString();
        }

        public string getResPathRelative(ResNamePrefix resNamePrefix, ResType resType) {
            return getResPathRelative(ResSubDir.None, resNamePrefix, "", ResNamePostfix.None, resType);
        }

        public string getResPathAbsolute(string rootPath, ResNamePrefix resNamePrefix, ResType resType) {
            StringBuilder strBuilder = new StringBuilder();
            try {
                strBuilder.Append(rootPath);
                strBuilder.Append(getResPathRelative(ResSubDir.None, resNamePrefix, "", ResNamePostfix.None, resType));
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
            return strBuilder.ToString();
        }

        public string getResPathAbsolute(string rootPath, string resSubDir, ResNamePrefix resNamePrefix, string custom, ResNamePostfix resNamePostfix, ResType resType) {
            StringBuilder strBuilder = new StringBuilder();
            try {
                strBuilder.Append(rootPath);
                strBuilder.Append(getResPathRelative(resSubDir, resNamePrefix.ToString(), custom, resNamePostfix.ToString(), resType.ToString()));
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
            return strBuilder.ToString();
        }
    }
}
