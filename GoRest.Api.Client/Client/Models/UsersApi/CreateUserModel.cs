namespace GoRest.Api.Client.Client.Models
{
    public class CreateUserModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public Status Status { get; set; }
    }
}
