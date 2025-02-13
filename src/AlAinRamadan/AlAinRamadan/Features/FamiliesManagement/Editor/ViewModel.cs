using AlAinRamadan.Core;
using AlAinRamadan.Core.Abstraction.Services;
using AlAinRamadan.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AlAinRamadan.Features.FamiliesManagement.Editor
{
    internal abstract partial class ViewModel : EditorViewModelBase
    {
        public ViewModel(IFamiliesService familiesService) : base()
        {
            _familiesService = familiesService;
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
        
        protected readonly IFamiliesService _familiesService;

        async partial void OnNameChanged(string oldValue, string newValue)
        {
            if (_isCheckInputsEnabled)
            {
                FoundFamiliesByName = await _familiesService.GetFamiliesByNameAsync(newValue);
                HasFoundFamiliesByName = FoundFamiliesByName.Any();
            }
        }

        async partial void OnCardNumberChanged(string oldValue, string newValue)
        {
            if (_isCheckInputsEnabled) HasFoundFamiliesByCardNumber = (await _familiesService.GetFamiliesByCardNumberAsync(newValue)).Any();
        }

        [ObservableProperty]
        private bool _hasFoundFamiliesByName;

        [ObservableProperty]
        private bool _hasFoundFamiliesByCardNumber;

        [ObservableProperty]
        private IEnumerable<Family> _foundFamiliesByName;

        protected override bool CanSave() => !HasErrors && !HasFoundFamiliesByCardNumber;
    }
}
