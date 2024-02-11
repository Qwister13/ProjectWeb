using Microsoft.AspNetCore.Identity;
using ProjectWeb.DAL.Models;
using System.Data;

namespace ProjectWeb.DAL
{
    public class UserTokenDAL : IUserTokenDAL
    {
        public async Task<Guid> Create(int userid)
        {
            Guid tockenid = Guid.NewGuid();
            string sql = @"insert into UserToken (UserTokenID, UserId, Created)
                    values (@tockenid, @userid, NOW())";

            await DbHelper.ExecuteScalarAsync(sql, new { userid = userid, tockenid = tockenid });
            return tockenid;
        }

        public async Task<int?> Get(Guid tokenid)
        {
            string sql = @"select UserId from UserToken where UserTokenID = @tockenid";
            return await DbHelper.ExecuteScalarAsync(sql, new { tockenid = tokenid });
        }
    }
}
