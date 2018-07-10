using Student_Management_API_MVC.Models.ExtendedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Student_Management_API_MVC.Controllers
{
    [RoutePrefix("api/v1/student")]
    public class StudentController : ApiController
    {
        StudentDBEntities DB = new StudentDBEntities();


        [Route("search/{Searching}"),HttpGet]
        public IHttpActionResult Search(String Searching)
        {
            try
            {
                List<StudentDTO> model = DB.Students.Select(u => new StudentDTO
                {
                    Id = u.Id,
                    Age = u.Age,
                    Email = u.Email,
                    Fname = u.Fname,
                    Lname = u.Lname
                }).ToList();

                if (!String.IsNullOrEmpty(Searching))
                {
                    model = DB.Students.Where(s =>
                   (s.Id.ToString().Contains(Searching) ||
                   s.Age.ToString().Contains(Searching) ||
                   s.Email.Contains(Searching) ||
                   s.Fname.Contains(Searching) ||
                   s.Lname.Contains(Searching))).Select(u => new StudentDTO
                   {
                       Id = u.Id,
                       Age = u.Age,
                       Email = u.Email,
                       Fname = u.Fname,
                       Lname = u.Lname
                   }).ToList();
                }

                return Ok(model);
            }

            catch(Exception e)
            {
                return Ok(new { StatusCode = 200, e });
            }
            
        }

        [Route(""),HttpGet]
        public IHttpActionResult All()
        {
            try
            {
                List<StudentDTO> model = DB.Students.Select(u => new StudentDTO
                {
                    Id = u.Id,
                    Age = u.Age,
                    Email = u.Email,
                    Fname = u.Fname,
                    Lname = u.Lname
                }).ToList();
                return Ok(model);
            }

            catch(Exception e)
            {
                return Ok(new { StatusCode=200, e});
            }
            
        }

        [Route("add"),HttpPost]
        public IHttpActionResult Add([FromBody]StudentDTO student)
        {
            try
            {
                if (student != null)
                {
                    DB.Students.Add(new Student { Id = student.Id, Email = student.Email, Fname = student.Fname, Lname = student.Lname, Age = student.Age });
                    DB.SaveChanges();
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return Ok(new { StatusCode = 200, e });
            }
        }

        [Route("edit"),HttpPut]
        public IHttpActionResult Edit([FromBody]StudentDTO student)
        {
            try
            {
                var s = DB.Students.Where(r => r.Id == student.Id).FirstOrDefault();
                s.Fname = student.Fname;
                s.Lname = student.Lname;
                s.Email = student.Email;
                s.Age = student.Age;
                DB.SaveChanges();
                return Ok(true);
            }
            catch(Exception e)
            {
                return Ok(new { StatusCode = 200, e });
            }
            
        }

        [Route("delete/{id}"),HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var s = DB.Students.Where(r => r.Id == id).FirstOrDefault();
                DB.Students.Remove(s);
                DB.SaveChanges();
                return Ok(true);
            }
            catch(Exception e)
            {
                return Ok(new { StatusCode = 200, e });
            }
        }
    }
    
}
