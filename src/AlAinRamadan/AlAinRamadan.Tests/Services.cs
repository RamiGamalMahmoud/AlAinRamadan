using AlAinRamadan.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace AlAinRamadan.Tests
{
    public class Services
    {
        public Services()
        {
            services = new ServiceCollection();
            services.AddSingleton<IAppDbContextFactory, TestAppDbContextFactory>();
            services.AddSingleton<Core.Abstraction.Repositories.IFamiliesRepository, AlAinRamadan.Data.Repositories.FamiliesRepository>();
            Provider = services.BuildServiceProvider();
        }

        public IServiceProvider Provider { get; }

        private IServiceCollection services;
    }
}
