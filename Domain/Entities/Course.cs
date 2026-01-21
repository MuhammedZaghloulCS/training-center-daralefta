using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course: BaseClass
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Prerequisites { get; set; }
        public int Duration { get; set; }

        public ICollection<Session> Sessions { get; set; }

        public int? TrainingId { get; set; }
        [ForeignKey(nameof(TrainingId))]
        public Training Training { get; set; }

        public ICollection<ApplicationUser> applicationUsers { get; set; }

    }

}
