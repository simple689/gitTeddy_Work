namespace TeddyNetCore_EngineEnum {
    public enum MainCmdType {
        DLLType,
        ConfigType,
        HostType,
        ConfigFile,
        Other
    }

    public enum ConfigType {
        Debug
    }

    public enum HostType {
        None,
        Local,
        Lan,
        Wan
    }
}
