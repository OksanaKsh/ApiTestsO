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
    public class DeleteUserPositiveTests
    {
        [Test]
        public async Task VerifyUserIsDeleted()
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
            var responseDeleteUser = await GoRestClient.For<IUsersApi>().DeleteUser(userId);

            // Assert
            responseDeleteUser.Code.Should().Be(HttpStatusCode.NoContent);
            responseDeleteUser.Data.Should().BeNull();

            var responseGetUser = await GoRestClient.For<IUsersApi>().GetUser(userId);
            responseGetUser.Code.Should().Be(HttpStatusCode.NotFound);
            responseGetUser.Meta.Should().BeNull(); ;
            responseGetUser.Data.Id.Should().Be(0);
            responseGetUser.Data.Email.Should().BeNull();
            responseGetUser.Data.Name.Should().BeNull();
            responseGetUser.Data.Gender.Equals(null);
            responseGetUser.Data.Status.Equals(null);
        }
    }
}
