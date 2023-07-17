using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBFirst.Models
{
    public class LoginVM
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}
