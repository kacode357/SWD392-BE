using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IShirtSizeRepository
    {
        Task<bool> CreateShirtSizeAsync(ShirtSize shirtSize);
        Task<bool> UpdateShirtSizeAsync(ShirtSize shirtSize);
        Task<bool> DeleteShirtSizeAsync(int shirtSizeId);
        Task<ShirtSize> GetShirtSizeByIdAsync(int shirtSizeId);
        Task<List<ShirtSize>> GetAllShirtSizeAsync();
        Task<List<ShirtSize>> GetAllTypeShirtByShirtId(int shirtId);
    }
}
