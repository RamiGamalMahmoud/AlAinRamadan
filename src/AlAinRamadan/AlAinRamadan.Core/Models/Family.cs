using CommunityToolkit.Mvvm.ComponentModel;

namespace AlAinRamadan.Core.Models
{
    [ObservableObject]
    public partial class Family : ModelBase
    {
        private Family() { }

        public Family(string cardNumber, string name, string notes)
        {
            CardNumber = cardNumber;
            Name = name;
            Notes = notes;
        }

        [ObservableProperty]
        private string _cardNumber;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private bool? _isDeleted;

        [ObservableProperty]
        private string _notes;
    }
}
