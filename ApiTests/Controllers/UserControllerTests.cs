using API.Controllers;
using DataAccessLayer.Logic;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.Controllers
{
   public class UserControllerTests
    {
        Mock<IUnitOfWork> mockUnit;
        UserController userController;
        public UserControllerTests()
        {
            mockUnit = new Mock<IUnitOfWork>();
           
        }
        #region BadRequestTest
        [Theory]
        [InlineData(0,0)]
        [InlineData(0,1)]
        [InlineData(1,0)]
        public async Task  GetByPageTest(int pageSize,int pageIndex)
        {
            
            userController = new UserController(mockUnit.Object);
            var result = await userController.GetPage(pageSize,pageIndex);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Theory]
        [InlineData(null,0, 0)]
        [InlineData("",0, 1)]
        [InlineData("FirstName",0, 1)]
        public async Task GetFilterByNameTest(string propName, int pageSize, int pageIndex)
        {
            userController = new UserController(mockUnit.Object);
            var result = await userController.GetByFilter(propName,pageSize, pageIndex);
            Assert.IsType<BadRequestObjectResult>(result);

        }
        [Theory]
        [InlineData(null, 0, 0)]
        [InlineData("", 0, 1)]
        [InlineData("John", 0, 1)]
        public async Task GetDataBySearch(string searchString, int pageSize, int pageIndex)
        {
            userController = new UserController(mockUnit.Object);
            var result = await userController.GetBySearchString(searchString, pageSize, pageIndex);
            Assert.IsType<BadRequestObjectResult>(result);

        }
     
        #endregion
        #region notFoundTests
        PageModel<User> GetEmptyUsers(int currentPage,int pageSize)
        {
            return new PageModel<User>() {CurrentPage= currentPage,PageSize= pageSize,TotalNumberPages=2,Results=new List<User>() };
        }
        [Theory]
        [InlineData("FirstName", 10, 2)]
        [InlineData("notExist", 100, 3)]
        [InlineData("LastName", 31, 21)]
        public async Task GetFilterByNameNotFound(string propName, int pageSize, int pageIndex)
        {
            mockUnit.Setup(unit => unit.UserRepository.FilterByPredicateAsync(propName,pageSize, pageIndex)).Returns(Task.FromResult(GetEmptyUsers(pageSize, pageIndex)));
            userController = new UserController(mockUnit.Object);
            var result = await userController.GetByFilter(propName, pageSize, pageIndex);
            Assert.IsType<NotFoundObjectResult>(result);

        }
        [Theory]
        [InlineData("josh", 10, 2)]
        [InlineData("dou", 100, 3)]
        [InlineData("lol", 31, 21)]
        public async Task GetSearchNotFound(string searchQuery, int pageSize, int pageIndex)
        {
            mockUnit.Setup(unit => unit.UserRepository.SearchAsync(searchQuery, pageSize, pageIndex)).Returns(Task.FromResult(GetEmptyUsers(pageSize, pageIndex)));
            userController = new UserController(mockUnit.Object);
            var result = await userController.GetBySearchString(searchQuery, pageSize, pageIndex);
            Assert.IsType<NotFoundObjectResult>(result);

        }
        [Theory]
        [InlineData(100, 10)]
        [InlineData(10, 100)]
        [InlineData(2, 9999)]
        public async Task GetByPageTestNotFound(int pageSize, int pageIndex)
        {
            mockUnit.Setup(unit => unit.UserRepository.GetPageAsync(pageSize, pageIndex)).Returns(Task.FromResult(GetEmptyUsers(pageSize,pageIndex)));
            userController = new UserController(mockUnit.Object);
            var result = await userController.GetPage(pageSize, pageIndex);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion
        #region okResultsTests
        PageModel<User> GetUsersList(int currentPage, int pageSize)
        {
            return new PageModel<User>() { CurrentPage = currentPage, PageSize = pageSize, TotalNumberPages = 2,
                Results = new List<User>() { new User() {FirstName="Josh",LastName="Dou",Title="CTO",SoldCount=22,Refer="Koshy" } } };
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(100, 1)]
        [InlineData(11, 1)]
        public async Task GetByPageTestOk(int pageSize, int pageIndex)
        {
            mockUnit.Setup(unit => unit.UserRepository.GetPageAsync(pageSize, pageIndex)).Returns(Task.FromResult(GetUsersList(pageSize, pageIndex)));
            userController = new UserController(mockUnit.Object);
            var result = await userController.GetPage(pageSize, pageIndex);
            Assert.IsType<OkObjectResult>(result);
        }
        [Theory]
        [InlineData("josh", 10, 2)]
        [InlineData("dou", 100, 3)]
        [InlineData("lol", 31, 21)]
        public async Task GetSearchOk(string searchQuery, int pageSize, int pageIndex)
        {
            mockUnit.Setup(unit => unit.UserRepository.SearchAsync(searchQuery, pageSize, pageIndex)).Returns(Task.FromResult(GetUsersList(pageSize, pageIndex)));
            userController = new UserController(mockUnit.Object);
            var result = await userController.GetBySearchString(searchQuery, pageSize, pageIndex);
            Assert.IsType<OkObjectResult>(result);
        }
        [Theory]
        [InlineData("FirstName", 10, 2)]
        [InlineData("Id", 100, 3)]
        [InlineData("LastName", 31, 21)]
        public async Task GetFilterOk(string propName, int pageSize, int pageIndex)
        {
            mockUnit.Setup(unit => unit.UserRepository.FilterByPredicateAsync(propName, pageSize, pageIndex)).Returns(Task.FromResult(GetUsersList(pageSize, pageIndex)));
            userController = new UserController(mockUnit.Object);
            var result = await userController.GetByFilter(propName, pageSize, pageIndex);
            Assert.IsType<OkObjectResult>(result);

        }
        #endregion
    }
}
