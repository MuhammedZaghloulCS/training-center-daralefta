using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Features.User.DTOs
{
    public class CreateUserDTO
    {
        public string CreatedBy { get; set; }= "Admin";
        public string pin { set; get; }

        public string Email { get; set; }

        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$", 
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }



        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [RegularExpression(@"^\+?[0-9]\d{3,14}$", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string PhoneNumber { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public Gender Gender { get; set; }


        [MaxLength(300)]
        public string? JobTitle { get; set; }

        [MaxLength(500)]
        public string? AcademicTitle { get; set; }

        [MaxLength(500)]
        public string? Organization { get; set; }

        [MaxLength(500)]
        public string? Specialization { get; set; }

        [MaxLength(500)]
        public string? Skills { get; set; }

        [RegularExpression(@"^\+?[0-9]\d{3,14}$", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string? WhatsappNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? NationalIdImage { get; set; }

        [MaxLength(500)]
        public string? AddressInsideCairo { get; set; }

        [MaxLength(500)]
        public string? AddressOutsideCairo { get; set; }

        public string? Doctrine { get; set; }
        public MaritalStatus? MaritalState { get; set; }
        public string? AcademicQualification { get; set; }
        public string ?Appreciation { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? ImageFile { get; set; }


        public List<string> roles {get;set;}
    }
}
