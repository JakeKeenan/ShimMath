using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShimMathAdmin.Models;
using ShimMathAdmin.Models.AdminModels;
using ShimMathCore.BL;

namespace ShimMathAdmin.Controllers
{
    public class HomeController : AdminController
    {

        public HomeController(ILogger<HomeController> logger, UserService userService) : base(logger, userService)
        {
        }

        public IActionResult Index()
        {
            LayoutModel layoutModel = new LayoutModel();
            return View(layoutModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
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
