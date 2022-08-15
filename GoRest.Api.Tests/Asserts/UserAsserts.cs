using FluentAssertions;
using GoRest.Api.Client.Client.Extentions;
using GoRest.Api.Client.Client.Models;
using System.Collections.Generic;
using System.Net;

namespace API_Tests.Asserts
{
    public static class UserAsserts
    {
        public static void VerifyUserInfoIsUpdated(GeneralResponse<GetUserResponseModel> response, UpdateUserModel userModel, string userId)
        {
            response.ShouldBeOK();
            response.Meta.Should().BeNull();
            response.Data.Id.Equals(userId);
            response.Data.Name.Should().Be(userModel.Name);
            response.Data.Status.Should().Be(userModel.Status);
        }
        public static void VerifyGetUserInfo(GeneralResponse<GetUserResponseModel> response, string userId)
        {
            response.ShouldBeOK();
            response.Meta.Should().BeNull(); ;
            response.Data.Id.Should().BePositive();
            response.Data.Id.Equals(userId);
            response.Data.Name.Should().NotBeEmpty();
            response.Data.Status.Should().BeOneOf(Status.Inactive, Status.Active);
        }
        public static void VerifyGetAllUsers(GeneralResponse<List<GetUserResponseModel>> response)
        {  
            response.ShouldBeOK();
            response.Meta.Should().NotBeNull();
            response.Data.Should().NotBeEmpty();

            response.Meta.Pagination.Should().NotBeNull();
            response.Meta.Pagination.Limit.Should().BePositive();
            response.Meta.Pagination.Page.Should().BePositive();
            response.Meta.Pagination.Pages.Should().BePositive();
            response.Meta.Pagination.Total.Should().BePositive();
        }
        public static void VerifyUserIsDeleted(GeneralResponse<GetUserResponseModel> responseGetUser)
        {
            responseGetUser.ShouldBeNotFound();
            responseGetUser.Meta.Should().BeNull(); ;
            responseGetUser.Data.Id.Should().Be(0);
            responseGetUser.Data.Email.Should().BeNull();
            responseGetUser.Data.Name.Should().BeNull();
            responseGetUser.Data.Gender.Equals(null);
            responseGetUser.Data.Status.Equals(null);
        }

        public static void VerifyTotalIncreasedAfterAddingNewUser(GeneralResponse<List<GetUserResponseModel>> responseAfterAddingUser, int initialTotal)
        {
            responseAfterAddingUser.ShouldBeOK();
            responseAfterAddingUser.Meta.Should().NotBeNull();
            responseAfterAddingUser.Data.Should().NotBeEmpty();
            responseAfterAddingUser.Meta.Pagination.Should().NotBeNull();
            responseAfterAddingUser.Meta.Pagination.Total.Should().Be(initialTotal + 1);
        }
        public static void VerifyTotalDecreasedAfterRemoveUser(GeneralResponse<List<GetUserResponseModel>> responseAfterRemoveUser, int initialTotal)
        {
            responseAfterRemoveUser.ShouldBeOK();
            responseAfterRemoveUser.Meta.Should().NotBeNull();
            responseAfterRemoveUser.Data.Should().NotBeEmpty();
            responseAfterRemoveUser.Meta.Pagination.Should().NotBeNull();
            responseAfterRemoveUser.Meta.Pagination.Total.Should().Be(initialTotal - 1);
        }

        public static void VerifyThatGetAllUsersDoNotReturnInfoForUnauthorizedUser(GeneralResponse<List<GetUserErrorResponseModel>> response)
        {
            response.ShouldBeOK();
            response.Meta.Pagination.Total.Should().Be(0);
            response.Meta.Pagination.Pages.Should().Be(0);
            response.Meta.Pagination.Page.Should().Be(1);
            response.Meta.Pagination.Limit.Should().Be(10);
            response.Data[0].Message.Should().BeEmpty();
        }
    }
}
