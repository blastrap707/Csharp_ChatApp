using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using iRally.Model;
using iRally.Model.DB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iRally.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserInfo userInfo)
        {
            if (!ModelState.IsValid) return View(userInfo);

            var userDb = new UserDB(userInfo);
            if (!userDb.IfUserExits) return LocalRedirect("/Account/Register");
            if (!userDb.IfHashed) return LocalRedirect("/Account/ChangePassword");
            if (!userDb.IfPasswordCorrect) return View(userInfo);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userInfo.UserId)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
            return LocalRedirect("/Chat/Index");
        }

        // ログアウト
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}