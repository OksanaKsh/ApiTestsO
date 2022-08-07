using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using GoRest.Api.Client.Client.Models;
using System;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class GetUserWithInvalidIdTests
    {
        [TestCase(0)]
        [TestCase(-1)]
        [Test]
        public async Task VerifyThatRequiredUserIsReturned(int userId)
        {
            // Arrange

            // Arrange & Act
            var response = await GoRestClient.For<IUsersApi>().GetUserNegative(userId.ToString());

            // Assert
            response.Code.Should().Be(HttpStatusCode.NotFound);
            response.Meta.Should().BeNull(); ;
            response.Data.Message.Should().Be("Resource not found");
        }

        [Test]
        public async Task VerifyGetUserNotReturnsInfoWhenInvalidToken()
        {
            // Arrange
            var userModel = new CreateUserModel()
            {
                Email = new Random().Next(1111, 5555) + "@gmail.com",
                Gender = Gender.Female,
                Name = Guid.NewGuid().ToString(),
                Status = Status.Active
            };

            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(userModel);
            var userId = responseCreateUser.Data.Id.ToString();

            // Act
            var response = await GoRestClient.ForInvalidToken<IUsersApi>().GetUserNegative(userId);

            // Assert
            response.Code.Should().Be(HttpStatusCode.NotFound);
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Resource not found");
        }
        
        [Test]
        public async Task VerifyUserNotReturnsInfoWhenWithoutToken()
        {
            // Arrange
            var userModel = new CreateUserModel()
            {
                Email = new Random().Next(1111, 5555) + "@gmail.com",
                Gender = Gender.Female,
                Name = Guid.NewGuid().ToString(),
                Status = Status.Active
            };

            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(userModel);
            var userId = responseCreateUser.Data.Id.ToString();

            // Act
            var response = await GoRestClient.ForWithoutToken<IUsersApi>().GetUserNegative(userId);

            // Assert
            response.Code.Should().Be(HttpStatusCode.NotFound);
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Resource not found");
        }
    }
}