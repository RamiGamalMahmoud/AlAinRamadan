using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.Abstraction.Views;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AlAinRamadan.Views
{
    internal partial class DisbursementsListingView : UserControl, IDisbursementsListingView
    {
        public DisbursementsListingView(IDisbursementsListingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            Loaded += async (s, e) => await Dispatcher.Invoke(() => viewModel.LoadAsync());
            Loaded += (s, e) => Dispatcher.Invoke(() => TextBoxFamilyId.Focus());
        }

        private void TextBoxCardNumber_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            (DataContext as IDisbursementsListingViewModel).InputType = InputType.CardNumber;
        }

        private void TextBoxFamilyId_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            (DataContext as IDisbursementsListingViewModel).InputType = InputType.FamilyId;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBoxFamilyId.Focus();
        }

        private void ListView_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            (DataContext as IDisbursementsListingViewModel).InputType = InputType.Family;
        }
    }
}
