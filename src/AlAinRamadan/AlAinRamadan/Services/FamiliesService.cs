using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.Abstraction.Services;
using AlAinRamadan.Core.DTOs;
using AlAinRamadan.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlAinRamadan.Services
{
    internal class FamiliesService : IFamiliesService
    {
        public FamiliesService(IFamiliesRepository familiesRepository)
        {
            _familiesRepository = familiesRepository;
        }

        public async Task<Family> CreateFamilyAsync(FamilyDTO familyDTO) => await _familiesRepository.CreateAsync(familyDTO);

        public async Task<bool> UpdateFamilyAsync(FamilyDTO familyDTO) => await _familiesRepository.UpdateAsync(familyDTO);

        private readonly IFamiliesRepository _familiesRepository;

        public async Task<IEnumerable<Family>> GetAllFamiliesAsync() => await _familiesRepository.GetAllFamiliesAsync();

        public async Task<bool> UpdateAsync(FamilyDTO familyDTO) => await _familiesRepository.UpdateAsync(familyDTO);

        public async Task<IEnumerable<Family>> GetFamiliesByNameAsync(string name)
        {
            return await _familiesRepository.GetFamiliesByNameAsync(name);
        }

        public async Task<IEnumerable<Family>> GetFamiliesByCardNumberAsync(string cardNumber)
        {
            return await _familiesRepository.GetFamiliesByCardNumberAsync(cardNumber);
        }

        public async Task<int> GetTotalFamiliesAsync()
        {
            return await _familiesRepository.GetTotalFamiliesAsync();
        }

        public async Task<IEnumerable<Family>> GetFamiliesByPartOfCardNumberAsync(string cardNumber)
        {
            return await _familiesRepository.GetFamiliesByPartOfCardNumberAsync(cardNumber);
        }
    }
}
