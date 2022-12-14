using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using GoRest.Api.Client.Client.Builder;
using GoRest.Api.Client.Client.Extentions;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class DeleteNotExistedUseNegativerTests
    {
        [Test]
        public async Task VerifyWhenDeleteNotExistedUserNotFoundStatusCodeReceived()
        {
            // Arrange
            string userId = "1000000000000";

            // Act
            var response = await GoRestClient.For<IUsersApi>().DeleteUser(userId);

            // Assert
            response.ShouldBeNotFound();
        }

        [Ignore("Bug: Received 404 instead of 401 when delete without token")]
        [Test]
        public async Task VerifyDeleteNotDoneWithoutToken()
        {
            // Arrange
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();

            // Act
            var responseDeleteUser = await GoRestClient.ForWithoutToken<IUsersApi>().DeleteUserNegativeAuth(userId);

            // Assert
            responseDeleteUser.ShouldBeUnathorized();
            responseDeleteUser.Data.Message.Should().Be("Authentication failed");
        }

        [Ignore("Bug: Received 404 instead of 401 when delete with invalid token")]
        [Test]
        public async Task VerifyDeleteNotPerformedWithInvalidToken()
        {
            // Arrange
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();

            // Act
            var responseDeleteUser = await GoRestClient.ForInvalidToken<IUsersApi>().DeleteUserNegativeAuth(userId);

            // Assert
            responseDeleteUser.ShouldBeUnathorized();
            responseDeleteUser.Data.Message.Should().Be("Authentication failed");
        }
    }
}
