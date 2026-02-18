using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Models
{
    public class AddAccessLevelRequest
    {
        public string AreaName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string TimeSegName { get; set; } = null!;
    }
}
