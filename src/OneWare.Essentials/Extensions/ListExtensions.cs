﻿namespace OneWare.Essentials.Extensions;

public static class ListExtensions
{
    public static int BinarySearchNext<TItem, TSearch>(this IList<TItem> list, TSearch value, Func<TSearch, TItem, TItem, int> comparer)
    {
        if (list is null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (comparer is null)
        {
            throw new ArgumentNullException(nameof(comparer));
        }

        int lower = 0;
        int upper = list.Count - 1;

        while (lower <= upper)
        {
            int middle = lower + ((upper - lower) / 2);
            int comparisonResult = comparer(value, list[middle], middle + 1 < list.Count ? list[middle+1] : list[middle]);
            if (comparisonResult < 0)
            {
                upper = middle - 1;
            }
            else if (comparisonResult > 0)
            {
                lower = middle + 1;
            }
            else
            {
                return middle;
            }
        }

        return ~lower;
    }
}