using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace own_project.Controllers
{
    public class patientController : Controller
    {
        // GET: patient
        public ActionResult Index()
        {
            return View();
        }
    }
}