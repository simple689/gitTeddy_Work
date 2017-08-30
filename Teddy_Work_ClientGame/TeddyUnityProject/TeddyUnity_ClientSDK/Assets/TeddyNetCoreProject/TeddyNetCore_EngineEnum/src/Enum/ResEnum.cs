namespace TeddyNetCore_EngineEnum {
    public enum ResSubDir {
        None,
        Config,
        [Display(Name = "Config/Base")]
        Config_Base,
        [Display(Name = "Config/Center")]
        Config_Center,
        Assets,
        Product_ClientSDK,
        Product_ClientGame
    }

    public enum ResNamePrefix {
        None,
        DLLConfig,
        CommonConfig,
        ServerConfig
    }

    public enum ResNamePostfix {
        None,
        Debug
    }

    public enum ResType {
        None,
        config,
        json,
        png
    }
}
