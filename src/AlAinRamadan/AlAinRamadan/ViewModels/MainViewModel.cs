using AlAinRamadan.Core.Abstraction.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;

namespace AlAinRamadan.ViewModels
{
    internal partial class MainViewModel : ObservableObject, IMainViewModel
    {
        public MainViewModel(IServiceProvider services)
        {
            _services = services;
            ViewItems = new ObservableCollection<ViewItem>()
            {
                new ViewItem("العائلات", typeof(Core.Abstraction.Views.IFamiliesListingView)),
                new ViewItem("الصرف", typeof(Core.Abstraction.Views.IDisbursementsListingView)),
            };

            Printers = PrinterSettings.InstalledPrinters.Cast<string>().ToList<string>();
        }

        public string Version => "0.0.8" ;

        [ObservableProperty]
        private IEnumerable<string> _printers;

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(RecipePrinter) || e.PropertyName == nameof(LabelPrinter))
            {
                Properties.Settings.Default.Save();
            }
            base.OnPropertyChanged(e);
        }

        public string RecipePrinter
        {
            get => Properties.Settings.Default.RecipePrinter;
            set => SetProperty(Properties.Settings.Default.RecipePrinter, value, Properties.Settings.Default, (settings, recipeprinter) => settings.RecipePrinter = recipeprinter);
        }

        public string LabelPrinter
        {
            get => Properties.Settings.Default.LabelPrinter;
            set => SetProperty(Properties.Settings.Default.LabelPrinter, value, Properties.Settings.Default, (settings, labelPrinter) => settings.LabelPrinter = labelPrinter);
        }

        partial void OnSelectedViewItemChanged(ViewItem oldValue, ViewItem newValue)
        {
            try
            {
                CurrentView = _services.GetRequiredService(newValue.Type);
            }
            catch (InvalidOperationException ex)
            {
                WeakReferenceMessenger.Default.Send(new Core.Messages.Common.ShowNotificationMessage(new Core.ErrorNotification(ex.Message)));
            }
        }

        partial void OnCurrentViewChanged(object oldValue, object newValue)
        {

        }

        [ObservableProperty]
        private object _currentView;

        public IEnumerable<ViewItem> ViewItems
        {
            get => _viewItems;
            private set => SetProperty(ref _viewItems, value);
        }

        private IEnumerable<ViewItem> _viewItems;

        [ObservableProperty]
        private ViewItem _selectedViewItem;

        private readonly IServiceProvider _services;
    }

    public record ViewItem(string Title, Type Type);
}
