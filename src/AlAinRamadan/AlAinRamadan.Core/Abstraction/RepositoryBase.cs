using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AlAinRamadan.Core.Abstraction
{
    public abstract class RepositoryBase<TModel>
    {
        protected void SetModels(IEnumerable<TModel> models)
        {
            _models = new ObservableCollection<TModel>(models);
        }

        protected static ObservableCollection<TModel> _models;
    }
}
