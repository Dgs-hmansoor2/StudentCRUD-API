using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_Management_API_MVC.Models.DTOModels
{
    public class StudentCourseDTO
    {
        public int CourseCode { get; set; }
        public int StudentId { get; set; }
        public string Grade { get; set; }
    }
}