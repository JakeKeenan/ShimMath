using ShimMath.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShimMathCore.Repository
{
    public class UserRepo
    {
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
            return false;
        }

        public bool IsUsedUsername(string username)
        {
            return false;
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
            return false;
        }
    }
}
