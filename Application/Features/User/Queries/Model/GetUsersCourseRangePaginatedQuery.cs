using Application.Common;
using Application.Features.Course.DTOs;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Model
{
    public class GetUsersCourseRangePaginatedQuery : IRequest<BaseResponse<List<UserDTO>>>
    {
        public int CourseId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
    }
}
