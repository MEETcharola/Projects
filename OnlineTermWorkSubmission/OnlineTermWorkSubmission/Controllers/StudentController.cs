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

        public ActionResult studentlogin()
        {

            return View();
        }

        public ActionResult logout()
        {
            Session["UID"] = null;
            Session["UserID"] = null;
            Session["adminID"] = null;
            return RedirectToAction("studentlogin");
        }

        [HttpPost]
        public ActionResult studentlogin(Student stud)
        {
            if (ModelState.IsValid)
            {
                var result = db.Students.Where(a => a.student_email == stud.student_email & a.student_password == stud.student_password).FirstOrDefault();
                if (result != null)
                {
                    Session["UserID"] = result.student_email;
                    Session["ID"] = result.student_id;
                    return RedirectToAction("Details");
                }
                else
                {
                    ViewBag.message = "Wrong Credentials";
                }

            }
            return View(stud);
        }

        // GET: Students/Details/5
        public ActionResult Details()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("studentlogin");
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