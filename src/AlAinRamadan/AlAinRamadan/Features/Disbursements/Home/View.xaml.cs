using AlAinRamadan.Core.Abstraction.Disbursements;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AlAinRamadan.Features.Disbursements.Home
{
    internal partial class View : UserControl, IDisbursementsHomeView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            Loaded += async (s, e) => await Dispatcher.Invoke(() => viewModel.LoadAsync());
            Loaded += (s, e) => Dispatcher.Invoke(() => TextBoxFamilyId.Focus());
        }

        private void TextBoxCardNumber_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            (DataContext as ViewModel).InputType = InputType.CardNumber;
        }

        private void TextBoxFamilyId_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            (DataContext as ViewModel).InputType = InputType.FamilyId;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBoxFamilyId.Focus();
        }
    }
}
