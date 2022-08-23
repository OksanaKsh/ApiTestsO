using System.Threading.Tasks;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using API_Tests.Asserts;
using GoRest.Api.Client.Client.Builder;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class PutUpdateUserPositiveTests
    {
        [Test]
        public async Task VerifyUserIsUpdated()
        {
            // Arrange
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();
            var userModelUpdate = new PutUpdateUserBuilder().Build();
            
            //Act
            var responseUpdateUser = await GoRestClient.For<IUsersApi>().UpdateUser(userId, userModelUpdate);

            // Assert
            UserAsserts.VerifyUserIsUpdated(responseUpdateUser, userModelUpdate, userId);
        }
    }
}
