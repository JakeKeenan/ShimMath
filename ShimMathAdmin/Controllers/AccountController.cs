
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShimMath.DTO;
using ShimMathAdmin.Controllers.ControllerTools;
using ShimMathAdmin.Models.AccountModels;
using ShimMathCore.BL;

namespace ShimMathAdmin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private UserService userSvc;
        private readonly ILogger<HomeController> logger;

        public AccountController(ILogger<HomeController> logger, UserService userService)
        {
            userSvc = userService;
            this.logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            ObjectResult retVal = BadRequest(400);
            ShimMathUser newUser = new ShimMathUser()
            {
                Username = registerModel.Username,
                Email = registerModel.Email,
                Password = registerModel.Password,
            };
            ReturnStatus registerReturnStatus = await userSvc.RegisterAsync(newUser);
            if (registerReturnStatus.IsSuccessful)
            {
                ReturnStatus emailSendReturnStatus = await SendEmailVarification(newUser.Email);
                retVal = Ok(registerReturnStatus);
            }
            else
            {
                retVal = BadRequest(registerReturnStatus);
            }
            return retVal;
            //return Ok(returnStatus);
         }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            ObjectResult retVal = BadRequest(400);
            ShimMathUser loginUser = new ShimMathUser()
            {
                Username = loginModel.Username,
                Email = loginModel.Email,
                Password = loginModel.Password,
            };
            ReturnStatus returnStatus = await userSvc.Login(loginUser);
            if (returnStatus.IsSuccessful)
            {
                //await userSvc.Login(newUser);
                retVal = Ok(returnStatus);
            }
            else
            {
                retVal = BadRequest(returnStatus);
            }
            return retVal;
        }

        [HttpGet("IsUsedUsername")]
        public async Task<bool> IsUsedUsername(string username)
        {
            bool returnStatus = await userSvc.IsUsedUsernameAsync(username);

            return returnStatus;

        }

        [HttpGet("IsNotUsedUsername")]
        public async Task<bool> IsNotUsedUsername(string username)
        {
            bool returnStatus = await userSvc.IsNotUsedUsernameAsync(username);

            return returnStatus;
        }

        [HttpGet("IsUsedEmail")]
        public async Task<bool> IsUsedEmail(string email)
        {
            bool returnStatus = await userSvc.IsUsedEmailAsync(email);
            return returnStatus;
        }

        [HttpGet("IsNotUsedEmail")]
        public async Task<bool> IsNotUsedEmail(string email)
        {
            bool returnStatus = await userSvc.IsNotUsedEmailAsync(email);

            return returnStatus;
        }

        [HttpGet("SendEmailVerification")]
        public async Task<ReturnStatus> SendEmailVarification(string emailAdress)
        {
            ReturnStatus returnStatus = new ReturnStatus()
            {
                IsSuccessful = false,
            };
            string confirmationToken = await userSvc.GetVerificationCodeAsync(emailAdress);
            ShimMathUser user = await userSvc.GetUserByEmailAsync(emailAdress);
            VerifyEmailModel model = new VerifyEmailModel()
            {
                UserName = user.Username,
                ConfirmationUrl = Url.ActionLink(
                    "ConfirmEmail/" + user.ID + "/" + confirmationToken,
                    "Account",
                    protocol: HttpContext.Request.Scheme),
            };
            string emailView = await ControllerExtensions.RenderViewAsync<VerifyEmailModel>(this, "/Views/Email/VerifyEmail.cshmtl", model, true);
            if (! string.IsNullOrEmpty(emailView))
            {
                returnStatus = await userSvc.SendVerificationEmailAsync(emailAdress, emailView);
            }
            
            return returnStatus;
        }

        [HttpGet("ConfirmEmail/{userId:alpha}/{confirmationCode:alpha}")]
        public async Task<IActionResult> ConfirmEmail(string userId, string confirmationCode)
        {
            //IdentityUser user = userManager.FindByIdAsync(userid).Result;
            //IdentityResult result = userManager.
            //            ConfirmEmailAsync(user, token).Result;
            //if (result.Succeeded)
            //{
            //    ViewBag.Message = "Email confirmed successfully!";
            //    return View("Success");
            //}
            //else
            //{
            //    ViewBag.Message = "Error while confirming your email!";
            //    return View("Error");
            //}
            return View("Error");
        }
    }
}
