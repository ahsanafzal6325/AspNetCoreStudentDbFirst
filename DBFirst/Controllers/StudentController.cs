using DATA.Models;
using DBFirst.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBFirst.Controllers
{
    //[Authorize]
    public class StudentController : Controller
    {
        FinalDBCotext db = new FinalDBCotext();
        public IActionResult Index(int? page,int DepId,int SubId)
        {
            int pageNumber = page ?? 1;
            int pageSize = 7;
            var totalCount = db.Student.Count();
            var Departments = (from f in db.Department
                               select new { Text = f.Name, Value = f.Id }).ToList();
            ViewBag.Departments = new SelectList(Departments, "Value", "Text");
            var Subjects = (from f in db.Subject
                            select new { Text = f.SubName, Value = f.SubId });
            ViewBag.Subjects = new SelectList(Subjects, "Value", "Text");

            {
                //var studDetail = (from s in db.Student
                //                      join d in db.Department on s.DepID equals d.Id
                //                      //  join ss in db.StudentSubject on s.Id equals ss.StudId
                //                      //  join sub in db.Subject on ss.SubId equals sub.SubId

                //                      where (DepId == null || s.DepID == DepId )


                //                      select new StudentVM
                //                      {
                //                          stud = s,
                //                          dep = d,
                //                          Subjects = (from f in db.StudentSubject
                //                                      join sub in db.Subject on f.SubId equals sub.SubId
                //                                      where (f.StudId == s.Id)
                //                                      select sub).ToList()
                //                      }).Skip((pageNumber - 1) * pageSize)
                //       .Take(pageSize)
                //       .ToList();
                //    var model = new PaginatedStudentViewModel
                //    {
                //        Students = studDetail,
                //        TotalCount = totalCount,
                //        PageSize = pageSize,
                //        CurrentPage = pageNumber
                //    };
            }

            var liststudent = db.StudentSubject.Where(x => x.SubId == SubId).Select(s => s.StudId).ToList();


            var studDetail = (from s in db.Student
                              join d in db.Department on s.DepID equals d.Id
                              where (
                              
                              (DepId == 0 || s.DepID == DepId || DepId == null)
                              
                              && (SubId==null || (liststudent == null?true: (liststudent.Count==0?true:liststudent.Any(x=>x==s.Id))))
                              )


                              //where (DepId == null || s.DepID == DepId)
                              select new StudentVM
                              {
                                  stud = s,
                                  dep = d,
                                  Subjects = (from f in db.StudentSubject
                                              join sub in db.Subject on f.SubId equals sub.SubId
                                              where (f.StudId == s.Id)
                                              select sub).ToList()
                              }).ToList();
            return View(studDetail);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var Departments = (from f in db.Department
                               select new { Text = f.Name, Value = f.Id }).ToList();
            ViewBag.Departments = new SelectList(Departments, "Value", "Text");
            var Subjects = (from f in db.Subject
                            select new { Text = f.SubName, Value = f.SubId });
            ViewBag.Subjects = new SelectList(Subjects, "Value", "Text");
            return PartialView("_Create");
        }
        [HttpPost]
        public IActionResult Create(AddStudentVM stud)
        {
            var student = new Student()
            {
                Name = stud.Name,
                Email = stud.Email,
                DepID = stud.DepID
            };
            db.Student.Add(student);
            db.SaveChanges();

            foreach (int subjectId in stud.SelectedSubjects)
            {
                var studentSubject = new StudentSubject
                {
                    StudId = student.Id,
                    SubId = subjectId
                };
                db.StudentSubject.Add(studentSubject);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = db.Student.FirstOrDefault(x => x.Id == id);
            //var studSub = db.StudentSubject.FirstOrDefault(x => x.StudId == id);
        
            if (student!=null)
            {
                var viewModel = new UpdateStudentVM()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    DepID = student.DepID,
                    SelectedSubjects = db.StudentSubject.Where(ss => ss.StudId == student.Id).Select(ss => ss.SubId).ToList()
                    //SubID = studSub.StudId
                };
                var Departments = (from f in db.Department
                                   select new { Text = f.Name, Value = f.Id }).ToList();
                ViewBag.Departments = new SelectList(Departments, "Value", "Text");
                var Subjects = (from s in db.Subject
                                select new { Text = s.SubName, Value = s.SubId });
                ViewBag.Subjects = new SelectList(Subjects, "Value", "Text",viewModel.SelectedSubjects);
                return PartialView("_Edit", viewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(UpdateStudentVM model)
        {
            var student = db.Student.Find(model.Id);

            var list = db.StudentSubject.Where(x => x.StudId == model.Id).ToList();
            db.StudentSubject.RemoveRange(list);
            db.SaveChanges();

            //var studSub = db.StudentSubject.Find(model.SelectedSubjects);
            if (student!=null)
            {
                student.Name = model.Name;
                student.Email = model.Email;
                student.DepID = model.DepID;
                student.StudentSubject.Clear();
                foreach (var subjectId in model.SelectedSubjects)
                {
                    var subject = db.Subject.Find(subjectId);
                    if (subject != null)
                    {
                        var studentSubject = new StudentSubject
                        {
                            StudId = student.Id,
                            SubId = subject.SubId
                        };
                        db.StudentSubject.Add(studentSubject);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var student = db.Student.Find(id);
            if (student != null)
            {
                // Delete associated records in StudentSubject table
                var studentSubjects = db.StudentSubject.Where(ss => ss.StudId == id);
                db.StudentSubject.RemoveRange(studentSubjects);

                db.Student.Remove(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        //[HttpPost]
        //public IActionResult ConfirmDelete(int Id)
        //{
        //    var student = db.Student.FirstOrDefault(x => x.Id == Id);
        //    if (student != null)
        //    {
        //        db.Student.Remove(student);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return NotFound();
        //}
    }
}
