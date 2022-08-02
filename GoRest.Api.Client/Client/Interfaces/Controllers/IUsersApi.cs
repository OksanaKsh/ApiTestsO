using GoRest.Api.Client.Client.Models;
using RestEase;

namespace GoRest.Api.Client.Client.Interfaces.Controllers
{
    [Header("Accept", "application/json")]
    [Header("Content-Type", "application/json")]
    public interface IUsersApi : ISupportBearerAuth
    {
        [Get("users/")]   
        Task<GeneralResponse<List<GetUserResponseModel>>> GetAll();
        
        [Get("users/{userId}")]   
        Task<GeneralResponse<GetUserResponseModel>> GetUser([Path] string userId);
       
        [Post("users/")]
        Task<GeneralResponse<GetUserResponseModel>> CreateUser([Body] CreateUserModel userModel);

        [Put("users/{userId}")]
        Task<GeneralResponse<GetUserResponseModel>> UpdateUser([Path] string userId,[Body] UpdateUserModel userModel);

        [Delete("users/{userId}")]
        Task<GeneralResponse<GetUserResponseModel>> DeleteUser([Path] string userId);
    }
}
