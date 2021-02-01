using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ProjectWithHelper.Models
{
    public class Employee
    {
        public string Emp_ID { get; set; }
        public string Emp_Name { get; set; }
        public string Emp_Sex { get; set; }
        public string Emp_DOB { get; set; }
        public string Emp_Address { get; set; }
        public string Emp_Email { get; set; }
        public string Emp_Country { get; set; }
        public string Emp_State { get; set; }
        public string Emp_City { get; set; }
        public string Emp_MobNo { get; set; }
        public string Emp_Salary { get; set; }
        public string Emp_Img { get; set; }
        public string Emp_DevLangview { get; set; }
        public IList<string> Emp_DevLang { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
        /// <summary>
        /// 
        /// 
        /// 
        /// 
        /// 
        /// </summary>
        //public IList<string> SelectedFruits { get; set; }
        //public IList<SelectListItem> AvailableFruits { get; set; }

        //public Employee()
        //{
        //    SelectedFruits = new List<string>();
        //    AvailableFruits = new List<SelectListItem>();
        //}

    }
}

