using ProjectWeb.DAL.Models;
using ProjectWeb.DAL;
using Npgsql;
using Dapper;
using System.Security.Cryptography;
using System;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.DataAnnotations;
using ProjectWeb.BL;
using Microsoft.AspNetCore.Server.IIS.Core;
using ProjectWeb.BL.General;

namespace ProjectWeb.BL.Auth
{
    public class Auth : IAuth
    {
        private readonly IAuthDAL authDal;
        private readonly IEncrypt encrypt;
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;
        public Auth(IAuthDAL authDal,
            IEncrypt encrypt,
            IWebCookie webCookie,
            IDbSession dbSession,
            IUserTokenDAL userTokenDAL
            )
        {
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.dbSession = dbSession;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
        }

        public async Task<UserModel> GetUser(string email)
        {
            var name = "string";
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
                if (rememberMe)
                {
                    Guid tokenId = await userTokenDAL.Create(user.UserId ?? 0);
                    this.webCookie.AddSecure(AuthConstants.RememberMeCookieName, tokenId.ToString(), 30);
                }
                return user.UserId ?? 0;
            }
            throw new AuthorizationException();
        }

        public async Task ValidateEmail(string email)     
        {
            var user = await authDal.GetUser(email);
            if (user.UserId != null)
            {
                throw new DuplicateEmailException();
            }
        }

        public async Task CheckEmail(string email)
        {
            var check = await authDal.GetUser(email);
            if (check.UserId == null)
            {
                throw new AuthorizationException();
            }
        }

        public async Task Register(UserModel user)
         {
             using (var scope = Helpers.CreateTransactionScope())
             {
                 await dbSession.Lock();
                 await ValidateEmail(user.Email);
                 await CreateUser(user);
                 scope.Complete();
             }
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
