using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
