using ProjectWeb.BL;
using ProjectWebTest.Helpers;
using System.Transactions;

namespace ProjectWebTest
{
    public class RegisterTest : Helpers.BaseTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task BaseRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.com";
                
                // validate: should not be in the DB
                Assert.Throws<AuthorizationException>(delegate { authBL.ValidateEmail(email).GetAwaiter().GetResult(); });

                // create user
                int userId = await authBL.CreateUser(
                    new ProjectWeb.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });

                Assert.Greater(userId, 0);

                var userDalResult = await authDal.GetUser(userId);
                Assert.That(email, Is.EqualTo(userDalResult.Email)); // 1 аргумент - ожидаемое значение
                                                                     // 2 аргумент - реальное значение,                                                                
                Assert.IsNotNull(userDalResult.Salt);

                var userByEmailDalResult = await authDal.GetUser(email);
                Assert.That(email, Is.EqualTo(userDalResult.Email));

                // validate: should be in the DB
                Assert.Throws<DuplicateEmailException>(delegate { authBL.ValidateEmail(email).GetAwaiter().GetResult(); });

                string encPassword = encrypt.HashPassword("qwer1234", userByEmailDalResult.Salt);
                Assert.That(encPassword, Is.EqualTo(userByEmailDalResult.Password));
            }
        }
    }
}