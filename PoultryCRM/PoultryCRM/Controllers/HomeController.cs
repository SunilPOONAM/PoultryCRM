using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoultryCRM.Models;
using System.Web.Mvc;

namespace PoultryCRM.Controllers
{
    public class HomeController : Controller
    { 

        public DBManager dbmanager;
       
        // GET: Flock_Management

      public HomeController()
        {
            dbmanager = new DBManager();
           
        }
      public ActionResult Login()
       {
         return View();
       }
        [HttpPost]
        public ActionResult Login(Registration Details)
        {
            var tid = dbmanager.GetLogindeatils(Details);
            if (tid.Count==1)
            {
                Session["UserName"] = Details.Email;
                if(Details.Email=="admin@gmail.com"&&Details.Password=="admin123")
                {
                    
                     Response.Write("<script>window.location='/Master/Index';</script>");
                }
                else
                {
                  return  RedirectToAction("Dashboard", "Flock_Management");
                }
                
            }
            else
            {
                Response.Write("<script>alert('Invalid UserId And Password');window.location='/Home/Login';</script>");
            }
            return View();
        }

    public ActionResult Register()
    {
        return View();
    }
        [HttpPost]
        public ActionResult Register(Registration dtls)
        {
            try
            {
              var tid = dbmanager.SaveRegistrationDetails(dtls);
              Response.Write("<script>alert('Saved Success.....');window.location='/Home/Login';</script>");

                }
            catch(Exception ex)
            {

            }
            return View();
        }

        public ActionResult Logout()
        {
            try
            {
                Session["UserName"] = null;
                Response.Write("<script>alert('Good Byee...');window.location='/Home/Login';</script>");
            }
            catch(Exception ex)
            {

            }
            return View();
        }
       
    }
}