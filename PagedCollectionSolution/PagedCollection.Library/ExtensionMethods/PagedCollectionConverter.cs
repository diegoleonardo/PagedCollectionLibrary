using System.Collections.Generic;

namespace PagedCollection.Library.ExtensionMethods
{
    public static class PagedCollectionConverter
    {
        public static IPagedCollection<T> ToPagedCollection<T>(this IEnumerable<T> listOfObjects, int currentPage = 1, int recordsPerPage = 50)
        {
            var pagedCollection = new PagedCollection<T>(listOfObjects, currentPage, recordsPerPage);
            return pagedCollection;
        }
    }
}
