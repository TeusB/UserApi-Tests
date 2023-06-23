// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using System.Text;
// using System.Threading.Tasks;
// using AutoMapper;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.ModelBinding;
// using Moq;
// using Newtonsoft.Json;
// using NUnit.Framework;
// using user.Controllers;
// using user.Data;
// using user.DTO;
// using user.Models;

// namespace user.Tests
// {
//     [TestFixture]
//     public class UserControllerTests
//     {
//         private UserController _controller;
//         private Mock<IUserRepository> _userRepositoryMock;
//         private Mock<IMapper> _mapperMock;

//         [SetUp]
//         public void Setup()
//         {
//             _userRepositoryMock = new Mock<IUserRepository>();
//             _mapperMock = new Mock<IMapper>();
//             _controller = new UserController(_userRepositoryMock.Object, _mapperMock.Object);
//         }

//         [Test]
//         public async Task GetAll_UserList_ReturnsOkResult()
//         {
//             // Arrange
//             var userList = new List<User>()
//             {
//              new User
//              {
//                  Id = 1,
//                  Guid = Guid.NewGuid(),
//                  Email = "john@example.com",
//                  FirstName = "John",
//                  LastName = "Doe",
//                  PhoneNumber = "1234567890",
//                  UserName = "johndoe",
//                  _created = DateTime.Now,
//                  _updated = DateTime.Now
//              },
//             new User
//             {
//                 Id = 2,
//                 Guid = Guid.NewGuid(),
//                 Email = "jane@example.com",
//                 FirstName = "Jane",
//                 LastName = "Smith",
//                 PhoneNumber = "0987654321",
//                 UserName = "janesmith",
//                 _created = DateTime.Now,
//                 _updated = DateTime.Now
//             }};
//             _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(userList);

//             // Act
//             var result = await _controller.GetAll();

//             // Assert
//             Assert.IsInstanceOf<OkObjectResult>(result);
//             var okResult = result as OkObjectResult;
//             Assert.AreEqual(userList, okResult!.Value);
//         }

//         [Test]
//         public async Task AddUser_WithValidData_ReturnsOkResult()
//         {
//             // Arrange
//             var insertUser = new InsertUser
//             {
//                 Email = "heuy",
//                 // FirstName = "John",
//                 LastName = "Doe",
//                 PhoneNumber = "1234567890",
//                 UserName = "johndoe"
//             };

//             // Create a new HttpContext with a request
//             var httpContext = new DefaultHttpContext();
//             httpContext.Request.Method = "POST";
//             httpContext.Request.Headers["Content-Type"] = "application/json";
//             httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(insertUser)));
            
//             // Assign the HttpContext to the controller
//             _controller.ControllerContext.HttpContext = httpContext;

//             // Act
//             var result = await _controller.AddUser(insertUser);

//             // Assert
//             Assert.NotNull(result);
//             Assert.IsInstanceOf<OkResult>(result);
//             Assert.AreEqual(200, (result as OkResult)?.StatusCode);
//         }



//         // [Test]
//         public async Task AddUser_WithInValidData_ReturnsBadRequest()
//         {
//             // Arrange
//             var insertUser = new InsertUser
//             {
//                 FirstName = "John",
//                 LastName = "Doe",
//                 PhoneNumber = "1234567890",
//                 UserName = "johndoe"
//             };

//             // Act
//             var result = await _controller.AddUser(insertUser);

//             // Assert
//             Assert.AreEqual(500, (result as ObjectResult)?.StatusCode);
//         }

//         // Add more tests for other methods (e.g., GetById, UpdateUser, DeleteUser)
//     }
// }
