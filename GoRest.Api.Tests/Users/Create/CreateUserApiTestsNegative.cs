using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder;
using GoRest.Api.Client.Client.Extentions;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class CreateUserApiTestsNegative
    {

        [Test]
        public async Task VerifyErrorMessageReceivedWhenCreateAlreadyCreatedUser()
        {
            // Arrange
            var createdUser = new CreateUserBuilder().Build();
            await GoRestClient.For<IUsersApi>().CreateUser(createdUser);

            // Act
            var response = await GoRestClient.For<IUsersApi>().CreateUserNegative(createdUser);

            // Assert
            response.ShouldBeUnprocessableEntity();
            response.Data[0].Field.Should().Be("email");
            response.Data[0].Message.Should().Be("has already been taken");
        }

        [Test]
        public async Task VerifyUserIsNotCreatedWhenInvalidToken()
        {
            // Arrange & Act
            var response = await GoRestClient.ForInvalidToken<IUsersApi>().CreateUserNegativeAuth(new CreateUserBuilder().Build());

            // Assert
            response.ShouldBeUnathorized();
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }

        [Test]
        public async Task VerifyUserIsNotCreatedWithoutToken()
        {
            // Arrange & Act
            var response = await GoRestClient.ForWithoutToken<IUsersApi>().CreateUserNegativeAuth(new CreateUserBuilder().Build());

            // Assert
            response.ShouldBeUnathorized();
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }
    }
}