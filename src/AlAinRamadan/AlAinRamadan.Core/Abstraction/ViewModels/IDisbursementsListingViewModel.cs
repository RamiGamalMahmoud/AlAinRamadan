using System.Threading.Tasks;

namespace AlAinRamadan.Core.Abstraction.ViewModels
{
    public interface IDisbursementsListingViewModel
    {
        Task LoadAsync();
        InputType InputType { get; set; }
    }

    public enum InputType
    {
        CardNumber,
        FamilyId
    }
}
