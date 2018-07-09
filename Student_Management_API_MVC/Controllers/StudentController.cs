using Student_Management_API_MVC.Models.ExtendedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Student_Management_API_MVC.Controllers
{
    public class StudentController : ApiController
    {
        StudentDBEntities DB = new StudentDBEntities();


        [HttpGet]
        public IEnumerable<StudentApplication> Index(String Searching)
        {
            List<StudentApplication> model = DB.Students.Where(s =>
            (s.Id.ToString().Contains(Searching) ||
            s.Age.ToString().Contains(Searching) ||
            s.Email.Contains(Searching) ||
            s.Fname.Contains(Searching) ||
            s.Lname.Contains(Searching))).Select(u => new StudentApplication { Id=u.Id,Age = u.Age,
                Email =u.Email,
                Fname =u.Fname,
                Lname =u.Lname}).ToList();
            return model;
        }

        [HttpGet]
        public IEnumerable<StudentApplication> All()
        {
            List<StudentApplication> model = DB.Students.Select(u => new StudentApplication
            {
                Id = u.Id,
                Age = u.Age,
                Email = u.Email,
                Fname = u.Fname,
                Lname = u.Lname
            }).ToList();
            return model;
        }

        [HttpPost]
        Boolean Add(StudentApplication student)
        {
            if(student!=null)
            {
                DB.Students.Add(new Student { Id = student.Id, Email = student.Email, Fname = student.Fname, Lname = student.Lname, Age = student.Age });
                DB.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpPut]
        public Boolean Edit(StudentApplication student)
        {
                
            var s = DB.Students.Where(r => r.Id == student.Id).FirstOrDefault();
            if(s!=null)
            {
                s.Fname = student.Fname;
                s.Lname = student.Lname;
                s.Email = student.Email;
                s.Age = student.Age;
                DB.SaveChanges();

                return true;
            }
            return false;
        }

        [HttpDelete]
        public Boolean Delete(int id)
        {

            var s = DB.Students.Where(r => r.Id == id).FirstOrDefault();
            if (s != null)
            {
                DB.Students.Remove(s);
                return true;
            }
            return false;
        }
    }
    
}
