using Application.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Training.Queries.Model
{
    public class GetInstructorScheduleQuery : IRequest<BaseResponse<List<Schedule>>>
    {
        public Guid Id { get; set; }
    }
}
