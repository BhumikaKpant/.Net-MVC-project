using Dapper;
using own_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace own_project.Repository
{
    public class departmentRepository
    {
        DBconnection.DBconnection db = new DBconnection.DBconnection();


        public List<departmentmodel> getdepartmentlist()
        {
            db.connect();
            try
            {
                db.con.Open();
                DynamicParameters param = new DynamicParameters();

                param.Add("@flag", "d_list");

                var data = SqlMapper.Query<departmentmodel>(db.con, "departmentprocedure", param, commandType: System.Data.CommandType.StoredProcedure).ToList();
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


       

        public departmentmodel Insertdata(departmentmodel de)
        {

            db.connect();
            try
            {
                db.con.Open();
                
                
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@flag", de.flag);
                    param.Add("@departmentid", de.departmentid);
                    param.Add("@departmentname", de.departmentname);
                    param.Add("@image", de.image);
                    param.Add("@doctorid", de.doctorid);
                    param.Add("@doctorname", de.doctorname);
                    param.Add("@sid", de.sid);
                    param.Add("@dspecialization", de.dspecialization);
                    //param.Add("@dimage", de.dimage);
                    param.Add("@patientid", de.patientid);
                    param.Add("@patientname", de.patientname);
                    param.Add("@patientgender", de.patientgender);
                    param.Add("@listid", de.listid);
                    var data = SqlMapper.Query<departmentmodel>(db.con, "departmentprocedure", param, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
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




        public List<departmentmodel> getdoctorlist(int doctid)
        {
            db.connect();
            try
            {
                db.con.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@depid", doctid);
                param.Add("@flag", "do_select_all_data");

                var data = SqlMapper.Query<departmentmodel>(db.con, "departmentprocedure", param, commandType: System.Data.CommandType.StoredProcedure).ToList();
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





        public List<departmentmodel> getpatientlist(int patid)
        {
            db.connect();
            try
            {
                db.con.Open();

                DynamicParameters param = new DynamicParameters();
                param.Add("@docid", patid);
                param.Add("@flag", "p_select_all_data");

                var data = SqlMapper.Query<departmentmodel>(db.con, "departmentprocedure", param, commandType: System.Data.CommandType.StoredProcedure).ToList();
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


        public List<departmentmodel> getdepartmentlist1()
        {
            db.connect();
            try
            {
                db.con.Open();
                DynamicParameters param = new DynamicParameters();

                param.Add("@flag", "select_all_data");

                var data = SqlMapper.Query<departmentmodel>(db.con, "departmentprocedure", param, commandType: System.Data.CommandType.StoredProcedure).ToList();
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




        public List<departmentmodel> getspecializationlist(int spid)
        {
            db.connect();
            try
            {
                db.con.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@doctorid", spid);
                param.Add("@flag", "s_list");

                var data = SqlMapper.Query<departmentmodel>(db.con, "departmentprocedure", param, commandType: System.Data.CommandType.StoredProcedure).ToList();
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


            

    


