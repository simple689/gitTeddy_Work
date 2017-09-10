using System;
using System.IO;
using TeddyNetCore_EngineEnum;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace TeddyNetCore_EngineCore {
    public class FileController {
        EngineBase _controller;

        public void init(EngineBase controller) {
            _controller = controller;
        }

        public string getPath(FilePathType pathType) {
            string path = "";
            try {
                switch (pathType) {
                    case FilePathType.DLL:
                        path = Environment.CurrentDirectory; // 获取和设置当前目录(该进程从中启动的目录)的完全限定目录。
                        break;
                    case FilePathType.Run:
                        path = Directory.GetCurrentDirectory(); // 获取应用程序的当前工作目录。bat路径
                        break;
                    default:
                        //path = AppDomain.CurrentDomain.BaseDirectory; // 获取程序的基目录。
                        //path = Process.GetCurrentProcess().MainModule.FileName; // 获取模块的完整路径。
                        //path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase; // 获取和设置包括该应用程序的目录的名称。
                        break;
                }
                String[] strAry = path.Split('\\');
                StringBuilder strBuilder = new StringBuilder();
                foreach (var v in strAry) {
                    if (v.Length <= 0) {
                        continue;
                    }
                    strBuilder.Append(v)
                              .Append('/');
                }
                path = strBuilder.ToString(0, strBuilder.Length - 1);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
            return path;
        }

        #region readFile
        public string readFile(string path) {
            string readStr = "";
            try {
                using (FileStream fsRead = new FileStream(path, FileMode.Open)) {
                    byte[] bytes = new byte[fsRead.Length];
                    while (fsRead.Position < fsRead.Length) {
                        fsRead.Read(bytes, 0, bytes.Length);
                    }
                    readStr = Encoding.UTF8.GetString(bytes);
                }
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
            return readStr;
        }

        public void readFileThread(ReadFileParameter readFileParameter) {
            try {
                Thread t = new Thread(readFile);
                t.Name = "readFileThread";
                t.Start(readFileParameter);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        void readFile(object obj) {
            try {
                ReadFileParameter readFileParameter = (ReadFileParameter)obj;
                string readStr = readFile(readFileParameter._path);
                readFileParameter._callBackReadFile(readFileParameter._path, readStr);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }
        #endregion

        #region writeFile
        public bool writeFile(string path, string writeStr) {
            bool result = true;
            try {
                using (FileStream fsWrite = new FileStream(path, FileMode.Create)) {
                    byte[] bytes = Encoding.UTF8.GetBytes(writeStr);
                    fsWrite.Write(bytes, 0, bytes.Length);
                }
            } catch (Exception e) {
                result = false;
                _controller.callBackLogPrint(e.Message);
            }
            return result;
        }

        public void writeFileThread(WriteFileParameter writeFileParameter) {
            try {
                Thread t = new Thread(readFile);
                t.Name = "writeFileThread";
                t.Start(writeFileParameter);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }

        void writeFile(object obj) {
            try {
                WriteFileParameter writeFileParameter = (WriteFileParameter)obj;
                bool result = writeFile(writeFileParameter._path, writeFileParameter._writeStr);
                writeFileParameter._callBackWriteFile(writeFileParameter._path, result);
            } catch (Exception e) {
                _controller.callBackLogPrint(e.Message);
            }
        }
        #endregion

        #region readAndWriteFile
        public bool readAndWriteFile(string readPath, string writePath) {
            bool result = true;
            try {
                using (FileStream fsRead = new FileStream(readPath, FileMode.Open)) { // 当没有读取到文件的末尾的时候就需要循环读取
                    using (FileStream fsWrite = new FileStream(writePath, FileMode.Create)) {
                        byte[] bytes = new byte[1024];
                        while (fsRead.Position < fsRead.Length) {
                            int count = fsRead.Read(bytes, 0, bytes.Length); // 读取的时候position属性会自动变化，记住当前读取到的位置，以字节为单位，count可以获取当前具体读取到的字节数
                            if (count == 0) {
                                break;
                            }
                            fsWrite.Write(bytes, 0, count); // 只需要写入读取到的字节数就可以了
                        }
                    }
                }
            } catch (Exception e) {
                result = false;
                _controller.callBackLogPrint(e.Message);
            }
            return result;
        }
        #endregion
    }
}
