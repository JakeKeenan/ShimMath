using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShimMathCore.Repository.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserSalt { get; set; }

        //Foreign Key
        public int UserRoleID { get; set; }  
        public UserRole UserRole { get; set; }
    }
}
