using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SurveyCategory :BaseClass
    {
        [MaxLength(150)]
        public string Name { get; set; }

        // Optional description field
        [MaxLength(500)]
        public string Description { get; set; }

        // Navigation property
        public virtual ICollection<Survey> Survies { get; set; }
    }
}
