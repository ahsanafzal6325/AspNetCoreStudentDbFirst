using DATA.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBFirst.Models
{
    public class AddStudentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int DepID { get; set; }
        public List<int> SelectedSubjects { get; set; }

        public MultiSelectList Subjects { get; set; }

        //public List<SelectListItem> TheSubjects { get; set; }
        //public int[] SubjectIds { get; set; }
    }
}
