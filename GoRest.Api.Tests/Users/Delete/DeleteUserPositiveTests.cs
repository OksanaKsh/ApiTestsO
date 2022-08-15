using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using API_Tests.Asserts;
using GoRest.Api.Client.Client.Builder;
using GoRest.Api.Client.Client.Extentions;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class DeleteUserPositiveTests
    {
        [Test]
        public async Task VerifyUserIsDeleted()
        {
            // Arrange
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();

            // Act
            var responseDeleteUser = await GoRestClient.For<IUsersApi>().DeleteUser(userId);

            // Assert
            responseDeleteUser.ShouldBeNoContent();
            responseDeleteUser.Data.Should().BeNull();

            var responseGetUser = await GoRestClient.For<IUsersApi>().GetUser(userId);
            UserAsserts.VerifyUserIsDeleted(responseGetUser);
        }
    }
}
