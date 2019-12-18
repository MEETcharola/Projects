using RESULTportal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RESULTportal.Controllers
{
    public class result_uploaderController : Controller
    {

        private ResultDBEntities db = new ResultDBEntities();

        public ActionResult uploaderlogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uploaderlogin(result_uploader rp)
        {
            if (ModelState.IsValid)
            {
                var result = db.result_uploader.Where(a => a.UID == rp.UID & a.pssd == rp.pssd).FirstOrDefault();
                if (result != null)
                {
                    Session["UID"] = result.UID;
                    Session["ID"] = result.Id;
                    Session["ub"] = result.branch;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Wrong Credentials";
                }
            }
            return View();
        }


        // GET: result_uploader
        public ActionResult Index()
        {
            if (Session["UID"] == null)
            {
                return RedirectToAction("studentlogin");
            }

            return View(db.Students.ToList());
        }




        public ActionResult Edit(int? id)
        {
            if (Session["UID"] == null)
            {
                return RedirectToAction("studentlogin");
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SID,sname,email,semister,Branch,pointer,pssd")] Student student)
        {
            if (Session["UID"] == null)
            {
                return RedirectToAction("studentlogin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public ActionResult logout()
        {
            Session["UID"] = null;
            Session["UserID"] = null;
            Session["adminID"] = null;
            return RedirectToAction("uploaderlogin");
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