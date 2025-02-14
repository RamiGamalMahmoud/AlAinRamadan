using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.Abstraction.Views;
using System.Windows.Controls;

namespace AlAinRamadan.Views
{
    internal partial class FamiliesListingView : UserControl, IFamiliesListingView
    {
        public FamiliesListingView(IFamiliesListingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_isLoaded)
            {
                await Dispatcher.Invoke(() => (DataContext as IFamiliesListingViewModel).LoadAsync());
                _isLoaded = true;
            }
        }

        private bool _isLoaded;
    }
}
