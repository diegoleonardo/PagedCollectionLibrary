using System;
using System.Collections.Generic;

namespace PagedCollection.Library
{
    public interface IPagedCollection<T> : IEnumerable<T>
    {
        int TotalPages { get; }
        int TotalRecords { get; }
        int TotalRecordsInPage { get; }
        int StartOfSequence { get; }
        int EndOfSequence { get; }
        int RecordsPerPage { get; }
        int CurrentPage { get; }
        int NextPage { get; }
        int PreviousPage { get; }
        int StartPage { get; }
        int LastPage { get; }
        int Skip { get; }
        int GetTotalPages();
        bool IsFirstPage();
        bool IsLastPage();
        bool HasPreviousPage();
        bool HasNextPage();
        bool HasMoreThanOnePage();
        void Page(int? pageNumber);
        void ToSkip(int? pageNumber);
        int GetIntegerTotalSumByColumn(Func<T, int> funcColumnToSum);
        double GetDoubleTotalSumByColumn(Func<T, double> funcColumnToSum);
        int GetIntegerTotalSumPerPage(int? pageNumber, Func<T, int> funcColumnToSum);
        decimal GetDecimalTotalSumPerPage(int? pageNumber, Func<T, decimal> funcColumnToSum);
        double GetDoubleTotalSumPerPage(int? pageNumber, Func<T, double> funcColumnToSum);
        IEnumerable<T> GetEnumarableSkiped();
        IEnumerable<T> GetEnumarableSkipedAndOrderlyBy(Func<T, bool> funcToOrder);
        IList<T> GetListSkiped();
        IList<T> GetListSkipedAndOrderlyBy(Func<T, bool> funcToOrder);
    }
}
