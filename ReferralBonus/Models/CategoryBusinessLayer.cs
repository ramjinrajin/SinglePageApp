using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ReferralBonus.Models
{
    public class CategoryBusinessLayer
    {
        string ConnectionName = "GS_Connect_online";
       


        public List<Category> Get_Category()
        {

             string connectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;

                List<Category> Categories = new List<Category>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select Category_Id,Category_Name from Gs_Category order by 1 asc", con);
                    cmd.CommandType = CommandType.Text;
                    


                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Category _Category = new Category();
                        _Category.Category_Id = (int)rdr["Category_Id"];
                        _Category.Category_Name = rdr["Category_Name"].ToString();


                        Categories.Add(_Category);
                    }
                }

                return Categories;
        }

    }
}