using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace own_project.Models
{
    public class departmentmodel
    {

        public int departmentid { get; set; }
        public string departmentname { get; set; }

        public string image { get; set; }   
        public int doctorid { get; set; }
        public string doctorname { get; set; }
        public string dspecialization { get; set; }
        public string dimage { get; set; }
       
        public int patientid { get; set; }
        public string patientname { get; set; }
        public string patientgender { get; set; }
        public int sid { get; set; }
        public int listid { get; set; }


        public string flag { get; set;}
    }
}