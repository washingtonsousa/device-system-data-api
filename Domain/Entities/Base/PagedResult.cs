using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Base
{
    public class PagedResult<T>
    {
        public PagedResult(IList<T> items, int page, int total, int itemsPerPage)
        {
            Items = items;
            Page = page;
            Total = total;
            ItemsPerPage = itemsPerPage;
        }

        public IList<T> Items { get; private set; }
        public int Page { get; private set; }
        public int Total { get; private set; }
        public int ItemsPerPage { get; private set; }


    }
}
