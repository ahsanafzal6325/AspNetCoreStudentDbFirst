using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBFirst.Controllers
{
    public class AhsanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
