using Student_Management_API_MVC.Models.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Student_Management_API_MVC.Controllers
{
    [RoutePrefix("api/v1/course")]
    public class CourseController : ApiController
    {
        StudentDBEntities DB = new StudentDBEntities();


        [Route("search/{Searching}"), HttpGet]
        public IHttpActionResult Search(String Searching)
        {
            try
            {
                List<CourseDTO> model = DB.Courses.Select(u => new CourseDTO
                {
                    Code = u.Code,
                   Cname = u.Cname
                }).ToList();

                if (!String.IsNullOrEmpty(Searching))
                {
                    model = DB.Courses.Where(s =>
                   (s.Code.ToString().Contains(Searching) ||
                   s.Cname.Contains(Searching)))
                   .Select(u => new CourseDTO
                   {
                       Code=u.Code,
                       Cname=u.Cname
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
                List<CourseDTO> model = DB.Courses.Select(u => new CourseDTO
                {
                    Code=u.Code,
                    Cname=u.Cname
                }).ToList();
                return Ok(model);
            }

            catch (Exception e)
            {
                return Ok(new { StatusCode = 200, e });
            }

        }

        [Route("add"), HttpPost]
        public IHttpActionResult Add([FromBody]CourseDTO course)
        {
            try
            {
                if (course != null)
                {
                    DB.Courses.Add(new Cours { Code=course.Code,Cname=course.Cname });
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
        public IHttpActionResult Edit([FromBody]CourseDTO course)
        {
            try
            {
                var c = DB.Courses.Where(r => r.Code == course.Code).FirstOrDefault();  
                c.Cname = course.Cname;
                DB.SaveChanges();
                return Ok(true);
            }
            catch (Exception e)
            {
                return Ok(new { StatusCode = 200, e });
            }

        }

        [Route("delete/{code}"), HttpDelete]
        public IHttpActionResult Delete(int code)
        {
            try
            {
                var c = DB.Courses.Where(r => r.Code == code).FirstOrDefault();
                DB.Courses.Remove(c);
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
