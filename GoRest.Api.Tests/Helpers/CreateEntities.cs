using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder;
using GoRest.Api.Client.Client.Builder.PostsApi;
using GoRest.Api.Client.Client.Builder.TodosApi;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using System.Threading.Tasks;
namespace API_Tests.Helpers
{
    public class CreateEntities
    {
        public async Task<string> CreateUser()
        {
            var responseCreateUser = await GoRestClient.For<IUsersApi>().CreateUser(new CreateUserBuilder().Build());
            var userId = responseCreateUser.Data.Id.ToString();
            return userId;
        }

        public async Task<(string userId, string postId)> CreatePost()
        {
            var userId = await CreateUser();
            var responseCreatePost = await GoRestClient.For<IPostsApi>().CreatePost(userId, new CreatePostBuilder().Build());
            var postId = responseCreatePost.Data.Id.ToString();
            return await Task.FromResult((userId, postId));
        }

        public async Task<(string postId, string commentId)> CreateComment()
        {
            var postId = CreatePost().Result.postId;
            var responseCreateComment = await GoRestClient.For<ICommentsApi>().CreateComment(new CreateCommentBuilder().Build(), postId);
            var commentId = responseCreateComment.Data.Id.ToString();
            return await Task.FromResult((postId, commentId));
        }

        public async Task<(string userId, string postId)> CreateTodo()
        {
            var userId = await CreateUser();
            var responseCreateTodo = await GoRestClient.For<ITodosApi>().CreateTodo(userId, new CreateTodoBuilder().Build());
            var postId = responseCreateTodo.Data.Id.ToString();
            return await Task.FromResult((userId, postId));
        }
    }
}
