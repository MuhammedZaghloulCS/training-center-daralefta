using Application.Common;
using Application.Features.User.DTOs;
using MediatR;



namespace Application.Features.User.Queries.Model
{
    public class GetAllUsersQuery : IRequest<BaseResponse<List<UserDTO>>>
    {
    }
}
