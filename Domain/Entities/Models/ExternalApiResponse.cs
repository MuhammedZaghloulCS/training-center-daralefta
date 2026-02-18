using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Models
{
    public class ExternalApiResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; } = null!;
        public T Data { get; set; } = default!;
    }
}
