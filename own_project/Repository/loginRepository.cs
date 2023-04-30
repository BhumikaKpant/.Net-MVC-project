using Dapper;
using own_project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace own_project.Repository
{
    public class loginRepository
    {
        string proc = "pprocedure";
        DBconnection.DBconnection db = new DBconnection.DBconnection();


        public List<login1> getloginlist(login1 lo)
        {
            db.connect();
            try
            {
                db.con.Open();
                DynamicParameters param = new DynamicParameters();

                param.Add("@flag", lo.flag);
                param.Add("@userid", lo.userid);
                param.Add("@username", lo.username);
                param.Add("@userpassword", lo.userpassword);
                param.Add("@useremail", lo.userid);
                //param.Add("@useraddress", lo.useraddress);
                //param.Add("@usercontact", lo.usercontact);

                var data = SqlMapper.Query<login1>(db.con, "loginprocedure", param, commandType: CommandType.StoredProcedure).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.con.Close();
            }

        }


        public login1 Insert(login1 lrm)
        {
            db.connect();
            try
            {
                db.con.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@userid", lrm.userid);
                param.Add("@username", lrm.username);
                param.Add("@userpassword", lrm.userpassword);
                param.Add("@useremail", lrm.useremail);
                param.Add("@useraddress", lrm.useraddress);
                param.Add("@usercontact", lrm.usercontact);
                param.Add("@entrydate", lrm.entrydate);
                param.Add("@flag", "insert");
                var data = SqlMapper.Query<login1>(db.con, "loginprocedure", param, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.con.Close();
            }
        }






        //to reset password
        public string check(login1 u)
        {
            db.connect();
            try
            {
                db.con.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@id", u.id);
                param.Add("@userid", u.userid);
                param.Add("@authcode", u.authcode);
                param.Add("@validate", u.validate);
                param.Add("@useremail", u.useremail);
                param.Add("@userpassword", u.userpassword);
                param.Add("@flag", "list");

                var data = SqlMapper.Query<string>(db.con, proc, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return data;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                db.con.Close();

            }

        }




        //send mail to user

        public string insert(login1 u)
        {



            db.connect();
            try
            {
                db.con.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@id", u.id);
                param.Add("@userid", u.userid);
                param.Add("@authcode", u.authcode);
                param.Add("@validate", u.validate);
                param.Add("@useremail", u.useremail);
                param.Add("@userpassword", u.userpassword);
                param.Add("@flag", "Insert");

                var data = SqlMapper.Query<string>(db.con, proc, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return data;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                db.con.Close();

            }


        }


        //to update password in database
        public string update(login1 u)
        {
            db.connect();
            try
            {
                db.con.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@id", u.id);
                param.Add("@userid", u.userid);
                param.Add("@authcode", u.authcode);
                param.Add("@validate", u.validate);
                param.Add("@useremail", u.useremail);
                param.Add("@userpassword", u.userpassword);
                param.Add("@flag", "update");

                var data = SqlMapper.Query<string>(db.con, proc, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return data;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                db.con.Close();

            }

        }




        //to send a mail after the  password being resetted

        public string resendmail(login1 u)
        {
            db.connect();
            try
            {
                db.con.Open();
                DynamicParameters param = new DynamicParameters();
               
                param.Add("@authcode", u.authcode);
              
                param.Add("@flag", "message");
                var data = SqlMapper.Query<string>(db.con, proc, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.con.Close();
            }
        }



    }
}