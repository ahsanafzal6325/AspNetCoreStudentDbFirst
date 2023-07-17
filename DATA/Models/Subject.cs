using System;
using System.Collections.Generic;

namespace DATA.Models
{
    public partial class Subject
    {
        public Subject()
        {
            StudentSubject = new HashSet<StudentSubject>();
        }

        public int SubId { get; set; }
        public string SubName { get; set; }

        public virtual ICollection<StudentSubject> StudentSubject { get; set; }
    }
}
