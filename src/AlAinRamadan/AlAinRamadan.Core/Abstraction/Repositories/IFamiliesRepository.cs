using AlAinRamadan.Core.DTOs;
using AlAinRamadan.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlAinRamadan.Core.Abstraction.Repositories
{
    public interface IFamiliesRepository
    {
        Task<Family> CreateAsync(FamilyDTO dTO);
        Task<IEnumerable<Family>> GetAllFamiliesAsync(bool reload = false);
        Task<IEnumerable<Family>> GetFamiliesByCardNumberAsync(string cardNumber);
        Task<IEnumerable<Family>> GetFamiliesByNameAsync(string name);
        Task<Family> GetFamilyByCardNumberAsync(string cardNumber);
        Task<IEnumerable<Family>> GetFamiliesByPartOfCardNumberAsync(string cardNumber);
        Task<Family> GetFamilyByIdAsync(int id);
        Task<int> GetTotalFamiliesAsync();
        Task<bool> UpdateAsync(FamilyDTO dTO);
        Task DeleteAsync(int id);
        Task RestoreAsync(int id);
        Task<IEnumerable<Family>> GetDeletedFamiliesAsync();
        Task MarkFamilyIsDeleted(int id);
        Task MarkFamilyHasNotce(int id, bool hasNotice);
    }
}
