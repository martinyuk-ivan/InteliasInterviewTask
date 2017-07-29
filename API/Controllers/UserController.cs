using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Logic;
using DataAccessLayer.Model;
using System.Reflection;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [EnableCors("MyPolicy")] 
    [Route("api/userController")]
    public class UserController : Controller
    {
        IUnitOfWork unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        IActionResult chekIfPagingIsNotNull(int pageSize,int pageIndex)
        {
            if (pageSize == 0 || pageIndex == 0)
            {
                return BadRequest("Bad pageIndex or pageSize ");
            }
            return null;

        }
        public async Task<IActionResult> GetPage([FromQuery]int pageSize=10,[FromQuery] int pageIndex=1)
        {
            if ( pageSize == 0 || pageIndex == 0)
            {
                return BadRequest("Bad pageIndex or pageSize ");
            }
            var usersPage = await unitOfWork.UserRepository.GetPageAsync(pageSize, pageIndex);
            if (usersPage.Results.Count()==0)
            {
                return NotFound("chek if pageSize and page index is valid");
            }
            return Ok(usersPage);
        }
        [Route("filterBy")]
        public async Task<IActionResult> GetByFilter([FromQuery] string propertyName, [FromQuery]int pageSize = 10, [FromQuery] int pageIndex = 1)
        {
            if (string.IsNullOrEmpty(propertyName)|| pageSize==0||pageIndex==0)
            {
                return BadRequest("Bad input parameters");

            }
            PropertyInfo[] allModelProperties= typeof(User).GetProperties();
            if (allModelProperties.Any(pn=>pn.Name==propertyName))
            {
                var filterPage = await unitOfWork.UserRepository.FilterByPredicateAsync(propertyName, pageSize, pageIndex);
                if (filterPage.Results.Count() == 0)
                {
                    return NotFound("chek if pageSize and page index is valid");
                }
                return Ok(filterPage);
            }
            return NotFound("not  found this property chek if property is valid ");

        }
        [Route("search")]
        public async Task<IActionResult> GetBySearchString([FromQuery]string searchString, [FromQuery]int pageSize = 10, [FromQuery] int pageIndex = 1)
        {
            if (string.IsNullOrEmpty(searchString) || pageSize == 0 || pageIndex == 0)
            {
                return BadRequest("Bad input parameters");

            }
            var seachedPage = await unitOfWork.UserRepository.SearchAsync(searchString, pageSize, pageIndex);
            if (seachedPage.Results.Count() == 0)
            {
                return NotFound("chek if pageSize and page index is valid");
            }
            return Ok(seachedPage);
        }

    }
}
