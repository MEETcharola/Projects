using OnlineTermWorkSubmission.Models;
using System;
using System.Collections.Generic;
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
            if (ModelState.IsValid)
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
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
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