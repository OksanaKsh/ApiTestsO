using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class GetAllUsersApiTests
    {
        [Test]
        public async Task Verify_GetAllUsers_Endpoint_ReturnsInfo()
        {
            // Arrange & Act
            var response = await GoRestClient.For<IUsersApi>().GetAll();

            // Assert
            response.Code.Should().Be(HttpStatusCode.OK);
            response.Meta.Should().NotBeNull();
            response.Data.Should().NotBeEmpty();

            response.Meta.Pagination.Should().NotBeNull();    
            response.Meta.Pagination.Limit.Should().BePositive();    
            response.Meta.Pagination.Page.Should().BePositive();    
            response.Meta.Pagination.Pages.Should().BePositive();
            response.Meta.Pagination.Total.Should().BePositive();
            // Can be better assert than be.positive?

        }
    }
}