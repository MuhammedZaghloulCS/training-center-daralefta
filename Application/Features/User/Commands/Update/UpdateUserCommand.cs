using Application.Common;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.Update
{
    public class UpdateUserCommand : IRequest<BaseResponse<UserDTO>>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public int Gender { get; set; }

        public string? JobTitle { get; set; }

        public string? AcademicTitle { get; set; }

        public string? Organization { get; set; }

        public string? Specialization { get; set; }

        public string? Skills { get; set; }

        public string? PhoneNumber { get; set; }

        public string? WhatsappNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? NationalIdImage { get; set; }

        public string? AddressInsideCairo { get; set; }

        public string? AddressOutsideCairo { get; set; }

        public string? Doctrine { get; set; }

        public string? MaritalState { get; set; }

        public string? AcademicQualification { get; set; }

        public string? Appreciation { get; set; }

        public string? ImagePath { get; set; }

        public bool? IsActive { get; set; }
    }
}
