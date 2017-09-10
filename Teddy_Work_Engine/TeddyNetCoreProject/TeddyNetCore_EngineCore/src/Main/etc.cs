//    Socket socket = socketListener.Accept();
//    string receivedValue = string.Empty;
//    while (true) {
//        byte[] receivedBytes = new byte[1024];
//        int numBytes = socket.Receive(receivedBytes);
//        Console.WriteLine("Receiving .");
//        receivedValue += Encoding.ASCII.GetString(receivedBytes, 0, numBytes);
//        if (receivedValue.IndexOf("[FINAL]") > -1) {
//            break;
//        }
//    }
//    Console.WriteLine("Received value: {0}", receivedValue);
//    string replyValue = "";
//    byte[] replyMessage = Encoding.ASCII.GetBytes(replyValue);
//    socket.Send(replyMessage);
//    socket.Shutdown(SocketShutdown.Both);

//string print = string.Format("File: {0} | Method: {1} | Line Number: {2} | Column Number: {3}", "a", "a", "a", "a");

//IHostingEnvironment _hostingEnvironment;
//_hostingEnvironment = new HostingEnvironment();
//string environmentName = _hostingEnvironment.EnvironmentName;
//string applicationName = _hostingEnvironment.ApplicationName;
//string webRootPath = _hostingEnvironment.WebRootPath;
//string contentRootPath = _hostingEnvironment.ContentRootPath;
//_controller.callBackLogPrint(environmentName);
//_controller.callBackLogPrint(applicationName);
//_controller.callBackLogPrint(webRootPath);
//_controller.callBackLogPrint(contentRootPath);

//string applicationName = applicationEnvironment.ApplicationName;
//string applicationVersion = applicationEnvironment.ApplicationVersion;
//FrameworkName runtimeFramework = applicationEnvironment.RuntimeFramework;
//_controller.callBackLogPrint(applicationBasePath);
//            _controller.callBackLogPrint(applicationName);
//            _controller.callBackLogPrint(applicationVersion);
//            Console.WriteLine(runtimeFramework);

//[JsonProperty(PropertyName = "服务器")]
//public Server _server { get; set; }


//string path_Config_Base = EnumUtil<ResSubDir>.getDisplayValue(ResSubDir.Config_Base);
//            callBackLogPrint(path_Config_Base);



//Data_ServerConfig_ServerBase ss = new Data_ServerConfig_ServerBase();
//ss._server = new Server();
//ss._server._host = "aaa";
//            string a = _jsonController.serializeObjectToStr(ss);
//            callBackLogPrint(a);


//foreach (byte b in msgSendByte) {
//    Console.Write(b.ToString("X2") + " ");
//}
//Console.Write("");

//Console.WriteLine("主线程，id = {0}", Thread.CurrentThread.ManagedThreadId);
//Thread t = new Thread(() => {
//Console.WriteLine("任务开始，id = {0}", Thread.CurrentThread.ManagedThreadId);
//});
//t.Start();

//Thread parameterThread = new Thread(new ParameterizedThreadStart(readFileA));

//IFileProvider _fileProvider { get; set; }

//public void test() {
//    ApplicationEnvironment applicationEnvironment = PlatformServices.Default.Application;
//    string applicationBasePath = applicationEnvironment.ApplicationBasePath; // dll路径
//    string applicationName = applicationEnvironment.ApplicationName;
//    string applicationVersion = applicationEnvironment.ApplicationVersion;
//    FrameworkName runtimeFramework = applicationEnvironment.RuntimeFramework;
//    _controller.callBackLogPrint(applicationBasePath);
//    _controller.callBackLogPrint(applicationName);
//    _controller.callBackLogPrint(applicationVersion);
//    Console.WriteLine(runtimeFramework);

//    Console.WriteLine(Directory.GetCurrentDirectory()); // bat路径

//    IFileProvider fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
//    _fileProvider = fileProvider;
//    showStructure((layer, name) => Console.WriteLine("{0}{1}", new string('\t', layer), name));
//}

//public void showStructure(Action<int, string> render) {
//    int layer = -1;
//    Render("", ref layer, render);
//}

//private void Render(string subPath, ref int layer, Action<int, string> render) {
//    layer++;
//    foreach (var fileInfo in _fileProvider.GetDirectoryContents(subPath)) {
//        render(layer, fileInfo.Name);
//        if (fileInfo.IsDirectory) {
//            Render($@"{subPath}\{fileInfo.Name}".TrimStart('\\'), ref layer, render);
//        }
//    }
//    layer--;
//}

//public T Fan<T>(string className) {
//    Type type = Type.GetType(className);
//    T ib = type.Assembly.CreateInstance(className) as T;
//    return ib;
//}

//Program program = new Program();
//Console.WriteLine(program.GetType().Name);