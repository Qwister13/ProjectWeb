using ProjectWeb.BL;
using ProjectWebTest.Helpers;
using System.Transactions;

namespace ProjectWebTest
{
    public class AuthTest : Helpers.BaseTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task AuthRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.com";

                // create user
                int userId = await authBL.CreateUser(
                    new ProjectWeb.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "rjvg.nth123E!",
                    });

                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate("saeqwfs", "111", false).GetAwaiter().GetResult(); });

                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate(email, "111", false).GetAwaiter().GetResult(); });

                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate("fdsdfxvcb", "rjvg.nth123E!", false).GetAwaiter().GetResult(); });

                await authBL.Authenticate(email, "rjvg.nth123E!", false);
            }
        }
    }
}