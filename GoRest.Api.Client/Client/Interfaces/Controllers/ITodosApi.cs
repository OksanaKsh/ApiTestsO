using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Models.PostsApi;
using RestEase;

namespace GoRest.Api.Client.Client.Interfaces.Controllers
{
    [Header("Accept", "application/json")]
    [Header("Content-Type", "application/json")]
    public interface ITodosApi : ISupportBearerAuth
    {

        #region Get Todo(s)
        [Get("users/{userId}/todos/")]
        Task<GeneralResponse<List<GetTodoResponseModel>>> GetAllTodos([Path] string userId);

        [Get("users/{userId}/todos/")]
        Task<GeneralResponse<ErrorResponseModel>> GetAllTodosNegative([Path] string userId);

        #endregion

        #region Todo Todo
        [Post("users/{userId}/todos/")]
        Task<GeneralResponse<GetTodoResponseModel>> CreateTodo([Path] string userId, [Body] CreateTodoModel TodoModel);

        [Post("users/{userId}/todos/")]
        Task<GeneralResponse<AuthentificationFailedModel>> CreateTodoNegativeAuth([Body] CreateTodoModel TodoModel, [Path] string userId);

        [Post("users/{userId}/todos/")]
        Task<GeneralResponse<List<ErrorResponseModel>>> CreateTodoNegative([Body] CreateTodoModel TodoModel, [Path] string userId);
        #endregion
    }
}
