using OnlineTermWorkSubmission.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineTermWorkSubmission.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudentLogin()
        {

            return View();
        }

        public ActionResult Logout()
        {
            Session["UID"] = null;
            Session["UserID"] = null;
            Session["adminID"] = null;
            return RedirectToAction("StudentLogin");
        }

        [HttpPost]
        public ActionResult StudentLogin(Student student)
        {
           
                var result = db.Students.Where(a => a.Student_Email == student.Student_Email & a.Student_Password == student.Student_Password).FirstOrDefault();
                if (result != null)
                {
                    Session["UserID"] = result.Student_Email;
                    Session["ID"] = result.Student_Id;
                    return RedirectToAction("Details");
                }
                else
                {
                    ViewBag.message = "Wrong Credentials";
                }

           
            return View(student);
        }

        // GET: Students/Details/5
        public ActionResult Details()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("StudentLogin");
            }

            if (Session["ID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(Convert.ToInt32(Session["ID"]));
            ViewBag.id = student.Student_Id;
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult ViewSubject(int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("StudentLogin");
            }
            ViewBag.id = id;
            return View(db.Subjects.Where(x => x.Students.Any(y => y.Student_Id == id)).ToList());
        }

        public ActionResult ViewLabs(int? sid, int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("StudentLogin");
            }
            ViewBag.id = id;
            ViewBag.sid = sid;
            ViewBag.subname = db.Subjects.Where(x => x.subject_id == sid).Select(x => x.subject_name).FirstOrDefault();
            return View(db.Labs.Where(x => x.subject_id == sid).ToList());
        }

        public ActionResult ViewAssignments(int? lid, int? sid, int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = id;
            ViewBag.sid = sid;
            ViewBag.lid = lid;
            ViewBag.labno = db.Labs.Where(x => x.lab_id == lid).Select(x => x.lab_no).FirstOrDefault();
            return View(db.Assignments.Where(x => x.lab_id == lid).ToList());
        }

        [HttpGet]
        public ActionResult UploadFile(int? asgId, int? lid, int? sid, int? id)
        {
            ViewBag.asgId = asgId;
            ViewBag.lid = lid;
            ViewBag.sid = sid;
            ViewBag.id = id;
            ViewBag.Message = "Testing";
            return View();
        }


        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, int? asgId, int? lid, int? sid, int? id)
        {

            if (Session["UserID"] == null)
            {
                RedirectToAction("StudentLogin");
            }

            ViewBag.asgId = asgId;
            ViewBag.lid = lid;
            ViewBag.sid = sid;
            ViewBag.id = id;
            try
            {

                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);

<<<<<<< HEAD
                    var result = db.Assignments.Where(x => x.assignment_id == asgId).Select(x => x.assignment_enddate.ToShortDateString()).FirstOrDefault();
                    //ViewBag.test = result;
                    // DateTime result = Convert.ToDateTime(db.Assignments.Where(x => x.assignment_id == asgId).Select(x => (x.assignment_enddate.ToString("dd.MM.yyyy"))).SingleOrDefault());
                    DateTime EndDate = Convert.ToDateTime(result, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                    if (DateTime.Compare(DateTime.Now, EndDate) < 0)
=======
                    var result = db.Assignments.Where(x => x.assignment_id == asgId).Select(x => x.assignment_enddate).FirstOrDefault();
                    
                    if (DateTime.Compare(DateTime.Now, result) < 0)
>>>>>>> 0dbe2d821551cd963903bd7d9ee4f874ca19069b
                    {
                        file.SaveAs(_path);
                        ViewBag.Message = "File Uploaded Successfully!!";
                    }
                    else
                    {
                        ViewBag.Message = "sorry,You are out of date!!";
                    }

                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e;
                return View();
            }
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