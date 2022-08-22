using API_Tests.Asserts;
using API_Tests.Helpers;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace API_Tests.Todos.Get.GetAllTodos
{
    [Parallelizable]
    [TestFixture]
    public class GetAllTodosNegativeTests
    {
        [Test]
        public async Task VerifyGetAllTodosReturnInfo()
        {
            // Arrange
            (string userId, string TodoId) createdTodo = await new CreateEntities().CreateTodo();

            //Act
            var response = await GoRestClient.For<ITodosApi>().GetAllTodos(createdTodo.userId);

            // Assert
            TodosAsserts.VerifyGetAllTodos(response);
        }
    }
}
