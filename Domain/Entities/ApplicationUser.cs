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
        public bool IsDeleted { get; set; } = false;

        [RegularExpression(@"^\d+$",
        ErrorMessage = "يقبل أرقام فقط")]
        public string? pin { get; set; } = "";
        public String FirstName { get; set; }
        public String LastName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; private set; }
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

        [RegularExpression(@"^\+?[1-9]\d{7,14}$", ErrorMessage = "رقم الهاتف غير صحيح")]
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
        public string? Appreciation { get; set; }
        public string? ImagePath { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreatingDate{ get; set; } = DateTime.Now;

        //refresh token
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

     
        public ICollection<UserSession> LecturerersSessions { get; set; }
        public ICollection<UsersTrainings> UsersTrainings { get; set; }
        public ICollection<UsersCourse> UsersCourse { get; set; }

        public ICollection<UserNotification>UserNotifications { get; set; }
        public ICollection<SurveyResponse> SurveyResponses { get; set; } = new List<SurveyResponse>();
        public ICollection<SurveyUsers> SurveyUsers { get; set; }

    }
}
