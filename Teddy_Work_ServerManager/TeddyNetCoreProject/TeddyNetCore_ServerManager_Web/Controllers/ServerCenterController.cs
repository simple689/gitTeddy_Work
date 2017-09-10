using Microsoft.AspNetCore.Mvc;

namespace TeddyNetCore_ServerManager_Web {
    public class ServerCenterController : Controller {
        public IActionResult Index(string a) {
            return View();
        }

        public IActionResult Error() {
            return View();
        }

        public string test() {
            return "test";
        }
    }
}
