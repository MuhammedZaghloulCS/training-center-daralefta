using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ZkPersonCreateDto
    {
  
            public string Id { get; set; }
            public string Pin { get; set; }
            public string DeptCode { get; set; }
            public string DeptName { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public DateTime? Birthday { get; set; }
            public string CardNo { get; set; }
            public string SupplyCards { get; set; }
            public string PersonPhoto { get; set; }
            public string SelfPwd { get; set; }
            public bool? IsSendMail { get; set; }
            public string MobilePhone { get; set; }
            public string PersonPwd { get; set; }
            public string CarPlate { get; set; }
            public string Email { get; set; }
            public string Ssn { get; set; }
            public string AccLevelIds { get; set; }
            public DateTime? AccStartTime { get; set; }
            public DateTime? AccEndTime { get; set; }
            public string CertType { get; set; }
            public string CertNumber { get; set; }
            public string PhotoPath { get; set; }
            public DateTime? HireDate { get; set; }
            public bool? IsDisabled { get; set; }
            public string VislightPhoto { get; set; }
            public string VislightPhotoPath { get; set; }
            public string LeaveId { get; set; }
        
    }
}
