namespace TeddyNetCore_EngineEnum {
    public enum PlotStepStatus {
        None,
        Init,
        Start,
        Update,
        Stop
    }

    public enum PlotStatus {
        None,
        Init,
        Start,
        Update,
        FadeIn,
        Smooth,
        FadeOut,
        Stop
    }

    public enum PlotType {
        Plot,
        PlotEvent,
        PlotSound,
        PlotUI
    }
}
