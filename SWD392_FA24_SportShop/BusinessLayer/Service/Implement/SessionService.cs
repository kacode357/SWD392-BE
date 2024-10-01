using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Implement
{
    public class SessionService : ISessionService
    {
        private readonly ISessionService _sessionService;
        public SessionService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public async Task<Session> CreateSessionAsync(Session session)
        {
            return await _sessionService.CreateSessionAsync(session);
        }

        public async Task<bool> DeleteSessionAsync(int sessionId)
        {
            return await _sessionService.DeleteSessionAsync(sessionId);
        }

        public async Task<Session> GetSessionById(int sessionId)
        {
            return await _sessionService.GetSessionById(sessionId);
        }

        public async Task<IEnumerable<Session>> GetSessions()
        {
            return await _sessionService.GetSessions();
        }

        public async Task<Session> UpdateSessionAsync(Session session)
        {
            return await _sessionService.UpdateSessionAsync(session);
        }
    }
}
