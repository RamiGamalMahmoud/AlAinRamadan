using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AlAinRamadan.Data.Repositories
{
    internal class DisbursementsRepository : IDisbursementsRepository
    {
        public DisbursementsRepository(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<int> GetLastTicketNumberAsync(DateTime date)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Disbursements
                    .Where(d => d.DateCreated.Date == date.Date)
                    .MaxAsync(d => (int?)d.TicketNumber) ?? 0;
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
