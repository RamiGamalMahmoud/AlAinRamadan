using AlAinRamadan.Core.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace AlAinRamadan.Data
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureData(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionStringFactory>(s =>
            {
                IDirectoriesService directoriesService = s.GetRequiredService<IDirectoriesService>();
                return new ConnectionStringFactory(directoriesService.DatabasePath);
            });

            #region Families Management
            #endregion


            #region Disbursements Management
            #endregion

            return services;
        }
    }
}
