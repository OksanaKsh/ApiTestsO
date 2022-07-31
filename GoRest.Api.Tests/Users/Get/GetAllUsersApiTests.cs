using System.Net;
using System.Threading.Tasks;
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
        public async Task Verify_GetAllUsers_Endpoint_ReturnsInfo()
        {
            // Arrange & Act
            var client = await GoRestClient.For<IUsersApi>().GetAll();

            // Assert
            client.Code.Should().Be(HttpStatusCode.OK);
            client.Meta.Should().NotBeNull();
            client.Data.Should().NotBeEmpty();
        }

        // verify pagination

    }
}