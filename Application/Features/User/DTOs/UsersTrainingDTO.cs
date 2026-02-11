using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.DTOs
{
    public class UsersTrainingDTO
    {
        public List<Guid> UserIds { get; set; }
        public int TrainingId { get; set; }
    }
}
