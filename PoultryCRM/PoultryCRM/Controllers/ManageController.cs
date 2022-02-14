using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PoultryCRM.Models;
using System.Data;
using System.IO;
using ClosedXML.Excel;
namespace PoultryCRM.Controllers
{
    //[Authorize]
    public class ManageController : Controller
    {
        Poultry_CRMEntities dbb = new Poultry_CRMEntities();
        public DBManager dbmanager;
        public ManageController()
        {
            dbmanager = new DBManager();
           
        }
      public ActionResult Daily_Fedding()
        {
            return View();
        }
      public ActionResult vaccine_report()
      {
          return View();
      }
      public ActionResult medicine_report()
      {
          return View();
      }
      public ActionResult EggCollection_report()
      {
          return View();
      }
      public ActionResult Exportclick(string table)
      {
          PrintExe(table);
          return View();
      }

      public void PrintExe(string tbl)
      {
          string cmd = "select * from " + tbl + "";
          DataTable dt = dbmanager.GetAllRecord(cmd);
          XLWorkbook wb = new XLWorkbook();
          wb.Worksheets.Add(dt, tbl);
          Response.Clear();
          Response.Buffer = true;
          Response.Charset = "";
          Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
          Response.AddHeader("content-disposition", "attachment;filename=" + tbl + ".xlsx");
          MemoryStream MyMemoryStream = new MemoryStream();
          wb.SaveAs(MyMemoryStream);
          MyMemoryStream.WriteTo(Response.OutputStream);
          Response.Flush();
          Response.End();
      }

      
    }
}
   