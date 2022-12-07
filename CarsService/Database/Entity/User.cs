using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

//CREATE TABLE Users (User_ID int IDENTITY(1,1) PRIMARY KEY, Login varchar(255), Password varchar(255))

namespace CarsService.Database.Entity
{
    [Table("Users")]
    public class User
    {
        #region Members

        private string? login;
        private string? password;

        #endregion

        #region Properties

        [Key]
        public int User_ID { get; private set; }
        public string? Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }
        }
        public string? Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value; 
            }
        }

        #endregion

        #region Ctor

        public User()
        {

        }

        public User(string? userLogin, string? userPassword, int user_ID = 0)
            :base()
        {
            User_ID = user_ID; //we don't use it like that so its okay. I could also just never set it up
            Login = userLogin;
            Password = userPassword;
        }

        #endregion

        #region Methods

        public string CreateSalt(string? password)
        {
            if(password == null)
            {
                return "";
            }

            var salt = new byte[16];
            string refreshToken = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                refreshToken = Convert.ToBase64String(salt);
            }

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);

            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }

        public bool CheckSalt(string userpassword, string savedPasswordHash)
        {
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];

            Array.Copy(hashBytes, 0, salt, 0, 16);

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(userpassword, salt, 100000);

            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}