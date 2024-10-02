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
    public class ShirtRepository : IShirtRepository
    {
        private readonly db_aad141_swd392Context _swd392Context;

        public ShirtRepository(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }

        public async Task<Shirt> CreateShirtAsync(Shirt shirt)
        {
            _swd392Context.Shirts.AddAsync(shirt);
            await _swd392Context.SaveChangesAsync();
            return shirt;
        }

        public async Task<bool> DeleteShirtAsync(int shirtId)
        {
            try
            {
                var shirt = await _swd392Context.Shirts.FindAsync(shirtId);
                if (shirt == null)
                {
                    return false;
                }

                _swd392Context.Shirts.Remove(shirt);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Shirt>> GetAllShirts()
        {
            return await _swd392Context.Shirts.ToListAsync();
        }

        public async Task<Shirt> GetShirtById(int shirtId)
        {
            try
            {
                return await _swd392Context.Shirts.FindAsync(shirtId);
            }
            catch(Exception ex)
            {
                throw new Exception("Not found!" + ex);
            }
            
        }

        public async Task<Shirt> UpdateShirtAsync(Shirt shirt)
        {
            try
            {
                _swd392Context.Shirts.Update(shirt);
                await _swd392Context.SaveChangesAsync();
                return shirt;
            }
            catch(Exception ex)
            {
                throw new Exception("Not found!" + ex);
            }
        }

        
    }
}