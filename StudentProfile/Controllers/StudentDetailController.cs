using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;
using StudentProfile.Models;
using StudentProfile.Class;
using System.Web.Http;
using Newtonsoft.Json;
using StudentProfile.Cors;

namespace StudentProfile.Controllers
{
    
    public class StudentDetailController : ApiController
    {
    
        [AllowCrossSite]
        [ActionName("StudentList")]
        [HttpPost]

        public clsStudentDetailList StudentList([FromBody]JObject data)
        {
            DataTable dt = new DataTable();
            clsStudentDetailList response = new clsStudentDetailList();
            StudentDetail objStudent = new StudentDetail(true); 
            try
            {
                dt = objStudent.funSelectAllData().Tables[0];
                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;
                foreach (DataRow row in dt.Rows)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        childRow.Add(col.ColumnName, row[col]);
                    }
                    parentRow.Add(childRow);
                }
                var Jdata = JsonConvert.SerializeObject(parentRow);
                List<StudentDetailVM> List = JsonConvert.DeserializeObject<List<StudentDetailVM>>(Jdata);
                response.StudentDetailList = List;
                response.Message = "success";
                response.isSuccess = true;
            }
            catch (Exception Ex)
            {
                response.Message = Ex.Message.ToString();
                response.isSuccess = false;
            }
            return response;
        }

        [ActionName("InsertStudent")]
        [HttpPost]
        public ServiceResponse InsertStudent([FromBody]StudentDetailVM data)
        {
            ServiceResponse response = new ServiceResponse();
            StudentDetail obj = new StudentDetail(true);
            int id = 0;
            try
            {
                
                if (clsSetting.CheckRefrence("tblstudentdetail", " and phnno='" + data.phnno.Trim() + "' and id <> " + data.id) == true)
                {
                    response.Message = "Phone no already exist";
                    response.isSuccess = false;
                    return response;
                }
                if (clsSetting.CheckRefrence("tblstudentdetail", " and email='" + data.email.Trim() + "' and id <> " + data.id) == true)
                {
                    response.Message = "Email ID already exist";
                    response.isSuccess = false;
                    return response;
                }
               
                if (data.id > 0)
                {
                    obj.UpdateCustom(data.id, data.name.Trim(), data.email, data.phnno, data.address, data.stateid, data.cityid);
                    response.Message = "Record Update SuccessFully";
                }
                else
                {
                    id = Convert.ToInt32(obj.InsertCustom(data.name.Trim(), data.email, data.phnno, data.address, data.stateid, data.cityid));
                    response.Message = "Record Insert SuccessFully";
                }

                response.isSuccess = true;
                response.recordId = id;
            }
            catch (Exception Ex)
            {
                response.Message = Ex.Message.ToString();
                response.isSuccess = false;
                response.recordId = 0;
            }
            return response;
        }

        [ActionName("EditStudent")]
        [HttpPost]
        public StudentDetailVM EditStudent([FromBody]StudentDetailVM data)
        {
            DataTable dt = new DataTable();
            StudentDetailVM response = new StudentDetailVM();
            StudentDetail obj = new StudentDetail(true);
            int id = data.id;

            try
            {
                dt = obj.funEdit(data.id).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    response.id = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                    response.name = dt.Rows[0]["name"].ToString().Trim();
                    response.phnno = dt.Rows[0]["phnno"].ToString();
                    response.email = dt.Rows[0]["email"].ToString();
                    response.address = dt.Rows[0]["address"].ToString();
                    response.cityid = Convert.ToInt32(dt.Rows[0]["cityid"].ToString());
                    response.stateid = Convert.ToInt32(dt.Rows[0]["stateid"].ToString());
                    response.isSuccess = true;
                    response.Message = "success";
                }
                else {
                    response.isSuccess = true;
                    response.Message = "Record does not exist";
                }

            }
            catch (Exception Ex)
            {
                response.isSuccess = false;
                response.Message = Ex.Message.ToString();
            }

            return response;
        }

        [ActionName("DeleteStudent")]
        [HttpPost]
        public ServiceResponse DeleteStudent([FromBody]StudentDetailVM data)
        {
            DataTable dt = new DataTable();
            ServiceResponse response = new ServiceResponse();
            StudentDetail obj = new StudentDetail(true);

            try
            {
                obj.funDelete(data.id);
                response.Message = "Delete Successfully";
                response.isSuccess = true;
            }
            catch (Exception Ex)
            {
                response.isSuccess = false;
                response.Message = Ex.Message.ToString();
            }

            return response;
        }
    }
}
