using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface ISessionRepository
    {
        Task<bool> CreateSessionAsync(Session session);
        Task<bool> UpdateSessionAsync(Session session);
        Task<bool> DeleteSessionAsync(int sessionId);
        Task<Session>GetSessionById(int sessionId);
        Task<List<Session>> GetSessions();
    }
}
