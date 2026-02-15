using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ZkPersonCreateDto
    {
        public string Pin { get; set; }          // REQUIRED
        public string Name { get; set; }         // REQUIRED
        public string DeptCode { get; set; }     // REQUIRED

        public string LastName { get; set; }
        public char Gender { get; set; }       // "M" or "F"
        public string CardNo { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string AccStartTime { get; set; } // yyyy-MM-dd HH:mm:ss
        public string AccEndTime { get; set; }   // yyyy-MM-dd HH:mm:ss
    }
}
