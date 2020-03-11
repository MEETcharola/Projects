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

        public ActionResult StudentLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentLogin(Student student)
        {

            var result = db.Students.Where(a => a.Student_Email == student.Student_Email & a.Student_Password == student.Student_Password).FirstOrDefault();
            if (result != null)
            {
                Session["studentID"] = result.Student_Email;
                Session["ID"] = result.Student_Id;
                int studentId = result.Student_Id;
                return RedirectToAction("Details", new { id = studentId });
            }
            else
            {
                ViewBag.message = "Wrong Credentials";
            }


            return View(student);
        }

        public ActionResult Logout()
        {
            Session["ID"] = null;
            Session["studentID"] = null;
            return RedirectToAction("StudentLogin");
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["studentID"] == null)
            {
                return RedirectToAction("StudentLogin");
            }

            if (Session["ID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);
            ViewBag.id = id;
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult ViewSubject(int? id)
        {
            if (Session["studentID"] == null)
            {
                return RedirectToAction("StudentLogin");
            }
            ViewBag.id = id;
            return View(db.Subjects.Where(x => x.Students.Any(y => y.Student_Id == id)).ToList().OrderBy(x => x.semester));
        }

        public ActionResult ViewLabs(int? sid, int? id)
        {
            if (Session["studentID"] == null)
            {
                return RedirectToAction("StudentLogin");
            }
            ViewBag.id = id;
            ViewBag.sid = sid;
            ViewBag.subname = db.Subjects.Where(x => x.subject_id == sid).Select(x => x.subject_name).FirstOrDefault();
            return View(db.Labs.Where(x => x.subject_id == sid).ToList().OrderBy(x => x.lab_no));
        }

        public ActionResult ViewAssignments(int? lid, int? sid, int? id)
        {
            if (Session["studentID"] == null)
            {
                return RedirectToAction("StudentLogin");
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
            if (Session["studentID"] == null)
            {
                RedirectToAction("StudentLogin");
            }

            ViewBag.asgId = asgId;
            ViewBag.lid = lid;
            ViewBag.sid = sid;
            ViewBag.id = id;
         
            return View();
        }


        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, int? asgId, int? lid, int? sid, int? id)
        {

            if (Session["studentID"] == null)
            {
                RedirectToAction("StudentLogin");
            }

            ViewBag.asgId = asgId;
            ViewBag.lid = lid;
            ViewBag.sid = sid;
            ViewBag.id = id;
            try
            {
                var subname = db.Subjects.Where(x => x.subject_id == sid).Select(x => x.subject_name).FirstOrDefault();
                string labno = db.Labs.Where(x => x.lab_id == lid).Select(x => x.lab_no).FirstOrDefault().ToString();
                string assignmentno = db.Assignments.Where(x => x.assignment_id == asgId).Select(x => x.assignment_no).FirstOrDefault().ToString();

                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string subjectFolder = Server.MapPath("~/UploadFiles/"+"_"+subname);
                    string labFolder = Path.Combine(subjectFolder + "/_LabNo_" + labno);
                    string assignmentFolder = Path.Combine(labFolder + "/_AssignmentNo_" + assignmentno);
                    

                    if (!Directory.Exists(subjectFolder))
                    {
                        //If  (Subjec Folder) does not exists. Create it,lab adnd assignment folders.
                        Directory.CreateDirectory(subjectFolder);
                        
                        Directory.CreateDirectory(labFolder);

                        Directory.CreateDirectory(assignmentFolder);

                    }
                    else if(!Directory.Exists(labFolder))
                    {
                        //If  (Lab Folder) does not exists. Create it and assignment folders.
                        Directory.CreateDirectory(labFolder);

                        Directory.CreateDirectory(assignmentFolder);
                    }
                    else if(!Directory.Exists(assignmentFolder))
                    {
                        //If  (Assignment Folder) does not exists. Create it.

                        Directory.CreateDirectory(assignmentFolder);
                    }

                    string _path = Path.Combine(assignmentFolder, _FileName);

                    var result = db.Assignments.Where(x => x.assignment_id == asgId).Select(x => x.assignment_enddate).FirstOrDefault();
                    
                    if (DateTime.Compare(DateTime.Now, result) < 0)

                    {
                        file.SaveAs(_path);
                        ViewBag.Message = "File Uploaded Successfully!!";
                    }
                    else
                    {
                        ViewBag.Message = "sorry,You are out of deadline!!";
                    }

                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = "Select any file first ";
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