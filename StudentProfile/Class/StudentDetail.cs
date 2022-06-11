using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyDBManager;
using BusinessLayer;

namespace StudentProfile.Class
{
    public class StudentDetail : DBManager
    {
        static Int32 _id = 0;
        #region Constructors
        public StudentDetail()
        {
        }

        public StudentDetail(Boolean openConnection)
        {
            InitConnectionObject(clsSetting.GetConnectionString());
        }

        public StudentDetail(string conn)
        {
            InitConnectionObject(conn);
        }

        #endregion


        #region funSelectAllData
        public DataSet funSelectAllData()
        {
            string strWhr = string.Empty;
            
            string str = "Select * From tblStudentDetail where 1=1 ORDER BY name";
            return GetAnyDataset(str, CType.SQL);
        }
        #endregion

        #region Update
        public int UpdateCustom(int id, string name, string email, string phnno, string address, int stateid, int cityid)
        {
            Prepare("SP_tblStudentDetailUpdate", CType.StoredProcedure);
            AddCmdParameter("@id", DType.Int, id, ParaDirection.In, true);
            AddCmdParameter("@name", DType.VarChar, name, ParaDirection.In, true);
            AddCmdParameter("@email", DType.VarChar, email, ParaDirection.In, true);
            AddCmdParameter("@phnno", DType.VarChar, phnno, ParaDirection.In, true);
            AddCmdParameter("@address", DType.VarChar, address, ParaDirection.In, true);
            AddCmdParameter("@stateid", DType.Int, stateid, ParaDirection.In, true);
            AddCmdParameter("@cityid", DType.Int, cityid, ParaDirection.In, true);
            _id = Convert.ToInt32(ExecuteMyScalar());
            ReleaseCommand();
            return _id;
        }
        #endregion

        #region Insert
        public Int32 InsertCustom(string name, string email, string phnno, string address, int stateid, int cityid)
        {
            Prepare("SP_tblStudentDetailInsert", CType.StoredProcedure);
            AddCmdParameter("@name", DType.VarChar, name, ParaDirection.In, true);
            AddCmdParameter("@email", DType.VarChar, email, ParaDirection.In, true);
            AddCmdParameter("@phnno", DType.VarChar, phnno, ParaDirection.In, true);
            AddCmdParameter("@address", DType.VarChar, address, ParaDirection.In, true);
            AddCmdParameter("@stateid", DType.Int, stateid, ParaDirection.In, true);
            AddCmdParameter("@cityid", DType.Int, cityid, ParaDirection.In, true);
            AddCmdParameter("@Inserted", DType.Int, null, ParaDirection.Out, true);
            _id = Convert.ToInt32(ExecuteMyScalar());
            ReleaseCommand();
            return _id;
        }
        #endregion

        #region funEdit
        public DataSet funEdit(int id)
        {
            string str = $@"EXEC SP_GetStudentDataByID {id}" ;
            return GetAnyDataset(str, CType.SQL);
        }
        #endregion

        #region funDelete
        public DataSet funDelete(int id)
        {
            string str = $@"EXEC SP_StudentDetailDelete {id}";
            return GetAnyDataset(str, CType.SQL);
        }
        #endregion

        #region funGetState
        public DataSet funGetState()
        {
            string strWhr = string.Empty;

            string str = "select 0 as id,'Select State' as state union Select * From tblStateMaster ORDER BY id";
            return GetAnyDataset(str, CType.SQL);
        }
        #endregion

        #region funGetCity
        public DataSet funGetCity(int stateID)
        {
            string strWhr = string.Empty;

            string str = "select 0 as id,'Select City' as city union Select id,city From tblCityMaster WHERE stateid = " + stateID + " ORDER BY id";
            return GetAnyDataset(str, CType.SQL);
        }
        #endregion

        #region GetAnyDataset
        public DataSet GetAnyDataset(string cmdName, CType cmdType)
        {
            DataSet ds = new DataSet();
            Prepare(cmdName, cmdType);
            ds = ExecuteDataset();
            ReleaseCommand();
            return ds;
        }
        #endregion

    }

}