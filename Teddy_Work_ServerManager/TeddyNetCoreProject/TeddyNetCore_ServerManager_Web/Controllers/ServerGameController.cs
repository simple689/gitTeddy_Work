using Microsoft.AspNetCore.Mvc;

namespace TeddyNetCore_ServerManager_Web {
    public class ServerGameController : Controller {
        public IActionResult Index() {
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
