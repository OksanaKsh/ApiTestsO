using System;
using System.Net;
using System.Threading.Tasks;
using API_Tests.Asserts;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder;
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
            await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());

            // Act
            var responseAfterAddingUser = await GoRestClient.For<IUsersApi>().GetAll();

            // Assert
            UserAsserts.VerifyTotalIncreasedAfterAddingNewUser(responseAfterAddingUser, initialTotal);
        }
    }
}