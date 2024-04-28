using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFC7807ProblemDetails.Core.Models
{
    public class ProblemDetail
    {
        public string Type { get; set; } = "about:blank";
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public string ErrorCode { get; set; }
        public List<AdditionalInfo> AdditionalProperties { get; set; } = new List<AdditionalInfo>();

    }
}
