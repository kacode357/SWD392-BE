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
    public class ShirtRepository : IShirtRepository
    {
        private readonly db_aad141_swd392Context _swd392Context;

        public ShirtRepository(db_aad141_swd392Context swd392Context)
        {
            _swd392Context = swd392Context;
        }

        public async Task<bool> CreateShirtAsync(Shirt shirt)
        {
            try
            {
                _swd392Context.Shirts.AddAsync(shirt);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                return await _swd392Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<ShirtDto>> GetAllShirts()
        {
            try
            {
                var query = from shirt in _swd392Context.Shirts
                            join typeShirt in _swd392Context.TypeShirts on shirt.TypeShirtId equals typeShirt.Id
                            join player in _swd392Context.Players on shirt.PlayerId equals player.Id
                            select new ShirtDto
                            {
                                Id = shirt.Id,
                                Name = shirt.Name,
                                Number = shirt.Number,
                                Price = shirt.Price,
                                Date = shirt.Date,
                                Description = shirt.Description,
                                Status = shirt.Status,
                                UrlImg = shirt.UrlImg,
                                TypeShirtId = shirt.TypeShirtId,
                                TypeShirtName = typeShirt.Name,
                                PlayerId = shirt.PlayerId,
                                PlayerName = player.FullName
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Shirt> GetShirtById(int shirtId)
        {
            try
            {
                return await _swd392Context.Shirts.FindAsync(shirtId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<bool> UpdateShirtAsync(Shirt shirt)
        {
            try
            {
                _swd392Context.Shirts.Update(shirt);
                await _swd392Context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Not found!" + ex);
            }
        }

        
    }
}
