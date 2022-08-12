using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class GetAllUsersApiNegativeTests
    {
        [Test]
        public async Task VerifyGetAllUsersNotReturnsInfoWhenInvalidToken()
        {
            // Arrange & Act
            var response = await GoRestClient.ForInvalidToken<IUsersApi>().GetAllNegative();

            // Assert
            response.Code.Should().Be(HttpStatusCode.OK);
            response.Meta.Pagination.Total.Should().Be(0);
            response.Meta.Pagination.Pages.Should().Be(0);
            response.Meta.Pagination.Page.Should().Be(1);
            response.Meta.Pagination.Limit.Should().Be(10);
            response.Data.Message.Should().BeEmpty();
        }
        
        [Test]
        public async Task VerifyGetAllUsersNotReturnsInfoWhenWithoutToken()
        {
            // Arrange & Act
            var response = await GoRestClient.ForWithoutToken<IUsersApi>().GetAllNegative();

            // Assert
            response.Code.Should().Be(HttpStatusCode.OK);
            response.Meta.Pagination.Total.Should().Be(0);
            response.Meta.Pagination.Pages.Should().Be(0);
            response.Meta.Pagination.Page.Should().Be(1);
            response.Meta.Pagination.Limit.Should().Be(10);
            response.Data.Message.Should().BeEmpty();
        }
    }
}