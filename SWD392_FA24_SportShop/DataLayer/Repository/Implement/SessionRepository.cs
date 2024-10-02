﻿using DataLayer.DBContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Implement
{
    internal class SessionRepository : ISessionRepository
    {
        private readonly db_aad141_swd392Context _swd392Context;

        public SessionRepository(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }
        public async Task<Session> CreateSessionAsync(Session session)
        {
            try
            {
                _swd392Context.Sessions.AddAsync(session);
                await _swd392Context.SaveChangesAsync();
                return session;
            }
            catch(Exception ex)
            {
                throw new Exception("Not found!" + ex);
            }
        }

        public async Task<bool> DeleteSessionAsync(int sessionId)
        {
            try
            {
                var session = await _swd392Context.Sessions.FindAsync(sessionId);
                if (session == null)
                {
                    return false;
                }

                _swd392Context.Sessions.Remove(session);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<Session> GetSessionById(int sessionId)
        {
            try
            {
                return await _swd392Context.Sessions.FindAsync(sessionId);
            }
            catch(Exception ex)
            {
                throw new Exception("Not found!" + ex);
            }
        }

        public async Task<IEnumerable<Session>> GetSessions()
        {
            try
            {
                return await _swd392Context.Sessions.ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Not found!" + ex);
            }
        }

        public async Task<Session> UpdateSessionAsync(Session session)
        {
            try
            {
                _swd392Context.Sessions.Update(session);
                await _swd392Context.SaveChangesAsync();
                return session;
            }
            catch(Exception ex)
            {
                throw new Exception("Not found!" + ex);
            }
        }
    }
}