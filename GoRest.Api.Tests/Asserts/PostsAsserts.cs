using FluentAssertions;
using GoRest.Api.Client.Client.Extentions;
using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Models.PostsApi;
using System.Collections.Generic;
namespace API_Tests.Asserts
{
    public static class PostsAsserts
    {
        public static void VerifyGetAllPosts(GeneralResponse<List<GetPostResponseModel>> response)
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

        public static void VerifyGetPostInfo(GeneralResponse<GetPostResponseModel> response, string userId, string postId)
        {
            response.ShouldBeOK();
            response.Meta.Should().BeNull();
            response.Data.Id.Equals(userId);
            response.Data.Title.Should().NotBeEmpty();
            response.Data.Body.Should().NotBeEmpty();
        }

        public static void VerifyCreatePostForUser(GeneralResponse<GetPostResponseModel> response, string userId)
        {
            response.ShouldBeCreated();
            response.Meta.Should().BeNull();
            response.Data.Id.Should().NotBeEmpty();
            response.Data.User_Id.Equals(userId);
            response.Data.Title.Should().NotBeEmpty();
            response.Data.Body.Should().NotBeEmpty();
        }

        public static void VerifyPostFieldsCannotBeBlank(GeneralResponse<List<ErrorResponseModel>> response)
        {
            response.ShouldBeUnprocessableEntity();
            response.Meta.Should().BeNull();
            response.Data[0].Field.Should().Be("title");
            response.Data[0].Message.Should().Be("can't be blank");
            response.Data[1].Field.Should().Be("body");
            response.Data[1].Message.Should().Be("can't be blank");
        }

        public static void VerifyPostWithBigTitleIsNotCreated(GeneralResponse<List<ErrorResponseModel>> responseCreatePostWithBigTitle)
        {
            responseCreatePostWithBigTitle.ShouldBeUnprocessableEntity();
            responseCreatePostWithBigTitle.Meta.Should().BeNull();
            responseCreatePostWithBigTitle.Data[0].Field.Should().Be("title");
            responseCreatePostWithBigTitle.Data[0].Message.Should().Be("is too long (maximum is 200 characters)");
        }

        public static void VerifyPostWithBigBodyIsNotCreated(GeneralResponse<List<ErrorResponseModel>> responseCreatePostWithBigBody)
        {
            responseCreatePostWithBigBody.ShouldBeUnprocessableEntity();
            responseCreatePostWithBigBody.Meta.Should().BeNull();
            responseCreatePostWithBigBody.Data[0].Field.Should().Be("body");
            responseCreatePostWithBigBody.Data[0].Message.Should().Be("is too long (maximum is 500 characters)");
        } 
        
        public static void VerifyPostForNotExistedUserIsNotCreated(GeneralResponse<List<ErrorResponseModel>> response)
        {
            response.ShouldBeUnprocessableEntity();
            response.Meta.Should().BeNull();
            response.Data[0].Field.Should().Be("user");
            response.Data[0].Message.Should().Be("must exist");
        }
    }
}
