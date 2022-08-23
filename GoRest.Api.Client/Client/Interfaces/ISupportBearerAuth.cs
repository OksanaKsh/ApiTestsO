using RestEase;
namespace GoRest.Api.Client.Client.Interfaces
{
    public interface ISupportBearerAuth
    {
        [Header("Authorization")]
        string AuthHeader { get; set; }
    }
}
