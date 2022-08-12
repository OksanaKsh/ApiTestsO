using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class DeleteNotExistedUseNegativerTests
    {
        [Test]
        public async Task VerifyWhenDeleteNotExistedUserNotFoundStatusCodeReceived()
        {
            // Arrange
            string userId = "1000000000000";

            // Act
            var response = await GoRestClient.For<IUsersApi>().DeleteUser(userId);

            // Assert
            response.Code.Should().Be(HttpStatusCode.NotFound);
        }
        [Ignore("Bug: Received 404 instead of 401 when delete without token")]
        [Test]
        public async Task VerifyDeleteNotDoneWithoutToken()
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
            var responseDeleteUser = await GoRestClient.ForWithoutToken<IUsersApi>().DeleteUserNegativeAuth(userId);

            // Assert
            responseDeleteUser.Code.Should().Be(HttpStatusCode.Unauthorized);
            responseDeleteUser.Data.Message.Should().Be("Authentication failed");

            var responseGetUser = await GoRestClient.For<IUsersApi>().GetUser(userId);
            responseGetUser.Code.Should().Be(HttpStatusCode.OK);
        }

        [Ignore("Bug: Received 404 instead of 401 when delete with invalid token")]
        [Test]
        public async Task VerifyDeleteNotPerformedWithInvalidToken()
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
            var responseDeleteUser = await GoRestClient.ForInvalidToken<IUsersApi>().DeleteUserNegativeAuth(userId);

            // Assert
            responseDeleteUser.Code.Should().Be(HttpStatusCode.Unauthorized);
            responseDeleteUser.Data.Message.Should().Be("Authentication failed");

            var responseGetUser = await GoRestClient.For<IUsersApi>().GetUser(userId);
            responseGetUser.Code.Should().Be(HttpStatusCode.OK);
        }
    }
}
