using Application.Common;
using Application.Features.Course.DTOs;
using Application.Features.Session.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Model
{
    public class GetCourseUsersRangePaginatedQuery : IRequest<BaseResponse<List<CourseDto>>>
    {
        public Guid UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; }
    }

}
