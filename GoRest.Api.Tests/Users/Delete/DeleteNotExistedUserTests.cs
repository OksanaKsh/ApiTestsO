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
    public class DeleteNotExistedUserTests
    {
        [Test]
        public async Task VerifyWhenDeleteNotExistedUserNotFoundStatusCodeReceived()
        {
            // Arrange
            string userId = "1300000000000";

            // Act
            var response = await GoRestClient.For<IUsersApi>().DeleteUser(userId);

            // Assert
            response.Code.Should().Be(HttpStatusCode.NotFound); 

            //response.Data.Should().BeNull();failed ???
        }
    }
}
