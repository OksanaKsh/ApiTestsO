using System;
using System.Net;
using System.Threading.Tasks;
using API_Tests.Asserts;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using GoRest.Api.Client.Client.Models;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class GetAllUsersAfterAddingNewUserApiTests
    {
        [Test]
        public async Task VerifyGetAllUsersReturnsIncreasedTotalAfterAddingNewUser()
        {

            // Arrange
            var response = await GoRestClient.For<IUsersApi>().GetAll();
            response.Code.Should().Be(HttpStatusCode.OK);
            var initialTotal = response.Meta.Pagination.Total; 

            var userModel = new CreateUserModel()
            {
                Email = new Random().Next(4444, 9999) + "@gmail.com",
                Gender = Gender.Female,
                Name = Guid.NewGuid().ToString(),
                Status = Status.Active
            };

            // Act
            await GoRestClient.For<IUsersApi>().CreateUser(userModel);
            var responseAfterAddingUser = await GoRestClient.For<IUsersApi>().GetAll();

            // Assert
            UserAsserts.VerifyTotalIncreasedAfterAddingNewUser(responseAfterAddingUser, initialTotal);
        }
    }
}