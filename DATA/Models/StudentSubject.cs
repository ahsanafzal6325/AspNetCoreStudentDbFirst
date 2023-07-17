using System;
using System.Collections.Generic;

namespace DATA.Models
{
    public partial class StudentSubject
    {
        public int StudSubId { get; set; }
        public int? StudId { get; set; }
        public int? SubId { get; set; }

        public virtual Student Stud { get; set; }
        public virtual Subject Sub { get; set; }
    }
}
