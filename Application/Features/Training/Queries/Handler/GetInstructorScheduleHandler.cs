using Application.Common;
using Application.Features.Training.Queries.Model;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Training.Queries.Handler
{
    public class GetInstructorScheduleHandler : IRequestHandler<GetInstructorScheduleQuery, BaseResponse<List<Schedule>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetInstructorScheduleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<Schedule>>> Handle(GetInstructorScheduleQuery request, CancellationToken cancellationToken)
        {
            var userTraining = await _unitOfWork.IUserTrainingRepository.GetLecturerScheduleAsync(ut => ut.UserId == request.Id);

            return BaseResponse<List<Schedule>>.SuccessResponse(userTraining);

        }
    }
}
