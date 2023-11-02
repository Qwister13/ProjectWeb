using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWeb.BL.Auth;
using ProjectWeb.DAL;
using Microsoft.AspNetCore.Http;

namespace ProjectWebTest.Helpers
{
    public class BaseTest
    {
        protected IAuthDAL authDal = new AuthDAL();
        protected IEncrypt encrypt = new Encrypt();
        protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        protected IAuth authBL;
        protected IDbSessionDAL dbSessionDAL = new DbSessionDAL();
        protected IDbSession dbSession;

        public BaseTest()
        {
            dbSession = new DbSession(dbSessionDAL, httpContextAccessor);
            authBL = new Auth(authDal, encrypt, httpContextAccessor, dbSession);
        }
    }
}
