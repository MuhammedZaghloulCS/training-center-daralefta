using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.DTOs
{
    public class UserSysDto
    {
        public DateTime? birthday { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string last_name { get; set; }

        public string mobile_phone { get; set; }

        public string name { get; set; }
        public string pin { get; set; }

    }
}
