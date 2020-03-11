using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineTermWorkSubmission.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Faculty> Faculties { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<Lab> Labs { get; set; }

        public virtual DbSet<Assignment> Assignments { get; set; }
        
        public virtual DbSet<Branch> Branches { get; set; }

        public virtual DbSet<Division> Divisions { get; set; }

        public virtual DbSet<Batch> Batches { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
    }
    public class Student
    {
        [Key]
        [Display(Name = "Id")]
        public int Student_Id { get; set; }
        
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Student Name")]
        public string Student_Name { get; set; }
        
        [Required(ErrorMessage = "Branch is required.")]
        [Display(Name = "Branch")]
        public string Branch { get; set; }
        
        [Required(ErrorMessage = "Division is required.")]
        [Display(Name = "Division")]
        public string Division { get; set; }
        
        [Required]
        [Display(Name = "Batch")]
        public string Batch { get; set; }
        
        [Required(ErrorMessage = "RollNo is required.")]
        [Range(1,200)]
        [Display(Name = "Roll No")]
        public int Roll_No { get; set; }
        
        [Required(ErrorMessage = "Semester is required.")]
        [Range(1,8)]
        [Display(Name = "Semester")]
        public int Semester { get; set; }
        
        [Required(ErrorMessage = "Email id is required.")]
        [EmailAddress]
        [Display(Name = "Email Id")]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",ErrorMessage = "Email is required and must be properly formatted.")]
        public string Student_Email { get; set; }
        
        [Required(ErrorMessage = "Phone no is required.")]
        [Phone]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Student_Contact { get; set; }
        
        [Required(ErrorMessage = "Birth date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime Student_Dob { get; set; }
        
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"(?-i)(?=^.{8,}$)((?!.*\s)(?=.*[A-Z])(?=.*[a-z]))(?=(1)(?=.*\d)|.*[^A-Za-z0-9])^.*$", ErrorMessage = "Password must have at least 8 characters long. - At least 1 uppercase, AND at least 1 lowercase - At least 1 digit OR at least 1 alphanumeric. - No spaces.")]
        public string Student_Password { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }


    }

    public class Branch
    {
        [Key]
        public int Branch_Id { get; set; }
        public string Branch_Name { get; set; }

        public virtual ICollection<Division> Divisions { get; set; }
    }

    public class Division
    {
        [Key]
        public int Division_Id { get; set; }
        public string Division_Name { get; set; }

        public int Branch_Id { get; set; }
        [ForeignKey("Branch_Id")]
        public virtual Branch Branches { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
    }

    public class Batch
    {
        [Key]
        public int Batch_Id { get; set; }
        public string Batch_Name { get; set; }

        public int Division_Id { get; set; }
        [ForeignKey("Division_Id")]
        public virtual Division Divisions { get; set; }
    }

    
    public class Faculty
    {
        [Key]
        [Display(Name = "Id")]
        public int faculty_id { get; set; }
        
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Facluty Name")]
        public string faculty_name { get; set; }
        
        [Required(ErrorMessage = "Email id is required.")]
        [EmailAddress]
        [Display(Name = "Email Id")]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$", ErrorMessage = "Email is required and must be properly formatted.")]
        public string faculty_email { get; set; }
        
        [Required(ErrorMessage = "Phone no is required.")]
        [Display(Name = "Phone Number")]
        [Phone]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string faculty_contact { get; set; }
        
        [Required(ErrorMessage = "Birth date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime faculty_dob { get; set; }
        
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"(?-i)(?=^.{8,}$)((?!.*\s)(?=.*[A-Z])(?=.*[a-z]))(?=(1)(?=.*\d)|.*[^A-Za-z0-9])^.*$", ErrorMessage = "Password must have at least 8 characters long. - At least 1 uppercase, AND at least 1 lowercase - At least 1 digit OR at least 1 alphanumeric. - No spaces.")]
        public string faculty_password { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

    }

    public class Subject
    {
        [Key]
        [Display(Name = "Id")]
        public int subject_id { get; set; }
        
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Subject Name")]
        public string subject_name { get; set; }

        [Required(ErrorMessage = "Semester is required.")]
        [Range(1,8)]
        [Display(Name = "Semester")]
        public int semester { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Lab> Labs { get; set; }
    }

    public class Lab
    {
        [Key]
        [Display(Name = "Id")]
        public int lab_id { get; set; }
        
        [Required(ErrorMessage = "LabNo is required.")]
        [Range(1,50)]
        [Display(Name = "Lab No")]
        public int lab_no { get; set; }
        
        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime lab_startdate { get; set; }


        public int subject_id { get; set; }
        [ForeignKey("subject_id")]
        public virtual Subject Subjects { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

    }

    public class Assignment
    {
        [Key]
        [Display(Name = "Id")]
        public int assignment_id { get; set; }

        [Required(ErrorMessage = "Assignment no is required.")]
        [Range(1,50)]
        [Display(Name = "Assignment No")]
        public int assignment_no { get; set; }

        [Required(ErrorMessage = "Description can't be empty.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string assignment_text { get; set; }

        [Required(ErrorMessage = "Assignment enddate is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime assignment_enddate { get; set; }

        public int lab_id { get; set; }
        [ForeignKey("lab_id")]
        public virtual Lab Labs { get; set; }
    }
}