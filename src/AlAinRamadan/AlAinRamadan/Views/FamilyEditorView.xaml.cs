using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.Abstraction.Views;
using System.Windows;

namespace AlAinRamadan.Views
{
    internal partial class FamilyEditorView : Window, IFamilyEditorView
    {
        public FamilyEditorView(IFamilyEditoViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
