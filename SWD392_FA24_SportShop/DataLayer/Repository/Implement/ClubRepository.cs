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
    public class ClubRepository : IClubRepository
    {
        private readonly db_aad141_swd392Context _swd392Context;

        public ClubRepository(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }
        public async Task<Club> CreateClubAsync(Club club)
        {
            try
            {
                _swd392Context.Clubs.AddAsync(club);
                await _swd392Context.SaveChangesAsync();
                return club;
            }
            catch(Exception ex)
            {
                throw new Exception("Not found" + ex.Message);
            }
        }

        public async Task<bool> DeleteClubAsync(int clubId)
        {
            try
            {
                var club = await _swd392Context.Clubs.FindAsync(clubId);
                if (club == null)
                {
                    return false;
                }

                _swd392Context.Clubs.Remove(club);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Club>> GetAllClubs()
        {
            return await _swd392Context.Clubs.ToListAsync();
        }

        public async Task<Club> GetClubById(int clubId)
        {
            try
            {
                return await _swd392Context.Clubs.FindAsync(clubId);
            }
            catch(Exception ex)
            {
                throw new Exception("Not found!" + ex.Message);
            }
        }

        public async Task<Club> UpdateClubAsync(Club club)
        {
            try
            {
                _swd392Context.Clubs.Update(club);
                await _swd392Context.SaveChangesAsync();
                return club;
            }
            catch(Exception ex)
            {
                throw new Exception("Not found!" + ex.Message);
            }
        }
    }
}
