using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder;
using GoRest.Api.Client.Client.Extentions;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [Parallelizable]
    [TestFixture]
    public class CreateUserApiTestsPositive
    {
        [Test]
        public async Task VerifyUserIsCreated()
        {
            // Arrange & Act
            var response =  GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());

            // Assert
            response.ShouldBeCreated();
            response.Result.Data.Id.Should().BePositive();
        }
    }
}