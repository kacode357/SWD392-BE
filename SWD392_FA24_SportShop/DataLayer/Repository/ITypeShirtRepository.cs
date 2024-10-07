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
        Task<bool> DeleteTypeShirtAsync(TypeShirt typeShirtId);
        Task<bool> UpdateTypeShirtAsync(TypeShirt typeShirtId);
        Task<TypeShirt> GetTypeShirtById(int typeShirtId);
        Task<List<TypeShirt>> GetAllShirtAsync();
    }
}
