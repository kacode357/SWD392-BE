using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IClubService
    {
        Task<Club> CreateClubAsync(Club club);
        Task<Club> UpdateClubAsync(Club club);
        Task<bool> DeleteClubAsync(int clubId);
        Task<Club> GetClubById(int clubId);
        Task<IEnumerable<Club>> GetAllClubs();
    }
}
