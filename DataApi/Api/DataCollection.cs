using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DataApi.Api
{
    public class DataCollection<T> : ReadOnlyObservableCollection<T>
    {
        public DataCollection(ObservableCollection<T> list) : base(list)
        {

        }

        public event NotifyCollectionChangedEventHandler DataChanged
        {
            add { CollectionChanged += value; }
            remove { CollectionChanged -= value; }
        }
    }
}