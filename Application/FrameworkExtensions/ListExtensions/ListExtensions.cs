using System.Collections.Generic;

namespace CodeConcepts.FrameworkExtensions.ListExtensions
{
    public static class ListExtensions
    {
        //TODO test this
        public static void AddAll<TItem>(this IList<TItem> list, IEnumerable<TItem> items)
        {
            var stdList = list as List<TItem>;
            if (stdList != null)
            {
                stdList.AddRange(items);
            }
            else
            {
                foreach (var item in items)
                {
                    list.Add(item);
                }
            }
        }
    }
}