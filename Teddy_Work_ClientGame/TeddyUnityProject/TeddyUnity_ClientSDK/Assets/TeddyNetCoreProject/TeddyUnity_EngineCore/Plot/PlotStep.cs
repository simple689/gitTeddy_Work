using System;
using System.Collections.Generic;
using System.Reflection;
using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;
using UnityEngine;

namespace TeddyUnity_EngineCore {
    public class PlotStep {
        EngineBase _controller;

        PlotSection _plotSection;
        PlotStep _parentPlotStep;
        string _plotStepKey;
        public DataBase_PlotStep _plotStepData;

        PlotStepStatus _plotStepStatus = PlotStepStatus.None;
        int _plotFrameStart = 0;

        public Dictionary<string, object> _plotCacheDict = new Dictionary<string, object>();
        List<Plot> _plotList = new List<Plot>();

        Dictionary<string, PlotStep> _plotStepCacheDict = new Dictionary<string, PlotStep>();
        Dictionary<string, PlotStep> _plotStepDict = new Dictionary<string, PlotStep>();
        List<string> _plotStepStopAndDelList = new List<string>();

        public void init(EngineBase controller, PlotSection plotSection, PlotStep parentPlotStep, string key, DataBase_PlotStep data) {
            _controller = controller;
            _plotSection = plotSection;
            _parentPlotStep = parentPlotStep;
            _plotStepKey = key;
            _plotStepData = data;

            _plotStepStatus = PlotStepStatus.Init;
        }

        public void start(bool reloadFile) {
            try {
                if (_plotStepData._plotList != null) {
                    foreach (var item in _plotStepData._plotList) {
                        Type type = Type.GetType("TeddyUnity_EngineCore" + "." + item._plotType.ToString(), true);
                        MethodInfo method = type.GetMethod("init");
                        string plotCacheKey = type.ToString() + _plotStepKey;
                        object plot = null;
                        if (_plotCacheDict.ContainsKey(plotCacheKey)) {
                            plot = _plotCacheDict[plotCacheKey];
                        } else {
                            plot = Activator.CreateInstance(type);
                            _plotCacheDict.Add(plotCacheKey, plot);
                        }
                        object[] parameters = { _controller, this, item };
                        method.Invoke(plot, parameters);

                        _plotList.Add(plot as Plot);
                        method = type.GetMethod("start");
                        method.Invoke(plot, null);
                    }
                }

                if (_plotStepData._plotStepDict != null) {
                    foreach (var item in _plotStepData._plotStepDict) {
                        PlotStep plotStep = null;
                        if (reloadFile) {
                            plotStep = new PlotStep();
                            plotStep.init(_controller, _plotSection, this, item.Key, item.Value);
                        } else {
                            plotStep = _plotStepCacheDict[item.Key];
                            plotStep.init(_controller, _plotSection, this, item.Key, _plotStepData);
                        }
                        _plotStepCacheDict.Remove(item.Key);
                        _plotStepCacheDict.Add(item.Key, plotStep);
                        plotStep.start(reloadFile);
                    }
                }
                _plotStepStatus = PlotStepStatus.Start;
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        public void update() {
            try {
                if (_plotStepStatus == PlotStepStatus.Start) {
                    _plotFrameStart = Time.frameCount;
                    _plotStepStatus = PlotStepStatus.Update;
                }
                _controller.callBackLogPrint("PlotStep 更新：" + _plotStepKey);

                foreach (var item in _plotList) {
                    if (item._plotData._timeStart <= (Time.frameCount - _plotFrameStart)) {
                        if (item.isUpdate()) {
                            item.update();
                        }
                    }
                }

                foreach (var item in _plotStepStopAndDelList) {
                    getPlotStep(item).stop();
                    _plotStepDict.Remove(item);
                }
                _plotStepStopAndDelList.Clear();
                foreach (var item in _plotStepDict) {
                    item.Value.update();
                }
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        public void stop() {
            try {
                foreach (var item in _plotList) {
                    item.stop();
                }
                foreach (var item in _plotStepDict) {
                    item.Value.stop();
                }
                _plotStepDict.Clear();
                _plotStepStatus = PlotStepStatus.Stop;
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        public PlotStep getPlotStep(string key) {
            _controller.callBackLogPrint("/* 获取 PlotStep */");
            PlotStep plotStep = null;
            try {
                plotStep = _plotStepDict[key];
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
            return plotStep;
        }
    }
}
