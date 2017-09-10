namespace TeddyNetCore_EngineData {
    public class DataBase_PlotSound : DataBase_Plot {
        public DataBase_PlotSoundBase _sound = new DataBase_PlotSoundBase();
    }

    public class DataBase_PlotSoundBase : DataBase {
        public string _name;

        public bool _isFadeIn;
        public bool _isFadeOut;
    }
}
