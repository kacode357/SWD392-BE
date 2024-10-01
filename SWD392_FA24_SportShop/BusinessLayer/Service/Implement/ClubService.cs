using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Implement
{
    public class ClubService : IClubService
    {
        private readonly IClubService _clubService;
        public ClubService(IClubService clubService)
        {
            _clubService = clubService;
        }
        public async Task<Club> CreateClubAsync(Club club)
        {
            return await _clubService.CreateClubAsync(club);
        }

        public async Task<bool> DeleteClubAsync(int clubId)
        {
            return await _clubService.DeleteClubAsync(clubId);
        }

        public async Task<IEnumerable<Club>> GetAllClubs()
        {
            return await _clubService.GetAllClubs();
        }

        public async Task<Club> GetClubById(int clubId)
        {
            return await _clubService.GetClubById(clubId);
        }

        public async Task<Club> UpdateClubAsync(Club club)
        {
            return await _clubService.UpdateClubAsync(club);
        }
    }
}
