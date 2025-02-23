using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AlAinRamadan.ViewModels
{
    internal partial class DeletedFamiliesViewModel : ObservableObject, Core.Abstraction.ViewModels.IDeletedFamiliesViewModel
    {
        public DeletedFamiliesViewModel(IFamiliesRepository familiesRepository)
        {
            _familiesRepository = familiesRepository;
        }

        [RelayCommand]
        public async Task LoadAsync() => Families = new ObservableCollection<Family>(await _familiesRepository.GetDeletedFamiliesAsync());

        [RelayCommand]
        private async Task RestoreFamily(Family family)
        {
            await _familiesRepository.RestoreAsync(family.Id);
            Families.Remove(family);
        }

        [ObservableProperty]
        private ObservableCollection<Family> _families = new ObservableCollection<Family>();

        private readonly IFamiliesRepository _familiesRepository;
    }
}
