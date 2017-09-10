namespace TeddyNetCore_EngineData {
    public class DataBase_PlotEvent : DataBase_Plot {
        public DataBase_PlotEventBase _event = new DataBase_PlotEventBase();
    }

    public class DataBase_PlotEventBase : DataBase {
        public string _name;
    }
}
