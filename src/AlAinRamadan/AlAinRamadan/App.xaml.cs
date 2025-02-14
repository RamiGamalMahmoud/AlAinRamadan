using AlAinRamadan.Core;
using AlAinRamadan.Core.Abstraction;
using AlAinRamadan.Core.Abstraction.Views;
using AlAinRamadan.Data;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notification.Wpf;
using System;
using System.Linq;
using System.Windows;

namespace AlAinRamadan
{
    public partial class App : Application
    {
        public App()
        {
            Velopack.VelopackApp.Build().Run();
            _services = new ServiceCollection()
                .ConfigureServices()
                .ConfigureData()
                .BuildServiceProvider();

            WeakReferenceMessenger.Default.Register<Core.Messages.Common.ShowNotificationMessage>(this, (r, m) => ShowNotification(m.AppNitification));
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            MainWindow = new Window();
            IDirectoriesService directoriesService = _services.GetRequiredService<IDirectoriesService>();
            directoriesService.CreateDirectories();
            directoriesService.CreateDatabase();

            using (AppDbContext dbContext = _services.GetRequiredService<AppDbContextFactory>().CreateDbContext())
            {
                if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    WeakReferenceMessenger.Default.Send(new Core.Messages.Common.ShowNotificationMessage(new AppNitification("يوجد تحديث لقاعدة البيانات")));
                    await dbContext.Database.MigrateAsync();
                    WeakReferenceMessenger.Default.Send(new Core.Messages.Common.ShowNotificationMessage(new AppNitification("تم تحديث قاعدة البيانات")));
                }
            }

            MainWindow = _services.GetRequiredService<IHomeView>() as Window;
            MainWindow.Show();
            WeakReferenceMessenger.Default.Send(new Core.Messages.Common.ShowNotificationMessage(new AppNitification("App Started")));
            base.OnStartup(e);
        }

        private void ShowNotification(AppNitification appNitification)
        {
            if (appNitification is ErrorNotification)
            {
                _notificationManager.Show("", appNitification.Message, NotificationType.Error);
            }

            else if (appNitification is SuccessNotification)
            {
                _notificationManager.Show("", appNitification.Message, NotificationType.Success);
            }

            else
            {
                _notificationManager.Show("", appNitification.Message, NotificationType.Information);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        private readonly IServiceProvider _services;
        private readonly NotificationManager _notificationManager = new NotificationManager();
    }

}
