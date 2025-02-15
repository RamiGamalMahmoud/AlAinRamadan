using AlAinRamadan.Core.DTOs;
using AlAinRamadan.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlAinRamadan.Core.Abstraction.Services
{
    public interface IFamiliesService
    {
        Task<Family> CreateFamilyAsync(FamilyDTO familyDTO);
        Task<IEnumerable<Family>> GetAllFamiliesAsync();
        Task<IEnumerable<Family>> GetFamiliesByNameAsync(string name);
        Task<IEnumerable<Family>> GetFamiliesByCardNumberAsync(string cardNumber);
        Task<IEnumerable<Family>> GetFamiliesByPartOfCardNumberAsync(string cardNumber);
        Task<bool> UpdateAsync(FamilyDTO familyDTO);
        Task<int> GetTotalFamiliesAsync();
    }
}
