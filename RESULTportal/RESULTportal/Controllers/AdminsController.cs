using RESULTportal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RESULTportal.Controllers
{
    public class AdminsController : Controller
    {

        private ResultDBEntities db = new ResultDBEntities();

        // GET: Admins
        public ActionResult Index()
        {
            if (Session["adminID"] == null)
            {
                return RedirectToAction("loginadmin");
            }
            return View();
        }

        // GET: Students/Create
        public ActionResult createstudent()
        {
            if (Session["adminID"] == null)
            {
                return RedirectToAction("loginadmin");
            }
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createstudent([Bind(Include = "Id,SID,sname,email,semister,Branch,pointer,pssd")] Student student)
        {
            if (Session["adminID"] == null)
            {
                return RedirectToAction("loginadmin");
            }
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("viewstudent");
            }

            return View(student);
        }

        // GET: result_uploader/Create
        public ActionResult createuploader()
        {
            if (Session["adminID"] == null)
            {
                return RedirectToAction("loginadmin");
            }
            return View();
        }

        // POST: result_uploader/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createuploader([Bind(Include = "Id,UID,name,branch,pssd")] result_uploader result_uploader)
        {
            if (Session["adminID"] == null)
            {
                return RedirectToAction("loginadmin");
            }
            if (ModelState.IsValid)
            {
                db.result_uploader.Add(result_uploader);
                db.SaveChanges();
                return RedirectToAction("viewuploader");
            }

            return View(result_uploader);
        }

        // GET: Students/Delete/5
        public ActionResult deletestudent(int? id)
        {
            if (Session["adminID"] == null)
            {
                return RedirectToAction("loginadmin");
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
            if (Session["adminID"] == null)
            {
                return RedirectToAction("loginadmin");
            }
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("viewstudent");
        }

        // GET: result_uploader/Delete/5
        public ActionResult deleteuploader(int? id)
        {
            if (Session["adminID"] == null)
            {
                return RedirectToAction("loginadmin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            result_uploader result_uploader = db.result_uploader.Find(id);
            if (result_uploader == null)
            {
                return HttpNotFound();
            }
            return View(result_uploader);
        }

        // POST: result_uploader/Delete/5
        [HttpPost, ActionName("deleteuploader")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["adminID"] == null)
            {
                return RedirectToAction("loginadmin");
            }
            result_uploader result_uploader = db.result_uploader.Find(id);
            db.result_uploader.Remove(result_uploader);
            db.SaveChanges();
            return RedirectToAction("viewuploader");
        }

        // GET: Admins
        public ActionResult viewstudent()
        {
            if (Session["adminID"] == null)
            {
                return RedirectToAction("loginadmin");
            }
            return View(db.Students.ToList());
        }

        // GET: Admins
        public ActionResult viewuploader()
        {
            if (Session["adminID"] == null)
            {
                return RedirectToAction("loginadmin");
            }
            return View(db.result_uploader.ToList());
        }

        // GET: Admins
        public ActionResult loginadmin()
        {

            return View();
        }

        [HttpPost]
        public ActionResult loginadmin(Admin adm)
        {
            if (ModelState.IsValid)
            {
                var result = db.Admins.Where(a => a.adminID == adm.adminID & a.pssd == adm.pssd).FirstOrDefault();
                if (result != null)
                {
                    Session["adminID"] = result.adminID;
                    Session["ID"] = result.Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Wrong Credentials";
                }
            }
            return View();
        }

        public ActionResult logout()
        {
            Session["UID"] = null;
            Session["UserID"] = null;
            Session["adminID"] = null;
            return RedirectToAction("loginadmin");
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