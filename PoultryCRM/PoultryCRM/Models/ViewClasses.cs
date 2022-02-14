using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoultryCRM.Models
{
   
 public class Registration
    {

        public string Id { set; get; }
        public string Name { set; get; }
        public string Mobile_No { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Confirm_Password { set; get; }
        //public string Name { set; get; }
        //public string Name { set; get; }
    }
 public class Daily_FeedingDetails
 {
     public string Total_Issue { set; get; }
     public string Total_Consume { set; get; }
     public string Balance { set; get; }
     public string agofChiks { set; get; }
     public string Food_Consume { set; get; }
     public string Feed_Cost { set; get; }
     public string Total_Feedconsume_Perchicks { set; get; }

    
 }

}