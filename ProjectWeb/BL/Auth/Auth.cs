using ProjectWeb.DAL.Models;
using ProjectWeb.DAL;
using Npgsql;
using Dapper;
using System.Security.Cryptography;
using System;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.DataAnnotations;
using ProjectWeb.BL;

namespace ProjectWeb.BL.Auth
{
    public class Auth : IAuth
    {
        private readonly IAuthDAL authDal;
        private readonly IEncrypt encrypt;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDbSession dbSession;
        public Auth(IAuthDAL authDal,
            IEncrypt encrypt,
            IHttpContextAccessor httpContextAccessor,
            IDbSession dbSession)
        {
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.httpContextAccessor = httpContextAccessor;
            this.dbSession = dbSession;
        }

        public async Task<UserModel> GetUser(string email)
        {
            return await authDal.GetUser(email);
        }

        public async Task<UserModel> GetUser(int id)
        {
            return await authDal.GetUser(id);
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);
            int id = await authDal.CreateUser(user);
            await Login(id);
            return id;
        }

        public async Task Login(int id)
        {
            await dbSession.SetUserId(id);
        }

        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            var user = await authDal.GetUser(email);
            if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                await Login(user.UserId ?? 0);
                return user.UserId ?? 0; 
            }
            throw new AuthorizationException();         
        }

        public async Task<ValidationResult?> ValidateEmail(string email)
        {
            var user = await authDal.GetUser(email);
            if (user.UserId != null)
            {
                return new ValidationResult("Email уже зарегистрирован");
            }
            return null;
        }

        public async Task<bool> IsValidEmailDomain(string email, string[] allowedDomains)
        {
            if(!string.IsNullOrEmpty(email))
            {
                var domain = email.Substring(email.IndexOf('@') + 1);
                return allowedDomains.Any(allowedDomains => domain.Equals(allowedDomains, StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }
    }
}
