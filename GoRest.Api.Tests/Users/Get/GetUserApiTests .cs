using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class GetUserApiTests
    {
        [Test]
        public async Task VerifyThatRequiredUserIsReturned()
        {
            // Arrange
            string userId = "133";

            // Arrange & Act
            var response = await GoRestClient.For<IUsersApi>().GetUser(userId);

            // Assert
            response.Code.Should().Be(HttpStatusCode.OK);
            response.Meta.Should().BeNull();;
            response.Data.Id.Should().BePositive();
            response.Data.Id.Equals(userId);
            response.Data.Name.Should().NotBeEmpty();
            //response.Data.Status.Should().; How to assert enums???
        }
    }
}