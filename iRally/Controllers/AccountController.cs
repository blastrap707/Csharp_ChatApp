using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using iRally.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iRally.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(userInfo);
            }
            //if (userInfo.UserId == null)
            //{
            //    return RedirectToAction(nameof(Login));view
            //}

            //パスワードのちぇっくに失敗したとする
            ModelState.AddModelError("", "userId or password is incorrect. ");

            var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, userInfo.UserId),
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
            return RedirectToAction("Index", "Chat");
        }

        // ログアウト
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}