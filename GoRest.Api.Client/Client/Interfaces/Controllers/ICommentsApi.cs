using GoRest.Api.Client.Client.Models;
using GoRest.Api.Client.Client.Models.CommentApi;
using GoRest.Api.Client.Client.Models.PostsApi;
using RestEase;

namespace GoRest.Api.Client.Client.Interfaces.Controllers
{
    [Header("Accept", "application/json")]
    [Header("Content-Type", "application/json")]
    public interface ICommentsApi : ISupportBearerAuth
    {

        #region Get Comment(s)
        [Get("posts/{postId}/comments")]
        Task<GeneralResponse<List<GetCommentResponseModel>>> GetAllComments([Path] string postId);

        [Get("posts/{postId}/comments")]
        Task<GeneralResponse<GetCommentErrorResponseModel>> GetAllCommentsNegative([Path] string postId);

        [Get("posts/{postId}/comments/{commentId}")]
        Task<GeneralResponse<GetCommentResponseModel>> GetComment([Path] string postId, [Path] string commentId);

        [Get("posts/{postId}/comments/{commentId}")]
        Task<GeneralResponse<GetCommentErrorResponseModel>> GetCommentNegative([Path] string postId, [Path] string commentId);
        #endregion

        #region Post(create) Comments
        [Post("posts/{postId}/comments")]
        Task<GeneralResponse<GetCommentResponseModel>> CreateComment([Body] CreateCommentModel postModel, [Path] string postId);

        [Post("posts /{postId}/comments")]
        Task<GeneralResponse<AuthentificationFailedModel>> CreateCommentNegativeAuth([Body] CreateCommentModel postModel, [Path] string postId);

        [Post("posts/{postId}/comments")]
        Task<GeneralResponse<List<ErrorResponseModel>>> CreateCommentNegative([Body] CreateCommentModel postModel, [Path] string postId);
        #endregion
    }
}
