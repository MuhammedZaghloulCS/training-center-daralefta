using Application.Common;
using Application.Features.Room.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Room.Commands.Update
{
    public class UpdateRoomHandler : IRequestHandler<UpdateRoomCommand, BaseResponse<RoomDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoomHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<RoomDto>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _unitOfWork.IRooms.GetByPkAsync(request.Id);
            if (room == null)
            {
                return BaseResponse<RoomDto>.NotFoundResponse("Room not found");
            }

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("Name is required");

            if (string.IsNullOrWhiteSpace(request.Location))
                errors.Add("Location is required");

            if (request.Capacity < 0)
                errors.Add("Capacity is invalid");

            if (request.BuildId.HasValue && request.BuildId.Value < 1)
                errors.Add("BuildId is invalid");

            if (errors.Any())
                return BaseResponse<RoomDto>.FailureResponse("Validation failed", errors);

            room.Name = request.Name;
            room.Capacity = request.Capacity;
            room.Location = request.Location;
            room.BuildId = request.BuildId;
            room.HaveProjector = request.HaveProjector;
            room.UpdatedBy = request.UpdatedBy;
            room.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.IRooms.Update(room);
            await _unitOfWork.Complete();

            var dto = new RoomDto
            {
                Id = room.Id,
                CreatedBy = room.CreatedBy,
                CreatedDate = room.CreatedDate,
                UpdatedBy = room.UpdatedBy,
                UpdatedAt = room.UpdatedAt,
                Name = room.Name,
                Capacity = room.Capacity,
                Location = room.Location,
                AttRoomId = room.AttRoomIdOutSide,

                BuildId = room.BuildId,
                HaveProjector = room.HaveProjector
            };

            return BaseResponse<RoomDto>.SuccessResponse(dto, "Room updated successfully");
        }
    }
}
