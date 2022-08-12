﻿using System;
using System.Net;
using System.Threading.Tasks;
using API_Tests.Asserts;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using GoRest.Api.Client.Client.Models;
using NUnit.Framework;

namespace GoRest.Api.Tests.Users
{
    [TestFixture]
    public class GetAllUsersAfterRemoveUserApiTests
    {
        [Test]
        public async Task VerifyGetAllUsersReturnsDecreasedTotalAfterRemoveUser()
        {

            // Arrange             
            var userModel = new CreateUserModel()
            {
                Email = new Random().Next(4444, 9999) + "@gmail.com",
                Gender = Gender.Female,
                Name = Guid.NewGuid().ToString(),
                Status = Status.Active
            };

            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(userModel);
            var userId = responseCreateUser.Data.Id;

            var responseGetUser = await GoRestClient.For<IUsersApi>().GetAll();
            responseGetUser.Code.Should().Be(HttpStatusCode.OK);
            var initialTotal = responseGetUser.Meta.Pagination.Total;

            // Act
            await GoRestClient.For<IUsersApi>().DeleteUser(userId.ToString());
            var responseAfterRemoveUser = await GoRestClient.For<IUsersApi>().GetAll();

            // Assert
            UserAsserts.VerifyTotalDecreasedAfterRemoveUser(responseAfterRemoveUser, initialTotal);
        }
    }
}