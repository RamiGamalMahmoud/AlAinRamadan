using AlAinRamadan.Core.Abstraction.Services;
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
        public FamiliesListingViewModel(IMediator mediator, IFamiliesService familiesService)
        {
            _mediator = mediator;
            _familiesService = familiesService;
        }

        [ObservableProperty]
        private IEnumerable<Family> _families;

        [ObservableProperty]
        private int _totalFamilies;

        [ObservableProperty]
        private string _familyName;

        async partial void OnFamilyNameChanged(string oldValue, string newValue)
        {
            Families = await _familiesService.GetFamiliesByNameAsync(newValue);
            if(newValue == string.Empty)
            {
                Families = await _familiesService.GetAllFamiliesAsync();
            }
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            Families = await _familiesService.GetAllFamiliesAsync();
            TotalFamilies = await _familiesService.GetTotalFamiliesAsync();
        }

        [RelayCommand]
        private async Task AddFamily()
        {
            await _mediator.Send(new Core.Commands.Common.ShowCreateCommand<Family>());
            TotalFamilies = await _familiesService.GetTotalFamiliesAsync();
        }

        [RelayCommand]
        private async Task ShowEditFamily(Family family)
        {
            await _mediator.Send(new Core.Commands.Common.ShowEditCommand<Family>(family));
        }

        [RelayCommand]
        private async Task PrintFamilyBarcode(Family family)
        {
            string barcodeImageString = Services.GenerateBarCode.ToBarCodeString(family.Id);
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "Barcode", barcodeImageString }
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

        private readonly IMediator _mediator;
        private readonly IFamiliesService _familiesService;
    }
}
