using Dapper;
using Npgsql;
using ProjectWeb.DAL.Models;
using System.Security.Cryptography;
using System.Text;

namespace ProjectWeb.DAL
{
	public class AuthDAL : IAuthDAL
	{
		public async Task<UserModel> GetUser(string email)
		{
            var result = await DbHelper.QueryAsync<UserModel>(@"
					SELECT UserId, Email, Password, Salt, Status 
					FROM AppUser
					WHERE Email = @email", new { email = email }); 
            return result.FirstOrDefault()?? new UserModel();
		}

		public async Task<UserModel> GetUser(int id)
        {
            var result = await DbHelper.QueryAsync<UserModel>(@"
                    select UserId, Email, Password, Salt, Status
                    from AppUser
                    where UserId = @id", new { id = id });
            return result.FirstOrDefault() ?? new UserModel();
        }

		public async Task<int> CreateUser(UserModel model)
		{
            string sql = @"insert into AppUser(Email, Password, Salt, Status)
                     values(@Email, @Password, @Salt, @Status) returning UserId";
            var result = await DbHelper.QueryAsync<int>(sql, model);
            return result.First();  
        }
	}
}
