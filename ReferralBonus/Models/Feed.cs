using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReferralBonus.Models
{
    public class Feed
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; } //get the value from html and set it into varible
        [Required]
        public string Description { get; set; }

        public string image { get; set; }
        [Required]
        public string link { get; set; }

        public HttpPostedFileBase File { get; set; }

        public char Rowstatus { get; set; }
     
        public int CategoryID { get; set; }

    }



    public class testfeed
    {
       
        [Required]
        public string Title { get; set; } //get the value from html and set it into varible
        [Required]
        public string Description { get; set; }

        public string image { get; set; }
        [Required]
        public string link { get; set; }

        public HttpPostedFileBase File { get; set; }

        public char Rowstatus { get; set; }

        public int CategoryID { get; set; }

    }


}


