using AlAinRamadan.Core.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AlAinRamadan.Data
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureData(this IServiceCollection services)
        {
            services.AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddSingleton<IConnectionStringFactory>(s =>
            {
                IDirectoriesService directoriesService = s.GetRequiredService<IDirectoriesService>();
                return new ConnectionStringFactory(directoriesService.DatabasePath);
            });

            #region Families Management
            services.AddSingleton<Core.Abstraction.Repositories.IFamiliesRepository, Repositories.FamiliesRepository>();
            #endregion


            #region Disbursements Management
            services.AddSingleton<Core.Abstraction.Repositories.IDisbursementsRepository, Repositories.DisbursementsRepository>();
            #endregion

            return services;
        }
    }
}
