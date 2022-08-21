using System.Threading.Tasks;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using API_Tests.Asserts;
using GoRest.Api.Client.Client.Builder;

namespace GoRest.Api.Tests.Users
{
    [Parallelizable]
    [TestFixture]
    public class GetUserApiPositiveTests
    {
        [Test]
        public async Task VerifyThatRequiredUserIsReturned()
        {
            // Arrange
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();

            // Act
            var response = await GoRestClient.For<IUsersApi>().GetUser(userId);

            // Assert
            UserAsserts.VerifyGetUserInfo(response, userId);
        }
    }
}