using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFC7807ProblemDetails.Core.Models
{
    public class AdditionalInfo
    {
        public string Key { get; set; }
        public object Value { get; set; }

        public AdditionalInfo(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
