
using AlAinRamadan.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlAinRamadan.Core.Abstraction.Repositories
{
    public interface IDisbursementsRepository
    {
        Task<Disbursement> CreateAsync(Disbursement disbursement);
        Task<int> GetLastTicketNumberAsync(DateTime date);
        Task<IEnumerable<Disbursement>> GetFamilyDisbursementsAsync(int familyId);
    }
}
