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
    public class TeacherController : Controller
    {
        FinalDBCotext db = new FinalDBCotext();
        public IActionResult Index()
        {
            var list = db.Teacher.ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
            {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddTeacherVM model)
        {
            Teacher tdb = new Teacher()
            {
                TName = model.TName,
                TContact = model.TContact
            };
            db.Teacher.Add(tdb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AjIndex(int id)
        {
            return View();
        }
        public PartialViewResult GetTeacherList()
        {
            var list = db.Teacher.ToList();
            return PartialView(list);
        }
        public PartialViewResult GetTeacher(int id)
        {
            Teacher teach = new Teacher();
            if (id > 0)
            {
                teach = db.Teacher.Find(id);
            }
            return PartialView(teach);
        }
        public IActionResult AjCreate(Teacher model)
        {
            if (model.TeacherId > 0)
            {
                db.Teacher.Update(model);
                db.SaveChanges();
                return RedirectToAction("GetTeacherList");
            }
            else
            {
                db.Teacher.Add(model);
                db.SaveChanges();
                return RedirectToAction("GetTeacherList");
            }
        }
    }
}
