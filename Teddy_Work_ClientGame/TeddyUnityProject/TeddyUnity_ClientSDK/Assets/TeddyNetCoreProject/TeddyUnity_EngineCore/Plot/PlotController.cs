using System;
using System.Collections.Generic;
using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;

namespace TeddyUnity_EngineCore {
    public class PlotController {
        EngineBase _controller;

        string _plotSubDir;
        string _mainPlotKey;

        Dictionary<string, PlotSection> _plotSectionDict = new Dictionary<string, PlotSection>();
        Dictionary<string, PlotSection> _plotSectionCacheDict = new Dictionary<string, PlotSection>();
        List<string> _plotSectionStopAndDelList = new List<string>();

        public void init(EngineBase controller) {
            _controller = controller;

            initPlotConfig();         }

        public void start() {
            startAndAddPlotSection(_mainPlotKey, true);
        }

        public void update() {
            try {
                foreach (var item in _plotSectionStopAndDelList) {
                    getPlotSection(item).stop();
                    _plotSectionDict.Remove(item);
                }
                _plotSectionStopAndDelList.Clear();
                foreach (var item in _plotSectionDict) {
                    PlotSection plotSection = item.Value;
                    plotSection.update();
                }
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        public void stop() {
            try {
                foreach (var item in _plotSectionDict) {
                    PlotSection plotSection = item.Value;
                    plotSection.stop();
                }
                _plotSectionDict.Clear();
                _plotSectionStopAndDelList.Clear();
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        #region init
        void initPlotConfig() {
            _controller.callBackLogPrint("/* 初始化PlotConfig */");
            try {
                string path = _controller._resController.getResPathAbsolute(
                                                            _controller._resController._resPath,
                                                            ResSubDir.Config,
                                                            ResNamePrefix.PlotConfig,
                                                            ResType.json);
                _controller.callBackLogPrint("PlotConfig路径 = " + path);
                string file = _controller._fileController.readFile(path);
                _controller.callBackLogPrint("PlotConfig内容 = \n" + file);
                var data = _controller._jsonController.deserializeObject<DataFile_PlotConfig>(file);
                _controller._dataFileController.addData<DataFile_PlotConfig>(data);

                _plotSubDir = data._plotSubDir;
                _mainPlotKey = data._mainPlotKey;
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }
        #endregion

        void startAndAddPlotSection(string key, bool reloadFile) {
            _controller.callBackLogPrint("/* 开启并且添加 PlotSection 文件 */");
            try {
                PlotSection plotSection = null;
                if (_plotSectionCacheDict.ContainsKey(key) && reloadFile == false) {
                    plotSection = _plotSectionCacheDict[key];
                    plotSection.init(_controller, key, plotSection._plotSectionData);
                } else {
                    string path = _controller._resController.getResPathAbsolute(
                                                            _controller._resController._resPath,
                                                            _plotSubDir,
                                                            ResNamePrefix.None,
                                                            key,
                                                            ResNamePostfix.None,
                                                            ResType.json);
                    _controller.callBackLogPrint("PlotSection路径 = " + path);
                    string file = _controller._fileController.readFile(path);
                    _controller.callBackLogPrint("PlotSection内容 = \n" + file);
                    var data = _controller._jsonController.deserializeObject<DataFile_PlotSection>(file, new JsonPlotConverter());

                    plotSection = new PlotSection();
                    plotSection.init(_controller, key, data);
                }
                startAndAddPlotSection(key, plotSection, reloadFile);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        bool startAndAddPlotSection(string key, PlotSection plotSection, bool reloadFile) {
            _controller.callBackLogPrint("/* 开启并且添加 PlotSection */");
            bool result = true;
            try {
                plotSection.start(reloadFile);
                _plotSectionDict.Remove(key);
                _plotSectionCacheDict.Remove(key);
                _plotSectionDict.Add(key, plotSection);
                _plotSectionCacheDict.Add(key, plotSection);
            } catch (Exception e) {
                result = false;
                _controller.callBackLogPrint(e.Message);
            }
            return result;
        }

        public bool stopAndDelPlotSection(string key) {
            _controller.callBackLogPrint("/* 停止并且删除 PlotSection */");
            bool result = true;
            try {
                _plotSectionStopAndDelList.Add(key);
            } catch (Exception e) {
                result = false;
                _controller.callBackLogPrint(e.Message);
            }
            return result;
        }

        public PlotSection getPlotSection(string key) {
            _controller.callBackLogPrint("/* 获取 PlotSection */");
            PlotSection plotSection = null;
            try {
                plotSection = _plotSectionDict[key];
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
            return plotSection;
        }
    }
}
