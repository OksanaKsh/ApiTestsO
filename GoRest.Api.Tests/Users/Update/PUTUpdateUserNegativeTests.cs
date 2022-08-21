using System.Net;
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
    public class PutUpdateUserNegativeTests
    {
        [Test]
        public async Task VerifyUserIsNotUpdatedWithAlreadyPresentInDBEmail()
        {
            // Arrange
            var responseCreateFirstUser = GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build()).ShouldBeCreated();
            var responseCreateSecondUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());      
            var userId = responseCreateSecondUser.Data.Id.ToString();
            var userUpdateModel = new PutUpdateUserBuilder().With(x =>
            {
                x.Email = responseCreateFirstUser.Data.Email;
            }
            ).Build() ;

            // Act
            var response = await GoRestClient.For<IUsersApi>().UpdateUserNegative(userId, userUpdateModel);

            // Assert
            response.ShouldBeUnprocessableEntity();
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
            response.ShouldBeUnathorized();
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
            response.ShouldBeUnathorized();
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }
    }
}
