using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.DTOs
{
    public class UserDTO :CreateUserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
         public bool IsActive { get; set; }
    }
}
