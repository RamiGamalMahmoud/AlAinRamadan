using AlAinRamadan.Core.Abstraction.Contexts;
using AlAinRamadan.Core.Abstraction.Services;
using AlAinRamadan.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AlAinRamadan.Services
{
    public partial class ApplicationContext : ObservableObject, IApplicationContext, IFamilyContext
    {
        private Family _currentFamily;

        public Family CurrentFamily
        {
            get => _currentFamily;
            set => SetProperty(ref _currentFamily, value);
        }

        public Family GetCurrentFamily()
        {
            Family family = CurrentFamily;
            CurrentFamily = null;
            return family;
        }
    }
}
