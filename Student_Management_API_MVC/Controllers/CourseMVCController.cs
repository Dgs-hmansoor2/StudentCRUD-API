using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student_Management_API_MVC.Controllers
{
    public class CourseMVCController : Controller
    {
        // GET: CourseMVC
        public ActionResult Index()
        {
            return View();
        }
    }
}