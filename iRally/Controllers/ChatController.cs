using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using iRally.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace iRally.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var history = DBmanager.GetChatHistory();
            return View(history);
        }

        [HttpPost]
        public IActionResult Index(string message)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DBmanager.AddMessageInfo(message, userId);
            return RedirectToAction(nameof(Index));
        }
    }
}