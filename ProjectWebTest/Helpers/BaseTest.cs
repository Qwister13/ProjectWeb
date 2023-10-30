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
        protected IAuthBL authBL;

        public BaseTest()
        {
            authBL = new AuthBL(authDal, encrypt, httpContextAccessor);
        }
    }
}
