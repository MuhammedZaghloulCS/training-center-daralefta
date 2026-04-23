using Application.Common;
using Application.Features.Room.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// Alias for clarity
using CairoTime = Application.Common.DateTimeHelper;

namespace Application.Features.Room.Commands.Create
{
    public class CreateRoomHandler : IRequestHandler<CreateRoomCommand, BaseResponse<RoomDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysUnitOfWork _sysUnitOfWork;

        public CreateRoomHandler(IUnitOfWork unitOfWork, ISysUnitOfWork sysUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sysUnitOfWork=sysUnitOfWork;
        }

        public async Task<BaseResponse<RoomDto>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            var validRoom= await _unitOfWork.IRooms.GetFirstByPropAsync(r => (r.Name == request.Name)&&!r.IsDeleted);
            var validRoomByAtt= await _unitOfWork.IRooms.GetFirstByPropAsync(r => (r.AttRoomIdOutSide == request.AttRoomIdOutSide)&&!r.IsDeleted);
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("الاسم مطلوب");
            if(request.Name.Length>50)
                errors.Add("اسم الغرفة لا يجب أن يتجاوز 50 حرفًا");

            if (string.IsNullOrWhiteSpace(request.Location))
                errors.Add("المكان مطلوب");

            if (request.Capacity < 0)
                errors.Add("السعة مطلوبة");

            if (request.Capacity> 300)
                errors.Add("السعة اكبر من الممكن");

            if (request.BuildId.HasValue && request.BuildId.Value < 1)
                errors.Add("المبني المقابل غير موجود");
            if(validRoom!=null)
                errors.Add("الاسم مستخدم من قبل");
            if(validRoomByAtt != null)
                errors.Add("الغرفة المقابلة مستخدمة بالفعل");


            if (errors.Any())
                return BaseResponse<RoomDto>.FailureResponse("خطأ", errors);

            var outsideDoor=await _sysUnitOfWork.ISysDoorRepository.GetByPropAsync(d => d.id == request.AttRoomIdOutSide);
            var outsideDoorName = outsideDoor.name
                .Replace("-outside", "", StringComparison.OrdinalIgnoreCase);
            var insideDoor=await _sysUnitOfWork.ISysDoorRepository.GetByPropAsync(d => d.name.Contains(outsideDoorName)&&d.id!=outsideDoor.id);

            var room = new Domain.Entities.Room
            {
                CreatedBy = request.CreatedBy,
                CreatedDate = CairoTime.Now,
                Name = request.Name,
                Capacity = request.Capacity,
                Location = request.Location,
                BuildId = request.BuildId,
                AttRoomIdOutSide = request.AttRoomIdOutSide,
                AttRoomIdinside=insideDoor.id,
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
                AttRoomIdOutSide = room.AttRoomIdOutSide,
                BuildId = room.BuildId,
                HaveProjector = room.HaveProjector
            };

            return BaseResponse<RoomDto>.SuccessResponse(dto, "Room created successfully");
        }
    }
}
