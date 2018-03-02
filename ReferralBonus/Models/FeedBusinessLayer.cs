using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace ReferralBonus.Models
{
    public class FeedBusinessLayer
    {
        string ConnectionName = "GS_Connect_online";
        public IEnumerable<Feed> Feeds
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;

                List<Feed> Feeds = new List<Feed>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from GS_FEED WHERE Rowstatus='A' order by 1 desc", con);
                    cmd.CommandType = CommandType.Text;
                    
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Feed _feed = new Feed();
                        _feed.ID = (int)rdr["FeedID"];
                        _feed.Title = rdr["Title"].ToString();
                        _feed.Description = rdr["Description"].ToString();
                        _feed.image = rdr["Images"].ToString();
                        _feed.link = rdr["link"].ToString();
                        _feed.CategoryID =(int)rdr["Category_Id"];
                        _feed.Rowstatus =Convert.ToChar(rdr["Rowstatus"]);

                        Feeds.Add(_feed);
                    }
                }

                return Feeds;
            }
        }



        public IEnumerable<Feed> GetEditFeeds
        {
            
            get
            {
               
                string connectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;

                List<Feed> Feeds = new List<Feed>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from GS_FEED order by 1 desc", con);
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Feed _feed = new Feed();
                        _feed.ID = (int)rdr["FeedID"];
                        _feed.Title = rdr["Title"].ToString();
                        _feed.Description = rdr["Description"].ToString();
                        _feed.image = rdr["Images"].ToString();
                        _feed.link = rdr["link"].ToString();
                        _feed.CategoryID = (int)rdr["Category_Id"];
                        _feed.Rowstatus = Convert.ToChar(rdr["Rowstatus"]);

                        Feeds.Add(_feed);
                    }
                }

                return Feeds;
            }
        }


        public void DeleteFeed(int FeedID)
        {
            string Connection_string = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
            using (SqlConnection conn = new SqlConnection(Connection_string))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM GS_FEED WHERE FEEDID=@feed_id", conn);
                cmd.CommandType = CommandType.Text;

                if (conn.State == ConnectionState.Open)
                {
                    try
                    {
                        SqlParameter paramfeedid = new SqlParameter();
                        paramfeedid.ParameterName = "@feed_id";
                        paramfeedid.Value = FeedID;
                        cmd.Parameters.Add(paramfeedid);

                       
                        cmd.ExecuteNonQuery();

                    }

                    catch 
                    {

                        throw;
                    }
                }







            }
        }

        public string DeleteImage(int FeedID)
        {
            string FileName = "";
             string Connection_string = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
             using (SqlConnection conn = new SqlConnection(Connection_string))
             {
                 conn.Open();
                 SqlCommand cmd = new SqlCommand("SELECT TOP 1 Images FROM GS_FEED WHERE FEEDID=@feed_id", conn);
                   SqlParameter paramfeedid = new SqlParameter();
                        paramfeedid.ParameterName = "@feed_id";
                        paramfeedid.Value = FeedID;
                        cmd.Parameters.Add(paramfeedid);
                 SqlDataReader rdr = cmd.ExecuteReader();
                 while (rdr.Read())
                 {
                     FileName = rdr[0].ToString();
                 }
             }

             return FileName;
        }


        public void UpdateFeed(Feed feed_cs)
        {
            string Connection_string = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
            using (SqlConnection conn = new SqlConnection(Connection_string))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("USP_FeedUPDATE", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (conn.State == ConnectionState.Open)
                {
                    try
                    {
                        SqlParameter paramTitle = new SqlParameter();
                        paramTitle.ParameterName = "@Title";
                        paramTitle.Value = feed_cs.Title;
                        cmd.Parameters.Add(paramTitle);

                        SqlParameter ParamDesc = new SqlParameter();
                        ParamDesc.ParameterName = "@Description";
                        ParamDesc.Value = feed_cs.Description;
                        cmd.Parameters.Add(ParamDesc);


                        SqlParameter ParamImage = new SqlParameter();
                        ParamImage.ParameterName = "@Images";
                        ParamImage.Value = feed_cs.image;
                        cmd.Parameters.Add(ParamImage);

                        SqlParameter Paramlink = new SqlParameter();
                        Paramlink.ParameterName = "@link";
                        Paramlink.Value = feed_cs.link;
                        cmd.Parameters.Add(Paramlink);


                        SqlParameter ParamID = new SqlParameter();
                        ParamID.ParameterName = "@FeedID";
                        ParamID.Value = feed_cs.ID;
                        cmd.Parameters.Add(ParamID);

                        SqlParameter ParamRowStatus = new SqlParameter();
                        ParamRowStatus.ParameterName = "@Rowstatus";
                        ParamRowStatus.Value = feed_cs.Rowstatus;
                        cmd.Parameters.Add(ParamRowStatus);

                        SqlParameter ParamCategory_Id = new SqlParameter();
                        ParamCategory_Id.ParameterName = "@Category_Id";
                        ParamCategory_Id.Value = feed_cs.CategoryID;
                        cmd.Parameters.Add(ParamCategory_Id);


                        cmd.ExecuteNonQuery();

                    }

                    catch (Exception ex)
                    {

                        throw;
                    }
                }







            }
        }


        public void AddFeed(Feed feed_cs)
        {

            string Connection_string = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
            using (SqlConnection conn = new SqlConnection(Connection_string))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("USP_FeedInsert", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (conn.State == ConnectionState.Open)
                {
                    try
                    {
                        //var HtmlDescription = HttpUtility.HtmlEncode();


                        SqlParameter paramTitle = new SqlParameter();
                        paramTitle.ParameterName = "@Title";
                        paramTitle.Value = feed_cs.Title;
                        cmd.Parameters.Add(paramTitle);

                        SqlParameter ParamDesc = new SqlParameter();
                        ParamDesc.ParameterName = "@Description";
                        ParamDesc.Value = feed_cs.Description;
                        cmd.Parameters.Add(ParamDesc);


                        SqlParameter ParamImage = new SqlParameter();
                        ParamImage.ParameterName = "@Images";
                        ParamImage.Value = feed_cs.image;
                        cmd.Parameters.Add(ParamImage);

                        SqlParameter Paramlink = new SqlParameter();
                        Paramlink.ParameterName = "@link";
                        Paramlink.Value = feed_cs.link;
                        cmd.Parameters.Add(Paramlink);


                        SqlParameter ParamCategory_Id = new SqlParameter();
                        ParamCategory_Id.ParameterName = "@Category_Id";
                        ParamCategory_Id.Value = feed_cs.CategoryID;
                        cmd.Parameters.Add(ParamCategory_Id);

                       
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