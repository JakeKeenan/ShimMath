using ShimMathAdmin.Models.AdminModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using ShimMathAdmin.Models.CodeSpaceModels;
using Microsoft.Extensions.Logging;
using ShimMathCore.BL;
using System.Runtime.InteropServices.WindowsRuntime;
using ShimMath.DTO;

namespace ShimMathAdmin.Controllers
{
    public class AdminController : Controller
    {
        private UserService userSvc;
        private readonly ILogger<HomeController> logger;

        public AdminController(ILogger<HomeController> logger, UserService userService)
        {
            userSvc = userService;
            this.logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult EditText(string newText, string oldText, string fileToEdit)//Change to take in file name to edit
        {
            string fileContent;
            using (StreamReader streamReader = new StreamReader(fileToEdit))//needs actual file name, not just the path
            {
                fileContent = streamReader.ReadToEnd();
                streamReader.Close();
            }
            //<h1 id="WelcomeHeading">Welcome To Code Space</h1>
            //\<([a-zA-Z1-9]*)\s.*id\s*=\s*\"editHeadExitOptions\"((.|\n)*?)\<\/\1>
            //Match match = Regex.Match(fileContent, "\\<([a-zA-Z1-9]*)\\s.*id\\s*=\\s*\"" + elementID + "\\s*\"((.|\n)*?)\\<\\/\\1>");
            //string modifiedContent = Regex.Replace(fileContent, "\\<.*id\\s*=\\s*\"" + elementID + "\".*\\>.*\\</.*\\>", "");
            string modifiedContent = Regex.Replace(fileContent,
                    oldText,
                    newText);
            using (StreamWriter streamWriter = new StreamWriter(fileToEdit))
            {
                streamWriter.Write(modifiedContent);
                streamWriter.Close();
            }

            return CreatedAtAction("EditText", oldText);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult IsNotUser(string username)
        {
            //RedirectToAction();
            ReturnStatus returnStatus = userSvc.IsNotUser(username: username);

            return Json(returnStatus);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult IsNotUsedEmail(string email)        {
            //RedirectToAction();
            ReturnStatus returnStatus = userSvc.IsNotUser(email: email);

            return Json(returnStatus);
        }

        [HttpGet]
        [ActionName("Admin/Login")]
        public IActionResult Login()
        {
            return View("~/Views/Admin/Login.cshtml");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [ActionName("Admin/Register")]
        public IActionResult Register(AdminRegisterModel model)
        {
            IActionResult retVal = new BadRequestResult();
            AdminUserCredentials newAdminUser = new AdminUserCredentials()
            {
                Username = model.Username,
                Password = model.Password,
            };
            AdminUserCredentials.SecretKey = "";
            ReturnStatus returnStatus = userSvc.AddAdmin(newAdminUser);
            if (returnStatus.IsSuccessful)
            {
                 retVal = CreatedAtAction("Register", model.Username);
            }
            return retVal;
        }

        [HttpGet]
        public IActionResult testAPICall()
        {

            return Json("hey");
        }

    }
}