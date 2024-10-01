using DataLayer.DBContext;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Implement
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly db_aad141_swd392Context _swd392Context;

        public PlayerRepository(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }
        public async Task CreatePlayerAsync(Player player)
        {
            try
            {
                _swd392Context.Players.AddAsync(player);
                await _swd392Context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Not found!" + ex);
            }
        }

        public async Task<bool> DeletePlayerAsync(Player playerId)
        {
            try
            {
                _swd392Context.Players.Remove(playerId);
                await _swd392Context.SaveChangesAsync();
                return true; 
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public Task<Player> GetPlayerById(int playerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Player>> GetPlayers()
        {
            throw new NotImplementedException();
        }

        public Task<Player> UpdatePlayerAsync(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
