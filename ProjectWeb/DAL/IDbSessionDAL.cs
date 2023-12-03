using ProjectWeb.DAL.Models;

namespace ProjectWeb.DAL
{
    public interface IDbSessionDAL
    {
        Task<SessionModel?> Get(Guid sessionId);
        Task<int> Update(SessionModel model);
        Task<int> Create(SessionModel model);
        Task Lock(Guid sessionId);
    }
}
