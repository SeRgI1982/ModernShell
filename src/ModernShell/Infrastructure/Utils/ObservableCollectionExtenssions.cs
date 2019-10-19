using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infrastructure.Utils
{
    public static class ObservableCollectionExtenssions
    {
        public static void AddRange<T>(this ObservableCollection<T> source, IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return;
            }

            foreach (var item in enumerable)
            {
                source.Add(item);
            }
        }
    }
}