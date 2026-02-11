using Application.Common;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Model
{
    public class GetUsersTrainingRangePaginatedQuery : IRequest<BaseResponse<List<UserDTO>>>
    {
        public int TrainingId { get; set; }
        public int PageNumber { get; set; }=1;
        public int PageSize { get; set; }=10;
        public string Search { get; set; }
    }
}
