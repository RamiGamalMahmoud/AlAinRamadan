using AlAinRamadan.Core;
using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlAinRamadan.ViewModels
{
    internal abstract partial class FamilyEditorViewModel : EditorViewModelBase, IFamilyEditoViewModel
    {
        public FamilyEditorViewModel(IFamiliesRepository familiesRepository, IMediator mediator) : base()
        {
            _familiesRepository = familiesRepository;
            _mediator = mediator;
        }

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _cardNumber;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _name;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _applicantName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _notes;

        protected readonly IFamiliesRepository _familiesRepository;
        private readonly IMediator _mediator;

        async partial void OnNameChanged(string oldValue, string newValue)
        {
            if (_isCheckInputsEnabled)
            {
                FoundFamiliesByName = await _familiesRepository.GetFamiliesByNameAsync(newValue);
                HasFoundFamiliesByName = FoundFamiliesByName.Any();
            }
        }

        async partial void OnCardNumberChanged(string oldValue, string newValue)
        {
            if (_isCheckInputsEnabled)
            {
                FoundFamiliesByName = await _familiesRepository.GetFamiliesByPartOfCardNumberAsync(newValue);
                HasFoundFamiliesByCardNumber = FoundFamiliesByName.Select(x => x.CardNumber).Contains(newValue);
            }
        }

        async partial void OnApplicantNameChanged(string oldValue, string newValue)
        {
            if (!_isCheckInputsEnabled)
                return;
            FoundFamiliesByName = await _mediator.Send(new Core.Queries.GetFamiliesByApplicantNameQuery(newValue));
            HasFoundFamiliesByName = FoundFamiliesByName.Any();
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasFamilies))]
        private bool _hasFoundFamiliesByName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasFamilies))]
        private bool _hasFoundFamiliesByCardNumber;

        public bool HasFamilies => HasFoundFamiliesByCardNumber || HasFoundFamiliesByName;

        [ObservableProperty]
        private IEnumerable<Family> _foundFamiliesByName;

        protected override bool CanSave() => !HasErrors && !HasFoundFamiliesByCardNumber;
    }
}
