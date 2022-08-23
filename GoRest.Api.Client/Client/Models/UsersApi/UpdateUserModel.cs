using Newtonsoft.Json;
namespace GoRest.Api.Client.Client.Models
{
    public class UpdateUserModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        public Gender Gender { get; set; }

        public Status Status { get; set; }
    }
}
