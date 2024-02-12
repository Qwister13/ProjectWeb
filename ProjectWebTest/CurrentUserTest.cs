using ProjectWebTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ProjectWeb.BL.Auth;
using ProjectWeb.BL;
using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Identity;

namespace ProjectWebTest
{
    internal class CurrentUserTest : Helpers.BaseTest
    {

        [Test]
        public async Task BaseRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                await CreateAndAuthUser();

                bool isLoggedIn = await currentUser.IsLoggedIn();
                Assert.True(isLoggedIn);

                webCookie.Delete(AuthConstants.SessionCookieName);
                dbSession.ResetSessionCache();

                isLoggedIn = await this.currentUser.IsLoggedIn();
                Assert.True(isLoggedIn);

                webCookie.Delete(AuthConstants.SessionCookieName);
                webCookie.Delete(AuthConstants.RememberMeCookieName);
                dbSession.ResetSessionCache();

                isLoggedIn = await this.currentUser.IsLoggedIn();
                Assert.False(isLoggedIn);
            }
        }

        public async Task<int> CreateAndAuthUser()
        {
            string email = Guid.NewGuid().ToString() + "@test.com";

            // create user
            int userId = await authBL.CreateUser(
                new ProjectWeb.DAL.Models.UserModel()
                {
                    Email = email,
                    Password = "qwer1234"
                });
            return await authBL.Authenticate(email, "qwer1234", true);
        }
    }
}
