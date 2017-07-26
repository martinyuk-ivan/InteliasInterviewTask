using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Logic.Implementation
{
    public class PageManager<TEntity> : IPageManager<TEntity> where TEntity : class
    {
        public async Task<PageModel<TEntity>> CreateAsync(IQueryable<TEntity> query, int pageSize, int pageIndex)
        {
            PageModel<TEntity> page = new PageModel<TEntity>();
            IEnumerable<TEntity> listObjects = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            page.Results = listObjects;
            page.PageSize = pageSize;
            page.CurrentPage = pageIndex;
            int totalRecords= await query.CountAsync();
            page.TotalNumberPages = (int)Math.Ceiling((decimal)totalRecords / page.PageSize);
            return page;
        }
    }
}
