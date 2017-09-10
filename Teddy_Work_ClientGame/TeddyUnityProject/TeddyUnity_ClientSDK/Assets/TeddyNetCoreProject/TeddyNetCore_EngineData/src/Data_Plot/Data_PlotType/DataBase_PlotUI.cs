namespace TeddyNetCore_EngineData {
    public class DataBase_PlotUI : DataBase_Plot {
        public DataBase_PlotUIBase _ui = new DataBase_PlotUIBase();
    }

    public class DataBase_PlotUIBase : DataBase {
        public string _name;
    }
}
