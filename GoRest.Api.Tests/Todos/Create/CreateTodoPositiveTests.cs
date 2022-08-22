using API_Tests.Asserts;
using API_Tests.Helpers;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder.TodosApi;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace API_Tests.Todos.Create.CreateTodoPositiveTests
{
    [Parallelizable]
    [TestFixture]
    public class CreateTodoNegativeTests
    {
        [Test]
        public async Task VerifyCreateTodoForUser()
        {
            // Arrange
            var userId = await new CreateEntities().CreateUser();

            //Act
            var responseCreateTodo = await GoRestClient.For<ITodosApi>().CreateTodo(userId, new CreateTodoBuilder().Build());

            // Assert
            TodosAsserts.VerifyCreateTodoForUser(responseCreateTodo, userId);
        }
    }
}
