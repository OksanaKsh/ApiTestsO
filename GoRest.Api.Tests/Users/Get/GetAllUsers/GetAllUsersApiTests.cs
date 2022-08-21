using System.Threading.Tasks;
using API_Tests.Asserts;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [Parallelizable]
    [TestFixture]
    public class GetAllUsersApiTests
    {
        [Test]
        public async Task VerifyGetAllUsersReturnsInfo()
        {
            // Arrange & Act
            var response = await GoRestClient.For<IUsersApi>().GetAll();

            // Assert
            UserAsserts.VerifyGetAllUsers(response);
        }

        [Test]
        public async Task VerifyGetAllUsersReturnsInfoWhenInvalidToken()
        {
            // Arrange & Act
            var response = await GoRestClient.ForInvalidToken<IUsersApi>().GetAll();

            // Assert
            UserAsserts.VerifyGetAllUsers(response);
        }

        [Test]
        public async Task VerifyGetAllUsersReturnsInfoWhenWithoutToken()
        {
            // Arrange & Act
            var response = await GoRestClient.ForWithoutToken<IUsersApi>().GetAll();

            // Assert
            UserAsserts.VerifyGetAllUsers(response);
        }
    }
}