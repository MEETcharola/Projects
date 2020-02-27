using OnlineTermWorkSubmission.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult Index(int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            ViewBag.id = id;
            return View();
        }

        // GET: Students/Create
        public ActionResult CreateStudent()
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent([Bind(Include = "student_id,student_name,student_email,student_address,student_contact,student_dob,student_password")] Student student)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("ViewStudent");
            }

            return View(student);
        }



        // GET: Students/Delete/5
        public ActionResult DeleteStudent(int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
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
        [HttpPost, ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public ActionResult Deleteconformedstudent(int id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("ViewStudent");
        }


        // GET: Admins
        public ActionResult ViewStudent()
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            return View(db.Students.ToList());
        }

        // GET: Admins
        public ActionResult LoginFaculty()
        {

            return View();
        }

        [HttpPost]
        public ActionResult LoginFaculty(Faculty adm)
        {
            var result = db.Faculties.Where(a => a.faculty_email == adm.faculty_email && a.faculty_password == adm.faculty_password).FirstOrDefault();
            if (result != null)
            {
                Session["facultyID"] = result.faculty_email;
                Session["ID"] = result.faculty_id;
                return RedirectToAction("Index", new { id = result.faculty_id });
            }
            else
            {
                ViewBag.message = "Wrong Credentials";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["UID"] = null;
            Session["UserID"] = null;
            Session["adminID"] = null;
            return RedirectToAction("LoginFaculty");
        }

        public ActionResult CreateSubject(int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = fid;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubject(Subject subject, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            if (ModelState.IsValid && subject != null)
            {

                Faculty result = db.Faculties.Find(fid);
                result.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("ViewSubject", new { id = fid });
            }
            ViewBag.id = fid;
            return View(subject);
        }

        public ActionResult DeleteSubject(int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (subId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(subId);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = fid;
            return View(subject);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("DeleteSubject")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedSubject(int subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = fid;
            Subject subject = db.Subjects.Find(subId);
            db.Subjects.Remove(subject);
            db.SaveChanges();
            return RedirectToAction("ViewSubject", new { id = fid });
        }

        public ActionResult ViewSubject(int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = id;
            return View(db.Subjects.Where(x => x.Faculties.Any(y => y.faculty_id == id)).ToList());
        }

        // GET: Faculties/Edit/5
        public ActionResult EditSubject(int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (subId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(subId);
            if (subject == null)
            {
                return HttpNotFound();
            }
            TempData["SubjectID"] = subId;
            TempData.Keep();
            ViewBag.id = fid;
            return View(subject);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubject([Bind(Include = "subject_name")] Subject subject, int? fid)
        {
            if (ModelState.IsValid)
            {
                int SubjectId = (int)TempData["SubjectID"];
                var result = db.Subjects.Where(x => x.subject_id == SubjectId).FirstOrDefault();
                if (result != null)
                {
                    result.subject_name = subject.subject_name;
                    db.Entry(result).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.id = fid;
                return RedirectToAction("ViewSubject", new { id = fid });
            }
            return View(subject);
        }

        public ActionResult CreateLabs(int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLabs(Lab lab, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (ModelState.IsValid && lab != null)
            {
                Subject result = db.Subjects.Find(subId);
                result.Labs.Add(lab);
                db.SaveChanges();
                return RedirectToAction("ViewLabs", new { sid = subId, id = fid });
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            return View(lab);
        }

        public ActionResult ViewLabs(int? sid, int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = id;
            ViewBag.sid = sid;
            return View(db.Labs.Where(x => x.subject_id == sid).ToList());
        }

        public ActionResult EditLabs(int? labId, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (subId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Labs.Find(labId);
            if (lab == null)
            {
                return HttpNotFound();
            }
            TempData["LabID"] = labId;
            TempData.Keep();
            ViewBag.id = fid;
            ViewBag.sid = subId;
            return View(lab);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLabs([Bind(Include = "lab_no, lab_startdate")] Lab lab, int? subId, int? fid)
        {
            if (ModelState.IsValid)
            {
                int LabId = (int)TempData["LabID"];
                var result = db.Labs.Where(x => x.lab_id == LabId).FirstOrDefault();
                if (result != null)
                {
                    result.lab_no = lab.lab_no;
                    result.lab_startdate = lab.lab_startdate;
                    db.Entry(result).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.id = fid;
                ViewBag.sid = subId;
                return RedirectToAction("ViewLabs", new { sid = subId, id = fid });
            }
            return View(lab);
        }

        public ActionResult DeleteLabs(int? labId, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (labId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Labs.Find(labId);
            if (lab == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            return View(lab);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("DeleteLabs")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedLabs(int? labId, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            Lab lab = db.Labs.Find(labId);
            db.Labs.Remove(lab);
            db.SaveChanges();
            return RedirectToAction("ViewLabs", new { sid = subId, id = fid });
        }

        public ActionResult CreateAssignments(int? labId, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            ViewBag.lid = labId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAssigments(Assignment assignment, int? labId, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (ModelState.IsValid && assignment != null)
            {
                Lab result = db.Labs.Find(labId);
                result.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("ViewAssignments", new { lid = labId, sid = subId, id = fid });
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            ViewBag.lid = labId;
            return View(assignment);
        }


        public ActionResult ViewAssignments(int? lid, int? sid, int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = id;
            ViewBag.sid = sid;
            ViewBag.lid = lid;
            return View(db.Assignments.Where(x => x.lab_id == lid).ToList());
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