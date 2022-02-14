using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PoultryCRM.Models
{
    public class DBManager
    {
        Poultry_CRMEntities dbb = new Poultry_CRMEntities();
    
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;initial catalog=Poultry_CRM;user id=sa;password=sa@123");
       // SqlConnection con = new SqlConnection(@"Data Source=SUNIL-PC\SQLEXPRESS;Initial Catalog=Poultry_CRM;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");

        public DataTable GetAllRecord(string command)
        {
            //SqlDataAdapter sa = new SqlDataAdapter(command, con);
            //DataTable dt = new DataTable();
            //sa.Fill(dt);
            //return dt;
           // var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand(command, con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Daily_Feeding_tbl";
            da.Fill(ds);
            return ds;
        }
        public long SaveEggCollection(Egg_Collection_tbl dtls)
        {
            try
            {
                dbb.Egg_Collection_tbl.Add(dtls);
                dbb.SaveChanges();
                return dtls.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
        public long SaveDailyFeeding(Daily_Feeding_tbl dtls)
        {
            try
            {
                dbb.Daily_Feeding_tbl.Add(dtls);
                dbb.SaveChanges();
                return dtls.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public long SaveFlockManagement(Flock_Management_tbl dtls)
        {
            try
            {
                dbb.Flock_Management_tbl.Add(dtls);
                dbb.SaveChanges();
                return dtls.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
        public long SaveVaccine(Vaccination_tbl dtls)
        {
            try
            {
                dbb.Vaccination_tbl.Add(dtls);
                dbb.SaveChanges();
                return dtls.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
        public long SaveMedicine(Medication_tbl dtls)
        {
            try
            {
                dbb.Medication_tbl.Add(dtls);
                dbb.SaveChanges();
                return dtls.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public long SaveFarmDetails(Farm_Master dtls)
        {
            try
            {
                var medicin = dbb.Farm_Master.AsQueryable().AsEnumerable().Where(x => x.Farm_Name == dtls.Farm_Name).Select(x => x.Farm_Name = x.Farm_Name).ToList();
                if (medicin.Count == 0)
                {
                    dtls.date = DateTime.Now.ToString("dd/MM/yyyy");
                    dbb.Farm_Master.Add(dtls);
                    dbb.SaveChanges();
                    return dtls.Id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public long SaveRegistrationDetails(Registration dtls)
        {
            Login_Reg_tbl reg = new Login_Reg_tbl();
            try
            {
                reg.Name = dtls.Name;
                reg.Mobile_number = dtls.Mobile_No;
                reg.Email = dtls.Email;
                reg.Password = dtls.Password;
                dbb.Login_Reg_tbl.Add(reg);
                dbb.SaveChanges();
                return reg.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
        public long SaveIssueDetails(Issue_Items_master dtls)
        {
            var farmnumber = dbb.Issue_Items_master.AsEnumerable().Where(x =>
                    x.Farm_name == dtls.Farm_name && x.Item_Type == dtls.Item_Type).Select(x => x.Farm_number).ToList();
            int value = Convert.ToInt16(farmnumber.Max());
            value = value + 1;
            dtls.Issue_Date = DateTime.Now.ToString("dd/MM/yyyy");
            dtls.Farm_number = value.ToString();
            dtls.Farm_Id = dtls.Farm_name + value;
            dbb.Issue_Items_master.Add(dtls);
            dbb.SaveChanges();
            if (dtls.Item_Name == "Chicks")
            {
               // var registration = dbb.Bird_Issue_master.AsEnumerable().Where(x => x.Farm_name == dtls.Farm_name).Select(x => x.Batch).ToList();
                //int val = Convert.ToInt16(registration.Max());
                //val = val + 1;

                Bird_Issue_master dtldj = new Bird_Issue_master();
                dtldj.Farm_name = dtls.Farm_name;
                dtldj.Item_Name = dtls.Item_Name;
                dtldj.Quentity = Convert.ToInt16(dtls.Quentity);
                dtldj.Batch = dtls.Farm_Id;
                 dtldj.Unit = dtls.Unit;
                 dtldj.Price = Convert.ToDecimal(dtls.Price);
                dtldj.date = (DateTime.Now.ToShortDateString());
                dbb.Bird_Issue_master.Add(dtldj);
                dbb.SaveChanges();
            }
            return dtls.Id;
        }
        public List<Registration> GetLogindeatils(Registration dtls)
        {
            try
            {
                var registration = dbb.Login_Reg_tbl.AsEnumerable().Where(x =>
                     x.Password == dtls.Password && x.Email == dtls.Email || x.Mobile_number == dtls.Email).Select(
                    x => new Registration
                    {
                        Email = x.Email,
                        Password = x.Password,
                       
                    }).ToList();
                if (registration.Count==0)
                {
                     registration = dbb.Farm_Master.AsEnumerable().Where(x =>
                     x.Password == dtls.Password && x.Email == dtls.Email || x.Mobile == dtls.Email).Select(
                    x => new Registration
                    {
                        Email = x.Email,
                        Password = x.Password,
                        
                    }).ToList();
                }
                return registration;
                
            }
            catch (Exception ex)
            { }

            return new List<Registration>();

        }
       
        public List<customSelectList> GetItemNamedeatils(string itemtype)
        {
           
            try
            {
                if(itemtype=="Bird")
                {
                    var result = dbb.Birds_Master.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Birds_Name, value = x.Birds_Name }).ToList();
                return result;
                }
                else if (itemtype == "Medicin")
                {
                    var result = dbb.Medication_Master.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Medication, value = x.Medication }).ToList();
                    return result;
                }
                else if (itemtype == "Vaccine")
                {
                    var result = dbb.Vaccine_Master.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Vaccine, value = x.Vaccine }).ToList();
                    return result;
                }
                else if (itemtype == "Feed")
                {
                    var result = dbb.FeedType_Master.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Feed_Type, value = x.Feed_Type }).ToList();
                    return result;
                }

            }
            catch (Exception ex)
            { }

            return new List<customSelectList>();

        }
        public List<customSelectList> GetItemDetails(string Item) 
        {
            List<customSelectList> DesObj = new List<customSelectList>();
            try
            {
                string query = "select Item_Name from Issue_Items_master where Item_Type='" + Item + "' group by Item_Name order by Item_Name";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sa = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sa.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    customSelectList dst = new customSelectList();
                    dst.text = dt.Rows[i]["Item_Name"] + "";
                    dst.value = dt.Rows[i]["Item_Name"] + "";
                    DesObj.Add(dst);
                }

                return DesObj;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new List<customSelectList>();
        }

        public List<customSelectList> GetFlockId(string Flockname)
        {
            List<customSelectList> DesObj = new List<customSelectList>();
            try
            {
              string query = "select Farm_Id from Issue_Items_master where Farm_name='" + Flockname + "' group by Farm_Id order by Farm_Id";
               // string query = "select Farm_Id from [Poultry_CRM].[dbo].[Issue_Items_master] where Farm_name='PTRfarm' group by Farm_Id order by Farm_Id";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sa = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sa.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    customSelectList dst = new customSelectList();
                    dst.text = dt.Rows[i]["Farm_Id"] + "";
                    dst.value = dt.Rows[i]["Farm_Id"] + "";
                    DesObj.Add(dst);
                }

                return DesObj;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new List<customSelectList>();
        }
        public long Savediseases(diseases_Master dtls)
        {
             var medicin = dbb.diseases_Master.AsQueryable().AsEnumerable().Where(x => x.Diseases == dtls.Diseases).Select(x => x.Diseases = x.Diseases).ToList();
             if (medicin.Count == 0)
             {
                 dbb.diseases_Master.Add(dtls);
                 dbb.SaveChanges();
                 return dtls.Id;
             }
             else
             {
                 return 0;
             }
        }
        public long Savediseases_Details(diseases_tbl dtls)
        {

            dbb.diseases_tbl.Add(dtls);
            dbb.SaveChanges();
            return dtls.Id;
        }

        #region Datatable

        public DataTable GetIssueMasterData()
        {
            var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  Issue_Items_master where Issue_Date like '%" + date + "%'  order by Farm_name asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Issue_Items_master";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetFarmData()
        {
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  Farm_Master  order by Farm_Name asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Farm_Master";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetBirds()
        {
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  Birds_Master  order by Birds_Name asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Birds_Master";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetChiken_Breed()
        {
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  Chiken_Breed_master  order by Chiken_Breed asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Chiken_Breed_master";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetFeed_type()
        {
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  FeedType_Master  order by Feed_Type asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "FeedType_Master";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetMedication()
        {
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  Medication_Master  order by Medication asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Medication_Master";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetUnits()
        {
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  Unit_Master  order by Unit asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Unit_Master";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetVaccine()
        {
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  Vaccine_Master  order by Vaccine asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Vaccine_Master";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetFeedingdata()
        {
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  Daily_Feeding_tbl  order by Flock asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Daily_Feeding_tbl";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetMedicationdata()
        {
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  Medication_tbl  order by Flock asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Medication_tbl";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetVaccination()
        {
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  Vaccination_tbl  order by Flock asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Vaccination_tbl";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetEgg_Collection()
        {
            
            SqlCommand com = new SqlCommand("select * from  Egg_Collection_tbl  order by Flock asc  ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Egg_Collection_tbl";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetDiseases()
        {
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  diseases_Master  order by Diseases asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "diseases_Master";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetDiseases_Details()
        {
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlCommand com = new SqlCommand("select * from  diseases_tbl  order by Flock asc  ", con);
            //com.Parameters.AddWithValue();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "diseases_tbl";
            da.Fill(ds);
            return ds;
        }
       // SqlCommand com = new SqlCommand("SELECT SUM(Quentity) FROM Issue_Items_master where Item_Type='Food' and Farm_name='PTRfarm'", con);
        public decimal GetTotalissue_bird(string Flockname, string item)
        {
            item = "Feed";
            var value = dbb.Issue_Items_master.AsEnumerable().Where(row => row.Item_Type == item && row.Farm_Id == Flockname).Sum(row => Convert.ToDecimal(row.Quentity));

            return value;
        }
        public decimal GetTotalfedding(string Flockname, string item)
        {
           // item="Chicks";
            var value = dbb.Daily_Feeding_tbl.AsEnumerable().Where(row => row.Flock == Flockname).Sum(row => Convert.ToDecimal(row.Quantity_Fed));

            return value;
        }
        public decimal GetTotalissue_Food(string Flockname, string item)
        {
            item = "Feed";
            var value = dbb.Issue_Items_master.AsEnumerable().Where(row => row.Item_Type == item && row.Farm_Id == Flockname).Sum(row => Convert.ToDecimal(row.Quentity));

            return value;
        }
        public string Getchiks_age(string Flockname, string item)
        {
            item = "Chicks";
            var  chiks_age ="";
          
            var value= dbb.Issue_Items_master.AsQueryable().AsEnumerable().Where(x => x.Farm_Id == Flockname && x.Item_Name == item).Select(x => new customSelectList { text = x.Issue_Date, value = x.Issue_Date }).ToList();
            if (value.Count!=0)
            {
                chiks_age = value[0].text;
            }
            else
            {
                chiks_age = "";
            }

            return chiks_age;
        }
        public decimal GetTotalFeed_Cost(string Flockname, string item)
        {
            item = "Feed";
            var value = dbb.Issue_Items_master.AsEnumerable().Where(row => row.Farm_Id == Flockname && row.Item_Type == "Feed").Sum(row => Convert.ToDecimal(row.Price));

            return value;
        }
        public decimal GetTotalChiks(string Flockname)
        {
          
            var value = dbb.Bird_Issue_master.AsEnumerable().Where(row => row.Batch == Flockname ).Sum(row => Convert.ToDecimal(row.Quentity));

            return value;
        }
        #endregion
    }
}