using System.Threading.Tasks;
using API_Tests.Asserts;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder;
using GoRest.Api.Client.Client.Extentions;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture]
    public class GetAllUsersAfterRemoveUserApiTests
    {
        [Test]
        public async Task VerifyGetAllUsersReturnsDecreasedTotalAfterRemoveUser()
        {
            // Arrange             
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();
            var responseGetUser = GoRestClient.For<IUsersApi>().GetAll().ShouldBeOK(); ;
            var initialTotal = responseGetUser.Meta.Pagination.Total;

            // Act
            await GoRestClient.For<IUsersApi>().DeleteUser(userId);
            var responseAfterRemoveUser = await GoRestClient.For<IUsersApi>().GetAll();

            // Assert
            UserAsserts.VerifyTotalDecreasedAfterRemoveUser(responseAfterRemoveUser, initialTotal);
        }
    }
}