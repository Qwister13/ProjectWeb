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
                var emailValidationResult = await authBL.ValidateEmail(email);
                Assert.IsNull(emailValidationResult);

                // create user
                int userId = await authBL.CreateUser(
                    new ProjectWeb.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });

                Assert.Greater(userId, 0);

                // validate: should be in the DB
                emailValidationResult = await authBL.ValidateEmail(email);
                Assert.IsNotNull(emailValidationResult);

                var userDalResult = await authDal.GetUser(userId);
                Assert.That(email, Is.EqualTo(userDalResult.Email)); // 1 аргумент - ожидаемое значение
                                                                     // 2 аргумент - реальное значение,                                                                
                Assert.IsNotNull(userDalResult.Salt);

                var userByEmailDalResult = await authDal.GetUser(email);
                Assert.That(email, Is.EqualTo(userDalResult.Email));

                // validate: should be in the DB
                emailValidationResult = await authBL.ValidateEmail(email);
                Assert.IsNotNull(emailValidationResult);

                string encPassword = encrypt.HashPassword("qwer1234", userByEmailDalResult.Salt);
                Assert.That(encPassword, Is.EqualTo(userByEmailDalResult.Password));
            }
        }
    }
}