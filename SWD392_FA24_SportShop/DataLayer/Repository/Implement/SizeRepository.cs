﻿using DataLayer.DBContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Implement
{
    public class SizeRepository : ISizeRepository
    {
        private readonly db_aad141_swd392Context _swd392Context;
        public SizeRepository(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }

        public async Task<bool> CreateSizeAsync(Size size)
        {
            try
            {
                _swd392Context.Sizes.AddAsync(size);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteSizeAsync(int sizeId)
        {
            try
            {
                var size = await _swd392Context.Sizes.FindAsync(sizeId);
                if (size == null)
                {
                    return false;
                }
                _swd392Context.Sizes.Remove(size);
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Size>> GetAllSizesAsync()
        {
            try
            {
                return await _swd392Context.Sizes.OrderByDescending(s => s.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Size> GetSizeByIdAsync(int sizeId)
        {
            try
            {
                return await _swd392Context.Sizes.FindAsync(sizeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateSizeAsync(Size size)
        {
            try
            {
                _swd392Context.Sizes.Update(size);
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
