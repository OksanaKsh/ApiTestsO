using API_Tests.Asserts;
using API_Tests.Helpers;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder.TodosApi;
using GoRest.Api.Client.Client.Extentions;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace API_Tests.Todos.Create.CreateTodoNegativeTests
{
    [Parallelizable]
    [TestFixture]
    public class CreateTodoNegativeTests
    {
        [Test]
        public async Task VerifyTodoIsNotCreateWhenEmptyTitleAndEmptyBody()
        {
            // Arrange
            var userId = await new CreateEntities().CreateUser();
            var TodoModelEmptyFields = new CreateTodoBuilder().With(x =>
            {
                x.Title = "";
                x.Status = "";
                x.User_Id = "";
            }
              ).Build();

            //Act
            var responseCreateTodo = await GoRestClient.For<ITodosApi>().CreateTodoNegative(TodoModelEmptyFields, userId);

            // Assert
            TodosAsserts.VerifyTodoFieldsCannotBeBlank(responseCreateTodo);
        }

        [Test]
        public async Task VerifyTodoIsNotCreateWhenFieldsExceedsAllowedLength()
        {
            // Arrange
            var userId = await new CreateEntities().CreateUser();
            var TodoFieldsExceedLengthModel = new CreateTodoBuilder().With(x =>
            {
                x.Title = "QA_201_characters_entered_negative_tests ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exer";
            }
              ).Build();

            //Act
            var responseCreateTodoFieldsExceedsAllowedLength = await GoRestClient.For<ITodosApi>().CreateTodoNegative(TodoFieldsExceedLengthModel, userId);

            // Assert
            TodosAsserts.VerifyTodoWithTitleExceedsAllowedLengthIsNotCreated(responseCreateTodoFieldsExceedsAllowedLength);
        }

        [Test]
        public async Task VerifyTodoIsNotCreateWhenStatusIsInvalid()
        {
            // Arrange
            var userId = await new CreateEntities().CreateUser();
            var TodoModelBigBody = new CreateTodoBuilder().With(x =>
            {
                x.Status = "Started";
            }
              ).Build();

            //Act
            var responseCreateTodoWithInvalidStatus = await GoRestClient.For<ITodosApi>().CreateTodoNegative(TodoModelBigBody, userId);

            // Assert
            TodosAsserts.VerifyTodoWithInvalidStatusIsNotCreated(responseCreateTodoWithInvalidStatus);
        }     


        [TestCase("0")]
        [TestCase("-1")]
        [TestCase("10000000")]
        public async Task VerifyTodoIsNotCreatedForNotExistedUser(string userId)
        {
            // Arrange & Act
            var response = await GoRestClient.For<ITodosApi>().CreateTodoNegative(new CreateTodoBuilder().Build(), userId);

            // Assert
            TodosAsserts.VerifyTodoForNotExistedUserIsNotCreated(response);
        }

        [Test]
        public async Task VerifyTodoIsNotCreatedWhenInvalidToken()
        {
            // Arrange
            var userId = await new CreateEntities().CreateUser();

            // & Act
            var response = await GoRestClient.ForInvalidToken<ITodosApi>().CreateTodoNegativeAuth(new CreateTodoBuilder().Build(), userId);

            // Assert
            response.ShouldBeUnathorized();
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }

        [Test]
        public async Task VerifyTodoIsNotCreatedWithoutToken()
        {
            var userId = await new CreateEntities().CreateUser();

            // & Act
            var response = await GoRestClient.ForWithoutToken<ITodosApi>().CreateTodoNegativeAuth(new CreateTodoBuilder().Build(), userId);

            // Assert
            response.ShouldBeUnathorized();
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }
    }
}
