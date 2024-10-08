using DataLayer.DBContext;
using DataLayer.DTO;
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

        public async Task<List<PlayerDto>> GetPlayers()
        {
            try
            {
                var query = from player in _swd392Context.Players
                            join club in _swd392Context.Clubs on player.ClubId equals club.Id
                            select new PlayerDto
                            {
                                Id = player.Id,
                                FullName = player.FullName,
                                Height = player.Height,
                                Weight = player.Weight,
                                Birthday = player.Birthday,
                                Nationality = player.Nationality,
                                Status = player.Status,
                                ClubId = player.ClubId,
                                ClubName = club.Name
                            };
                return await query.ToListAsync();
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
