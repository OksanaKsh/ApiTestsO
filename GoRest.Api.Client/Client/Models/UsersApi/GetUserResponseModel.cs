using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoRest.Api.Client.Client.Models
{
    public class GetUserResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
    }
}
