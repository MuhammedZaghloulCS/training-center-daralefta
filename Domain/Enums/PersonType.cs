 
using System.ComponentModel;
 

namespace Domain.Enums
{
     
        public enum PersonType
        {
            [Description("طالب")]
            Student = 1,

            [Description("محاضر")]
            Lecturer = 2,
            [Description("الكل")]
            Both = 3
        }
     public enum UsersRolesEnum
    {
        [Description("طالب")]
        Student = 1,
        [Description("محاضر")]
        Lecturer = 2,
        [Description("الكل")]
        Both = 3,
        [Description("الكل")]
        Admin = 4


    }
 
}

 

   

