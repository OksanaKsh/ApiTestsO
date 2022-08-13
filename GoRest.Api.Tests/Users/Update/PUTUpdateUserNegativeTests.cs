using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System;
using GoRest.Api.Client.Client.Models.UsersApi;
using GoRest.Api.Client.Client.Builder;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class PutUpdateUserNegativeTests
    {
        [Test]
        public async Task VerifyUserIsNotUpdatedWithAlreadyPresentInDBEmail()
        {
            // Arrange
            var responseCreateFirstUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            responseCreateFirstUser.Code.Should().Be(HttpStatusCode.Created);

            var responseCreateSecondUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());      
            var userId = responseCreateSecondUser.Data.Id.ToString();

            var userModelUpdateSecondUserWithEmilOfFirstUser = new GeneralResponseModel()
            {
                Email = responseCreateFirstUser.Data.Email,
                Gender = Gender.Female,
                Name = "Oksi",
                Status = Status.Active
            };

            //Act
            var response = await GoRestClient.For<IUsersApi>().UpdateUserNegative(userId, userModelUpdateSecondUserWithEmilOfFirstUser);

            // Assert
            response.Code.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Meta.Should().BeNull();
            response.Data[0].Field.Should().Be("email");
            response.Data[0].Message.Should().Be("has already been taken");
        }

        //[Ignore("Bug: Expected 401 received 404")]
        [Test]
        public async Task VerifyUserIsNotUpdatedWhenInvalidToken()
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

            // Act
            var response = await GoRestClient.ForInvalidToken<IUsersApi>().UpdateUserNegativeAuth(userId, userModelUpdate);

            // Assert
            response.Code.Should().Be(HttpStatusCode.Unauthorized);
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }

        [Ignore("Bug: Expected 401 received 404")]
        [Test]
        public async Task VerifyUserIsNotUpdatedWithoutToken()
        {
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();

            var userModelUpdate = new GeneralResponseModel()
            {
                Email = new Random().Next(1111, 5555) + "@gmail.com",
                Gender = Gender.Female,
                Name = Guid.NewGuid().ToString(),
                Status = Status.Active
            };

            // Act
            var response = await GoRestClient.ForWithoutToken<IUsersApi>().UpdateUserNegativeAuth(userId, userModelUpdate);

            // Assert
            response.Code.Should().Be(HttpStatusCode.Unauthorized);
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }
    }
}
