using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Models.PostsApi;
using RestEase;

namespace GoRest.Api.Client.Client.Interfaces.Controllers
{
    [Header("Accept", "application/json")]
    [Header("Content-Type", "application/json")]
    public interface IPostsApi : ISupportBearerAuth
    {

        #region Get Post(s)
        [Get("users/{userId}/posts/")]
        Task<GeneralResponse<List<GetPostResponseModel>>> GetAllPosts([Path] string userId);

        [Get("users/{userId}/posts/")]
        Task<GeneralResponse<GetPostErrorResponseModel>> GetAllPOstsNegative([Path] string userId);

        [Get("users/{userId}/posts/{postId}")]
        Task<GeneralResponse<GetPostResponseModel>> GetPost([Path] string userId, [Path] string postId);

        [Get("users/{userId}/posts/{postId}")]
        Task<GeneralResponse<GetPostErrorResponseModel>> GetPostNegative([Path] string userId, [Path] string postId);
        #endregion

        #region Post Post
        [Post("users/{userId}/posts/")]
        Task<GeneralResponse<GetPostResponseModel>> CreatePost([Path] string userId, [Body] CreatePostModel postModel);

        [Post("users/{userId}/posts/")]
        Task<GeneralResponse<AuthentificationFailedModel>> CreatePostNegativeAuth([Body] CreatePostModel postModel, [Path] string userId);

        [Post("users/{userId}/posts/")]
        Task<GeneralResponse<List<CreatePostErrorResponseModel>>> CreatePostNegative([Body] CreatePostModel postModel, [Path] string userId);
        #endregion
    }
}
