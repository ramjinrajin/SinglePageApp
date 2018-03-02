using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReferralBonus.Controllers
{
    public class Contact_us
    {
        public int Cont_id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }


    public class SubscribeUs
    {
   
        public string Name { get; set; }

        public string Email { get; set; }

    }
}