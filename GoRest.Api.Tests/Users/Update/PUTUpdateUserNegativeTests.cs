using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using GoRest.Api.Client.Client.Builder;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class PutUpdateUserNegativeTests
    {
        [Test]
        public async Task VerifyUserIsNotUpdatedWithAlreadyPresentInDBEmail()
        {
            // Arrange
            var responseCreateFirstUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            responseCreateFirstUser.Code.Should().Be(HttpStatusCode.Created);

            var responseCreateSecondUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());      
            var userId = responseCreateSecondUser.Data.Id.ToString();
            var userModel = new PutUpdateUserBuilder().Build(responseCreateFirstUser.Data.Email) ;

            // Act
            var response = await GoRestClient.For<IUsersApi>().UpdateUserNegative(userId, userModel);

            // Assert
            response.Code.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Meta.Should().BeNull();
            response.Data[0].Field.Should().Be("email");
            response.Data[0].Message.Should().Be("has already been taken");
        }

        [Ignore("Bug: Expected 401 received 404")]
        [Test]
        public async Task VerifyUserIsNotUpdatedWhenInvalidToken()
        {
            // Arrange
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();

            // Act
            var response = await GoRestClient.For<IUsersApi>().UpdateUserNegativeAuth(userId, new PutUpdateUserBuilder().Build());

            // Assert
            response.Code.Should().Be(HttpStatusCode.Unauthorized);
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }

        [Ignore("Bug: Expected 401 received 404")]
        [Test]
        public async Task VerifyUserIsNotUpdatedWithoutToken()
        {
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();

            // Act
            var response = await GoRestClient.For<IUsersApi>().UpdateUserNegativeAuth(userId, new PutUpdateUserBuilder().Build());

            // Assert
            response.Code.Should().Be(HttpStatusCode.Unauthorized);
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }
    }
}
