using System.Collections.Generic;

namespace WebApi.Models
{
    public class BadRequestModel
    {
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
