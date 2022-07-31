using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class DeleteUserTests
    {
        [Test]
        public async Task Verify_User_Is_Deleted()
        {
            // Arrange
            string userId = "137";

            // Arrange & Act
            var response = await GoRestClient.For<IUsersApi>().DeleteUser(userId);

            // Assert
            response.Code.Should().Be(HttpStatusCode.NoContent);
            response.Data.Should().BeNull();
        }

        // verify pagination
    }
}
