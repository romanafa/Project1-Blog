
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Oblig2_Blog.Hubs;
using Oblig2_Blog.Models;

namespace Oblig2_Blog.Controllers
{
    public class SignalRController : Controller
    {
        private readonly IHubContext<NotificationHub> _notificationHub;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
