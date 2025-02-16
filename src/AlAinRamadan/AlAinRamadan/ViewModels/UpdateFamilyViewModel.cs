using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.DTOs;
using AlAinRamadan.Core.Models;
using System.Threading.Tasks;

namespace AlAinRamadan.ViewModels
{
    internal partial class UpdateFamilyViewModel : FamilyEditorViewModel, IUpdateFamilyViewModel
    {
        public UpdateFamilyViewModel(Family family, IFamiliesRepository familiesRepository) : base(familiesRepository)
        {
            _family = family;
            CardNumber = family.CardNumber;
            Name = family.Name;
            Notes = family.Notes;
            EnableCheckInputs();
        }

        private readonly Family _family;

        protected override async Task Save()
        {
            FamilyDTO familyDTO = new FamilyDTO(_family.Id , CardNumber, Name, Notes);
            bool result = await _familiesRepository.UpdateAsync(familyDTO);
            if(result)
            {
                _family.Name = Name;
                _family.CardNumber = CardNumber;
                _family.Notes = Notes;
            }
        }
    }
}
