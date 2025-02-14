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

            services.AddSingleton<Core.Abstraction.ViewModels.IMainViewModel, ViewModels.MainViewModel>();
            services.AddSingleton<Core.Abstraction.Views.IHomeView, Views.MainView>();

            #region Families Management
            services.AddSingleton<Core.Abstraction.ViewModels.IFamiliesListingViewModel, ViewModels.FamiliesListingViewModel>();
            services.AddSingleton<Core.Abstraction.Views.IFamiliesListingView, Views.FamiliesListingView>();
            services.AddSingleton<Core.Abstraction.Services.IFamiliesService, Services.FamiliesService>();
            #endregion


            #region Disbursements Management
            services.AddSingleton<Core.Abstraction.ViewModels.IDisbursementsListingViewModel, ViewModels.DisbursementsListingViewModel>();
            services.AddSingleton<Core.Abstraction.Views.IDisbursementsListingView, Views.DisbursementsListingView>();
            services.AddSingleton<Core.Abstraction.Services.IDisbursementsService, Services.DisbursementsService>();
            #endregion

            return services;
        }
    }
}
