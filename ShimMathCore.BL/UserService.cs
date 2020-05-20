using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ShimMath.Constants;
using ShimMath.DTO;
using ShimMathCore.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ShimMathCore.BL
{
    public class UserService
    {
        private UserRepo UserRepo;
        private List<UserCredentials> LoggedMembers;
        private List<AdminUserCredentials> LoggedAdmins;
        //var list = new List<string>();
        //var queryable = list.AsQueryable();
        string secretKey;

        public UserService(UserRepo userRepo)
        {
            UserRepo = userRepo;
            //read secret key from file or somthing? TODO: Research ways
            secretKey = "";
            LoggedMembers = new List<UserCredentials>();
        }
        public ReturnStatus AddAdmin(AdminUserCredentials user)
        {
            ReturnStatus retVal = new ReturnStatus()
            {
                IsSuccessful = true,
            };
            if (string.IsNullOrEmpty(user.Username))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_NO_USERNAME_ENTERED;
            }
            else if (string.IsNullOrEmpty(user.Password))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_NO_PASSWORD_ENTERED;
            }
            else if (passwordDoesNotHaveCorrectFormat(user.Password))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_PASSWORD_NOT_HASHED;
            }
            else if (string.IsNullOrEmpty(AdminUserCredentials.SecretKey))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_NO_SECRET_KEY;
            }
            else if (string.IsNullOrEmpty(secretKey))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_SECRET_KEY_NOT_POPULATED;
            }
            else if (!hashPassword(AdminUserCredentials.SecretKey, UserConstants.PublicSalt).Equals(secretKey))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_WRONG_SECRET_KEY;
            }
            else if (UserRepo.IsAdmin(user) || UserRepo.IsMember(user))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_USERNAME_ALREADY_EXISTS;
            }
            else if (!UserRepo.AddAdmin(new UserCredentials()
            {
                Username = user.Username,
                Password = keyEncryptPassword(user.Password)
            },
            generateSalt()))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_COULD_NOT_CREATE_ADMIN;
            }
            return null;
        }

        public ReturnStatus IsNotUser(string username = "", string email = "")
        {
            ReturnStatus retVal = new ReturnStatus()
            {
                IsSuccessful = true,
            };
            if (string.IsNullOrEmpty(username) == false)
            {
                if (UserRepo.IsUsedUsername(username))
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessage = ErrorCodeConstants.ERROR_USERNAME_ALREADY_EXISTS;
                }
            }
            else if (string.IsNullOrEmpty(email) == false)
            {
                if (UserRepo.IsUsedEmail(email))
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessage = ErrorCodeConstants.ERROR_USERNAME_ALREADY_EXISTS;
                }
            }

            return retVal;
        }

        public ReturnStatus LoginAdmin(AdminUserCredentials user)
        {
            ReturnStatus retVal = new ReturnStatus()
            {
                IsSuccessful = true,
            };

            if (string.IsNullOrEmpty(user.Username))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_NO_USERNAME_ENTERED;
            }
            else if (string.IsNullOrEmpty(user.Password))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_NO_PASSWORD_ENTERED;
            }
            else if (passwordDoesNotHaveCorrectFormat(user.Password))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_PASSWORD_NOT_HASHED;
            }
            else if (string.IsNullOrEmpty(AdminUserCredentials.SecretKey))
            {
                if (user.Username.Equals(UserConstants.SuperAdmin))
                {
                    LoginSuperAdmin(user);
                }
                else
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessage = ErrorCodeConstants.ERROR_NO_SECRET_KEY;
                }
            }
            else if (UserRepo.IsNotAdmin(user))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_NO_PERMISSION;
            }
            else
            {
                string salt = UserRepo.GetSalt(user);
                string hashedPassword = hashPassword(user.Password, salt);
                string encryptedPassword = UserRepo.GetEncryptedPassword(user);
                if (string.IsNullOrEmpty(encryptedPassword))
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessage = ErrorCodeConstants.ERROR_COULD_NOT_GET_PASSWORD;
                }
                else if (string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(encryptedPassword))
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessage = ErrorCodeConstants.ERROR_USER_DOES_NOT_EXIST;
                }
                else if (!string.Equals(keyDecryptPassword(encryptedPassword), hashedPassword))
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessage = ErrorCodeConstants.ERROR_WRONG_PASSWORD;
                }

            }

            if (retVal.IsSuccessful)
            {
                LoggedAdmins.Add(user);
            }
            return retVal;
        }

        private ReturnStatus LoginSuperAdmin(AdminUserCredentials user)
        {
            ReturnStatus retVal = new ReturnStatus()
            {
                IsSuccessful = true,
            };
            if (UserRepo.IsNotAdmin(user))
            {
                retVal.IsSuccessful = false;
                retVal.ErrorMessage = ErrorCodeConstants.ERROR_NO_PERMISSION;
            }
            else
            {
                secretKey = user.EnteredSecretKey;
                string salt = UserRepo.GetSalt(user);
                string hashedPassword = hashPassword(user.Password, salt);
                string encryptedPassword = UserRepo.GetEncryptedPassword(user);
                if (string.IsNullOrEmpty(encryptedPassword))
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessage = ErrorCodeConstants.ERROR_COULD_NOT_GET_PASSWORD;
                }
                else if (string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(encryptedPassword))
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessage = ErrorCodeConstants.ERROR_USER_DOES_NOT_EXIST;
                }
                else if (!string.Equals(keyDecryptPassword(encryptedPassword), hashedPassword))
                {
                    retVal.IsSuccessful = false;
                    retVal.ErrorMessage = ErrorCodeConstants.ERROR_WRONG_PASSWORD;
                }

            }

            if (retVal.IsSuccessful)
            {
                LoggedAdmins.Add(user);
            }
            else
            {
                secretKey = "";
            }
            return retVal;
        }

        private string keyDecryptPassword(string encryptedPassword)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(encryptedPassword);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        private string keyEncryptPassword(string password)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(password);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }

        private bool passwordDoesNotHaveCorrectFormat(string password)
        {
            return password.Length == UserConstants.PasswordLength;
        }

        private string hashPassword(string password, string stringSalt)
        {
            byte[] salt = Convert.FromBase64String(stringSalt);
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000, //<- I read that 10,000 is 'so 2012.'
            numBytesRequested: 256 / 8));
        }

        private string generateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
    }
}
