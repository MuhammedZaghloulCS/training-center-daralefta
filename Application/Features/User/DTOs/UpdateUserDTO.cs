using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Features.User.DTOs
{
    public class UpdateUserDTO
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public int? Gender { get; set; }

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
        // كلمة مرور البصمة - أرقام فقط
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "personPwd must contain only numbers.")]
        public string? personPwd { get; set; }
        public string? ImagePath { get; set; }
        public string? pin { get; set; }
        public List<string> roles { get; set; }

        public string UpdatedBy { get; set; }

    }
}
