using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBFirst.Models
{
    public class StudentVM
    {
       public Student stud;
       public Department dep;
        public int? DepID { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
