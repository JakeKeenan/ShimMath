using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShimMathCore.Repository.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleID { get; set; }
        public string UserRoleTitle { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
