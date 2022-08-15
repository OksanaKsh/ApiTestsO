using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder;
using GoRest.Api.Client.Client.Extentions;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using GoRest.Api.Client.Client.Models;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class CreateUserApiTestsPositive
    {
        [Test]
        public async Task Verify_UserIsCreated()
        {
            // Arrange & Act
            var response = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());

            // Assert
            response.ShouldBeCreated();
            response.Data.Id.Should().BePositive();
        }
    }
}