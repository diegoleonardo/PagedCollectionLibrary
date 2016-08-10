using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PagedCollection.Library
{
    public class PagedCollection<T> : IPagedCollection<T>
    {
        private const int DEFAULT_VALUE = 1;
        private const int DEFAULT_START_PAGE = DEFAULT_VALUE;
        private const int DEFAULT_END_OF_SEQUENCE = 7;
        private const int QUADRANT = 4;
        private IList<T> ListOfObjects;
        private IList<T> ListAfterPaged;
        private int GetValueOfStartQuadrant()
        {
            if (this.CurrentPage + QUADRANT >= this.TotalPages)
            {
                return Math.Abs(this.TotalPages - this.CurrentPage - QUADRANT);
            }
            return 0;
        }
        private int GetValueOfEndQuadrant()
        {
            if (this.CurrentPage - QUADRANT < DEFAULT_START_PAGE)
            {
                return Math.Abs(this.CurrentPage - QUADRANT) + DEFAULT_START_PAGE;
            }
            return 0;
        }
        private int GetValueStartOfSequence()
        {
            var start = this.CurrentPage - QUADRANT > DEFAULT_START_PAGE ? this.CurrentPage - QUADRANT : DEFAULT_VALUE;
            var afterStart = GetValueOfStartQuadrant();
            if (start - afterStart <= 0)
                start = DEFAULT_START_PAGE;
            else
                start = start - afterStart;
            return start;
        }
        private int GetValueEndOfSequence()
        {
            var end = this.CurrentPage + QUADRANT < this.TotalPages ? this.CurrentPage + QUADRANT : this.TotalPages;
            var beforeEnd = GetValueOfEndQuadrant();
            if (beforeEnd + end >= this.TotalPages)
                end = this.TotalPages;
            else
                end = end + beforeEnd;
            return end;
        }
        private void FillValueInStartAndEndOfSequenceOfPages()
        {
            this.StartOfSequence = GetValueStartOfSequence();
            this.EndOfSequence = GetValueEndOfSequence();
        }
        private int GetSkip(int? pageNumber)
        {
            return pageNumber.HasValue && pageNumber.Value > DEFAULT_VALUE ? (pageNumber.Value - DEFAULT_VALUE) * RecordsPerPage : 0;
        }
        private int GetCurrentPage(int? currentPage)
        {
            return currentPage.HasValue && currentPage.Value > DEFAULT_VALUE ? currentPage.Value : DEFAULT_START_PAGE;
        }
        public int TotalPages { get; private set; }
        public int TotalRecords { get; private set; }
        public int TotalRecordsInPage { get; private set; }
        public int StartOfSequence { get; private set; }
        public int EndOfSequence { get; private set; }
        public int RecordsPerPage { get; private set; }
        public int CurrentPage { get; private set; }
        public int NextPage { get { return CurrentPage + DEFAULT_VALUE; } }
        public int PreviousPage { get { return CurrentPage - DEFAULT_VALUE; } }
        public int StartPage { get { return DEFAULT_START_PAGE; } }
        public int LastPage { get { return TotalPages; } }
        public int Skip { get; private set; }

        public PagedCollection(IEnumerable<T> objects, int? currentPage, int recordsPerPage)
        {
            this.ListOfObjects = objects.ToList();
            this.TotalRecords = ListOfObjects.Count;
            this.RecordsPerPage = recordsPerPage;
            this.CurrentPage = GetCurrentPage(currentPage);
            this.Skip = GetSkip(this.CurrentPage);
            this.TotalPages = GetTotalPages();
            FillValueInStartAndEndOfSequenceOfPages();
            this.ListAfterPaged = this.ListOfObjects.Skip(this.Skip).Take(this.RecordsPerPage).ToList();
            this.TotalRecordsInPage = ListAfterPaged.Count;
        }

        public int GetTotalPages()
        {
            var countElementsInList = ListOfObjects.Count();
            var exactDivision = countElementsInList % RecordsPerPage == 0;
            var totalOfPages = (countElementsInList / RecordsPerPage);
            if (exactDivision)
                return totalOfPages;
            return totalOfPages + DEFAULT_VALUE;
        }
        public bool IsFirstPage()
        {
            return CurrentPage == DEFAULT_VALUE;
        }
        public bool IsLastPage()
        {
            return CurrentPage == TotalPages;
        }
        public bool HasPreviousPage()
        {
            return (CurrentPage - DEFAULT_VALUE) > 0;
        }
        public bool HasNextPage()
        {
            return (CurrentPage + DEFAULT_VALUE) <= TotalPages;
        }
        public bool HasMoreThanOnePage()
        {
            return TotalPages > DEFAULT_VALUE;
        }
        public void Page(int? pageNumber)
        {
            this.CurrentPage = GetCurrentPage(pageNumber);
            FillValueInStartAndEndOfSequenceOfPages();
            ToSkip(pageNumber);
        }
        public void ToSkip(int? pageNumber)
        {
            this.Skip = GetSkip(pageNumber);
            this.ListOfObjects.Skip(this.Skip).Take(RecordsPerPage);
        }
        public IEnumerable<T> GetEnumarableSkiped()
        {
            this.Skip = GetSkip(this.CurrentPage);
            return ListOfObjects.Skip(this.Skip).Take(RecordsPerPage);
        }
        public IEnumerable<T> GetEnumarableSkipedAndOrderlyBy(Func<T, bool> funcToOrder)
        {
            this.ListOfObjects.OrderBy(funcToOrder);
            this.Skip = GetSkip(this.CurrentPage);
            return ListOfObjects.Skip(this.Skip).Take(RecordsPerPage);
        }
        public IList<T> GetListSkiped()
        {
            return GetEnumarableSkiped().ToList();
        }
        public IList<T> GetListSkipedAndOrderlyBy(Func<T, bool> funcToOrder)
        {
            return GetEnumarableSkipedAndOrderlyBy(funcToOrder).ToList();
        }
        public int GetIntegerTotalSumByColumn(Func<T, int> funcColumnToSum)
        {
            return GetListSkiped().Sum(funcColumnToSum);
        }
        public double GetDoubleTotalSumByColumn(Func<T, double> funcColumnToSum)
        {
            return GetListSkiped().Sum(funcColumnToSum);
        }
        public int GetIntegerTotalSumPerPage(int? pageNumber, Func<T, int> funcColumnToSum)
        {
            var page = GetCurrentPage(pageNumber);
            var skip = GetSkip(page);
            return GetListSkiped().Skip(skip).Take(RecordsPerPage).Sum(funcColumnToSum);
        }
        public decimal GetDecimalTotalSumPerPage(int? pageNumber, Func<T, decimal> funcColumnToSum)
        {
            var page = GetCurrentPage(pageNumber);
            var skip = GetSkip(page);
            return GetListSkiped().Skip(skip).Take(RecordsPerPage).Sum(funcColumnToSum);
        }
        public double GetDoubleTotalSumPerPage(int? pageNumber, Func<T, double> funcColumnToSum)
        {
            var page = GetCurrentPage(pageNumber);
            var skip = GetSkip(page);
            return GetListSkiped().Skip(skip).Take(RecordsPerPage).Sum(funcColumnToSum);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return ListAfterPaged.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ListAfterPaged.GetEnumerator();
        }
    }
}
