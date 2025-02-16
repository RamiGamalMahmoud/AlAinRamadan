using AlAinRamadan.Core.Abstraction;
using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.DTOs;
using AlAinRamadan.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AlAinRamadan.Data.Repositories
{
    internal class FamiliesRepository : RepositoryBase<Family>, IFamiliesRepository
    {
        public FamiliesRepository(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Family> CreateAsync(FamilyDTO dTO)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Family family = new Family(dTO.CardNumber, dTO.Name, dTO.Notes);
                dbContext.Families.Add(family);
                try
                {
                    await dbContext.SaveChangesAsync();
                    _models.Insert(0, family);
                    return family;
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Family>> GetAllFamiliesAsync(bool reload = false)
        {
            if (!reload || _models is null || !_models.Any())
                using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
                {
                    IEnumerable<Family> families = await dbContext
                        .Families
                        .OrderBy(f => f.Name)
                        .Where(f => f.IsDeleted != true)
                        .ToListAsync();
                    SetModels(families);
                }
            return _models;
        }

        public async Task<bool> UpdateAsync(FamilyDTO dTO)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Family storedFamily = await dbContext.Families.FindAsync(dTO.Id);
                storedFamily.CardNumber = dTO.CardNumber;
                storedFamily.Name = dTO.Name;
                storedFamily.Notes = dTO.Notes;

                try
                {
                    dbContext.Families.Update(storedFamily);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
        }

        public async Task<IEnumerable<Family>> GetFamiliesByNameAsync(string name)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Families
                    .Where(f => !string.IsNullOrEmpty(name) && f.Name.StartsWith(name) && f.IsDeleted != true)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Family>> GetFamiliesByCardNumberAsync(string cardNumber)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Families
                    .Where(f => f.CardNumber == cardNumber && !string.IsNullOrEmpty(cardNumber) && f.IsDeleted != true)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Family>> GetFamiliesByPartOfCardNumberAsync(string cardNumber)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Families
                    .Where(f => f.CardNumber.StartsWith(cardNumber) && !string.IsNullOrEmpty(cardNumber) && f.IsDeleted != true)
                    .ToListAsync();
            }
        }

        public async Task<Family> GetFamilyByIdAsync(int id)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Families
                    .Where(f => f.IsDeleted != true && f.Id == id)
                    .FirstOrDefaultAsync();
            }
        }

        public Task<Family> GetFamilyByCardNumberAsync(string cardNumber)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext
                    .Families
                    .Where(f => f.IsDeleted != true)
                    .FirstOrDefaultAsync(f => f.CardNumber == cardNumber);
            }
        }

        public async Task<int> GetTotalFamiliesAsync()
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Families
                    .Where(f => f.IsDeleted != true)
                    .CountAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Family family = dbContext.Families.Find(id);
                family.IsDeleted = true;
                dbContext.Families.Update(family);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task RestoreAsync(int id)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Family family = dbContext.Families.Find(id);
                family.IsDeleted = false;
                dbContext.Families.Update(family);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Family>> GetDeletedFamiliesAsync()
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Families
                    .Where(f => f.IsDeleted == true)
                    .ToListAsync();
            }
        }

        private readonly AppDbContextFactory _dbContextFactory;
    }
}
