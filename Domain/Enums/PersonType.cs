 
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
            Both = 3,
             [Description("ادمن")]
            admin = 4
    }
     public enum UsersRolesEnum
    {
        [Description("Student")]
        Student = 1,
        [Description("Instructor")]
        Lecturer = 2,
        [Description("Admin")]
        Admin = 3


    }
 
}

 

   

