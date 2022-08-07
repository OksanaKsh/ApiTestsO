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
    public class CreateUserApiTestsPositive
    {
        [Test]
        public async Task Verify_UserIsCreated()
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
            var response = await GoRestClient.For<IUsersApi>().CreateUser(userModel);

            // Assert
            response.Code.Should().Be(HttpStatusCode.Created);
            response.Data.Id.Should().BePositive();
        }
    }
}