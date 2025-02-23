using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.Abstraction.Views;
using System.Windows;

namespace AlAinRamadan.Views
{
    public partial class DeletedFamiliesView : Window, IDeletedFamiliesView
    {
        public DeletedFamiliesView(IDeletedFamiliesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Loaded += (s, e) => Dispatcher.Invoke(async () => await viewModel.LoadAsync());
        }
    }
}
