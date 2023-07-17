using System;
using System.Collections.Generic;

namespace DATA.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentSubject = new HashSet<StudentSubject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? DepID { get; set; }

        public virtual ICollection<StudentSubject> StudentSubject { get; set; }
    }
}
