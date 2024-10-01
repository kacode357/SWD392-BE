using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IClubRepository
    {
        Task CreateClubAsync(Club club);
        Task<Club> UpdateClubAsync(Club club);
        Task<bool> DeleteClubAsync(Club club);
        Task<Club> GetClubById(int clubId);
        Task<List<Club>> GetAllClubs();
    }
}
