using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace TeddyNetCore_ServerManager_Web {
    public class Program {
        public static void Main(string[] args) {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
