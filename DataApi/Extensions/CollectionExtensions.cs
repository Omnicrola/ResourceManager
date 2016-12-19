using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataApi.Api;

namespace DataApi.Extensions
{
    public static class CollectionExtensions
    {
        public static void SlaveTo<T, K>(this ObservableCollection<K> slave, ObservableCollection<T> master, IConversionFactory<T, K> conversionFactory)
        {
            AddInitialData(slave, master, conversionFactory);
            master.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    AddItems(slave, args, conversionFactory);
                }
                else if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    RemoveItems(slave, args);
                }
                else if (args.Action == NotifyCollectionChangedAction.Reset)
                {
                    slave.Clear();
                }
            };
        }

        private static void AddInitialData<T, K>(ObservableCollection<K> slave, IEnumerable<T> master,
            IConversionFactory<T, K> conversionFactory)
        {
            foreach (var item in master)
            {
                slave.Add(conversionFactory.Build(item));
            }
        }

        public static void SlaveTo<T, K>(this ObservableCollection<K> slave, DataCollection<T> master, IConversionFactory<T, K> conversionFactory)
        {
            AddInitialData(slave, master, conversionFactory);
            master.DataChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    AddItems(slave, args, conversionFactory);
                }
                else if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    RemoveItems(slave, args);
                }
                else if (args.Action == NotifyCollectionChangedAction.Reset)
                {
                    slave.Clear();
                }
            };
        }

        private static void RemoveItems<T>(ObservableCollection<T> slave, NotifyCollectionChangedEventArgs args)
        {
            foreach (var oldItem in args.OldItems)
            {
                slave.Remove((T)oldItem);
            }
        }

        private static void AddItems<T, K>(ObservableCollection<K> slave, NotifyCollectionChangedEventArgs args, IConversionFactory<T, K> conversionFactory)
        {
            foreach (var newItem in args.NewItems)
            {
                slave.Add(conversionFactory.Build((T)newItem));
            }
        }
    }
}