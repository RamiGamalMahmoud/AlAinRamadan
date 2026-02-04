using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.DTOs;
using AlAinRamadan.Core.Models;
using MediatR;
using System.Threading.Tasks;

namespace AlAinRamadan.ViewModels
{
    internal partial class UpdateFamilyViewModel : FamilyEditorViewModel, IUpdateFamilyViewModel
    {
        public UpdateFamilyViewModel(Family family, IFamiliesRepository familiesRepository, IMediator mediator) : base(familiesRepository, mediator)
        {
            _family = family;
            CardNumber = family.CardNumber;
            Name = family.Name;
            Notes = family.Notes;
            ApplicantName = family.ApplicantName;
            EnableCheckInputs();
        }

        private readonly Family _family;

        protected override async Task Save()
        {
            FamilyDTO familyDTO = new FamilyDTO(_family.Id, CardNumber, Name, ApplicantName, Notes);
            bool result = await _familiesRepository.UpdateAsync(familyDTO);
            if (result)
            {
                _family.Name = Name;
                _family.CardNumber = CardNumber;
                _family.Notes = Notes;
                _family.ApplicantName = ApplicantName;
            }
        }
    }
}
