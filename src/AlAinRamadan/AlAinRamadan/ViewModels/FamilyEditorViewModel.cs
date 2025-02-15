using AlAinRamadan.Core;
using AlAinRamadan.Core.Abstraction.Services;
using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AlAinRamadan.ViewModels
{
    internal abstract partial class FamilyEditorViewModel : EditorViewModelBase, IFamilyEditoViewModel
    {
        public FamilyEditorViewModel(IFamiliesService familiesService) : base()
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
            FoundFamiliesByName = await _familiesService.GetFamiliesByPartOfCardNumberAsync(newValue);
            if (_isCheckInputsEnabled) HasFoundFamiliesByCardNumber = FoundFamiliesByName.Select(x => x.CardNumber).Contains(newValue);
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
