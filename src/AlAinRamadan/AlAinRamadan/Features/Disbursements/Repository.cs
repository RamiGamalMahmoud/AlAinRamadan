using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.Models;
using AlAinRamadan.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlAinRamadan.Features.Disbursements
{
    internal class Repository : IDisbursementsRepository
    {
        public Repository(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<int> GetLastTicketNumberAsync(DateTime date)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Disbursements.MaxAsync(d => (int?)d.TicketNumber) ?? 0;
            }
        }

        public async Task<Disbursement> GetDisbursementByFamilyId(int familyId)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Disbursements
                    .Include(d => d.Family)
                    .FirstOrDefaultAsync(d => d.FamilyId == familyId);
            }
        }

        public async Task<Disbursement> CreateAsync(Disbursement disbursement)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                EntityEntry<Disbursement> entityEntry = dbContext.Disbursements.Add(disbursement);
                await dbContext.SaveChangesAsync();
                entityEntry.Reference(d => d.Family).Load();
                return entityEntry.Entity;
            }
        }

        public async Task<IEnumerable<Disbursement>> GetFamilyDisbursementsAsync(int familyId)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Disbursements
                    .Where(d => d.FamilyId == familyId)
                    .Include(d => d.Family)
                    .OrderByDescending(d => d.DateCreated)
                    .ToListAsync();
            }
        }

        private readonly AppDbContextFactory _dbContextFactory;
    }
}
