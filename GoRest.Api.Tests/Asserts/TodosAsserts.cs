using FluentAssertions;
using GoRest.Api.Client.Client.Extentions;
using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Models.PostsApi;
using GoRest.Api.Client.Client.Models.TodoApi;
using System.Collections.Generic;

namespace API_Tests.Asserts
{
    public static class TodosAsserts
    {
        public static void VerifyGetAllTodos(GeneralResponse<List<GetTodoResponseModel>> response)
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

        public static void VerifyTodosAreNotReturnedForInvalidUser(GeneralResponse<List<ErrorResponseModel>> response)
        {
            response.ShouldBeOK();
            response.Meta.Should().NotBeNull();
            response.Data.Should().BeEmpty();
            response.Meta.Pagination.Should().NotBeNull();
            response.Meta.Pagination.Limit.Should().Be(10);
            response.Meta.Pagination.Page.Should().Be(1);
            response.Meta.Pagination.Pages.Should().Be(0);
            response.Meta.Pagination.Total.Should().Be(0);
        }

        public static void VerifyGetTodoInfo(GeneralResponse<GetTodoResponseModel> response, string userId, string TodoId)
        {
            response.ShouldBeOK();
            response.Meta.Should().BeNull();
            response.Data.Id.Equals(userId);
            response.Data.Title.Should().NotBeEmpty();
            response.Data.Status.Should().BeOneOf(TodoStatus.Pending, TodoStatus.Completed);
        }

        public static void VerifyCreateTodoForUser(GeneralResponse<GetTodoResponseModel> response, string userId)
        {
            response.ShouldBeCreated();
            response.Meta.Should().BeNull();
            response.Data.Id.Should().NotBeEmpty();
            response.Data.User_Id.Equals(userId);
            response.Data.Title.Should().NotBeEmpty();
            response.Data.Status.Should().BeOneOf(TodoStatus.Pending, TodoStatus.Completed);
        }

        public static void VerifyTodoFieldsCannotBeBlank(GeneralResponse<List<ErrorResponseModel>> response)
        {
            response.ShouldBeUnprocessableEntity();
            response.Meta.Should().BeNull();
            response.Data[0].Field.Should().Be("title");
            response.Data[0].Message.Should().Be("can't be blank");
            response.Data[1].Field.Should().Be("status");
            response.Data[1].Message.Should().Be("can't be blank, can be pending or completed");
        }

        public static void VerifyTodoWithTitleExceedsAllowedLengthIsNotCreated(GeneralResponse<List<ErrorResponseModel>> responseCreateTodoWithBigTitle)
        {
            responseCreateTodoWithBigTitle.ShouldBeUnprocessableEntity();
            responseCreateTodoWithBigTitle.Meta.Should().BeNull();
            responseCreateTodoWithBigTitle.Data[0].Field.Should().Be("title");
            responseCreateTodoWithBigTitle.Data[0].Message.Should().Be("is too long (maximum is 200 characters)");
        }

        public static void VerifyTodoWithInvalidStatusIsNotCreated(GeneralResponse<List<ErrorResponseModel>> responseCreateTodoWithBigBody)
        {
            responseCreateTodoWithBigBody.ShouldBeUnprocessableEntity();
            responseCreateTodoWithBigBody.Meta.Should().BeNull();
            responseCreateTodoWithBigBody.Data[0].Field.Should().Be("status");
            responseCreateTodoWithBigBody.Data[0].Message.Should().Be("can't be blank, can be pending or completed");
        }

        public static void VerifyTodoForNotExistedUserIsNotCreated(GeneralResponse<List<ErrorResponseModel>> response)
        {
            response.ShouldBeUnprocessableEntity();
            response.Meta.Should().BeNull();
            response.Data[0].Field.Should().Be("user");
            response.Data[0].Message.Should().Be("must exist");
        }
    }
}
