using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using GoRest.Api.Client.Client.Builder;
using GoRest.Api.Client.Client.Extentions;

namespace GoRest.Api.Tests.Users
{
    [Parallelizable]
    [TestFixture]
    public class GetUserWithInvalidIdTests
    {
        [TestCase(0)]
        [TestCase(-1)]
        public async Task VerifyThatRequiredUserIsNotReturned(int userId)
        {
            // Arrange & Act
            var response = await GoRestClient.For<IUsersApi>().GetUserNegative(userId.ToString());

            // Assert
            response.ShouldBeNotFound();
            response.Meta.Should().BeNull(); ;
            response.Data.Message.Should().Be("Resource not found");
        }

        [Test]
        public async Task VerifyGetUserNotReturnsInfoWhenInvalidToken()
        {
            // Arrange
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build()); ;
            var userId = responseCreateUser.Data.Id.ToString();

            // Act
            var response = await GoRestClient.ForInvalidToken<IUsersApi>().GetUserNegative(userId);

            // Assert
            response.ShouldBeNotFound();
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Resource not found");
        }
        
        [Test]
        public async Task VerifyUserNotReturnsInfoWhenWithoutToken()
        {
            // Arrange
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();

            // Act
            var response = await GoRestClient.ForWithoutToken<IUsersApi>().GetUserNegative(userId);

            // Assert
            response.ShouldBeNotFound();
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Resource not found");
        }
    }
}