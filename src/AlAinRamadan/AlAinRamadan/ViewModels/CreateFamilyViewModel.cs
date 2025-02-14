using AlAinRamadan.Core.Abstraction.Services;
using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.DTOs;
using System.Threading.Tasks;

namespace AlAinRamadan.ViewModels
{
    internal class CreateFamilyViewModel : FamilyEditorViewModel, ICreateFamilyViewModel
    {
        public CreateFamilyViewModel(IFamiliesService familiesService) : base(familiesService)
        {
            EnableCheckInputs();
        }

        protected override async Task Save()
        {
            await _familiesService.CreateFamilyAsync(new FamilyDTO(0, CardNumber, Name));
        }
    }
}
