using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace own_project.Models
{
    public class login1
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string userpassword { get; set; }
        public string useremail { get; set; }
        public string useraddress { get; set; }
        public string usercontact { get; set; }
        public int id { get; set; }
        public bool validate { get; set; }
        public string authcode { get; set; }
        public DateTime entrydate { get; set; }
        public string flag { get; set; }
    }
}