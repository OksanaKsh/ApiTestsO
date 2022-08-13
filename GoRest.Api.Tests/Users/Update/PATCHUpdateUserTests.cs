using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System;
using API_Tests.Asserts;
using GoRest.Api.Client.Client.Models.UsersApi;
using GoRest.Api.Client.Client.Builder;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class PatchUpdateUserTests
    {
        [Test]
        public async Task VerifyUserInfoIsUpdated()
        {
            // Arrange
            var responseCreateUser= await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();

            var userModel = new GeneralResponseModel()
            {
                Email = new Random().Next(1111, 5555) + "@gmail.com",
                Name = Guid.NewGuid().ToString(),
                Gender = Gender.Male,
                Status = Status.Inactive
            };

            //Act
            var response = await GoRestClient.For<IUsersApi>().UpdateUserInfo(userId, userModel);

            // Assert
            UserAsserts.VerifyUserInfoIsUpdated(response, userModel, userId);
        }
    }
}
