using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using ReferralBonus.Models;
using System.Data;


namespace ReferralBonus.Views.Home
{
    /// <summary>
    /// Summary description for GetFeedOnDemand
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GetFeedOnDemand : System.Web.Services.WebService
    {
        string ConnectionName = "GS_Connect_online";
        [WebMethod]
        public void GetFeed(int pageNumber,int pageSize)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;

            List<Feed> Feeds = new List<Feed>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //con.Open();
                //SqlCommand cmd = new SqlCommand("spGetFeed_pageload", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@PageNumber",pageNumber);
                //cmd.Parameters.AddWithValue("@PageSize", pageSize);
                //SqlDataReader rdr = cmd.ExecuteReader();
                
                //while (rdr.Read())
                {
                    //Feed _feed = new Feed();
                 
                    //_feed.Title = rdr["Title"].ToString();
                    //_feed.Description = rdr["Description"].ToString();
                    //_feed.image = rdr["Images"].ToString();
                    //_feed.link = rdr["link"].ToString();
                    //_feed.CategoryID = (int)rdr["Category_Id"];
                    //_feed.Rowstatus = Convert.ToChar(rdr["Rowstatus"]);

                    //Feeds.Add(_feed);


                    Feed _feed = new Feed
                    {
                        Title="Test",
                        Description="Test",
                        image="test",
                        link="test",
                        CategoryID=2,
                        Rowstatus='A'
                    };

                  

                    Feeds.Add(_feed);
                }

            }

            JavaScriptSerializer Js = new JavaScriptSerializer();
            Context.Response.Write(Js.Serialize(Feeds));
        }

        [WebMethod]
        public Feed GetSingleFeed(int ID)
        {
            Feed _feed = new Feed();

            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from GS_FEED where FeedID="+ID+"", con);
                
       
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
      
                    _feed.ID = (int)rdr["FeedID"];
                    _feed.Title = rdr["Title"].ToString();
                    _feed.Description = rdr["Description"].ToString();
                    _feed.image = rdr["Images"].ToString();
                    _feed.link = rdr["link"].ToString();
                    _feed.CategoryID = (int)rdr["Category_Id"];
                    _feed.Rowstatus = Convert.ToChar(rdr["Rowstatus"]);


                }

            }

            return _feed;
        }
    }
}
