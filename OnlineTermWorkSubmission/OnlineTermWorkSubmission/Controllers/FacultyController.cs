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
        private readonly ApplicationDbContext Db = new ApplicationDbContext();


        public ActionResult LoginFaculty()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginFaculty(Faculty faculty)
        {
            var result = Db.Faculties.Where(x => x.faculty_email == faculty.faculty_email && x.faculty_password == faculty.faculty_password).FirstOrDefault();
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
            Session["ID"] = null;
            Session["facultyID"] = null;
            return RedirectToAction("LoginFaculty");
        }

        public ActionResult Index(int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            ViewBag.id = id;
            return View();
        }



        // Controlling Students
        public ActionResult ViewStudent(int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = id;
            return View(Db.Students.ToList().OrderBy(x=>x.Roll_No));
        }

        public ActionResult CreateStudent(int? fid)
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
        public ActionResult CreateStudent(Student student, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (ModelState.IsValid)
            {
                Db.Students.Add(student);
                Db.SaveChanges();
                return RedirectToAction("ViewStudent", new { id = fid });
            }
            ViewBag.id = fid;
            return View(student);
        }

        public ActionResult DeleteStudent(int? id, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = Db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = fid;
            return View(student);
        }

        [HttpPost, ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedStudent(int id, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            Student student = Db.Students.Find(id);
            Db.Students.Remove(student);
            Db.SaveChanges();
            ViewBag.id = fid;
            return RedirectToAction("ViewStudent", new { id = fid });
        }

        public ActionResult EditStudent(int? id, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = Db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = fid;
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent([Bind(Exclude = "Student_Id, Subjects")] Student student, int? id, int? fid)
        {
            if (ModelState.IsValid)
            {
                var result = Db.Students.Where(x => x.Student_Id == id).FirstOrDefault();
                if (result != null)
                {
                    result.Student_Name = student.Student_Name;
                    result.Student_Password = student.Student_Password;
                    result.Student_Email = student.Student_Email;
                    result.Student_Dob = student.Student_Dob;
                    result.Student_Contact = student.Student_Contact;
                    result.Branch = student.Branch;
                    result.Division = student.Division;
                    result.Batch = student.Batch;
                    result.Roll_No = student.Roll_No;
                    result.Semester = student.Semester;
                    Db.Entry(result).State = EntityState.Modified;
                    Db.SaveChanges();
                }
                ViewBag.id = fid;
                return RedirectToAction("ViewStudent", new { id = fid });
            }
            return View(student);
        }




        public ActionResult SelectStudents(int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = id;
            return View(Db.Subjects.Where(x => x.Faculties.Any(y => y.faculty_id == id)).OrderBy(x => x.semester).ToList());
        }

        public ActionResult ViewEnrolledStudent(int? semno, int? sid, int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = id;
            ViewBag.sid = sid;
            ViewBag.semno = semno;
            return View(Db.Students.Where(x => x.Subjects.Any(y => y.subject_id == sid) && x.Semester == semno).ToList().OrderBy(x=>x.Roll_No));
        }

        public ActionResult EnrollStudent(int? semNo, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            ViewBag.semno = semNo;
            return View();
        }

        [HttpPost]
        public ActionResult EnrollStudent(FormCollection values, int? semNo, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            if (ModelState.IsValid)
            {
                var branchId = values["Branch_Id"];
                var divisionId = values["Division_Id"];
                var batchId = values["Batch_Id"];

                var brcResult = Db.Branches.Where(x => x.Branch_Id.ToString() == branchId).Select(x=>x.Branch_Name).FirstOrDefault();
                var divResult = Db.Divisions.Where(x => x.Division_Id.ToString() == divisionId).Select(x=>x.Division_Name).FirstOrDefault();
                var btcResult = Db.Batches.Where(x => x.Batch_Id.ToString() == batchId).Select(x=>x.Batch_Name).FirstOrDefault();
                
                var result = Db.Students.Where(x => x.Branch == brcResult && x.Division == divResult && x.Batch == btcResult && x.Semester == semNo).ToList();
                Subject result1 = Db.Subjects.Find(subId);
                foreach(var i in result)
                {
                    result1.Students.Add(i);
                    Db.SaveChanges();
                }
                
                return RedirectToAction("ViewEnrolledStudent", new { semno = semNo, sid = subId, id = fid });
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            ViewBag.semno = semNo;
            return View();
        }

        public ActionResult DeleteEnrolledStudent(int? semNo, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            ViewBag.semno = semNo;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteEnrolledStudent(FormCollection values, int? semNo, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("loginfaculty");
            }
            if (ModelState.IsValid)
            {
                var branchId = values["Branch_Id"];
                var divisionId = values["Division_Id"];
                var batchId = values["Batch_Id"];

                var brcResult = Db.Branches.Where(x => x.Branch_Id.ToString() == branchId).Select(x => x.Branch_Name).FirstOrDefault();
                var divResult = Db.Divisions.Where(x => x.Division_Id.ToString() == divisionId).Select(x => x.Division_Name).FirstOrDefault();
                var btcResult = Db.Batches.Where(x => x.Batch_Id.ToString() == batchId).Select(x => x.Batch_Name).FirstOrDefault();

                var result = Db.Students.Where(x => x.Branch == brcResult && x.Division == divResult && x.Batch == btcResult && x.Semester == semNo).ToList();
                Subject result1 = Db.Subjects.Find(subId);
                foreach (var i in result)
                {
                    result1.Students.Remove(i);
                    Db.SaveChanges();
                }

                return RedirectToAction("ViewEnrolledStudent", new { semno = semNo, sid = subId, id = fid });
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            ViewBag.semno = semNo;
            return View();
        }

        public ActionResult DeleteIndivisualEnrolledStudent(int? id, int? semNo, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = Db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            ViewBag.semno = semNo;
            return View(student);
        }

        [HttpPost, ActionName("DeleteIndivisualEnrolledStudent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedIndivisualStudent(int id, int? semNo, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            Student student = Db.Students.Find(id);
            Subject subject = Db.Subjects.Find(subId);
            student.Subjects.Remove(subject);
            Db.SaveChanges();
            ViewBag.id = fid;
            ViewBag.sid = subId;
            ViewBag.semno = semNo;
            return RedirectToAction("ViewEnrolledStudent", new { semno = semNo, sid = subId, id = fid });
        }




        //Controlling Subjects
        public ActionResult ViewSubject(int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = id;
            return View(Db.Subjects.Where(x => x.Faculties.Any(y => y.faculty_id == id)).ToList().OrderBy(x=>x.semester));
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

                Faculty result = Db.Faculties.Find(fid);
                result.Subjects.Add(subject);
                Db.SaveChanges();
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
            Subject subject = Db.Subjects.Find(subId);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = fid;
            return View(subject);
        }

        [HttpPost, ActionName("DeleteSubject")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedSubject(int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = fid;
            Subject subject = Db.Subjects.Find(subId);
            Db.Subjects.Remove(subject);
            Db.SaveChanges();
            return RedirectToAction("ViewSubject", new { id = fid });
        }

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
            Subject subject = Db.Subjects.Find(subId);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = fid;
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubject([Bind(Include = "subject_name,semester")] Subject subject, int? subId, int? fid)
        {
            if (ModelState.IsValid)
            {
                var result = Db.Subjects.Where(x => x.subject_id == subId).FirstOrDefault();
                if (result != null)
                {
                    result.subject_name = subject.subject_name;
                    result.semester = subject.semester;
                    Db.Entry(result).State = EntityState.Modified;
                    Db.SaveChanges();
                }
                ViewBag.id = fid;
                return RedirectToAction("ViewSubject", new { id = fid });
            }
            return View(subject);
        }



        // Controlling Labs
        public ActionResult ViewLabs(int? sid, int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = id;
            ViewBag.sid = sid;
            ViewBag.subname = Db.Subjects.Where(x => x.subject_id == sid).Select(x => x.subject_name).FirstOrDefault();
            return View(Db.Labs.Where(x => x.subject_id == sid).ToList().OrderBy(x=>x.lab_no));
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
                Subject result = Db.Subjects.Find(subId);
                result.Labs.Add(lab);
                Db.SaveChanges();
                return RedirectToAction("ViewLabs", new { sid = subId, id = fid });
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
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
            Lab lab = Db.Labs.Find(labId);
            if (lab == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            return View(lab);
        }

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
            Lab lab = Db.Labs.Find(labId);
            Db.Labs.Remove(lab);
            Db.SaveChanges();
            return RedirectToAction("ViewLabs", new { sid = subId, id = fid });
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
            Lab lab = Db.Labs.Find(labId);
            if (lab == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            return View(lab);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLabs([Bind(Include = "lab_no, lab_startdate")] Lab lab, int? labId, int? subId, int? fid)
        {
            if (ModelState.IsValid)
            {
                var result = Db.Labs.Where(x => x.lab_id == labId).FirstOrDefault();
                if (result != null)
                {
                    result.lab_no = lab.lab_no;
                    result.lab_startdate = lab.lab_startdate;
                    Db.Entry(result).State = EntityState.Modified;
                    Db.SaveChanges();
                }
                ViewBag.id = fid;
                ViewBag.sid = subId;
                return RedirectToAction("ViewLabs", new { sid = subId, id = fid });
            }
            return View(lab);
        }



        // Controlling Assignments
        public ActionResult ViewAssignments(int? lid, int? sid, int? id)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = id;
            ViewBag.sid = sid;
            ViewBag.lid = lid;
            ViewBag.labno = Db.Labs.Where(x => x.lab_id == lid).Select(x => x.lab_no).FirstOrDefault();
            return View(Db.Assignments.Where(x => x.lab_id == lid).ToList().OrderBy(x=>x.assignment_no));
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
        public ActionResult CreateAssignments(Assignment assignment, int? labId, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (ModelState.IsValid && assignment != null)
            {
                Lab result = Db.Labs.Find(labId);
                result.Assignments.Add(assignment);
                Db.SaveChanges();
                return RedirectToAction("ViewAssignments", new { lid = labId, sid = subId, id = fid });
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            ViewBag.lid = labId;
           
            return View(assignment);
        }

        public ActionResult DeleteAssignments(int? asgId, int? labId, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (asgId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = Db.Assignments.Find(asgId);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            ViewBag.lid = labId;
            return View(assignment);
        }

        [HttpPost, ActionName("DeleteAssignments")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedAssignments(int? asgId, int? labId, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            ViewBag.lid = labId;
            Assignment assignment = Db.Assignments.Find(asgId);
            Db.Assignments.Remove(assignment);
            Db.SaveChanges();
            return RedirectToAction("ViewAssignments", new { lid = labId, sid = subId, id = fid });
        }

        public ActionResult EditAssignments(int? asgId, int? labId, int? subId, int? fid)
        {
            if (Session["facultyID"] == null)
            {
                return RedirectToAction("LoginFaculty");
            }
            if (asgId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = Db.Assignments.Find(asgId);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = fid;
            ViewBag.sid = subId;
            ViewBag.lid = labId;
            return View(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAssignments([Bind(Include = "assignment_no,assignment_text,assignment_enddate")] Assignment assignment, int? asgId, int? labId, int? subId, int? fid)
        {
            if (ModelState.IsValid)
            {
                var result = Db.Assignments.Where(x => x.assignment_id == asgId).FirstOrDefault();
                if (result != null)
                {
                    result.assignment_no = assignment.assignment_no;
                    result.assignment_text = assignment.assignment_text;
                    result.assignment_enddate = assignment.assignment_enddate;
                    Db.Entry(result).State = EntityState.Modified;
                    Db.SaveChanges();
                }
                ViewBag.id = fid;
                ViewBag.sid = subId;
                ViewBag.lid = labId;
                return RedirectToAction("ViewAssignments", new {lid = labId, sid = subId, id = fid });
            }
            return View(assignment);
        }



        //Partial Views

        [ChildActionOnly]
        public PartialViewResult RenderBranch()
        {
            var branchResult = Db.Branches.Select(x => new SelectListItem() { Text = x.Branch_Name, Value = x.Branch_Id.ToString() }).ToList();
            ViewBag.Branch = branchResult;
            return PartialView(GetBranchModel());
        }

        [ChildActionOnly]
        public PartialViewResult RenderDivision()
        {
            var divisionResult = Db.Divisions.Select(x => new SelectListItem() { Text = x.Division_Name, Value = x.Division_Id.ToString() }).ToList();
            ViewBag.Division = divisionResult;
            return PartialView(GetDivisionModel());
        }

        [ChildActionOnly]
        public PartialViewResult RenderBatch()
        {
            var batchResult = Db.Batches.Select(x => new SelectListItem() { Text = x.Batch_Name, Value = x.Batch_Id.ToString() }).ToList();
            ViewBag.Batch = batchResult;
            return PartialView(GetBatchModel());
        }



        //GetModels

        public Branch GetBranchModel()
        {
            Branch branchModel = new Branch();
            return (branchModel);
        }

        public Division GetDivisionModel()
        {
            Division divisionModel = new Division();
            return (divisionModel);
        }

        public Batch GetBatchModel()
        {
            Batch batchModel = new Batch();
            return (batchModel);
        }

        // Dispose
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}