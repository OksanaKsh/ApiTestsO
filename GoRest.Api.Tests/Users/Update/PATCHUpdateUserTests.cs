using System.Threading.Tasks;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using API_Tests.Asserts;
using GoRest.Api.Client.Client.Builder;

namespace GoRest.Api.Tests.Users
{
    [Parallelizable]
    [TestFixture]
    public class PatchUpdateUserTests
    {
        [Test]
        public async Task VerifyUserInfoIsUpdated()
        {
            // Arrange
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();   

            var updateUserModel = new PatchUpdateUserBuilder().With(x =>
            {
                x.Gender = Gender.Male;
                x.Status = Status.Inactive;
            }
            ).Build();

            // Act
            var responseUpdateUser = await GoRestClient.For<IUsersApi>().UpdateUserInfo(userId, updateUserModel);

            // Assert
            UserAsserts.VerifyUserInfoIsUpdated(responseCreateUser, responseUpdateUser, updateUserModel, userId);
        }
    }
}
