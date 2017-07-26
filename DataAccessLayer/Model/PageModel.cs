using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Model
{
   public class PageModel<TEntity> where TEntity:class
    {
        public IEnumerable<TEntity> Results { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumberPages { get; set; }

    }
}
