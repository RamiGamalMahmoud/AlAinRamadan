using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.Abstraction.Views;

namespace AlAinRamadan.Views
{
    internal class UpdateFamilyView : FamilyEditorView, IUpdateFamilyView
    {
        public UpdateFamilyView(IUpdateFamilyViewModel viewModel) : base(viewModel)
        {
        }
    }
}
