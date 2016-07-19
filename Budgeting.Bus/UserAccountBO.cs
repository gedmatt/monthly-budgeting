using Budgeting.Core.Exceptions;
using Budgeting.Core.Interfaces;
using Budgeting.Entity;
using Budgeting.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

//Review this for MVC Auth Login: http://benfoster.io/blog/aspnet-identity-stripped-bare-mvc-part-1
//And this as well: http://stackoverflow.com/questions/4029369/asp-net-mvc-authentication-cookie-issue

namespace Budgeting.Bus
{
    public class UserAccountBO : ISave
    {
        protected UserAccount _userEntity;

        public int UserAccountId
        {
            get { return _userEntity.UserAccountId; }
        }
        public string UserName
        {
            get { return _userEntity.UserName; }
        }
        public DateTime CreatedDate
        {
            get { return _userEntity.CreatedDate; }
        }
        public DateTime? LastModifiedByDate
        {
            get { return _userEntity.LastModifiedDate; }
        }

        public bool IsAuthenticated { get; set; }

        public UserAccountBO(ModelContext context, string username, string password)
        {
            IsAuthenticated = false;

            //Instantiate based on whether the username and salted password match up???
            _userEntity = context.UserAccounts.SingleOrDefault(x => x.UserName == username);
            if(_userEntity == null)
            {
                throw new UserNotFoundException("Incorrect username");
            }

            var storedPassword = _userEntity.Password;
            var salt = _userEntity.Salt;

            var encryptedPassword = EncryptPasswordWithSalt(password, salt);

            if(storedPassword == encryptedPassword)
            {
                IsAuthenticated = true;
            }
            else
            {
                throw new UserNotFoundException("Incorrect password");
            }
        }

        public void Save()
        {
            if (_userEntity.CreatedDate == DateTime.MinValue)
            {
                _userEntity.CreatedDate = DateTime.Now;
            }
            else
            {
                _userEntity.LastModifiedDate = DateTime.Now;
            }
        }

        //public IdentityUser 

        public static IList<UserAccountViewModel> GetUserAccounts(ModelContext context)
        {
            var accounts = from u in context.UserAccounts
                           select new UserAccountViewModel { UserAccountId = u.UserAccountId, UserName = u.UserName };
            return accounts.ToList();
        }

        public static string EncryptPasswordWithSalt(string password, string salt)
        {
            var baPwd = Encoding.UTF8.GetBytes(password);

            var baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            var baSalt = Encoding.UTF8.GetBytes(salt);

            var baEncrypted = AES_Encrypt(baSalt, baPwdHash);

            var result = Convert.ToBase64String(baEncrypted);

            return result;
        }

        protected static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
    }
}
