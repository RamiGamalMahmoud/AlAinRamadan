using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.DTOs;
using AlAinRamadan.Core.Models;
using AlAinRamadan.Data;
using AlAinRamadan.Tests.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlAinRamadan.Tests.IntegrationTests
{
    public class FamiliesRepositoryTests(Services services) : TestBase(services)
    {
        [Theory]
        [ClassData(typeof(FamiliesData))]
        public async Task Test1(List<FamilyDTO> familyDtos)
        {
            var familiesRepository = _services.Provider.GetRequiredService<IFamiliesRepository>();

            foreach (var familyDto in familyDtos)
            {
                await familiesRepository.CreateAsync(familyDto);
            }

            var retrievedFamilies = await familiesRepository.GetAllFamiliesAsync();
            retrievedFamilies.Should().HaveCount(familyDtos.Count);
        }
    }
}