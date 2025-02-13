using CommunityToolkit.Mvvm.ComponentModel;

namespace AlAinRamadan.Core.Models
{
    [ObservableObject]
    public partial class Family : ModelBase
    {
        private Family() { }

        public Family(string cardNumber, string name)
        {
            CardNumber = cardNumber;
            Name = name;
        }

        [ObservableProperty]
        private string _cardNumber;

        [ObservableProperty]
        private string _name;
    }
}
