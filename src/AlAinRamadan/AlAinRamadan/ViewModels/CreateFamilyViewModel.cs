using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.DTOs;
using System.Threading.Tasks;

namespace AlAinRamadan.ViewModels
{
    internal class CreateFamilyViewModel : FamilyEditorViewModel, ICreateFamilyViewModel
    {
        public CreateFamilyViewModel(IFamiliesRepository familiesRepository) : base(familiesRepository)
        {
            EnableCheckInputs();
        }

        protected override async Task Save()
        {
            await _familiesRepository.CreateAsync(new FamilyDTO(0, CardNumber, Name, Notes));
        }
    }
}
