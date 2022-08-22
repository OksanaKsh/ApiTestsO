using GoRest.Api.Client.Client.Models;
using RestEase;

namespace GoRest.Api.Client.Client.Interfaces.Controllers
{
    [Header("Accept", "application/json")]
    [Header("Content-Type", "application/json")]
    public interface IUsersApi : ISupportBearerAuth
    {
        #region Get User(s)
        [Get("users/")]
        Task<GeneralResponse<List<GetUserResponseModel>>> GetAll();

        [Get("users/")]
        Task<GeneralResponse<GetUserErrorResponseModel>> GetAllNegative();

        [Get("users/{userId}")]
        Task<GeneralResponse<GetUserResponseModel>> GetUser([Path] string userId);

        [Get("users/{userId}")]
        Task<GeneralResponse<GetUserErrorResponseModel>> GetUserNegative([Path] string userId);
        #endregion

        #region Post User
        [Post("users/")]
        Task<GeneralResponse<GetUserResponseModel>> CreateUser([Body] CreateUserModel userModel);

        [Post("users/")]
        Task<GeneralResponse<AuthentificationFailedModel>> CreateUserNegativeAuth([Body] CreateUserModel userModel);

        [Post("users/")]
        Task<GeneralResponse<List<ErrorResponseModel>>> CreateUserNegative([Body] CreateUserModel userModel);
        #endregion

        #region Put User
        [Put("users/{userId}")]
        Task<GeneralResponse<GetUserResponseModel>> UpdateUser([Path] string userId, [Body] UpdateUserModel userModel);

        [Put("users/{userId}")]
        Task<GeneralResponse<List<ErrorResponseModel>>> UpdateUserNegative([Path] string userId, [Body] UpdateUserModel userModel);

        [Put("users/{userId}")]
        Task<GeneralResponse<AuthentificationFailedModel>> UpdateUserNegativeAuth([Path] string userId, [Body] UpdateUserModel userModel);
        #endregion

        #region Patch User
        [Patch("users/{userId}")]
        Task<GeneralResponse<GetUserResponseModel>> UpdateUserInfo([Path] string userId, [Body] PatchUpdateUserInfoModel userModel);
        #endregion

        #region Delete User
        [Delete("users/{userId}")]
        Task<GeneralResponse<GetUserResponseModel>> DeleteUser([Path] string userId);

        [Delete("users/{userId}")]
        Task<GeneralResponse<AuthentificationFailedModel>> DeleteUserNegativeAuth([Path] string userId);
        #endregion
    }
}
