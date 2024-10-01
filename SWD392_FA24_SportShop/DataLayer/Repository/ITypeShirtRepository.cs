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
        Task CreateTypeShirtAsync(TypeShirt typeShirt);
        Task<bool> DeleteTypeShirtAsync(TypeShirt typeShirt);
        Task<TypeShirt> UpdateTypeShirtAsync(TypeShirt typeShirt);
        Task<TypeShirt> GetTypeShirtById(int typeShirtId);
        Task<List<TypeShirt>> GetAllShirtAsync();
    }
}
