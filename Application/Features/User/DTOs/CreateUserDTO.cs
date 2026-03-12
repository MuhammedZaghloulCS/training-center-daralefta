using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Features.User.DTOs
{
    public class CreateUserDTO
    {

        public string pin { set; get; }

        public string Email { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public int passwordForPrint { get; set; }
        public string PhoneNumber { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public Gender Gender { get; set; }


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

        public DateTime BirthDate { get; set; }

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


        public List<string> roles {get;set;}
    }
}
