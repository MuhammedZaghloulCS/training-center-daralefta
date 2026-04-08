using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class CoursesTrainings
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
