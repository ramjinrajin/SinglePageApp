using ReferralBonus.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace ReferralBonus.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult admin()
        {
            // Use this for an action
            return RedirectToAction("ManageFeed");
            // Use this for a URL

        }

        public ActionResult test()
        {
            return View();

        }


        //Expection handled
        [AllowAnonymous]
        public ActionResult Index(string search, int? page, int? categoryId)
        {


            try
            {
                ViewBag.SelectedItem = categoryId;
                CategoryBusinessLayer CatBussLayer = new CategoryBusinessLayer();
                ViewBag.Categories = new SelectList(CatBussLayer.Get_Category(), "Category_Id", "Category_Name", "2");


                if (categoryId != null)
                {

                    if (categoryId == 1)
                    {
                        FeedBusinessLayer FeedBusLayer = new FeedBusinessLayer();
                        List<Feed> feeds = FeedBusLayer.Feeds.ToList();
                        return View(feeds.ToList().ToPagedList(page ?? 1, 15));
                    }
                    else
                    {
                        FeedBusinessLayer FeedBusLayer = new FeedBusinessLayer();
                        List<Feed> feeds = FeedBusLayer.Feeds.ToList();
                        return View(feeds.Where(x => x.CategoryID == categoryId).ToList().ToPagedList(page ?? 1, 15));
                    }

                }

                else if (search == null)
                {


                    FeedBusinessLayer FeedBusLayer = new FeedBusinessLayer();
                    List<Feed> feeds = FeedBusLayer.Feeds.ToList();
                    return View(feeds.ToList().ToPagedList(page ?? 1, 15));
                }

                else
                {
                    FeedBusinessLayer FeedBusLayer = new FeedBusinessLayer();
                    List<Feed> feeds = FeedBusLayer.Feeds.ToList();
                    //return View(feeds.Where(x => x.Title.StartsWith(search)).ToList().ToPagedList(page ??5,1));
                    return View(feeds.Where(x => x.Title.ToLowerInvariant().Contains(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, 15));
                }
            }


            catch (Exception ex)
            {
                string Module_name = "Home_index";
                string Action = "Read all post";
                ErrorExceptionHandler _handler = new ErrorExceptionHandler();
                _handler.ReportError(Module_name, ex.Message, Action);
                return RedirectToAction("index", "Exception");
            }




        }




        //Expection handled
        //two actions get and post. get user request the server to render a page to get data (i.e columns for the crete forum. (it call feed.cs for columns).
        // we user enters data, post is called.
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                CategoryBusinessLayer CatBussLayer = new CategoryBusinessLayer();
                ViewBag.Categories = new SelectList(CatBussLayer.Get_Category(), "Category_Id", "Category_Name");
                return View();
            }
            catch (Exception ex)
            {
                string Module_name = "Create";
                string Action = "Create_Get Request";
                ErrorExceptionHandler _handler = new ErrorExceptionHandler();
                _handler.ReportError(Module_name, ex.Message, Action);
                return RedirectToAction("index", "Exception");
            }


        }


        [Authorize]
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Save()
        {


            Feed feed_cs = new Feed();
            TryUpdateModel(feed_cs);

            //feed_cs.CategoryID = 1;   //We are manually setting Cate ID ,hidden in html   

            //if (ModelState.IsValid)
            try
            {
                if (feed_cs.File != null)
                {
                    if (feed_cs.File.ContentLength > 0)
                    {


                        var fileName = Path.GetFileName(feed_cs.File.FileName);

                        var path = Path.Combine(Server.MapPath("~/img"), fileName);
                        feed_cs.File.SaveAs(path);
                        feed_cs.image = fileName;
                    }
                    else
                    {
                        feed_cs.image = "NIL";
                    }
                }
                else
                {
                    feed_cs.image = "NIL";
                }


                FeedBusinessLayer feedBusLayer = new FeedBusinessLayer();
                feedBusLayer.AddFeed(feed_cs);



                TempData["AlertMessage"] = "Hai this is working";
                return RedirectToAction("create");


            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "Error";
                string Module_name = "Home_Create";
                string Action = "Create new post";
                ErrorExceptionHandler _handler = new ErrorExceptionHandler();
                _handler.ReportError(Module_name, ex.Message, Action);
                return RedirectToAction("index", "Exception");
            }










        }


        [Authorize]
        [HttpGet]
        public ActionResult ManageFeed(string search, int? page, int? categoryId)
        {

            try
            {
                //CategoryBusinessLayer CatBussLayer = new CategoryBusinessLayer();
                //ViewBag.Categories = new SelectList(CatBussLayer.Get_Category, "Category_Id", "Category_Name");

                ViewBag.SelectedItem = categoryId;
                CategoryBusinessLayer CatBussLayer = new CategoryBusinessLayer();
                ViewBag.Categories = new SelectList(CatBussLayer.Get_Category(), "Category_Id", "Category_Name", "2");

                //FeedBusinessLayer feedBusLayer = new FeedBusinessLayer();
                //List<Feed> _feeds = feedBusLayer.GetEditFeeds.ToList();

                //return View(_feeds.ToList());

                if (categoryId != null)
                {

                    if (categoryId == 1)
                    {
                        FeedBusinessLayer FeedBusLayer = new FeedBusinessLayer();
                        List<Feed> feeds = FeedBusLayer.GetEditFeeds.ToList();
                        return View(feeds.ToList().ToPagedList(page ?? 1, 8));
                    }
                    else
                    {
                        FeedBusinessLayer FeedBusLayer = new FeedBusinessLayer();
                        List<Feed> feeds = FeedBusLayer.GetEditFeeds.ToList();
                        return View(feeds.Where(x => x.CategoryID == categoryId).ToList().ToPagedList(page ?? 1, 15));
                    }

                }

                else if (search == null)
                {


                    FeedBusinessLayer FeedBusLayer = new FeedBusinessLayer();
                    List<Feed> feeds = FeedBusLayer.GetEditFeeds.ToList();
                    return View(feeds.ToList().ToPagedList(page ?? 1, 8));
                }

                else
                {
                    FeedBusinessLayer FeedBusLayer = new FeedBusinessLayer();
                    List<Feed> feeds = FeedBusLayer.GetEditFeeds.ToList();
                    //return View(feeds.Where(x => x.Title.StartsWith(search)).ToList().ToPagedList(page ??5,1));
                    return View(feeds.Where(x => x.Title.ToLowerInvariant().Contains(search) || search == null).ToList().ToPagedList(page ?? 1, 15));
                }

            }

            catch (Exception ex)
            {
                TempData["AlertMessage"] = "Error";
                string Module_name = "ManageFeed";
                string Action = "ManageFeed_GetAllFeeds_index";
                ErrorExceptionHandler _handler = new ErrorExceptionHandler();
                _handler.ReportError(Module_name, ex.Message, Action);
                return RedirectToAction("index", "Exception");
            }






        }



        [Authorize]
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            try
            {
                FeedBusinessLayer _feedBusLayer = new FeedBusinessLayer();
                string fileName = _feedBusLayer.DeleteImage(Id);
                var path = Path.Combine(Server.MapPath("~/img"), fileName);


                if ((System.IO.File.Exists(path)))
                {
                    System.IO.File.Delete(path);
                }


                _feedBusLayer.DeleteFeed(Id);
                return RedirectToAction("ManageFeed", "Home");
            }

            catch (Exception ex)
            {
                TempData["AlertMessage"] = "Error";
                string Module_name = "ManageFeed";
                string Action = "ManageFeed_Delete";
                ErrorExceptionHandler _handler = new ErrorExceptionHandler();
                _handler.ReportError(Module_name, ex.Message, Action);
                return RedirectToAction("index", "Exception");
            }




        }

        [Authorize]
        [HttpGet]//when press edit button
        public ActionResult Edit(int Id)
        {
            try
            {
                FeedBusinessLayer _feedBusLayer = new FeedBusinessLayer();
                Feed _feed = _feedBusLayer.GetEditFeeds.Single(fd => fd.ID == Id);
                return View(_feed);
            }



            catch (Exception ex)
            {
                TempData["AlertMessage"] = "Error";
                string Module_name = "ManageFeed_Edit";
                string Action = "Get";
                ErrorExceptionHandler _handler = new ErrorExceptionHandler();
                _handler.ReportError(Module_name, ex.Message, Action);
                return RedirectToAction("index", "Exception");
            }

        }





        [Authorize]
        [HttpPost]//when press edit button
        [ActionName("Edit")]
        public ActionResult Update(Feed _Feed)
        {
            FeedBusinessLayer _feedBusLayer = new FeedBusinessLayer();
            TryUpdateModel(_Feed);
           // _Feed.CategoryID = 1;//we are maually setting CategoryID
            //if (ModelState.IsValid)
            try
            {
                if (_Feed.File != null)
                {

                    if (_Feed.File.ContentLength > 0)
                    {


                        var fileName = Path.GetFileName(_Feed.File.FileName);

                        var path = Path.Combine(Server.MapPath("~/img"), fileName);
                        _Feed.File.SaveAs(path);
                        _Feed.image = fileName;
                    }

                }

                else if (_Feed.image == null)
                {
                    _Feed.image = "NIL";
                }

                _feedBusLayer.UpdateFeed(_Feed);
                TempData["AlertMessage"] = "Hai this is working";
                return RedirectToAction("ManageFeed", "Home");

            }

            catch (Exception ex)
            {
                TempData["AlertMessage"] = "Error";
                string Module_name = "ManageFeed_Update";
                string Action = "Save";
                ErrorExceptionHandler _handler = new ErrorExceptionHandler();
                _handler.ReportError(Module_name, ex.Message, Action);
                return RedirectToAction("index", "Exception");
            }




            //return RedirectToAction("ManageFeed", "Home");
        }


        private void VaryQualityLevel(string path)
        {
            // Get a bitmap.

            //Bitmap bmp1 = new Bitmap(@"D:\TestPhoto.jpeg");

            Bitmap bmp1 = new Bitmap(path);



            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one
            // EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(@"D:\TestPhotoQualityFifty.jpg", jpgEncoder, myEncoderParameters);

            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(@"D:\TestPhotoQualityHundred.jpg", jpgEncoder, myEncoderParameters);

            // Save the bitmap as a JPG file with zero quality level compression.
            myEncoderParameter = new EncoderParameter(myEncoder, 0L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(@"D:\TestPhotoQualityZero.jpg", jpgEncoder, myEncoderParameters);

        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        [HttpGet]
        public ActionResult ContactUs()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(FormCollection FormContactus)
        {
            bool _mail_status = false;
            Contact_us _cont_Us = new Contact_us();
            _cont_Us.Name = FormContactus["name"].ToString();
            _cont_Us.Email = FormContactus["email"].ToString();
            _cont_Us.Subject = FormContactus["subject"].ToString();
            _cont_Us.Message = FormContactus["message"].ToString();

            Contact_usBusinessLayer Con_BusLayer = new Contact_usBusinessLayer();
            try
            {
                _mail_status = Con_BusLayer.ContactUS(_cont_Us, _mail_status);

            }
            catch
            {

            }

            if (_mail_status == true)
                TempData["AlertMessage"] = "Success";
            //else
            //    TempData["AlertMessage"] = "Error";

            return View();
        }

        public ActionResult Subscribe()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Subscribe(FormCollection FormSubscribe)
        {
            SubscribeUs _sub_us = new SubscribeUs();
            _sub_us.Name = FormSubscribe["name"];
            _sub_us.Email = FormSubscribe["email"];

            SubscribeBusinessLayer SubBusLayer = new SubscribeBusinessLayer();
            try
            {
                SubBusLayer.Subscribe(_sub_us);
                TempData["AlertMessage"] = "Success";
            }
            catch
            {
                TempData["AlertMessage"] = "Error";
            }

            return View();
        }
    }
}