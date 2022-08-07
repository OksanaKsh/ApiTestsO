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

            // Arrange & Act
            var response = await GoRestClient.For<IUsersApi>().GetUser(userId);

            // Assert
            response.Code.Should().Be(HttpStatusCode.OK);
            response.Meta.Should().BeNull();;
            response.Data.Id.Should().BePositive();
            response.Data.Id.Equals(userId);
            response.Data.Name.Should().NotBeEmpty();
            response.Data.Status.Should().BeOneOf(Status.Inactive,Status.Active); 
        }
    }
}