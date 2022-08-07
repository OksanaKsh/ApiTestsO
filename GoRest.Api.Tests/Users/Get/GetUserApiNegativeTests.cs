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
    public class GetUserWithInvalidIdTests
    {
        [TestCase(0)]
        [TestCase(-1)]
        [Test]
        public async Task VerifyThatRequiredUserIsReturned(int userId)
        {
            // Arrange

            // Arrange & Act
            var response = await GoRestClient.For<IUsersApi>().GetUserNegative(userId.ToString());

            // Assert
            response.Code.Should().Be(HttpStatusCode.NotFound);
            response.Meta.Should().BeNull();;
            response.Data.Message.Should().Be("Resource not found");
        }
    }
}