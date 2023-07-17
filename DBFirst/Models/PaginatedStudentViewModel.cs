using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBFirst.Models
{
    public class PaginatedStudentViewModel
    {
        public List<StudentVM> Students { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int DepID { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling(TotalCount / (double)PageSize); }
        }
    }
}
