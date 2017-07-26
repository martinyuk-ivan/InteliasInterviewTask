using DataAccessLayer.Model;
using System;
using System.Threading.Tasks;

namespace DataAccessLayer.Logic
{
    public interface IRepositoryBase<TEntity,TKey> where TEntity:class
    {
        Task<PageModel<TEntity>> GetPageAsync(int pageSize,int pageIndex);
        Task<PageModel<TEntity>> FilterByPredicateAsync(string propertyName, int pageSize = 10, int pageIndex = 1);
        Task<PageModel<TEntity>> SearchAsync(string searchString, int pageSize=10, int pageIndex=1);

    }
}
