using ContactList.Configuration;
using ContactListMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace ContactListMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ContactListApiOptions _options;

        public HomeController(
            ILogger<HomeController> logger,
            IOptions<ContactListApiOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

      
        public IActionResult Index()
        {
            ViewBag.ContactListApiUrl = _options.Url;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Test()
        {
            return View("Test");
        }
    }
}
