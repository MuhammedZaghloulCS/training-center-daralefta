using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Lecture
    {
        public string Time { get; set; }
        public string Course { get; set; }
        public List<string> Lecturers { get; set; }
        public string Room { get; set; }
        public string Building { get; set; }

        
    }
}
