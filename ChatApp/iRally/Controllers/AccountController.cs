using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iRally.Model;
using Microsoft.AspNetCore.Mvc;

namespace iRally.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(UserInfo user)
        {
            var userId = TempData["userId"].ToString();
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string hoge)
        {
            var userId = TempData["userId"].ToString();
            return View();
        }
    }
}