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

    public class SubjectsController : Controller
    {
        FinalDBCotext db = new FinalDBCotext();
        public IActionResult Index()
        {
            var model = db.Subject.ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SubjectsVM model)
        {
            var sub = new Subject()
            {
                SubName = model.SubName
            };
            db.Subject.Add(sub);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var sub = db.Subject.FirstOrDefault(a => a.SubId == id);
            var viewModel = new SubjectsVM()
            {
                SubName = sub.SubName
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(SubjectsVM model)
        {
            var dep = db.Subject.FirstOrDefault(a => a.SubId == model.SubId);
            dep.SubName = model.SubName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
