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
    public class TypeShirtRepository : ITypeShirtRepository
    {
        private readonly db_aad141_swd392Context _swd392Context;

        public TypeShirtRepository(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }
        public async Task CreateTypeShirtAsync(TypeShirt typeShirt)
        {
            try
            {
                _swd392Context.TypeShirts.AddAsync(typeShirt);
                await _swd392Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Not found!" + ex.Message);
            }
        }

        public async Task<bool> DeleteTypeShirtAsync(TypeShirt typeShirt)
        {
            try
            {
                _swd392Context.TypeShirts.Remove(typeShirt);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<TypeShirt>> GetAllShirtAsync()
        {
            try
            {
                return await _swd392Context.TypeShirts.ToListAsync();
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
                return await _swd392Context.TypeShirts.FindAsync(typeShirtId);
            }
            catch (Exception ex)
            {
                throw new Exception("Not found!" + ex.Message);
            }
        }

        public async Task<TypeShirt> UpdateTypeShirtAsync(TypeShirt typeShirt)
        {
            try
            {
                _swd392Context.TypeShirts.Update(typeShirt);
                await _swd392Context.SaveChangesAsync() ;
                return typeShirt;
            }
            catch (Exception ex)
            {
                throw new Exception("Not found!" + ex.Message);
            }
        }
    }
}
