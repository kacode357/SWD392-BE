using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IPlayerRepository
    {
        Task CreatePlayerAsync(Player player);
        Task<Player> UpdatePlayerAsync(Player player);
        Task<bool> DeletePlayerAsync(Player playerId);
        Task<Player> GetPlayerById(int playerId);
        Task<List<Player>> GetPlayers();
    }
}
