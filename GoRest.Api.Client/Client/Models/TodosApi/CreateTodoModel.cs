namespace GoRest.Api.Client.Client.Models
{
    public class CreateTodoModel
    {
        public string User_Id { get; set; }
        public string Title { get; set; }
        public DateTime Due_On { get; set; }
        public string Status { get; set; }

    }
}
