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
        Task <Shirt>CreateShirtAsync(Shirt shirt);
        Task <Shirt> UpdateShirtAsync(Shirt shirt);
        Task <bool> DeleteShirtAsync(int shirtId);
        Task <Shirt> GetShirtById(int shirtId);
        Task<IEnumerable<Shirt>> GetAllShirts();
    }
}
