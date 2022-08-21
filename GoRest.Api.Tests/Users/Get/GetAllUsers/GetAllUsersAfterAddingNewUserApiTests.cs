using System.Threading.Tasks;
using API_Tests.Asserts;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder;
using GoRest.Api.Client.Client.Extentions;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [Parallelizable]
    [TestFixture]
    public class GetAllUsersAfterAddingNewUserApiTests
    {
        [Test]
        public async Task VerifyGetAllUsersReturnsIncreasedTotalAfterAddingNewUser()
        {

            // Arrange
            var response = GoRestClient.For<IUsersApi>().GetAll().ShouldBeOK();
            var initialTotal = response.Meta.Pagination.Total;
            await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());

            // Act
            var responseAfterAddingUser = await GoRestClient.For<IUsersApi>().GetAll();

            // Assert
            UserAsserts.VerifyTotalIncreasedAfterAddingNewUser(responseAfterAddingUser, initialTotal);
        }
    }
}