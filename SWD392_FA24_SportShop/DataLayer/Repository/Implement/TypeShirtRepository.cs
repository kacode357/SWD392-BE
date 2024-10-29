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
    public class TypeShirtRepository : ITypeShirtRepository
    {
        private readonly db_aad141_swd392Context _swd392Context;

        public TypeShirtRepository(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }
        public async Task<TypeShirt> CreateTypeShirtAsync(TypeShirt typeShirt)
        {
            try
            {
                await _swd392Context.TypeShirts.AddAsync(typeShirt);
                await _swd392Context.SaveChangesAsync();
                var fullTypeShirt = await _swd392Context.TypeShirts
                    .Include(ts => ts.Shirts)
                    .Include(ts => ts.Club)
                    .Include(ts => ts.Session)
                    .FirstOrDefaultAsync(ts => ts.Id == typeShirt.Id);
                return fullTypeShirt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteTypeShirtAsync(int typeShirtId)
        {
            try
            {
                var typeShirt = await _swd392Context.Shirts.FindAsync(typeShirtId);
                if (typeShirt == null)
                {
                    return false;
                }

                _swd392Context.Shirts.Remove(typeShirt);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<TypeShirtDto>> GetAllTypeShirtAsync()
        {
            try
            {
                var query = from typeShirt in _swd392Context.TypeShirts
                            join session in _swd392Context.Sessions on typeShirt.SessionId equals session.Id
                            join club in _swd392Context.Clubs on typeShirt.ClubId equals club.Id
                            select new TypeShirtDto
                            {
                                Id = typeShirt.Id,
                                Name = typeShirt.Name,
                                Description = typeShirt.Description,
                                Status = typeShirt.Status,
                                SessionId = typeShirt.SessionId,
                                SessionName = session.Name,  
                                ClubId = typeShirt.ClubId,
                                ClubName = club.Name     
                            };

                return await query.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Not found!" + ex.Message);
            }
        }

        public async Task<TypeShirt> GetTypeShirtById(int typeShirtId)
        {
            try
            {
                return await _swd392Context.TypeShirts
                    .Include(ts => ts.Shirts)
                    .Include(ts => ts.Club)
                    .Include(ts => ts.Session)
                    .FirstOrDefaultAsync(ts => ts.Id == typeShirtId);
            }
            catch (Exception ex)
            {
                throw new Exception("Not found!" + ex.Message);
            }
        }

        public async Task<bool> UpdateTypeShirtAsync(TypeShirt typeShirt)
        {
            try
            {
                _swd392Context.TypeShirts.Update(typeShirt);
                await _swd392Context.SaveChangesAsync() ;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Not found!" + ex.Message);
            }
        }
    }
}
