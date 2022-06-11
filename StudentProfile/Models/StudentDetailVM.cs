using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentProfile.Models
{
    public class StudentDetailVM : ServiceResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phnno { get; set; }
        public string address { get; set; }
        public int cityid { get; set; }
        public int stateid { get; set; }

    }
    public class ServiceResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public int recordId { get; set; }
    }
    public class clsStudentDetailList : ServiceResponse
    {
        public List<StudentDetailVM> StudentDetailList { get; set; }
    }
    public class DropDown
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public class DropDownListResponse : ServiceResponse
    {
        public List<DropDown> dropdownlist { get; set; }
    }
}