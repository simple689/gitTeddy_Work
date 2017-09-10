using System.IO;

namespace Teddy {
    public class UtilIO { // 各种文件的读写复制操作,主要是对System.IO的一些封装
        public static void CreateDirIfNotExists(string dirFullPath) { // 创建新的文件夹,如果存在则不创建
            if (!Directory.Exists(dirFullPath)) {
                Directory.CreateDirectory(dirFullPath);
            }
        }
    }
}
