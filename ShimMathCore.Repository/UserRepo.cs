using ShimMath.DTO;
using ShimMathCore.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShimMathCore.Repository
{
    public class UserRepo : IDisposable
    {
        private ShimMathContext _dbContext;

        public UserRepo(ShimMathContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public string GetSalt(UserCredentials user)
        {
            //getUserbyId and then extract salt.
            return "";
        }

        public string GetEncryptedPassword (UserCredentials user)
        {
            return "";
        }

        public bool IsUsedEmail(string email)
        {
            var exisistingUser = _dbContext.user.Where(user => user.UserEmail == email).FirstOrDefault();

            return exisistingUser != null;
        }

        public bool IsUsedUsername(string username)
        {
            /*
            UserRole newUserRole = new UserRole()
            {
                UserRoleTitle = "TestUserRole",
            };
            _dbContext.userrole.Add(newUserRole);
            _dbContext.SaveChanges();
            */
            var exisistingUser = _dbContext.user.Where(user => user.UserName == username).FirstOrDefault();

            return exisistingUser != null;
        }

        public bool IsNotAdmin(UserCredentials user)
        {
            return !IsAdmin(user);
        }

        public bool IsNotMember(UserCredentials user)
        {
            return !IsMember(user);
        }

        public bool IsMember(UserCredentials user)
        {
            return false;
        }

        public bool IsAdmin(UserCredentials user)
        {
            return false;
        }

        public bool AddAdmin(UserCredentials user, string salt)
        {
            User newUser = new User()
            {
                UserName = user.Username,
                UserEmail = user.UserEmail,
                UserPassword = user.Password,
                UserRole = _dbContext.userrole.SingleOrDefault(x => x.UserRoleTitle.Equals("Admin")),
                UserSalt = salt
            };

            _dbContext.user.Add(newUser);
            _dbContext.SaveChanges();

            return true;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
