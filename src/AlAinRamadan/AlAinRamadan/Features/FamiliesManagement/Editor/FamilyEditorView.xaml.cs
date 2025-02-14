using System.Windows;

namespace AlAinRamadan.Features.FamiliesManagement.Editor
{
    internal partial class FamilyEditorView : Window
    {
        public FamilyEditorView(FamilyEditorViewModel viewModel)
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
