using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Wprawka_1.Models;
using Wprawka_1.Data;

namespace Wprawka_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VoltShareContext _context;
        public HomeController(ILogger<HomeController> logger, VoltShareContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users
                .Include(u => u.Homes)
                .Include(u => u.Clusters)
                .ToList();

            ViewBag.Clusters = _context.Clusters
                .Include(c => c.Users)
                .ToList();

            return View(users);
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
    }
}
