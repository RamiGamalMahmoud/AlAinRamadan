using AlAinRamadan.Core.Abstraction.Home;
using System.Windows;

namespace AlAinRamadan.Features.Home
{
    internal partial class View : Window, IHomeView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
