using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class PATCHUpdateUserTests
    {
        [Test]
        public async Task Verify_User_Is_Updated()
        {
            // Arrange
            string userId = "136";
            var userModel = new UpdateUserModel()
            {
                Email = new Random().Next(1111, 5555) + "@gmail.com",
                Name = Guid.NewGuid().ToString(),
                Gender = Gender.Male,
                Status = Status.Inactive
            };

            //Act
            var response = await GoRestClient.For<IUsersApi>().UpdateUserInfo(userId, userModel);

            // Assert
            response.Code.Should().Be(HttpStatusCode.OK);
            response.Meta.Should().BeNull();
            response.Data.Id.Should().BePositive();
            response.Data.Id.Equals(userId);
            response.Data.Name.Should().Be(userModel.Name);
            response.Data.Status.Should().Be(userModel.Status);
        }
    }
}
