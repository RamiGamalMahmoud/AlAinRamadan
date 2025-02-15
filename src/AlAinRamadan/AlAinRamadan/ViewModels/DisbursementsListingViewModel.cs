﻿using AlAinRamadan.Core.Abstraction.Repositories;
using AlAinRamadan.Core.Abstraction.ViewModels;
using AlAinRamadan.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AlAinRamadan.ViewModels
{
    internal partial class DisbursementsListingViewModel : ObservableObject, IDisbursementsListingViewModel
    {
        public DisbursementsListingViewModel(IMediator mediator, IDisbursementsRepository disbursementsRepository, IFamiliesRepository familiesRepository)
        {
            _mediator = mediator;
            _disbursementsRepository = disbursementsRepository;
            _familiesRepository = familiesRepository;
        }

        public async Task LoadAsync()
        {
            TicketNumber = await _disbursementsRepository.GetLastTicketNumberAsync(DateTime.Now) + 1;
        }

        async partial void OnCardNumberChanged(string oldValue, string newValue)
        {
            if (InputType != InputType.CardNumber)
            {
                return;
            }
            Families = await _familiesRepository.GetFamiliesByPartOfCardNumberAsync(newValue);
            Family = await _familiesRepository.GetFamilyByCardNumberAsync(newValue);
            FamilyId = Family?.Id.ToString();
        }

        async partial void OnFamilyIdChanged(string oldValue, string newValue)
        {
            if (InputType != InputType.FamilyId)
            {
                return;
            }
            int.TryParse(newValue, out int result);
            Families = [];
            Family = await _familiesRepository.GetFamilyByIdAsync(result);
            CardNumber = Family?.CardNumber;
        }

        async partial void OnFamilyChanged(Family oldValue, Family newValue)
        {
            _disbursements.Clear();
            LastDisbursement = null;
            if(InputType == InputType.Family && newValue is not null)
            {
                CardNumber = newValue.CardNumber;
                FamilyId = newValue.Id.ToString();
            }
            if (newValue is not null)
            {
                var disbursements = await _disbursementsRepository.GetFamilyDisbursementsAsync(newValue.Id);
                LastDisbursement = disbursements.FirstOrDefault();
                foreach (Disbursement disbursement in disbursements)
                {
                    _disbursements.Add(disbursement);
                }
            }
        }

        partial void OnFamiliesChanged(IEnumerable<Family> oldValue, IEnumerable<Family> newValue)
        {
            HasFamilies = newValue is not null && newValue.Any();
        }

        [RelayCommand(CanExecute = nameof(CanPrintTicket))]
        private async Task DirectPrintTicket()
        {
            int familyId = int.Parse(FamilyId);
            Disbursement created = await _disbursementsRepository.CreateAsync(new Disbursement(familyId, TicketNumber, DateTime.Now, Notes));

            if (created is not null)
            {
                TicketNumber++;
                FamilyId = string.Empty;
                CardNumber = string.Empty;
                Notes = string.Empty;
                _disbursements.Clear();
            }

            await _mediator.Send(new Core.Commands.Common.DirectPrintCommand("DisbursementTicket.rdlc", Properties.Settings.Default.RecipePrinter, false, TicketParameters(created)));
        }

        [RelayCommand]
        private async Task DeleteDisbursement(Disbursement disbursement)
        {
            await _disbursementsRepository.DeleteAsync(disbursement.Id);
            _disbursements.Remove(disbursement);
        }

        private bool CanPrintTicket() => Family is not null;

        private Dictionary<string, string> TicketParameters(Disbursement disbursement)
        {
            return new Dictionary<string, string>()
            {
                { "Date", disbursement.DateCreated.ToString() },
                { "RationCard", disbursement.Family.CardNumber },
                { "FamilyId", disbursement.Family.Id.ToString() },
                { "Name", disbursement.Family.Name },
                { "TicketNumber", disbursement.TicketNumber.ToString() }
            };
        }

        [ObservableProperty]
        private InputType _inputType = InputType.FamilyId;

        public bool HasFamilies
        {
            get => _hasFamilies;
            set
            {
                SetProperty(ref _hasFamilies, value);
            }
        }
        public bool _hasFamilies;

        [ObservableProperty]
        private string _cardNumber = string.Empty;

        [ObservableProperty]
        private string _familyId = string.Empty;

        [ObservableProperty]
        private int _ticketNumber;

        [ObservableProperty]
        private Disbursement _lastDisbursement;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DirectPrintTicketCommand))]
        private Family _family;

        [ObservableProperty]
        private IEnumerable<Family> _families;

        [ObservableProperty]
        private string _notes = string.Empty;

        public IEnumerable<Disbursement> Disbursements => _disbursements;

        private readonly ObservableCollection<Disbursement> _disbursements = new ObservableCollection<Disbursement>();

        private readonly IMediator _mediator;
        private readonly IDisbursementsRepository _disbursementsRepository;
        private readonly IFamiliesRepository _familiesRepository;
    }
}
