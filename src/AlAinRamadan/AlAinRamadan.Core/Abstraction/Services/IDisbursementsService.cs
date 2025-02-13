using AlAinRamadan.Core.DTOs;
using AlAinRamadan.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlAinRamadan.Core.Abstraction.Services
{
    public interface IDisbursementsService
    {
        Task<Family> GetFamilyByCardNumberAsync(string newValue);
        Task<Family> GetFamilyByIdAsync(int result);
        Task<int> GetTicketNumberAsync();
        Task<Disbursement> CreateDisbursementAsync(Disbursement disbursement);
        Task<IEnumerable<Disbursement>> GetFamilyDisbursementsAsync(int familyId);
    }
}
