using System.Net;

namespace GoRest.Api.Client.Client.Models
{
    public class GeneralResponse<TData>
    {
        public HttpStatusCode Code { get; set; }
        public Meta Meta { get; set; }
        public TData Data { get; set; }
    }

    public class Meta
    {
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public int Total { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
