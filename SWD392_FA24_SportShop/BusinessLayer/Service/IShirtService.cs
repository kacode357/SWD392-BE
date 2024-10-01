using DataLayer.Entities;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IShirtService
    {
        Task<Shirt> CreateShirtAsync(Shirt shirt);
        Task<IEnumerable<Shirt>> GetShirtsAsync();
        Task<Shirt> UpdateShirtAsync(Shirt shirt);
        Task<bool> DeleteShirtAsync(int shirtId);
        Task<Shirt> GetShirtById(int shirtId);
    }
}
