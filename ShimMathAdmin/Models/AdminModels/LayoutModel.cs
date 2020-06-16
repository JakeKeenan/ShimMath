using ShimMath.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShimMathAdmin.Models.AdminModels
{
    public class LayoutModel
    {
        public string MainBodyView { get; set; }
        public string PublicSalt { get; set; }
        public LayoutModel()
        {
            MainBodyView = "Views/Shared/_Layout.cshtml";
            PublicSalt = UserConstants.PublicSalt;
        }

        //public bool UserIsLoggedIn { get; set; }
        //public string Username { get; set; }
    }
}
