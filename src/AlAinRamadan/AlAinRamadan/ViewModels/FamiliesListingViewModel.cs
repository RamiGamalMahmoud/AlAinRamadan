using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlAinRamadan.ViewModels
{
    internal partial class FamiliesListingViewModel : ObservableObject, IFamiliesListingViewModel
    {
        public FamiliesListingViewModel(IMediator mediator, IFamiliesRepository familiesRepository)
        {
            _mediator = mediator;
            _familiesRepository = familiesRepository;
        }

        [ObservableProperty]
        private IEnumerable<Family> _families;

        [ObservableProperty]
        private int _totalFamilies;

        [ObservableProperty]
        private string _familyName;

        async partial void OnFamilyNameChanged(string oldValue, string newValue)
        {
            Families = await _familiesRepository.GetFamiliesByNameAsync(newValue);
            if (newValue == string.Empty)
            {
                Families = await _familiesRepository.GetAllFamiliesAsync();
            }
        }

        [RelayCommand]
        private Task ShowDeletedFamilies()
        {
            return _mediator.Send(new Core.Commands.Common.ShowDeletedCommand<Family>());
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            Families = await _familiesRepository.GetAllFamiliesAsync();
            TotalFamilies = await _familiesRepository.GetTotalFamiliesAsync();
        }

        [RelayCommand]
        private async Task AddFamily()
        {
            await _mediator.Send(new Core.Commands.Common.ShowCreateCommand<Family>());
            TotalFamilies = await _familiesRepository.GetTotalFamiliesAsync();
        }

        [RelayCommand]
        private async Task ShowEditFamily(Family family)
        {
            await _mediator.Send(new Core.Commands.Common.ShowEditCommand<Family>(family));
        }

        [RelayCommand]
        private async Task PrintFamilyBarcode(Family family)
        {
            string barcodeImageString = Services.GenerateBarCode.ToBarCodeString(family.CardNumber);
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "Barcode", barcodeImageString },
                { "FamilyId", family.Id.ToString() }
            };

            await _mediator.Send(new Core.Commands.Common.DirectPrintCommand("FamilyBarcode.rdlc", Properties.Settings.Default.LabelPrinter, true, parameters));
        }

        [RelayCommand]
        private async Task PrintAllBarcodes()
        {
            foreach (Family family in Families)
            {
                await PrintFamilyBarcodeCommand.ExecuteAsync(family);
            }
        }

        [RelayCommand]
        private async Task DeleteFamily(Family family)
        {
            await _familiesRepository.DeleteAsync(family.Id);
            Families = await _familiesRepository.GetAllFamiliesAsync();
            TotalFamilies = await _familiesRepository.GetTotalFamiliesAsync();
        }

        private readonly IMediator _mediator;
        private readonly IFamiliesRepository _familiesRepository;
    }
}
