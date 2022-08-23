using GoRest.Api.Client.Client.Models.TodoApi;
namespace GoRest.Api.Client.Client.Models.PostsApi
{
    public class GetTodoResponseModel
    {
        public string Id { get; set; }
        public string User_Id { get; set; }
        public string Title { get; set; }
        public DateTime Due_on { get; set; }    
        public TodoStatus Status { get; set; }
    }
}
