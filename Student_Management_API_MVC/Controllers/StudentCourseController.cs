using Student_Management_API_MVC.Models.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Student_Management_API_MVC.Controllers
{
    [RoutePrefix("api/v1/studentcourse")]
    public class StudentCourseController : ApiController
    {
        StudentDBEntities DB = new StudentDBEntities();


        [Route("search/{Searching?}"), HttpGet]
        public IHttpActionResult Search(String Searching)
        {
            try
            {
                List<StudentCourseDTO> model = DB.StudentCourses.Select(u => new StudentCourseDTO
                {
                    CourseCode = u.CourseCode,
                    StudentId = u.StudentId,
                    Grade=u.Grade
                }).ToList();

                if (!String.IsNullOrEmpty(Searching))
                {
                    model = DB.StudentCourses.Where(s =>
                   (s.CourseCode.ToString().Contains(Searching) ||
                   s.StudentId.ToString().Contains(Searching) ||
                   s.Grade.Contains(Searching)))
                   .Select(u => new StudentCourseDTO
                   {
                       CourseCode = u.CourseCode,
                       StudentId = u.StudentId,
                       Grade=u.Grade
                   }).ToList();
                }

                return Ok(model);
            }

            catch (Exception e)
            {
                return Ok(new { StatusCode = 200, e });
            }

        }

        [Route(""), HttpGet]
        public IHttpActionResult All()
        {
            try
            {
                List<StudentCourseDTO> model = DB.StudentCourses.Select(u => new StudentCourseDTO
                {
                    CourseCode = u.CourseCode,
                    StudentId = u.StudentId,
                    Grade = u.Grade
                }).ToList();
                return Ok(model);
            }

            catch (Exception e)
            {
                return Ok(new { StatusCode = 200, e });
            }

        }

        [Route("add"), HttpPost]
        public IHttpActionResult Add([FromBody]StudentCourseDTO sc)
        {
            try
            {
                if (sc != null)
                {
                    DB.StudentCourses.Add(new StudentCourse { CourseCode = sc.CourseCode, StudentId = sc.StudentId, Grade=sc.Grade });
                    DB.SaveChanges();
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return Ok(new { StatusCode = 200, e });
            }
        }

        [Route("edit"), HttpPut]
        public IHttpActionResult Edit([FromBody]StudentCourseDTO sc)
        {
            try
            {
                var t = DB.StudentCourses.Where(r => (r.CourseCode == sc.CourseCode && r.StudentId==sc.StudentId)).FirstOrDefault();
                t.Grade = sc.Grade;
                DB.SaveChanges();
                return Ok(true);
            }
            catch (Exception e)
            {
                return Ok(new { StatusCode = 200, e });
            }

        }

        [Route("delete/{code}/{id}"), HttpDelete]
        public IHttpActionResult Delete(int code,int id)
        {
            try
            {
                var t = DB.StudentCourses.Where(r => (r.CourseCode == code && r.StudentId == id)).FirstOrDefault();
                DB.StudentCourses.Remove(t);
                DB.SaveChanges();
                return Ok(true);
            }
            catch (Exception e)
            {
                return Ok(new { StatusCode = 200, e });
            }
        }
    }
}
