using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Survey.DTOs
{
    public class SimpleUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; }
    }
}
