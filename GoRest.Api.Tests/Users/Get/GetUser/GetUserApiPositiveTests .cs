using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using GoRest.Api.Client.Client.Models;
using System;
using API_Tests.Asserts;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class GetUserApiPositiveTests
    {
        [Test]
        public async Task VerifyThatRequiredUserIsReturned()
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
            var response = await GoRestClient.For<IUsersApi>().GetUser(userId);

            // Assert
            UserAsserts.VerifyGetUserInfo(response, userId);
        }
    }
}