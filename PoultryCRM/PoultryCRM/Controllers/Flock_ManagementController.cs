using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoultryCRM.Models;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace PoultryCRM.Controllers
{
    public class Flock_ManagementController : Controller
    {
        Poultry_CRMEntities dbb = new Poultry_CRMEntities();
        public DBManager dbmanager;
        public Flock_ManagementController()
        {
            dbmanager = new DBManager();
           
        }
        public ActionResult Dashboard()
             {
        if(Session["UserName"]!=null)
        {

            var UserId= Session["UserName"].ToString();
             var result = dbb.Farm_Master.AsQueryable().AsEnumerable().Where(x => x.Mobile == UserId||x.Email==UserId).Select(x => new customSelectList { text = x.Farm_Name, value = x.Farm_Name }).ToList();
             Session["Farm_name"] = result[0].value;
           
        }
        else
        {
            Response.Write("<script>window.location='/Home/Login';</script>");
        }
        return View();
        }
        public ActionResult Feeding()
        { 
            if(Session["UserName"]==null)
        {
            Response.Write("<script>window.location='/Home/Login';</script>");
        }
            return View();
        }
        public ActionResult GetFeedName()
        {
           // List<string> feed = new List<string>();
            var result = dbb.FeedType_Master.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Feed_Type, value = x.Feed_Type }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBirdType()
        {
            var result = dbb.Birds_Master.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Birds_Name, value = x.Birds_Name }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
          [HttpPost]
        public ActionResult Feeding(Daily_Feeding_tbl Details)
        {
            try { 
                var tid = dbmanager.SaveDailyFeeding(Details);
                if (tid!=0)
                {


                    Response.Write("<script>alert('Saved Success.....');window.location='/Flock_Management/Feeding';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Not Saved.....');window.location='/Flock_Management/Feeding';</script>");
                }
            }
            catch(Exception ex)
            {

            }


            return View();
        }
        public ActionResult EggCollection()
        {
            if (Session["UserName"] == null)
            {
                Response.Write("<script>window.location='/Home/Login';</script>");
            }
            return View();
        }
        [HttpPost]                                                                                                         
        public ActionResult EggCollection(Egg_Collection_tbl dtls)
        {
            try
            {
               
                var tid = dbmanager.SaveEggCollection(dtls);
                if (tid != 0)
                {

                    Response.Write("<script>alert('Saved Success.....');window.location='/Flock_Management/EggCollection';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Not Saved.....');window.location='/Flock_Management/EggCollection';</script>");
                }
            }
            catch( Exception ex)
            { }
            return View();
        }
        public ActionResult Flock_Management()
        {
            return View();
        }
        public JsonResult GetChikenBreed()
        {
            var result = dbb.Chiken_Breed_master.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Chiken_Breed, value = x.Chiken_Breed }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Flock_Management(Flock_Management_tbl dtls)
        {
            try
            {
                var tid = dbmanager.SaveFlockManagement(dtls);
                if (tid != 0)
                {

                    Response.Write("<script>alert('Saved Success.....');window.location='/Flock_Management/Flock_Management';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Not Saved.....');window.location='/Flock_Management/Flock_Management';</script>");
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult Vaccination()
        {
            if (Session["UserName"] == null)
            {
                Response.Write("<script>window.location='/Home/Login';</script>");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Vaccination(Vaccination_tbl dtls)
        {
            try
            {
                var tid = dbmanager.SaveVaccine(dtls);
                if (tid != 0)
                {

                    Response.Write("<script>alert('Saved Success.....');window.location='/Flock_Management/Vaccination';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Not Saved.....');window.location='/Flock_Management/Vaccination';</script>");
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult GetVaccine()
        {
            var result = dbb.Vaccine_Master.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Vaccine, value = x.Vaccine }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // For Medications 0
        public ActionResult Medicin()
        {
            if (Session["UserName"] == null)
            {
                Response.Write("<script>window.location='/Home/Login';</script>");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Medicin(Medication_tbl dtls)
        {
            try
            {
                var tid = dbmanager.SaveMedicine(dtls);
                if (tid != 0)
                {
                    Response.Write("<script>alert('Saved Success.....');window.location='/Flock_Management/Medicin';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Not Saved.....');window.location='/Flock_Management/Medicin';</script>");
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult GetMedicatione()
        {
            var result = dbb.Medication_Master.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Medication, value = x.Medication }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFlockName(string Flockname)
        {
           // var result = dbb.Issue_Items_master.AsQueryable().AsEnumerable().Where(x => x.Farm_name == Flockname).Select(x => new customSelectList { text = x.Farm_name + x.Farm_number, value = x.Farm_name + x.Farm_number }).ToList();
            var result = dbmanager.GetFlockId(Flockname);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFlockDetails(string Item)
        {
            //var result = dbb.Issue_Items_master.AsQueryable().AsEnumerable().Where(x => x.Item_Type == Item).Select(x => new customSelectList { text = x.Item_Name, value = x.Item_Name }).ToList();
            var result = dbmanager.GetItemDetails(Item);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnit()
        {
            var result = dbb.Unit_Master.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Unit, value = x.Unit }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetIssueBirdss(string Flockname ,string Item)
        {
            Daily_FeedingDetails result=new Daily_FeedingDetails();
            Item = "Bird";
            var bird = dbmanager.GetTotalissue_bird(Flockname, Item);
            var Fedingbirds = dbmanager.GetTotalfedding(Flockname,Item);
            var chickage = dbmanager.Getchiks_age(Flockname,Item);
            if (chickage != "0" && chickage!="")
            {
                chickage = DateTime.ParseExact(chickage, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            }
          
            var food = dbmanager.GetTotalissue_Food(Flockname,Item);
            var feedCost = dbmanager.GetTotalFeed_Cost(Flockname, Item);
            var Total_chiks = dbmanager.GetTotalChiks(Flockname);
            result.Total_Issue = bird.ToString();
            result.Total_Consume = Fedingbirds.ToString();
            result.Balance = (bird - Fedingbirds).ToString();
            result.agofChiks = chickage;
            result.Food_Consume = food.ToString();
            result.Feed_Cost = feedCost.ToString();
            var feedConsumeperchiks=0;
            if(Total_chiks!=0)
            {
                feedConsumeperchiks = (Convert.ToInt32(Fedingbirds / Total_chiks));
            }

            result.Total_Feedconsume_Perchicks = feedConsumeperchiks.ToString();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult Rrrrr()
        //{
        //    var Url="http://technocraftsolution.club/api/PatientRecordWebapi?mobile=9651541312";
        //    var Result="";
        //    using (var client = new WebClient())
        //    {
        //        client.Headers[HttpRequestHeader.ContentType] = "application/text";
        //        var response = client.UploadString(Url, Result);
        //        var alll = response;
        //    }
        //    return View();
        //}
    }
}