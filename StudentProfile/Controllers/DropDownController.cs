using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentProfile.Models;
using StudentProfile.Class;
using Newtonsoft.Json.Linq;

namespace StudentProfile.Controllers
{
    public class DropDownController : ApiController
    {
        public DropDownListResponse SelectState()
        {
            DataTable dt = new DataTable();
            DropDownListResponse response = new DropDownListResponse();
            List<DropDown> list = new List<DropDown>();
            StudentDetail obj = new StudentDetail(true);
            try
            {
                dt = obj.funGetState().Tables[0];
                for (int i = 1; i < dt.Rows.Count + 1; i++)
                {
                    DropDown dropDown = new DropDown();

                    dropDown.text = dt.Rows[i - 1]["state"].ToString();
                    dropDown.value = dt.Rows[i - 1]["id"].ToString();

                    list.Add(dropDown);
                }
                response.dropdownlist = list;
                response.isSuccess = true;
                response.Message = "Success";
            }
            catch (Exception Ex)
            {
                response.isSuccess = false;
                response.Message = Ex.Message;
            }
            return response;
        }

        public DropDownListResponse SelectCity([FromBody]JObject data)
        {
            int stateID = Convert.ToInt32(data["stateID"]);
            DataTable dt = new DataTable();
            DropDownListResponse response = new DropDownListResponse();
            List<DropDown> list = new List<DropDown>();
            StudentDetail obj = new StudentDetail(true);
            try
            {
                dt = obj.funGetCity(stateID).Tables[0];
                for (int i = 1; i < dt.Rows.Count + 1; i++)
                {
                    DropDown dropDown = new DropDown();

                    dropDown.text = dt.Rows[i - 1]["city"].ToString();
                    dropDown.value = dt.Rows[i - 1]["id"].ToString();

                    list.Add(dropDown);
                }
                response.dropdownlist = list;
                response.isSuccess = true;
                response.Message = "Success";
            }
            catch (Exception Ex)
            {
                response.isSuccess = false;
                response.Message = Ex.Message;
            }
            return response;
        }
    }
}
