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

        public string getResPathRelative(ResSubDir resSubDir, ResNamePrefix resNamePrefix, string custom, ResNamePostfix resNamePostfix, ResType resType) {
            StringBuilder strBuilder = new StringBuilder();
            try {
                strBuilder.Append("/");
                if (resSubDir != ResSubDir.None) {
                    strBuilder.Append(EnumUtil<ResSubDir>.getDisplayValue(resSubDir));
                    strBuilder.Append("/");
                }
                if (resNamePrefix != ResNamePrefix.None) {
                    strBuilder.Append(resNamePrefix.ToString());
                    if (custom.Length > 0) {
                        strBuilder.Append("_");
                    } else if (resNamePostfix != ResNamePostfix.None) {
                        strBuilder.Append("_");
                    }
                }
                if (custom.Length > 0) {
                    strBuilder.Append(custom);
                    if (resNamePostfix != ResNamePostfix.None) {
                        strBuilder.Append("_");
                    }
                }
                if (resNamePostfix != ResNamePostfix.None) {
                    strBuilder.Append(resNamePostfix.ToString());
                }
                if (resType != ResType.None) {
                    strBuilder.Append(".");
                    strBuilder.Append(resType.ToString());
                }
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
            return strBuilder.ToString();
        }

        public string getResPathRelative(ResSubDir resSubDir, ResNamePrefix resNamePrefix, ResType resType) {
            return getResPathRelative(resSubDir, resNamePrefix, "", ResNamePostfix.None, resType);
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
    }
}
