using DataLayer.DTO;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface ITypeShirtRepository
    {
        Task<bool> CreateTypeShirtAsync(TypeShirt typeShirt);
        Task<bool> DeleteTypeShirtAsync(int typeShirtId);
        Task<bool> UpdateTypeShirtAsync(TypeShirt typeShirtId);
        Task<TypeShirt> GetTypeShirtById(int typeShirtId);
        Task<List<TypeShirtDto>> GetAllTypeShirtAsync();      
    }
}
