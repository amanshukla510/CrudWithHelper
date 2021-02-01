using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWithHelper.Models;
using ProjectWithHelper.Repository;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;

namespace ProjectWithHelper.Controllers
{
    public class EmployeeController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        EmpRepository EmpRepo = new EmpRepository();
        // GET: Employee





        public ActionResult AddEmployee()
        {
            //string[] Emp_DevLang =("using,Project,With,Helper,Models").Split(',');

            ViewBag.CountryList = EmpRepo.GetCountry();
            ViewBag.LanguageList = EmpRepo.GetLanguage();
            return View();
        }
        public ActionResult GetState(int CountryId)
        {
            string Data = string.Empty;
            Data = EmpRepo.GetState(CountryId);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCity(int StateId)
        {
            string Data = string.Empty;
            Data = EmpRepo.GetCity(StateId);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllEmpDetails()
        {
            return View(EmpRepo.GetAllEmployees());
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee Emp)
        {
            ViewBag.CountryList = EmpRepo.GetCountry();
            ViewBag.LanguageList = EmpRepo.GetLanguage();
            if (Emp.ImageUpload != null)
            {
                string Pic = DateTime.Now.ToString("ddmmyy_hhMMss") + Path.GetFileName(Emp.ImageUpload.FileName);
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Pic);
                Emp.ImageUpload.SaveAs(path);
                int success = EmpRepo.AddEmployee(Emp, Pic);
                if (success == 1)
                {
                    ViewBag.CountryList = EmpRepo.GetCountry();
                    ViewBag.LanguageList = EmpRepo.GetLanguage();
                    ViewBag.DataResult = "Data Successfully Insert";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.CountryList = EmpRepo.GetCountry();
                    ViewBag.LanguageList = EmpRepo.GetLanguage();
                    ViewBag.DataResult = "Data not Insert";
                    return View();
                }
            }
            else
            {
                ViewBag.CountryList = EmpRepo.GetCountry();
                ViewBag.LanguageList = EmpRepo.GetLanguage();
                ViewBag.DataResult = "Please Select Image";
            }
            ModelState.Clear();
            return View();
        }
        public ActionResult EditEmpDetails(int id)
        {


            ViewBag.CountryList = EmpRepo.GetCountry();
            ViewBag.LanguageList = EmpRepo.GetLanguage();
            return View(EmpRepo.GetAllEmployees().Find(Emp => Emp.Emp_ID == id.ToString()));

        }

        public ActionResult UpdateEmployee(Employee Emp)
        {
            ViewBag.CountryList = EmpRepo.GetCountry();
            ViewBag.LanguageList = EmpRepo.GetLanguage();
            if (Emp.ImageUpload != null)
            {
                string Pic = DateTime.Now.ToString("ddmmyy_hhMMss") + Path.GetFileName(Emp.ImageUpload.FileName);
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Pic);
                Emp.ImageUpload.SaveAs(path);
                int success = EmpRepo.UpdateEmployee(Emp, Pic);
                if (success == 1)
                {
                    ViewBag.CountryList = EmpRepo.GetCountry();
                    ViewBag.LanguageList = EmpRepo.GetLanguage();
                    ViewBag.DataResult = "Data Successfully Insert";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.CountryList = EmpRepo.GetCountry();
                    ViewBag.LanguageList = EmpRepo.GetLanguage();
                    ViewBag.DataResult = "Data not Insert";
                    return View();
                }
            }
            else
            {
                ViewBag.CountryList = EmpRepo.GetCountry();
                ViewBag.LanguageList = EmpRepo.GetLanguage();
                ViewBag.DataResult = "Please Select Image";
            }
            ModelState.Clear();
            return View();
        }
    }
}





// DataTable dt = new DataTable();
//.dt = EmpRepo.GetCountry();
// List<SelectListItem> li = new List<SelectListItem>();
// for (int rows = 0; rows <= dt.Rows.Count - 1; rows++)
// {
//     li.Add(new SelectListItem { Text = dt.Rows[rows][1].ToString(), Value = dt.Rows[rows][0].ToString() });
// }

// ViewBag.CountryList = li;
//return View();
// ViewBag.CountryList = ToSelectList(dt, "Country_ID", "Country_Name");
//var model1 = new Employee
//{
//    AvailableFruits = GetFruits()
//};
//  ViewBag.model = model1;





//string pic = System.IO.Path.GetFileName(Emp.ImageUpload.FileName);
//string path = System.IO.Path.Combine(Server.MapPath("~/images/profile"), pic);

//string Emp_DevLang = string.Join(",", Emp.SelectedFruits);
//con.Open();
//SqlCommand cmd = new SqlCommand("USP_Emp_Add_Details", con);
//cmd.CommandType = CommandType.StoredProcedure;
//cmd.Parameters.AddWithValue("@Emp_Name", Emp.Emp_Name);
//cmd.Parameters.AddWithValue("@Emp_Sex", Emp.Emp_Sex);
//cmd.Parameters.AddWithValue("@Emp_DOB", Emp.Emp_DOB);
//cmd.Parameters.AddWithValue("@Emp_Address", Emp.Emp_Address);
//cmd.Parameters.AddWithValue("@Emp_Email", Emp.Emp_Email);
//cmd.Parameters.AddWithValue("@Emp_Country", Emp.Emp_Country);
//cmd.Parameters.AddWithValue("@Emp_State", Emp.Emp_State);
//cmd.Parameters.AddWithValue("@Emp_City", Emp.Emp_City);
//cmd.Parameters.AddWithValue("@Emp_MobNo", Emp.Emp_MobNo);
//cmd.Parameters.AddWithValue("@Emp_Salary", Emp.Emp_Salary);
//cmd.Parameters.AddWithValue("@Emp_DevLang", Emp_DevLang);
//cmd.Parameters.AddWithValue("@Emp_Img", Emp.ImageUpload);
//// int i = cmd.ExecuteNonQuery();
//con.Close();
//DataTable dt = new DataTable();
//dt = EmpRepo.GetCountry();
//List<SelectListItem> li = new List<SelectListItem>();
//for (int rows = 0; rows <= dt.Rows.Count - 1; rows++)
//{
//    li.Add(new SelectListItem { Text = dt.Rows[rows][1].ToString(), Value = dt.Rows[rows][0].ToString() });
//}

//var fruits = string.Join(",", E.SelectedFruits);
//ViewBag.CountryList = li;
////return View();
//// ViewBag.CountryList = ToSelectList(dt, "Country_ID", "Country_Name");