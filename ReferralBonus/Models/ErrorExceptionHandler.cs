using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ReferralBonus.Models
{
    public class ErrorExceptionHandler
    {
        string ConnectionName = "GS_Connect_online";
        public void ReportError(string ModuleName,string Expection,string ActionName)
        {

            string Connection_string = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
            using (SqlConnection conn = new SqlConnection(Connection_string))
            {
                conn.Open();
                //SqlCommand cmd = new SqlCommand("USP_LOG", conn);
                SqlCommand cmd = new SqlCommand("INSERT INTO GS_LOG (ModuleName,ActionName,Exception) VALUES (@ModuleName ,@ActionName,@Exception)", conn);
                cmd.CommandType = CommandType.Text;

                if (conn.State == ConnectionState.Open)
                {
                    try
                    {
                        SqlParameter paramModuleName = new SqlParameter();
                        paramModuleName.ParameterName = "@ModuleName";
                        paramModuleName.Value = ModuleName;
                        cmd.Parameters.Add(paramModuleName);

                        SqlParameter ParamActionName = new SqlParameter();
                        ParamActionName.ParameterName = "@ActionName";
                        ParamActionName.Value = ActionName;
                        cmd.Parameters.Add(ParamActionName);


                        SqlParameter ParamExpection = new SqlParameter();
                        ParamExpection.ParameterName = "@Exception";
                        ParamExpection.Value = Expection;
                        cmd.Parameters.Add(ParamExpection);

                   


                        cmd.ExecuteNonQuery();

                    }

                    catch
                    {

                        throw;
                    }
                }







            }

        }


        public void Webservice_initialize(string ModuleName)
        {

            string Connection_string = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
            using (SqlConnection conn = new SqlConnection(Connection_string))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Gs_Webservice (ModuleName) VALUES (@ModuleName)", conn);
                cmd.CommandType = CommandType.Text;

                if (conn.State == ConnectionState.Open)
                {
                    try
                    {
                        SqlParameter paramModuleName = new SqlParameter();
                        paramModuleName.ParameterName = "@ModuleName";
                        paramModuleName.Value = ModuleName;
                        cmd.Parameters.Add(paramModuleName);
                        cmd.ExecuteNonQuery();

                    }

                    catch
                    {

                        throw;
                    }
                }







            }

        }






    }
}