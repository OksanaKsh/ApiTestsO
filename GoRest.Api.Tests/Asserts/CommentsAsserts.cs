
using FluentAssertions;
using GoRest.Api.Client.Client.Extentions;
using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Models.PostsApi;
using System.Collections.Generic;

namespace API_Tests.Asserts
{
    public class CommentsAsserts
    {
        public static void VerifyCommentFieldsCannotBeBlank(GeneralResponse<List<ErrorResponseModel>> response)
        {
            response.ShouldBeUnprocessableEntity();
            response.Meta.Should().BeNull();
            response.Data[0].Field.Should().Be("name");
            response.Data[0].Message.Should().Be("can't be blank");
            response.Data[1].Field.Should().Be("email");
            response.Data[1].Message.Should().Be("can't be blank, is invalid");
            response.Data[2].Field.Should().Be("body");
            response.Data[2].Message.Should().Be("can't be blank");
        }

        public static void VerifyCommentWithFieldsExceedLenghNotCreated(GeneralResponse<List<ErrorResponseModel>> response)
        {
            response.ShouldBeUnprocessableEntity();
            response.Meta.Should().BeNull();
            response.Data[0].Field.Should().Be("name");
            response.Data[0].Message.Should().Be("is too long (maximum is 200 characters)");
            response.Data[1].Field.Should().Be("email");
            response.Data[1].Message.Should().Be("is too long (maximum is 200 characters), is invalid");
            response.Data[2].Field.Should().Be("body");
            response.Data[2].Message.Should().Be("is too long (maximum is 500 characters)");
        }

        public static void VerifyCommentForNotExistedPostIsNotCreated(GeneralResponse<List<ErrorResponseModel>> response)
        {
            response.ShouldBeUnprocessableEntity();
            response.Meta.Should().BeNull();
            response.Data[0].Field.Should().Be("post");
            response.Data[0].Message.Should().Be("must exist");
        }
        public static void VerifyCommentWitInvalidEmailIsNotCreated(GeneralResponse<List<ErrorResponseModel>> response)
        {
            response.ShouldBeUnprocessableEntity();
            response.Meta.Should().BeNull();
            response.Data[0].Field.Should().Be("email");
            response.Data[0].Message.Should().Be("is invalid");
        }
        public static void VerifyCreateCommentForPost(GeneralResponse<GetCommentResponseModel> response, string postId)
        {
            response.ShouldBeCreated();
            response.Meta.Should().BeNull();
            response.Data.Id.Should().NotBeEmpty();
            response.Data.Post_Id.Equals(postId);
            response.Data.Name.Should().NotBeEmpty();
            response.Data.Email.Should().NotBeEmpty();
            response.Data.Body.Should().NotBeEmpty();
        }

        public static void VerifyGetCommentInfo(GeneralResponse<GetCommentResponseModel> response, string postId, string commentId)
        {
            response.ShouldBeOK();
            response.Meta.Should().BeNull();
            response.Data.Id.Equals(commentId);
            response.Data.Post_Id.Equals(postId);
            response.Data.Name.Should().NotBeEmpty();
            response.Data.Email.Should().NotBeEmpty();
            response.Data.Body.Should().NotBeEmpty();
        }
        public static void VerifyGetAllComments(GeneralResponse<List<GetCommentResponseModel>> response, string postId)
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
    }
}

