namespace Teddy {
    public enum LogLevel { // 日志等级，为不同输出配置用。
        Info = 0,
        Warning = 1,
        Assert = 2,
        Error = 3,
        Exception = 4,
        Max = 5,
    }

    public class LogData { // 日志数据类
        public string _log { get; set; }
        public string _stackTrace { get; set; }
        public LogLevel _level { get; set; }
    }
}