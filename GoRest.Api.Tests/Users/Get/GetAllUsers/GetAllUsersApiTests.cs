using System.Net;
using System.Threading.Tasks;
using API_Tests.Asserts;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class GetAllUsersApiTests
    {
        [Test]
        public async Task VerifyGetAllUsersReturnsInfo()
        {
            // Arrange & Act
            var response = await GoRestClient.For<IUsersApi>().GetAll();

            // Assert
            UserAsserts.VerifyGetAllUsers(response);
        }
    }
}