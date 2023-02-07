using backend.Controllers;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace backend.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUsersService> mockUsersService;
        private readonly UsersController controller;
        private readonly User user;
        private readonly List<User> users;
        public UsersControllerTests()
        {
            mockUsersService = new Mock<IUsersService>();
            controller = new UsersController(mockUsersService.Object);
            user = new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com", BillingAddress = new Address { Landmark = "Landmark", Street = "Street", City = "City", Country = "Country" }, PhysicalAddress = new Address { Landmark = "Landmark", Street = "Street", City = "City", Country = "Country" } };
            users = new List<User> { user };
        }
      
        [Fact]
        public void GetUsers_ReturnsOkResult()
        {
            mockUsersService.Setup(service => service.GetUsers()).Returns(users);
            var result = controller.GetUsers();
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
            var returnedUsers = okResult?.Value as List<User>;
            Assert.Equal(users, returnedUsers);
        }
        [Fact]
        public void GetUser_ReturnsOkResult()
        {
            mockUsersService.Setup(service => service.GetUser(1)).Returns(user);
            var okResult = controller.GetUser(1).Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
            var returnedUser = okResult?.Value as User;
            Assert.Equal(user, returnedUser);
        }
        [Fact]
        public void GetUser_ReturnsNotFoundResult()
        {
            mockUsersService.Setup(service => service.GetUser(2)).Returns((User?)null);
            var notFoundResult = controller.GetUser(2).Result as NotFoundResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult?.StatusCode);
        }

        [Fact]
        public void CreateUser_ReturnsCreatedResult()
        {
            var createdResult = controller.CreateUser(user).Result as CreatedAtActionResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult?.StatusCode);
            var returnedUser = createdResult?.Value as User;
            Assert.Equal(user, returnedUser);
            mockUsersService.Verify(service => service.AddUser(user), Times.Once());
        }



        [Fact]
        public void UpdateUser_WithValidIdAndUser_ShouldUpdateUser()
        {
            // Arrange
            var mockUsersService = new Mock<IUsersService>();
            var user = new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@email.com" };
            mockUsersService.Setup(service => service.GetUser(1)).Returns(user);
            var controller = new UsersController(mockUsersService.Object);

            // Act
            var result = controller.UpdateUser(1, user);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            mockUsersService.Verify(service => service.UpdateUser(user), Times.Once);
        }

        [Fact]
        public void UpdateUser_WithInvalidId_ShouldReturnBadRequest()
        {
            // Arrange
            var mockUsersService = new Mock<IUsersService>();
            var user = new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@email.com" };
            var controller = new UsersController(mockUsersService.Object);

            // Act
            var result = controller.UpdateUser(2, user);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void UpdateUser_WithNonExistingUser_ShouldReturnNotFound()
        {
            // Arrange
            var mockUsersService = new Mock<IUsersService>();
            var user = new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@email.com" };
            mockUsersService.Setup(service => service.GetUser(1)).Returns((User?)null);
            var controller = new UsersController(mockUsersService.Object);

            // Act
            var result = controller.UpdateUser(1, user);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
