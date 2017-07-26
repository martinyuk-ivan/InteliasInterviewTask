using System;
using System.Threading.Tasks;
using DataAccessLayer.Model;
using System.Linq;
using System.Reflection;

namespace DataAccessLayer.Logic.Implementation
{
    public class RepositoryUser : RepositoryBase<User, int>
    {
        public RepositoryUser(UserDbContext dbContext, IPageManager<User> pagingManager) : base(dbContext, pagingManager)
        {
        }

        public override async Task<PageModel<User>> FilterByPredicateAsync(string propName, int pageSize = 10, int pageIndex = 1)
        {
            var filterQuery = dbContext.Users.OrderBy(x=>x.GetType().GetProperty(propName));
            var usersPage = await pagingManager.CreateAsync(filterQuery, pageSize, pageIndex);
            return usersPage;

        }

        public override async Task<PageModel<User>> GetPageAsync(int pageSize, int pageIndex)
        {
            var usersPage = await pagingManager.CreateAsync(dbContext.Users, pageSize, pageIndex);
            return usersPage;
            
        }

        public override async Task<PageModel<User>> SearchAsync(string searchString, int pageSize = 10, int pageIndex = 1)
        {
            var searchQuery = dbContext.Users.Where(sear => sear.FirstName.Contains(searchString) || sear.LastName.Contains(searchString));
            var searchPage = await pagingManager.CreateAsync(searchQuery, pageSize, pageIndex);
            return searchPage;
        }
    }
}
