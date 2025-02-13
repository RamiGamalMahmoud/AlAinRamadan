using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace AlAinRamadan.Core
{
    public abstract partial class EditorViewModelBase : ObservableValidator
    {
        public EditorViewModelBase()
        {
            ValidateAllProperties();
        }

        protected virtual bool CanSave() => !HasErrors;
        [RelayCommand(CanExecute = nameof(CanSave))]
        protected abstract Task Save();

        protected bool _isCheckInputsEnabled = false;
        protected void EnableCheckInputs() => _isCheckInputsEnabled = true;
    }
}
