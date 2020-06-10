using ShimMathAdmin.Models.AdminModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using ShimMathAdmin.Models.CodeSpaceModels;
using Microsoft.Extensions.Logging;
using ShimMathCore.BL;
using ShimMath.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ShimMathAdmin.Controllers
{
    public class AdminController : Controller
    {
        protected UserService userSvc;
        protected UserManager<IdentityUser> UserManager;
        protected SignInManager<IdentityUser> SignInManager;

        private readonly ILogger<HomeController> logger;

        public AdminController(ILogger<HomeController> logger, UserService userService,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            userSvc = userService;
            UserManager = userManager;
            SignInManager = signInManager;
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
        public IActionResult IsNotUsedEmail(string email){
            //RedirectToAction();
            ReturnStatus returnStatus = userSvc.IsNotUser(email: email);
            
            return Json(returnStatus);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] AdminLoginModel model)
        {
            IdentityResult result = new IdentityResult();
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    
                };
                
            }

            return Json(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] AdminRegisterModel model)
        {
            //Todo: move stuff to userService
            IdentityResult result = new IdentityResult();
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser 
                { 
                    UserName = model.Username, Email = model.Email 
                };
                result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return Json(result);
        }

        [HttpGet]
        public IActionResult testAPICall()
        {

            return Json("hey");
        }

    }
}

/*
 if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser { UserName = model.Username, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
 */
