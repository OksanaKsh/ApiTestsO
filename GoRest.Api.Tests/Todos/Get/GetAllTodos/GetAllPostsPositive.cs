using API_Tests.Asserts;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;
namespace API_Tests.Todos.Get.GetAllTodos
{
    [TestFixture]
    public class GetAllTodosPositiveTests
    {
        [TestCase("0")]
        [TestCase("-1")]
        [TestCase("10000000")]
        public async Task VerifyTodosAreNotReturnedForInvalidUser(string userId)
        {
            // Arrange & Act
            var response = await GoRestClient.For<ITodosApi>().GetAllTodosNegative(userId);

            // Assert
            TodosAsserts.VerifyTodosAreNotReturnedForInvalidUser(response);
        }
    }
}

