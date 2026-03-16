using Application.Common;
using Application.Features.Session.DTOs;
using MediatR;

public class GetSessionsPagedQuery : IRequest<BaseResponse<List<SessionListDTO>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string Search { get; set; } = string.Empty;

    public int? CourseId { get; set; }   // الجديد
}