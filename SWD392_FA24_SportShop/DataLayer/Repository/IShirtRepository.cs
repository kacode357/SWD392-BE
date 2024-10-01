using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IShirtRepository
    {
        Task CreateShirtAsync(Shirt shirt);
        Task <Shirt> UpdateShirtAsync(Shirt shirt);
        Task <bool> DeleteShirtAsync(Shirt shirtId);
        Task <Shirt> GetShirtById(int shirtId);
        Task<List<Shirt>> GetAllShirts();
    }
}
