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
    public class ShirtSizeRepository : IShirtSizeRepository
    {
        private readonly db_aad141_swd392Context _swd392Context;
        public ShirtSizeRepository(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }

        public async Task<bool> CreateShirtSizeAsync(ShirtSize shirtSize)
        {
            try
            {
                _swd392Context.ShirtSizes.AddAsync(shirtSize);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteShirtSizeAsync(int shirtSizeId)
        {
            try
            {
                var shirtSize = await _swd392Context.ShirtSizes.FindAsync(shirtSizeId);
                if(shirtSize == null)
                {
                    return false;
                }
                _swd392Context.ShirtSizes.Remove(shirtSize);
                return await _swd392Context.SaveChangesAsync()>0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ShirtSize>> GetAllShirtSizeAsync()
        {
            try
            {
                return await _swd392Context.ShirtSizes.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ShirtSize> GetShirtSizeByIdAsync(int shirtSizeId)
        {
            try
            {
                return await _swd392Context.ShirtSizes.FindAsync(shirtSizeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateShirtSizeAsync(ShirtSize shirtSize)
        {
            try
            {
                _swd392Context.ShirtSizes.Update(shirtSize);
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
