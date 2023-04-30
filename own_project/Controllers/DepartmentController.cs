using own_project.Models;
using own_project.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace own_project.Controllers
{
    public class DepartmentController : Controller
    {
        departmentRepository drepo= new departmentRepository(); 
        // GET: Department
        public ActionResult Index()
        {

            departmentmodel cm= new departmentmodel();
            cm.flag = "d_list";
          
            ViewBag.dlist = drepo.getdepartmentlist();
            //ViewBag.slist = drepo.getspecializationlist();
            return View();

        }

        public JsonResult dList (int doctid)
        {
            departmentmodel cm= new departmentmodel();
            cm.flag = "do_select_all_data";
            var data = drepo.getdoctorlist(doctid);
            return Json(data ,JsonRequestBehavior.AllowGet);
        }


        public JsonResult pList(int patid)
        {
            departmentmodel cm = new departmentmodel();
            cm.flag = "p_select_all_data";
            var data = drepo.getpatientlist(patid);
             return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sList(int spid)
        {
            departmentmodel cm = new departmentmodel();
            cm.flag = "s_list";
            var data = drepo.getspecializationlist(spid);
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        [HttpPost, ValidateInput(false)]
        public JsonResult Insert(HttpPostedFileBase ImageFile, departmentmodel st)
        {
            st.flag = "d_insert";

            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            fileName = fileName + extension;
            st.image = "/photo/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/photo/"), fileName);
           
            drepo.Insertdata(st);

            ImageFile.SaveAs(fileName);
            return Json(1, JsonRequestBehavior.AllowGet);
        }





        //public JsonResult List()
        //{
        //    studentModel stm = new studentModel();
        //    stm.flag = "select";
        //    var data = strepo.list_studnet(stm);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetList2()
       {

            departmentmodel um = new departmentmodel();
            um.flag = "d_list";
            var data = drepo.getdepartmentlist1().Select(s => new
            {
                departmentid = s.departmentid,
                departmentname = s.departmentname,
                image=s.image,
                doctorname = s.doctorname,
                patientname = s.patientname,
                sid=s.sid,
                dspecialization = s.dspecialization,  
                doctorid =s.doctorid,

                patientid=s.patientid,  
                listid=s.listid,    

            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Delete(int id)
        {
            departmentmodel aj = new departmentmodel();
            aj.flag = "d_delete";
            aj.listid = id;
            drepo.Insertdata(aj);
            return Json(JsonRequestBehavior.AllowGet);
        }





















        [HttpGet]
        public JsonResult Details(int id)
        {
            departmentmodel aj = new departmentmodel();
            aj.listid = id;
            aj.flag = "d_get_by_id";
            var data = drepo.Insertdata(aj);
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult Update(departmentmodel aj)
        {
            aj.flag = "d_update";
             drepo.Insertdata(aj);
            return Json(JsonRequestBehavior.AllowGet);
        }


        
    }
}