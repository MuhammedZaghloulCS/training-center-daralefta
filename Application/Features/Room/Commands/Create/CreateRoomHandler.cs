using Application.Common;
using Application.Features.Room.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Room.Commands.Create
{
    public class CreateRoomHandler : IRequestHandler<CreateRoomCommand, BaseResponse<RoomDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<RoomDto>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.CreatedBy))
                errors.Add("CreatedBy is required");

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

            var room = new Domain.Entities.Room
            {
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                Name = request.Name,
                Capacity = request.Capacity,
                Location = request.Location,
                BuildId = request.BuildId,
                AttRoomId = request.AttRoomId,
                HaveProjector = request.HaveProjector
            };

            await _unitOfWork.IRooms.AddAsync(room);
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
                AttRoomId = room.AttRoomId,
                BuildId = room.BuildId,
                HaveProjector = room.HaveProjector
            };

            return BaseResponse<RoomDto>.SuccessResponse(dto, "Room created successfully");
        }
    }
}
