using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Logic
{
   public interface IPageManager<TEntity> where TEntity:class
    {
        Task<PageModel<TEntity>> CreateAsync(IQueryable<TEntity> query, int pageSize, int pageIndex);
    }
}
