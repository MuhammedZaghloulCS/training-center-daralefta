using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Schedule
    {
        public string Day { get; set; }
        public string Date { get; set; }
        public List<Lecture> Lectures { get; set; }
    }
}
