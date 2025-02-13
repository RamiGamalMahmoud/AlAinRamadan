using AlAinRamadan.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AlAinRamadan
{
    internal static class ServiceProviderExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<Core.Abstraction.IConnectionStringFactory>(s =>
            {
                Core.Abstraction.IDirectoriesService directoriesService = s.GetRequiredService<Core.Abstraction.IDirectoriesService>();
                return new ConnectionStringFactory(directoriesService.DatabasePath);
            });
            services.AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddSingleton<AppDbContextFactory>();
            services.AddSingleton<Core.Abstraction.IDirectoriesService, Services.DirectoriesService>();

            services.AddSingleton<Features.Home.ViewModel>();
            services.AddSingleton<Core.Abstraction.Home.IHomeView, Features.Home.View>();

            #region Families Management
            services.AddSingleton<Features.FamiliesManagement.Repository>();
            services.AddSingleton<Features.FamiliesManagement.Home.ViewModel>();
            services.AddSingleton<Core.Abstraction.Families.IFamiliesHomeView, Features.FamiliesManagement.Home.View>();
            services.AddSingleton<Core.Abstraction.Services.IFamiliesService, Services.FamiliesService>();
            services.AddSingleton<Core.Abstraction.Repositories.IFamiliesRepository, Features.FamiliesManagement.Repository>();
            #endregion


            #region Disbursements Management
            services.AddSingleton<Features.Disbursements.Repository>();
            services.AddSingleton<Features.Disbursements.Home.ViewModel>();
            services.AddSingleton<Core.Abstraction.Disbursements.IDisbursementsHomeView, Features.Disbursements.Home.View>();
            services.AddSingleton<Core.Abstraction.Repositories.IDisbursementsRepository, Features.Disbursements.Repository>();
            services.AddSingleton<Core.Abstraction.Services.IDisbursementsService, Services.DisbursementsService>();
            #endregion

            return services;
        }
    }
}
