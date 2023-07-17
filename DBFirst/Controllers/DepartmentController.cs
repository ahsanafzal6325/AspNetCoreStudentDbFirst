using DATA.Models;
using DBFirst.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBFirst.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        FinalDBCotext db = new FinalDBCotext();
        public IActionResult Index()
        {
            var model = db.Department.ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_CreateDep");
        }
        [HttpPost]
        public IActionResult Create(DepartmentVM model)
        {
            Department var = new Department()
            {
                Name = model.Name
            };
            db.Department.Add(var);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dep = db.Department.FirstOrDefault(a => a.Id == id);
            var viewModel = new DepartmentVM()
            {
                Name = dep.Name
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(DepartmentVM model)
        {
            var dep = db.Department.FirstOrDefault(a => a.Id == model.Id);
            dep.Name = model.Name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
