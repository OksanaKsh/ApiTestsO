using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using GoRest.Api.Client.Client.Models;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class CreateUserApiTestsNegative
    {

        [Test]
        public async Task VerifyErrorMessageReceivedWhenCreateAlreadyCreatedUser()
        {
            // Arrange
            var userModel = new CreateUserModel()
            {
                Email = new Random().Next(1111, 5555) + "@gmail.com",
                Gender = Gender.Female,
                Name = Guid.NewGuid().ToString(),
                Status = Status.Active
            };

            await GoRestClient.For<IUsersApi>().CreateUser(userModel);

            // Act
            var response = await GoRestClient.For<IUsersApi>().CreateUserNegative(userModel);

            // Assert
            response.Code.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Data[0].Field.Should().Be("email");
            response.Data[0].Message.Should().Be("has already been taken");
        }

        [Test]
        public async Task VerifyUserIsNotCreatedWhenInvalidToken()
        {
            // Arrange
            var userModel = new CreateUserModel()
            {
                Email = new Random().Next(1111, 5555) + "@gmail.com",
                Gender = Gender.Female,
                Name = Guid.NewGuid().ToString(),
                Status = Status.Active
            };

            // Act
            var response = await GoRestClient.ForInvalidToken<IUsersApi>().CreateUserNegativeAuth(userModel);

            // Assert
            response.Code.Should().Be(HttpStatusCode.Unauthorized);
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }

        [Test]
        public async Task VerifyUserIsNotCreatedWithoutToken()
        {
            // Arrange
            var userModel = new CreateUserModel()
            {
                Email = new Random().Next(1111, 5555) + "@gmail.com",
                Gender = Gender.Female,
                Name = Guid.NewGuid().ToString(),
                Status = Status.Active
            };

            // Act
            var response = await GoRestClient.ForWithoutToken<IUsersApi>().CreateUserNegativeAuth(userModel);

            // Assert
            response.Code.Should().Be(HttpStatusCode.Unauthorized);
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }
    }
}