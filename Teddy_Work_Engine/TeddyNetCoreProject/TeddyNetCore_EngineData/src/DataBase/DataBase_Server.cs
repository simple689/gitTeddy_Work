using TeddyNetCore_EngineEnum;

namespace TeddyNetCore_EngineData {
    public class DataBase_Server : DataBase {
        public string _hostLan;
        public string _hostWan;
        public string _hostLocal;
        public HostType _hostType;
        public int _hostPort;
    }
}
