﻿using Microsoft.AspNetCore.Mvc;

namespace TeddyNetCore_ServerManager_Web {
    public class ClientGameController : Controller {
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
