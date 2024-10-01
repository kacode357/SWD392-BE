using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Implement
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerService _playerService;
        public PlayerService(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        public async Task<Player> CreatePlayerAsync(Player player)
        {
            return await _playerService.CreatePlayerAsync(player);
        }

        public async Task<bool> DeletePlayerAsync(int playerId)
        {
            return await _playerService.DeletePlayerAsync(playerId);
        }

        public async Task<Player> GetPlayerById(int playerId)
        {
            return await _playerService.GetPlayerById(playerId);
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            return await _playerService.GetPlayers();
        }

        public async Task<Player> UpdatePlayerAsync(Player player)
        {
            return await _playerService.UpdatePlayerAsync(player);
        }
    }
}
