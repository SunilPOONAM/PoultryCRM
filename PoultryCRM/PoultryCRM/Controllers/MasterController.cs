using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoultryCRM.Models;

namespace PoultryCRM.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        Poultry_CRMEntities db = new Poultry_CRMEntities();
         public DBManager dbmanager;
       
        // GET: Flock_Management

         public MasterController()
        {
            dbmanager = new DBManager();
           
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Medication()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Medication(Medication_Master dtls)
        {
            try
            {
                // var medicin = db.Medication_Master.Select(x => x.Medication == dtls.Medication);
                var medicin = db.Medication_Master.AsQueryable().AsEnumerable().Where(x => x.Medication == dtls.Medication).Select(x => x.Medication = x.Medication).ToList();
                if (medicin.Count == 0)
                {
                    db.Medication_Master.Add(dtls);
                    db.SaveChanges();
                    Response.Write("<script>alert('Saved Success.....');window.location='/Flock_Management/Dashboard';</script>");
                }

            }
            catch (Exception ex)
            {

            }
            Response.Write("<script>alert('Alredy Exist....');window.location='/Flock_Management/Dashboard';</script>");
            return View();
        }
        public ActionResult Unit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Unit(Unit_Master dtls)
        {
            try
            {
                // var medicin = db.Medication_Master.Select(x => x.Medication == dtls.Medication);
                var medicin = db.Unit_Master.AsQueryable().AsEnumerable().Where(x => x.Unit == dtls.Unit).Select(x => x.Unit = x.Unit).ToList();
                if (medicin.Count == 0)
                {
                    db.Unit_Master.Add(dtls);
                    db.SaveChanges();
                    Response.Write("<script>alert('Saved Success.....');window.location='/Master/Unit';</script>");
                }

            }
            catch (Exception ex)
            {

            }
            Response.Write("<script>alert('Alredy Exist....');window.location='/Flock_Management/Dashboard';</script>");
            return View();
        }

        public ActionResult Birds()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Birds(Birds_Master dtls)
        {
            try
            {
                // var medicin = db.Medication_Master.Select(x => x.Medication == dtls.Medication);
                var medicin = db.Birds_Master.AsQueryable().AsEnumerable().Where(x => x.Birds_Name == dtls.Birds_Name).Select(x => x.Birds_Name = x.Birds_Name).ToList();
                if (medicin.Count == 0)
                {
                    dtls.Birds_ID = dtls.Birds_Name;
                    db.Birds_Master.Add(dtls);
                    db.SaveChanges();
                    Response.Write("<script>alert('Saved Success.....');window.location='/Flock_Management/Dashboard';</script>");
                }

            }
            catch (Exception ex)
            {

            }
            Response.Write("<script>alert('Alredy Exist....');window.location='/Flock_Management/Dashboard';</script>");
            return View();
        }

        public ActionResult Feed_Type()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Feed_Type(FeedType_Master dtls)
        {
            try
            {
                // var medicin = db.Medication_Master.Select(x => x.Medication == dtls.Medication);
                var medicin = db.FeedType_Master.AsQueryable().AsEnumerable().Where(x => x.Feed_Type == dtls.Feed_Type).Select(x => x.Feed_Type = x.Feed_Type).ToList();
                if (medicin.Count == 0)
                {
                    dtls.Feed_Type_Value = dtls.Feed_Type;
                    db.FeedType_Master.Add(dtls);
                    db.SaveChanges();
                    Response.Write("<script>alert('Saved Success.....');window.location='/Flock_Management/Dashboard';</script>");
                }

            }
            catch (Exception ex)
            {

            }
            Response.Write("<script>alert('Alredy Exist....');window.location='/Flock_Management/Dashboard';</script>");
            return View();
        }
        public ActionResult Chiken_Breed()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Chiken_Breed(Chiken_Breed_master dtls)
        {
            try
            {
                // var medicin = db.Medication_Master.Select(x => x.Medication == dtls.Medication);
                var medicin = db.Chiken_Breed_master.AsQueryable().AsEnumerable().Where(x => x.Chiken_Breed == dtls.Chiken_Breed).Select(x => x.Chiken_Breed = x.Chiken_Breed).ToList();
                if (medicin.Count == 0)
                {
                   
                    db.Chiken_Breed_master.Add(dtls);
                    db.SaveChanges();
                    Response.Write("<script>alert('Saved Success.....');window.location='/Flock_Management/Dashboard';</script>");
                }

            }
            catch (Exception ex)
            {

            }
            Response.Write("<script>alert('Alredy Exist....');window.location='/Flock_Management/Dashboard';</script>");
            return View();
        }
        public ActionResult Vaccine()
         {
            return View();
        }
        [HttpPost]
        public ActionResult Vaccine(Vaccine_Master dtls)
        {
            try
            {
                // var medicin = db.Medication_Master.Select(x => x.Medication == dtls.Medication);
                var medicin = db.Vaccine_Master.AsQueryable().AsEnumerable().Where(x => x.Vaccine == dtls.Vaccine).Select(x => x.Vaccine = x.Vaccine).ToList();
                if (medicin.Count == 0)
                {

                    db.Vaccine_Master.Add(dtls);
                    db.SaveChanges();
                    Response.Write("<script>alert('Saved Success.....');window.location='/Flock_Management/Dashboard';</script>");
                }

            }
            catch (Exception ex)
            {

            }
            Response.Write("<script>alert('Alredy Exist....');window.location='/Flock_Management/Dashboard';</script>");
            return View();
        }
        public ActionResult Farm_Master()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Farm_Master(Farm_Master dtls)
        {
            try
            {
                var tid = dbmanager.SaveFarmDetails(dtls);
                if(tid!=0)
                { 
                Response.Write("<script>alert('Saved Success.....');window.location='/Master/Farm_Master';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Alredy Exist.........');window.location='/Master/Farm_Master';</script>");
                }
            }
            catch(Exception ex)
            {

            }
            return View();
        }
        public ActionResult Issue_Item()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Issue_Item(Issue_Items_master Dtls)
        {
            try
            {
                var tid = dbmanager.SaveIssueDetails(Dtls);
               // Response.Write("<script>alert('Saved Success.....');window.location='/Master/Issue_Item';</script>");
                if (tid != 0)
                {

                    Response.Write("<script>alert('Saved Success.....');window.location='/Master/Issue_Item';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Not Saved.....');window.location='/Master/Issue_Item';</script>");
                }

            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult GetFarmName()
        {
            var result = db.Farm_Master.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Farm_Name, value = x.Farm_Name }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemName(string item)
        {
            var tid = dbmanager.GetItemNamedeatils(item);

            return Json(tid, JsonRequestBehavior.AllowGet);
        }


        public ActionResult diseases()
        {
            return View();
        }
        [HttpPost]
        public ActionResult diseases(diseases_Master Dtls)
        {
            try
            {
                var tid = dbmanager.Savediseases(Dtls);
                if (tid != 0)
                {
                    Response.Write("<script>alert('Saved Success.....');window.location='/Master/diseases';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Not Saved......');window.location='/Master/diseases';</script>");
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult diseases__Details()
        {
            return View();
        }
        [HttpPost]
        public ActionResult diseases__Details(diseases_tbl Dtls)
        {
             try
            {
                HttpPostedFileBase file = Request.Files["Image"];
                Dtls.Image = file.FileName.ToString();
                var tid = dbmanager.Savediseases_Details(Dtls);
                file.SaveAs(Server.MapPath("~/Images/" + file.FileName));
              
                if (tid!=0)
                {
                    Response.Write("<script>alert('Saved Success.....');window.location='/Master/diseases__Details';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Not Saved......');window.location='/Master/diseases__Details';</script>");
                }
                

            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public JsonResult GetDiseases()
        {
            var result = db.diseases_Master.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Diseases, value = x.Diseases }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RRRppp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RRRppp(string[] city)
        {
            return View();
        }
    }
}