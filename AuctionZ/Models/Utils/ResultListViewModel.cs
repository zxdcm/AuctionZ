using System.Collections.Generic;

namespace AuctionZ.Models.Utils
{
    public class ResultListViewModel<T> where T: class 
    {
        public IEnumerable<T> Items { get; }

        public int PagesNumber { get; }

        public int CurrentPage { get; }

        public ResultListViewModel()
        {

        }

        public ResultListViewModel(IEnumerable<T> items, int pagesNumbers, int currentPage)
        {
            this.Items = items;
            this.PagesNumber = pagesNumbers;
            this.CurrentPage = currentPage;
        }

   
    }
}