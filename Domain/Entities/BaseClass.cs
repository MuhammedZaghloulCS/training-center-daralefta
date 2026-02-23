using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class BaseClass
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }=false;
    }
}
