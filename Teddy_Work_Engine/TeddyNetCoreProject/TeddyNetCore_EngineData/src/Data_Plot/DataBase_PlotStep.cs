using System.Collections.Generic;

namespace TeddyNetCore_EngineData {
    public class DataBase_PlotStep : DataBase {
        public DataBase_PlotStepConfig _config = new DataBase_PlotStepConfig();

        public List<DataBase_Plot> _plotList = new List<DataBase_Plot>();
        public Dictionary<string, DataBase_PlotStep> _plotStepDict = new Dictionary<string, DataBase_PlotStep>();
    }
}
