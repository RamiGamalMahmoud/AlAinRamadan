using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.Abstraction.Services;
using AlAinRamadan.Core.DTOs;
using AlAinRamadan.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlAinRamadan.Services
{
    internal class DisbursementsService : IDisbursementsService
    {
        public DisbursementsService(IDisbursementsRepository disbursementsRepository, IFamiliesRepository familiesRepository)
        {
            _disbursementsRepository = disbursementsRepository;
            _familiesRepository = familiesRepository;
        }

        private readonly IDisbursementsRepository _disbursementsRepository;
        private readonly IFamiliesRepository _familiesRepository;

        public async Task<Family> GetFamilyByCardNumberAsync(string newValue)
        {
            return await _familiesRepository.GetFamilyByCardNumberAsync(newValue);
        }

        public async Task<Family> GetFamilyByIdAsync(int result)
        {
            return await _familiesRepository.GetFamilyByIdAsync(result);
        }

        public async Task<int> GetTicketNumberAsync()
        {
            return await _disbursementsRepository.GetLastTicketNumberAsync(DateTime.Now);
        }

        public async Task<Disbursement> CreateDisbursementAsync(Disbursement disbursement)
        {
            return await _disbursementsRepository.CreateAsync(disbursement);
        }

        public async Task<IEnumerable<Disbursement>> GetFamilyDisbursementsAsync(int familyId)
        {
            return await _disbursementsRepository.GetFamilyDisbursementsAsync(familyId);
        }
    }
}
