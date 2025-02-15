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
                return new AlAinRamadan.Data.ConnectionStringFactory(directoriesService.DatabasePath);
            });
            services.AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddSingleton<Core.Abstraction.Services.IApplicationContext, Services.ApplicationContext>();

            services.AddSingleton<AlAinRamadan.Data.AppDbContextFactory>();
            services.AddSingleton<Core.Abstraction.IDirectoriesService, Services.DirectoriesService>();

            services.AddSingleton<Core.Abstraction.ViewModels.IMainViewModel, ViewModels.MainViewModel>();
            services.AddSingleton<Core.Abstraction.Views.IHomeView, Views.MainView>();

            #region Families Management
            services.AddSingleton<Core.Abstraction.Contexts.IFamilyContext, Services.ApplicationContext>();
            services.AddSingleton<Core.Abstraction.ViewModels.IFamiliesListingViewModel, ViewModels.FamiliesListingViewModel>();
            services.AddSingleton<Core.Abstraction.Views.IFamiliesListingView, Views.FamiliesListingView>();
            services.AddSingleton<Core.Abstraction.Services.IFamiliesService, Services.FamiliesService>();
            services.AddTransient<Core.Abstraction.ViewModels.ICreateFamilyViewModel, ViewModels.CreateFamilyViewModel>();
            services.AddTransient<Core.Abstraction.Views.IUpdateFamilyView, Views.UpdateFamilyView>();
            services.AddTransient<Core.Abstraction.ViewModels.IUpdateFamilyViewModel, ViewModels.UpdateFamilyViewModel>(s =>
            {
                Core.Abstraction.Services.IFamiliesService familiesService = s.GetRequiredService<Core.Abstraction.Services.IFamiliesService>();
                Core.Abstraction.Contexts.IFamilyContext context = s.GetRequiredService<Core.Abstraction.Contexts.IFamilyContext>();
                return new ViewModels.UpdateFamilyViewModel(context.GetCurrentFamily(), familiesService);
            });
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
