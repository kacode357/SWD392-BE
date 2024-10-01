using DataLayer.Entities;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Implement
{
    public class ShirtService : IShirtService
    {
        private readonly IShirtRepository _shirtRepository;

        public ShirtService(IShirtRepository shirtRepository)
        {
            _shirtRepository = shirtRepository;
        }

        public async Task<Shirt> CreateShirtAsync(Shirt shirt)
        {
            return await _shirtRepository.CreateShirtAsync(shirt);
        }

        public async Task<bool> DeleteShirtAsync(int shirtId)
        {
            return await _shirtRepository.DeleteShirtAsync(shirtId);
        }

        public async Task<Shirt> GetShirtById(int shirtId)
        {
            return await _shirtRepository.GetShirtById(shirtId);
        }

        public async Task<IEnumerable<Shirt>> GetShirtsAsync()
        {
            return await _shirtRepository.GetAllShirts();
        }

        public async Task<Shirt> UpdateShirtAsync(Shirt shirt)
        {
            return await _shirtRepository.UpdateShirtAsync(shirt);
        }
    }
}
