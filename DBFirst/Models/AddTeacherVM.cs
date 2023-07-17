using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBFirst.Models
{
    public class AddTeacherVM
    {
        public int TeacherId { get; set; }
        public string TName { get; set; }
        public string TContact { get; set; }
    }
}
