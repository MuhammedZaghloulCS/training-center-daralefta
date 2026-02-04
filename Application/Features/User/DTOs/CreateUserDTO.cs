using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Features.User.DTOs
{
    public class CreateUserDTO
    {
     



        public string Email { get; set; }
        [RegularExpression(
       @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
       ErrorMessage = 
            "Password must be at least 8 characters and include uppercase, lowercase, digit, and special character.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public Gender Gender { get; set; }
        [Required]
        public PersonType PersonType { get; set; }

        [Required]

        [MaxLength(300)]
        public string JobTitle { get; set; }

        [MaxLength(500)]
        public string AcademicTitle { get; set; }

        [MaxLength(500)]
        public string Organization { get; set; }

        [MaxLength(500)]
        public string Specialization { get; set; }

        [MaxLength(500)]
        public string Skills { get; set; }

        [MaxLength(50)]
        public string WhatsappNumber { get; set; }

        public string BirthDate { get; set; }

        public string NationalIdImage { get; set; }

        [MaxLength(500)]
        public string AddressInsideCairo { get; set; }

        [MaxLength(500)]
        public string AddressOutsideCairo { get; set; }

        public string Doctrine { get; set; }
        public string MaritalState { get; set; }
        public string AcademicQualification { get; set; }
        public string Appreciation { get; set; }
        public string ImagePath { get; set; }

    }
}
