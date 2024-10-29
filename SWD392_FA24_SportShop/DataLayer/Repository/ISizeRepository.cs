using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface ISizeRepository
    {
        Task<Size> CreateSizeAsync(Size size);
        Task<bool> UpdateSizeAsync(Size size);
        Task<bool> DeleteSizeAsync(int sizeId);
        Task<Size> GetSizeByIdAsync(int sizeId);
        Task<List<Size>> GetAllSizesAsync();
    }
}
