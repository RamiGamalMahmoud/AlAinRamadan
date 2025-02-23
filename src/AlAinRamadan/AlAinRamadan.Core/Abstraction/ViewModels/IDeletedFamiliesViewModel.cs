using AlAinRamadan.Core.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AlAinRamadan.Core.Abstraction.ViewModels
{
    public interface IDeletedFamiliesViewModel
    {
        ObservableCollection<Family> Families { get; }
        Task LoadAsync();
    }
}
