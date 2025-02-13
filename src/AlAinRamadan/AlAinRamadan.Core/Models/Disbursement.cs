using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AlAinRamadan.Core.Models
{
    [ObservableObject]
    public partial class Disbursement : ModelBase
    {
        private Disbursement()
        {

        }

        public Disbursement(int familyId, int ticketNumber, DateTime dateCreated, string notes)
        {
            FamilyId = familyId;
            TicketNumber = ticketNumber;
            DateCreated = dateCreated;
            Notes = notes;
        }

        [ObservableProperty]
        private DateTime _dateCreated;

        [ObservableProperty]
        private int _ticketNumber;

        [ObservableProperty]
        private string _notes;

        public int FamilyId { get; set; }
        [ObservableProperty]
        private Family _family;

    }
}
