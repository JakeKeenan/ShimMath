using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShimMath.DTO;
using ShimMathAdmin.Models;
using ShimMathAdmin.Models.AdminModels;
using ShimMathCore.BL;
using ShimMathCore.Repository.Models;

namespace ShimMathAdmin.Controllers
{
    public class HomeController : AdminController
    {

        public HomeController(ILogger<HomeController> logger, UserService userService,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : 
            base(logger, userService, userManager, signInManager)
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
