﻿using AlAinRamadan.Core.Abstraction;
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
                Family family = new Family(dTO.CardNumber, dTO.Name);
                dbContext.Families.Add(family);
                try
                {
                    await dbContext.SaveChangesAsync();
                    _models.Add(family);
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
                return await dbContext.Families.Where(f => f.Name.Contains(name) && !string.IsNullOrEmpty(name)).ToListAsync();
            }
        }

        public async Task<IEnumerable<Family>> GetFamiliesByCardNumberAsync(string cardNumber)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Families.Where(f => f.CardNumber == cardNumber && !string.IsNullOrEmpty(cardNumber)).ToListAsync();
            }
        }

        public async Task<IEnumerable<Family>> GetFamiliesByPartOfCardNumberAsync(string cardNumber)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Families.Where(f => f.CardNumber.Contains(cardNumber) && !string.IsNullOrEmpty(cardNumber)).ToListAsync();
            }
        }

        public async Task<Family> GetFamilyByIdAsync(int id)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Families.FindAsync(id);
            }
        }

        public Task<Family> GetFamilyByCardNumberAsync(string cardNumber)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.Families.FirstOrDefaultAsync(f => f.CardNumber == cardNumber);
            }
        }

        public async Task<int> GetTotalFamiliesAsync()
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Families.CountAsync();
            }
        }

        private readonly AppDbContextFactory _dbContextFactory;
    }
}
