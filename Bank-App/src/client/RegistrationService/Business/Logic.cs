using RegistrationService.DTO;
using RegistrationService.Data;
using RegistrationService.Data.Entities;

using System.Text;
using System.Security.Cryptography;

namespace RegistrationService.Business;

    public class Logic : ILogic
    {
        private IDbManager _data;

        public Logic(IDbManager data)
        {
            _data = data;
        }

        public void RegisterUser(RegisterDTO register)
        {
            User user = new User();
                user.FName = register.FName;
                user.LName = register.LName;
                user.UserAddress = register.UserAddress;
                user.Email = register.Email;
                user.PhoneNum = register.PhoneNum;
            
            UserCred cred = new UserCred();
                cred.UserName = register.UserName;
                cred.Salt = Guid.NewGuid();

            var passwordInHash = GenerateSaltedHash(Encoding.ASCII.GetBytes(register.Password),
                                  Encoding.ASCII.GetBytes(cred.Salt.ToString()));
            
            cred.PassHash = Encoding.ASCII.GetString(passwordInHash);

            _data.RegisterUser(user, cred);
        }

        private static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm hash = SHA256.Create();
            
            byte[] plainTextWithSaltBytes =
                new byte[plainText.Length + salt.Length];
            
            for(int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i]=plainText[i];
            }
            for(int i =0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length +i] = salt[i];
            }

            return hash.ComputeHash(plainTextWithSaltBytes);
        }
    }