using System;
using System.Collections.Generic;
using System.Text;

namespace ShimMath.Constants
{
    public class UserConstants
    {
        //Passwords passed to ShimMath must be 128 characters exactly.
        public const int PasswordLength = 128;

        public const string PublicSalt = "ShimMath";

        public const string SuperAdmin = "Huglow";
    }
}
