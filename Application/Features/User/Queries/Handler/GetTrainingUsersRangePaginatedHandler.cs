using Application.Common;
using Application.Features.Training.DTOs;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Handler
{
    public class GetTrainingUsersRangePaginatedHandler : IRequestHandler<GetTrainingUsersRangePaginatedQuery, BaseResponse<List<TrainingDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public GetTrainingUsersRangePaginatedHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<List<TrainingDto>>> Handle(GetTrainingUsersRangePaginatedQuery request, CancellationToken cancellationToken)
        {

            var userExist= _userManager.Users.FirstOrDefault(u => u.Id == request.UserId);

            if (userExist is null)
                return BaseResponse<List<TrainingDto>>.NotFoundResponse("User not found for the provided ID.");


             var trainingIds = await _unitOfWork.IAssignUserTraining
                .GetTrainingIdsByUserId(request.UserId);



            
            if (trainingIds == null || !trainingIds.Any())
                return BaseResponse<List<TrainingDto>>
                    .NotFoundResponse("No trainings assigned to this training.");

            var query=await _unitOfWork.ITraining
                .FindRowAsync(s=>trainingIds.Contains(s.Id));

           var trainingDtos = query
                .OrderBy(u => u.StartDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(t => new TrainingDto
            {
                Id = t.Id,
                CreatedBy = t.CreatedBy,
                CreatedDate = t.CreatedDate,
                UpdatedBy = t.UpdatedBy,
                UpdatedAt = t.UpdatedAt,
                Title = t.Title,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
            }).ToList();
            return BaseResponse<List<TrainingDto>>.SuccessResponse(trainingDtos);
        }
    }
}
