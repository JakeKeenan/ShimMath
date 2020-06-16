using System;
using System.Collections.Generic;
using ShimMath.Constants;
using ShimMath.DTO;
using ShimMathCore.Repository;
//using Microsoft.AspNetCore.Cryptography.KeyDerivation;
//using Microsoft.AspNetCore.Identity.Owin;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace ShimMathCore.BL
{
    public class UserService
    {
        private UserManager<IdentityUser> UserManager;
        private SignInManager<IdentityUser> SignInManager;
        private EmailSenderService EmailSender;

        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, EmailSenderService emailSender)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            EmailSender = emailSender;
        }
        public async Task<bool> IsUsedUsernameAsync(string username)
        {
            bool retVal = true;
            if (string.IsNullOrEmpty(username) == false)
            {
                if (await UserManager.FindByNameAsync(username) == null)
                {
                    retVal = false;
                }
            }
            return retVal;
        }

        public async Task<bool> IsNotUsedUsernameAsync(string username)
        {
            bool retVal = await IsUsedUsernameAsync(username);
            return !retVal;
        }

        public async Task<bool> IsUsedEmailAsync(string email)
        {
            bool retVal = true;
            if (string.IsNullOrEmpty(email) == false)
            {
                if (await UserManager.FindByEmailAsync(email) == null)
                {
                    retVal = false;
                }
            }
            return retVal;
        }

        public async Task<bool> IsNotUsedEmailAsync(string email)
        {
            bool retVal = await IsUsedEmailAsync(email);
            return !retVal;
        }

        public async Task<ReturnStatus> RegisterAsync(ShimMathUser newUser)
        {
            ReturnStatus retVal = new ReturnStatus()
            {
                IsSuccessful = true,
                ErrorMessages = new List<string>(),
            };
            retVal = IsValidUserObject(newUser);

            IdentityResult result = new IdentityResult();
            if (retVal.IsSuccessful)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = newUser.Username,
                    Email = newUser.Email
                };
                result = await UserManager.CreateAsync(user, newUser.Password);
                
                if (result.Succeeded == false)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        retVal.ErrorMessages.Add(error.Description);
                    }
                    retVal.IsSuccessful = false;
                }
                else
                {
                    result = await UserManager.SetLockoutEnabledAsync(user, false);
                    if(result.Succeeded == false)
                    {
                        foreach(IdentityError error in result.Errors)
                        {
                            retVal.ErrorMessages.Add(error.Description);
                        }
                        retVal.IsSuccessful = false;
                    }
                }
            }

            return retVal;
        }

        public async Task<string> GetVerificationCodeAsync(string emailAddress)
        {
            string code = "";
            IdentityUser user = await UserManager.FindByEmailAsync(emailAddress);
            if (user == null)
            {
                code = null;
            }
            else
            {
                code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            }

            return code;
        }

        public async Task<ShimMathUser> GetUserByEmailAsync(string emailAdress)
        {
            IdentityUser identityUser = await UserManager.FindByEmailAsync(emailAdress);
            ShimMathUser user = null;
            if (identityUser != null)
            {
                user = new ShimMathUser()
                {
                    ID = identityUser.Id,
                    Email = identityUser.Email,
                    Username = identityUser.UserName,
                    Password = null,
                };
            }
            return user;
        }

        public async Task<ReturnStatus> SendVerificationEmailAsync(string emailAddress, string emailView)
        {
            ReturnStatus retVal = new ReturnStatus()
            {
                IsSuccessful = true,
                ErrorMessages = new List<string>(),
            };

            IdentityUser user = await UserManager.FindByEmailAsync(emailAddress);
            if(user == null)
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessages.Add(ErrorCodeConstants.ERROR_USER_DOES_NOT_EXIST);
            }
            else
            {
                SendGrid.Response response = await EmailSender.SendEmailAsync(emailAddress, "Hello from ShimMath.com", html: emailView);
                if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessages.Add(ErrorCodeConstants.ERROR_VERIFICATION_EMAIL_SEND_FAILED);
                }
            }
            return retVal;
        }

        public async Task<ReturnStatus> Login(ShimMathUser shimMathUser)
        {
            ReturnStatus retVal = new ReturnStatus()
            {
                IsSuccessful = true,
                ErrorMessages = new List<string>(),
            };

            IdentityUser user = new IdentityUser
            {
                //Id = shimMathUser.ID,
                UserName = shimMathUser.Username,
                Email = shimMathUser.Email
            };
            Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.PasswordSignInAsync(user, shimMathUser.Password, false, false);
            if (result.Succeeded == false)
            {
                if(result.IsLockedOut == true)
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessages.Add(ErrorCodeConstants.ERROR_ACCOUNT_IS_LOCKED);
                }
                else if (result.IsNotAllowed)
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessages.Add(ErrorCodeConstants.ERROR_ACCOUNT_NOT_ALLOWED);
                }
                else
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessages.Add(ErrorCodeConstants.ERROR_WRONG_PASSWORD);
                }
            }
            return retVal;
        }

        public async Task<ReturnStatus> Logout(ShimMathUser shimMathUser)
        {
            ReturnStatus retVal = new ReturnStatus()
            {
                IsSuccessful = true,
            };

            IdentityUser user = new IdentityUser
            {
                Id = shimMathUser.ID,
                UserName = shimMathUser.Username,
                Email = shimMathUser.Email,
            };
            //if (await SignInManager.IsSignedIn())
            //{

            //}
            await SignInManager.SignOutAsync();
            retVal.IsSuccessful = false;

            return retVal;
        }

        

        //returns ReturnStatus object with error message explaining why the user object is invalid
        //error message is empty if the object is valid and IsSuccessful is true too.
        private ReturnStatus IsValidUserObject(ShimMathUser user)
        {
            ReturnStatus retVal = new ReturnStatus()
            {
                IsSuccessful = true,
                ErrorMessages = new List<string>(),
            };
            passwordDoesNotHaveCorrectFormat(user.Password);
            return retVal;
        }

        private bool passwordDoesNotHaveCorrectFormat(string password)
        {
            return password.Length == UserConstants.PasswordLength;
        }

    }
}
