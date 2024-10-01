using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface ISessionService
    {
        Task<Session> CreateSessionAsync(Session session);
        Task<Session> UpdateSessionAsync(Session session);
        Task<bool> DeleteSessionAsync(int sessionId);
        Task<Session> GetSessionById(int sessionId);
        Task<IEnumerable<Session>> GetSessions();
    }
}
