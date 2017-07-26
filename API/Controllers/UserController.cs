using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Logic;

namespace API.Controllers
{
    [Route("api/[userController]")]
    public class UserController : Controller
    {
        IUnitOfWork unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> GetPage([FromRoute]int pageSize=10,[FromRoute] int pageIndex=1)
        {
            var usersPage = await unitOfWork.UserRepository.GetPageAsync(pageSize, pageIndex);
            return Ok(usersPage);
        }
    }
}
