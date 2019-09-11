using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineLibrary.Data.Models;

namespace OnlineLibrary.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public string SearchByBookTitle { get; set; }
        public string SearchByGenreName { get; set; }
        public string SearchByWriterName { get; set; }

        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public List<Genre> Genres { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static PaginatedList<T> Create(List<T> items, int pageIndex, int pageSize)
        {
            var count = items.Count();
            items = items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
