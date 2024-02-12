using ProjectWeb.BL.General;
using ProjectWeb.DAL;

namespace ProjectWeb.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;

        public CurrentUser(
            IDbSession dbSession,
            IWebCookie webCookie,
            IUserTokenDAL userTokenDAL)
        {
            this.dbSession = dbSession;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
        }

        public async Task<int?> GetUserIdByToken()
        {
            string? tokenCookie = webCookie.Get(AuthConstants.RememberMeCookieName);
            if (tokenCookie == null)
                return null;
            Guid? tokenGuid = Helpers.StringToGuidDef(tokenCookie ?? "");
            if (tokenGuid == null)
                return null;

            int? userid = await userTokenDAL.Get((Guid)tokenGuid);
            return userid;
        }

        public async Task<bool> IsLoggedIn()
        {
            bool isLoggedIn = await dbSession.IsLoggedIn();
            if (!isLoggedIn)
            {
                int? userid = await GetUserIdByToken();
                if (userid != null)
                {
                    await dbSession.SetUserId((int)userid);
                    isLoggedIn = true;
                }
            }
            return isLoggedIn;
        }
    }
}
