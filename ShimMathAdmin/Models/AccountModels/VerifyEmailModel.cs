using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShimMathAdmin.Models.AccountModels
{
    public class VerifyEmailModel
    {
        public string ConfirmationUrl { get; set; }
        public string UserName { get; set; }
    }
}
