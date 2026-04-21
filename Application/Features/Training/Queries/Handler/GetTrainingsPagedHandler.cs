using Application.Common;
using Application.Features.Training.DTOs;
using Application.Features.Training.Queries.Model;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Implementations.Repository;
using Mapster;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Training.Queries.Handler
{
    public class GetTrainingsPagedHandler : IRequestHandler<GetTrainingsPagedQuery, BaseResponse<List<TrainingPagedDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTrainingsPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<TrainingPagedDto>>> Handle(
            GetTrainingsPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
                return BaseResponse<List<TrainingPagedDto>>.BadRequestResponse("معلمات غير صالحة");

            Expression<Func<Domain.Entities.Training, bool>> filter = null;

            if (!string.IsNullOrEmpty(request.Search))
                filter = t => t.Title.Contains(request.Search);

            var (data, total) = await _unitOfWork.ITraining
                .GetTrainingsPagedProjectedAsync(request.PageNumber, request.PageSize, filter);
            return BaseResponse<List<TrainingPagedDto>>.SuccessResponse(
                data, request.PageNumber, request.PageSize, total, "تم تحميل البيانات بنجاح");
        }
    }
}
