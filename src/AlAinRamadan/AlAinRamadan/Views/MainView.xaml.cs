using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.Abstraction.Views;
using System.Windows;

namespace AlAinRamadan.Views
{
    internal partial class MainView : Window, IHomeView
    {
        public MainView(IMainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
