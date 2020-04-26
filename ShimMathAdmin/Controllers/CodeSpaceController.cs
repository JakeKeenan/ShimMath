using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShimMathAdmin.Models.CodeSpaceModels;

namespace ShimMathAdmin.Controllers
{
    public class CodeSpaceController : AdminController
    {
        [HttpGet]
        [Route("CodeSpace")]
        public IActionResult Index()
        {
             
            CodeSpaceModel model = new CodeSpaceHomeModel();
            ViewBag.PageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}{Request.Path}";
            return View("~/Views/Shared/CodeSpace/_CodeSpaceLayout.cshtml", model);
        }

        [HttpGet]
        [Route("CodeSpace/Home/Updates")]
        public IActionResult Updates()
        {
            CodeSpaceModel model = new CodeSpaceHomeModel();
            ViewBag["PageUrl"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}{Request.Path}";
            return View("~/Views/Shared/CodeSpace/_CodeSpaceLayout.cshtml", model);
        }

        [HttpGet]
        [Route("CodeSpace/Home/Announcements")]
        public IActionResult Announcements()
        {
            CodeSpaceModel model = new CodeSpaceHomeModel();
            ViewBag["PageUrl"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}{Request.Path}";
            return View("~/Views/Shared/CodeSpace/_CodeSpaceLayout.cshtml", model);
        }

        [HttpGet]
        [Route("CodeSpace/Home/About")]
        public IActionResult About()
        {
            CodeSpaceModel model = new CodeSpaceHomeModel();
            ViewBag["PageUrl"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}{Request.Path}";
            return View("~/Views/Shared/CodeSpace/_CodeSpaceLayout.cshtml", model);
        }

        [HttpGet]
        [Route("CodeSpace/Home/GettingStarted")]
        public IActionResult GettingStarted()
        {
            CodeSpaceModel model = new CodeSpaceHomeModel();
            ViewBag["PageUrl"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}{Request.Path}";
            return View("~/Views/Shared/CodeSpace/_CodeSpaceLayout.cshtml", model);
        }

        [HttpGet]
        [Route("CodeSpace/Spaceships")]
        public IActionResult Spaceships()
        {
            CodeSpaceModel model = new CodeSpaceSpaceshipsModel();
            ViewBag["PageUrl"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}{Request.Path}";
            return View("~/Views/Shared/CodeSpace/_CodeSpaceLayout.cshtml", model);
        }

        [HttpGet]
        [Route("CodeSpace/GamesList")]
        public IActionResult Games()
        {
            CodeSpaceModel model = new CodeSpaceGamesModel();
            ViewBag["PageUrl"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}{Request.Path}";
            return View("~/Views/Shared/CodeSpace/_CodeSpaceLayout.cshtml", model);
        }

        [HttpGet]
        [Route("CodeSpace/Community")]
        public IActionResult Community()
        {
            CodeSpaceModel model = new CodeSpaceCommunityModel();
            ViewBag["PageUrl"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}{Request.Path}";
            return View("~/Views/Shared/CodeSpace/_CodeSpaceLayout.cshtml", model);
        }

        private List<CodeSpaceCommentModel> getComments()
        {
            return new List<CodeSpaceCommentModel>();
        }
    }
}