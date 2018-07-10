using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_Management_API_MVC.Models.ExtendedModel
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
    }
}