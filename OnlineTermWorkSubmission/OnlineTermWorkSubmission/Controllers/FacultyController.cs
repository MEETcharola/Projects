using OnlineTermWorkSubmission.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineTermWorkSubmission.Controllers
{
    public class FacultyController : Controller
    {
        // GET: Faculty
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admins
        public ActionResult Index()
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            return View();
        }

        // GET: Students/Create
        public ActionResult createstudent()
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createstudent([Bind(Include = "student_id,student_name,student_email,student_address,student_contact,student_dob,student_password")] Student student)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("viewstudent");
            }

            return View(student);
        }

     
        
        // GET: Students/Delete/5
        public ActionResult deletestudent(int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("deletestudent")]
        [ValidateAntiForgeryToken]
        public ActionResult Deleteconformedstudent(int id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("viewstudent");
        }

       
        // GET: Admins
        public ActionResult viewstudent()
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            return View(db.Students.ToList());
        }

         // GET: Admins
        public ActionResult loginfaculty()
        {

            return View();
        }

        [HttpPost]
        public ActionResult loginfaculty(Faculty adm)
        {
            ViewBag.Parth = ModelState.IsValid.ToString();
            //if (ModelState.IsValid)
            //{
                var result = db.Faculties.Where(a => a.faculty_email == adm.faculty_email && a.faculty_password == adm.faculty_password).FirstOrDefault();
                if (result != null)
                {
                    Session["facultyID"] = result.faculty_email;
                    Session["ID"] = result.faculty_id;
                    return RedirectToAction("index");
                }
                else
                {
                    ViewBag.message = "Wrong Credentials";
                }
           // }
            return View();
        }

        public ActionResult logout()
        {
            Session["UID"] = null;
            Session["UserID"] = null;
            Session["adminID"] = null;
            return RedirectToAction("loginfaculty");
        }

        public ActionResult createsubject()
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createsubject(Subject subject, int? id)
        {
            
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            if (ModelState.IsValid && subject!=null)
            {
                //var result = db.Faculties.Select(a => a.faculty_id).FirstOrDefault();
                Faculty result = db.Faculties.Find(id);
                db.Subjects.Add(subject);
                subject.Faculties.Add(result);
                db.SaveChanges();
                return RedirectToAction("viewsubject");
            }

            return View(subject);
        }

        public ActionResult deletesubject(int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("deletesubject")]
        [ValidateAntiForgeryToken]
        public ActionResult Deleteconformedsubject(int id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
            db.SaveChanges();
            return RedirectToAction("viewsubject");
        }

        public ActionResult viewsubject()
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            return View(db.Subjects.ToList());
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}