using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public string pin { get; set; } = "";
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

        public DateTime? BirthDate { get; set; }

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


        //refresh token
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<UserSession> UserSession { get; set; }
        public ICollection<UsersTrainings> UsersTrainings { get; set; }
        public ICollection<UsersCourse> UsersCourse { get; set; }
        [InverseProperty(nameof(Survey.CreatedByUser))]
        public ICollection<Survey> CreatedSurveys { get; set; } = new List<Survey>();
        public ICollection<SurveyResponse> SurveyResponses { get; set; } = new List<SurveyResponse>();

    
    }
}
