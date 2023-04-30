using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace own_project.DBconnection
{

    public class DBconnection
    {
        public SqlConnection con;
        public void connect()
        {
            string connect = ConfigurationManager.ConnectionStrings["projectdb"].ToString();
            con = new SqlConnection(connect);
        }
    }
}