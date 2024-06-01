using System.Net;

namespace RFC7807ProblemDetails.Core.Models
{
    public class Error
    {
        public int ErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
