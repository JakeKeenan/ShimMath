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
using ShimMathAdmin.Controllers.ControllerTools;
using ShimMathAdmin.Models;
using ShimMathAdmin.Models.AccountModels;
using ShimMathAdmin.Models.AdminModels;
using ShimMathCore.BL;
using ShimMathCore.Repository.Models;

namespace ShimMathAdmin.Controllers
{
    public class HomeController : AdminController
    {

        public HomeController(ILogger<HomeController> logger, UserService userService) : 
            base(logger, userService)
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

        public IActionResult Home()
        {
            LayoutModel layoutModel = new LayoutModel();
            return View(layoutModel);
        }

        public async Task<IActionResult> TestAction()
        {
            string email = "Huglowk@gmail.com";
            VerifyEmailModel model = new VerifyEmailModel()
            {
                ConfirmationUrl = await userSvc.GetVerificationCodeAsync(email),
                UserName = "TestUsername",
            };
            string emailView = await ControllerExtensions.RenderViewAsync(this, "~/Views/Email/VerifyEmail.cshtml", model);
            //"Liam Cullers (Google Slides)" <liamcullers@gmail.com>
            await userSvc.SendVerificationEmailAsync(email, emailView);
            LayoutModel layoutModel = new LayoutModel();
            return View("~/Views/Home/Home.cshtml", layoutModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
