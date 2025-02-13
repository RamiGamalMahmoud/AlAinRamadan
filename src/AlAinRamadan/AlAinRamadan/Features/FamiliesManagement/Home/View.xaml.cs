using AlAinRamadan.Core.Abstraction.Families;
using System.Windows.Controls;

namespace AlAinRamadan.Features.FamiliesManagement.Home
{
    internal partial class View : UserControl, IFamiliesHomeView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_isLoaded)
            {
                await Dispatcher.Invoke(() => (DataContext as ViewModel).LoadCommand.ExecuteAsync(null));
                _isLoaded = true;
            }
        }

        private bool _isLoaded;
    }
}
