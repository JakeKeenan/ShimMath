using System;
using System.Collections.Generic;
using System.Text;

namespace ShimMath.Constants
{
    public class ErrorCodeConstants
    {
        public const string ERROR_NO_USERNAME_ENTERED = "No Username was provided, one must be entered to proceed";
        public const string ERROR_NO_PASSWORD_ENTERED = "No Password was provided, one must be entered to proceed";
        public const string ERROR_PASSWORD_NOT_HASHED = "It appears that your password was not sent properly. Please reload the page and try again.";
        public const string ERROR_USER_DOES_NOT_EXIST = "The user information entered does not exist for an exisisting user";
        public const string ERROR_WRONG_PASSWORD = "The password you entered is wrong";
        public const string ERROR_NO_PERMISSION = "Your credentials are not sufficient";
        public const string ERROR_USERNAME_ALREADY_EXISTS = "The username entered already exists";
        public const string ERROR_USEREMAIL_ALREADY_EXISTS = "The email entered already exists";

        public const string ERROR_ACCOUNT_IS_LOCKED = "This account is currently disabled.";
        public const string ERROR_ACCOUNT_NOT_ALLOWED = "This account is not allowed";

        public const string ERROR_VERIFICATION_EMAIL_SEND_FAILED = "The request to send a varification email failed";
    }
}
