using System;
using System.Collections.Generic;
using System.Text;

namespace ShimMath.DTO
{
    public class AdminUserCredentials : UserCredentials
    {
        public string EnteredSecretKey { get; set; }
    }
}
