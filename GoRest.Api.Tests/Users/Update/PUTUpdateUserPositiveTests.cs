using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System;
using GoRest.Api.Client.Client.Models.UsersApi;
using API_Tests.Asserts;
using GoRest.Api.Client.Client.Builder;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class PutUpdateUserPositiveTests
    {
        [Test]
        public async Task VerifyUserIsUpdated()
        {
            // Arrange
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();

            var userModelUpdate = new GeneralResponseModel()
            {
                Email = new Random().Next(1111, 5555) + "@gmail.com",
                Gender = Gender.Female,
                Name = Guid.NewGuid().ToString(),
                Status = Status.Active
            };

            //Act
            var response = await GoRestClient.For<IUsersApi>().UpdateUser(userId, userModelUpdate);

            // Assert
            UserAsserts.VerifyUserInfoIsUpdated(response, userModelUpdate, userId);
        }
    }
}
