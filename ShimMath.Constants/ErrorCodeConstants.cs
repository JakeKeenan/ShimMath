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
        public const string ERROR_USER_DOES_NOT_EXIST = "The Username entered does not exist";
        public const string ERROR_WRONG_PASSWORD = "The Password you entered is wrong";
        public const string ERROR_NO_PERMISSION = "Your credentials are not sufficient";
        public const string ERROR_USERNAME_ALREADY_EXISTS = "The username entered already exists";
        public const string ERROR_USEREMAIL_ALREADY_EXISTS = "The email entered already exists";
        public const string ERROR_COULD_NOT_GET_PASSWORD = "Could not access the password";

        public const string ERROR_COULD_NOT_CREATE_ADMIN = "Could not add requested user credentials";

        public const string ERROR_NO_SECRET_KEY = "Admin login requires secret key";

        public const string ERROR_SECRET_KEY_NOT_POPULATED = "Master Admin: Huglow has not logged in";

        public const string ERROR_WRONG_SECRET_KEY = "The Secret Key provided is not correct";
    }
}
