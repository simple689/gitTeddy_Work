using System;
using Newtonsoft.Json.Linq;
using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;

namespace TeddyUnity_EngineCore {
    public class JsonPlotConverter : JsonCreationConverter<DataBase_Plot> {
        protected override DataBase_Plot Create(Type objectType, JObject jsonObject) {
            PlotType plotType = (PlotType)Enum.Parse(typeof(PlotType), jsonObject["_plotType"].ToString(), true);
            switch (plotType) {
                case PlotType.PlotEvent:
                return new DataBase_PlotEvent();
                case PlotType.PlotSound:
                return new DataBase_PlotSound();
                case PlotType.PlotUI:
                return new DataBase_PlotUI();
                default:
                break;
            }
            return new DataBase_Plot();
        }
    }
}
