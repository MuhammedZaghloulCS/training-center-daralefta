using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Queries.Handler
{
    public class GetSurveyByIdHandler : IRequestHandler<GetSurveyByIdQuery, BaseResponse<Domain.Entities.Survey>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSurveyByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Domain.Entities.Survey>> Handle(GetSurveyByIdQuery request, CancellationToken cancellationToken)
        {

            var survey = await _unitOfWork.ISurvey.GetSurveyWithQuestion(request.Id);

            if (survey == null)
            {
                return BaseResponse<Domain.Entities.Survey>.NotFoundResponse("Survey not found");
            }
 
            return BaseResponse<Domain.Entities.Survey>.SuccessResponse(survey, "loaded successfully");
        }
    }
}
