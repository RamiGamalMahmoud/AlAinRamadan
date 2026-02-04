using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.DTOs;
using MediatR;
using System.Threading.Tasks;

namespace AlAinRamadan.ViewModels
{
    internal class CreateFamilyViewModel : FamilyEditorViewModel, ICreateFamilyViewModel
    {
        public CreateFamilyViewModel(IFamiliesRepository familiesRepository, IMediator mediator) : base(familiesRepository, mediator)
        {
            EnableCheckInputs();
        }

        protected override async Task Save()
        {
            await _familiesRepository.CreateAsync(new FamilyDTO(0, CardNumber, Name, ApplicantName, Notes));
        }
    }
}
