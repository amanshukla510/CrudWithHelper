using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ProjectWithHelper.Models;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;

namespace ProjectWithHelper.Repository
{
    public class EmpRepository
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);

        public List<SelectListItem> GetLanguage()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Asp.Net", Value = "Asp.Net"},
                new SelectListItem {Text = "Asp.Net Core", Value = "Asp.Net Core"},
                new SelectListItem {Text = "Angular", Value = "Angular" },
                new SelectListItem {Text = "Java Script", Value = "Java Script" },
                new SelectListItem {Text = "PHP", Value = "PHP" },
                new SelectListItem {Text = "Kotlin", Value = "Kotlin" },
            };
        }
        public List<SelectListItem> GetCountry()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("USP_Get_Country", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            con.Close();
            List<SelectListItem> Countrylist = new List<SelectListItem>();

            for (int rows = 0; rows <= dt.Rows.Count - 1; rows++)
            {
                Countrylist.Add(new SelectListItem { Text = dt.Rows[rows][1].ToString(), Value = dt.Rows[rows][0].ToString() });
            }
            return Countrylist;
        }
        public String GetState(int CountryId)
        {
            string Data = string.Empty;
            con.Open();
            SqlCommand cmd = new SqlCommand("USP_Get_State", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tblCountry", CountryId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            con.Close();
            Data = JsonConvert.SerializeObject(dt);
            return Data;
        }
        public String GetCity(int StateId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("USP_Get_City", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tblState", StateId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            con.Close();
            string Data = string.Empty;
            Data = JsonConvert.SerializeObject(dt);
            return Data;
        }
        public List<Employee> GetAllEmployees()
        {
            con.Open();
            List<Employee> EmpList = new List<Employee>();
            SqlCommand com = new SqlCommand("USP_Emp_Get_Details", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            //Uncooment if convert Datatable to list using AsEnumerable
            //List<DataRow> list = dt.AsEnumerable().ToList();
            //foreach (var item in list)
            //{
            //    EmpList.Add(

            //       new EmpModel
            //       {
            //           Empid = Convert.ToInt32(item["Id"]),
            //           Name = Convert.ToString(item["Name"]),
            //           City = Convert.ToString(item["City"]),
            //           Address = Convert.ToString(item["Address"])

            //       });
            //}

            //Uncomment if you wants to Bind EmpModel generic list using LINQ 
            //EmpList = (from DataRow dr in dt.Rows

            //        select new EmpModel()
            //        {
            //            Empid = Convert.ToInt32(dr["Id"]),
            //            Name = Convert.ToString(dr["Name"]),
            //            City = Convert.ToString(dr["City"]),
            //            Address = Convert.ToString(dr["Address"])
            //        }).ToList();


            //  Bind EmpModel generic list using dataRow
            foreach (DataRow dr in dt.Rows)
            {

                EmpList.Add(

                    new Employee
                    {
                        Emp_ID = Convert.ToString(dr["Emp_ID"]),
                        Emp_Img = Convert.ToString(dr["Emp_Img"]),
                        Emp_Name = Convert.ToString(dr["Emp_Name"]),
                        Emp_Address = Convert.ToString(dr["Emp_Address"]),
                        Emp_City = Convert.ToString(dr["Emp_City"]),
                        Emp_Country = Convert.ToString(dr["Emp_Country"]),
                        Emp_DevLangview = Convert.ToString(dr["Emp_DevLang"]),
                        Emp_Email = Convert.ToString(dr["Emp_Email"]),
                        Emp_DOB = Convert.ToString(dr["Emp_DOB"]),
                        Emp_MobNo = Convert.ToString(dr["Emp_MobNo"]),
                        Emp_Salary = Convert.ToString(dr["Emp_Salary"]),
                        Emp_Sex = Convert.ToString(dr["Emp_Sex"]),
                        Emp_State = Convert.ToString(dr["Emp_State"]),
                       // Emp_DevLang = Convert.ToString(dr["Emp_DevLang"]).Split(',')
                    }


                    );


            }

            return EmpList;


        }

        public int AddEmployee(Employee Emp, string Pic)
        {
            string SelectLanguage = string.Join(",", Emp.Emp_DevLang);
            con.Open();
            SqlCommand cmd = new SqlCommand("USP_Emp_Add_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Emp_Name", Emp.Emp_Name);
            cmd.Parameters.AddWithValue("@Emp_Sex", Emp.Emp_Sex);
            cmd.Parameters.AddWithValue("@Emp_DOB", Emp.Emp_DOB);
            cmd.Parameters.AddWithValue("@Emp_Address", Emp.Emp_Address);
            cmd.Parameters.AddWithValue("@Emp_Email", Emp.Emp_Email);
            cmd.Parameters.AddWithValue("@Emp_Country", Emp.Emp_Country);
            cmd.Parameters.AddWithValue("@Emp_State", Emp.Emp_State);
            cmd.Parameters.AddWithValue("@Emp_City", Emp.Emp_City);
            cmd.Parameters.AddWithValue("@Emp_MobNo", Emp.Emp_MobNo);
            cmd.Parameters.AddWithValue("@Emp_Salary", Emp.Emp_Salary);
            cmd.Parameters.AddWithValue("@Emp_DevLang", SelectLanguage);
            cmd.Parameters.AddWithValue("@Emp_Img", Pic);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return i;

            }
            else
            {

                return i;
            }
        }
        public int UpdateEmployee(Employee Emp, string Pic)
        {
            string SelectLanguage = string.Join(",", Emp.Emp_DevLang);
            con.Open();
            SqlCommand cmd = new SqlCommand("USP_Emp_Update_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Emp_ID", Emp.Emp_ID);
            cmd.Parameters.AddWithValue("@Emp_Name", Emp.Emp_Name);
            cmd.Parameters.AddWithValue("@Emp_Sex", Emp.Emp_Sex);
            cmd.Parameters.AddWithValue("@Emp_DOB", Emp.Emp_DOB);
            cmd.Parameters.AddWithValue("@Emp_Address", Emp.Emp_Address);
            cmd.Parameters.AddWithValue("@Emp_Email", Emp.Emp_Email);
            cmd.Parameters.AddWithValue("@Emp_Country", Emp.Emp_Country);
            cmd.Parameters.AddWithValue("@Emp_State", Emp.Emp_State);
            cmd.Parameters.AddWithValue("@Emp_City", Emp.Emp_City);
            cmd.Parameters.AddWithValue("@Emp_MobNo", Emp.Emp_MobNo);
            cmd.Parameters.AddWithValue("@Emp_Salary", Emp.Emp_Salary);
            cmd.Parameters.AddWithValue("@Emp_DevLang", SelectLanguage);
            cmd.Parameters.AddWithValue("@Emp_Img", Pic);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return i;

            }
            else
            {

                return i;
            }
        }
        //}//To view employee details with generic list 

        ////To Update Employee details
        //public bool UpdateEmployee(EmpModel obj)
        //{

        //    connection();
        //    SqlCommand com = new SqlCommand("UpdateEmpDetails", con);

        //    com.CommandType = CommandType.StoredProcedure;
        //    com.Parameters.AddWithValue("@EmpId", obj.Empid);
        //    com.Parameters.AddWithValue("@Name", obj.Name);
        //    com.Parameters.AddWithValue("@City", obj.City);
        //    com.Parameters.AddWithValue("@Address", obj.Address);
        //    con.Open();
        //    int i = com.ExecuteNonQuery();
        //    con.Close();
        //    if (i >= 1)
        //    {

        //        return true;

        //    }
        //    else
        //    {

        //        return false;
        //    }


        //}
        ////To delete Employee details
        //public bool DeleteEmployee(int Id)
        //{

        //    connection();
        //    SqlCommand com = new SqlCommand("DeleteEmpById", con);

        //    com.CommandType = CommandType.StoredProcedure;
        //    com.Parameters.AddWithValue("@EmpId", Id);

        //    con.Open();
        //    int i = com.ExecuteNonQuery();
        //    con.Close();
        //    if (i >= 1)
        //    {

        //        return true;

        //    }
        //    else
        //    {

        //        return false;
        //    }


        //}

    }
}