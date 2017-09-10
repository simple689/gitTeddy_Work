using Microsoft.AspNetCore.Mvc;

namespace TeddyNetCore_ServerManager_Web {
    public class HomeController : Controller {
        public IActionResult Index() {
            ViewBag._clientGameController = new ClientGameController();
            ViewBag._serverCenterController = new ServerCenterController();
            ViewBag._serverFinanceController = new ServerFinanceController();
            ViewBag._serverGameController = new ServerGameController();
            ViewBag._serverManagerController = new ServerManagerController();
            ViewBag._serverPassportController = new ServerPassportController();

            return View();
        }

        //[HttpGet("/{a}")]
        public IActionResult ServerCenter(string a, string b) {
            ViewBag.Name = a;
            ViewBag.Name2 = b;
            return View("Index");
        }

        public IActionResult Error() {
            return View();
        }
    }
}
