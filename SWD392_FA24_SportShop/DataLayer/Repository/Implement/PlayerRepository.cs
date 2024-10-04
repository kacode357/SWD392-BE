using DataLayer.DBContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> CreatePlayerAsync(Player player)
        {
            try
            {
                _swd392Context.Players.AddAsync(player);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeletePlayerAsync(int playerId)
        {
            try
            {
                var player = await _swd392Context.Players.FindAsync(playerId);
                if (player == null)
                {
                    return false;
                }

                _swd392Context.Players.Remove(player);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<Player> GetPlayerById(int playerId)
        {
            try
            {
                return await _swd392Context.Players.FindAsync(playerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Player>> GetPlayers()
        {
            try
            {
                return await _swd392Context.Players.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdatePlayerAsync(Player player)
        {
            try
            {
                _swd392Context.Players.Update(player);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
