using Application.Common;
using MediatR;

namespace Application.Features.Session.Commands.Files
{
    public class DeleteSessionFileCommand : IRequest<BaseResponse<bool>>
    {
        public int SessionId { get; set; }
        public string FilePath { get; set; }
    }
}
