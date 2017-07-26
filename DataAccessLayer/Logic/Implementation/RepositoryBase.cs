using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Logic.Implementation
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {
      protected UserDbContext dbContext;
        protected IPageManager<TEntity> pagingManager;
        public RepositoryBase(UserDbContext dbContext,IPageManager<TEntity> pagingManager)
        {
            this.dbContext = dbContext;
            this.pagingManager = pagingManager;
        }
        public abstract Task<PageModel<TEntity>> FilterByPredicateAsync(string propertyName, int pageSize = 10, int pageIndex = 1);

        public abstract Task<PageModel<TEntity>> GetPageAsync(int pageSize, int pageIndex);

        public abstract Task<PageModel<TEntity>> SearchAsync(string searchString, int pageSize = 10, int pageIndex = 1);
    }
}
