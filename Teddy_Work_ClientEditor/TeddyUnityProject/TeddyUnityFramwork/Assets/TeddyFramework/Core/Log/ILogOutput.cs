namespace Teddy {
    public interface ILogOutput { // 日志输出接口
		void print(LogData logData); // 输出日志数据。logData日志数据
        void close(); // 关闭
    }
}