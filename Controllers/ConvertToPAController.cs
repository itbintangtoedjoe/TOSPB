using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ConvertToPAController : Controller
    {
        // GET: ConvertToPA
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ConvertToPA()
        {
            return View();
        }
        public ActionResult CreateToPA()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            if(Session["LoginSuccess"].ToString() == "True")
            {
                return View();
            }
            else
            {
                return Redirect("/Login/index");
            }
            
        }
        public ActionResult EditMemo() 
        {
            return View();
        }
        public ActionResult GetNoMemo()
        {
            List<string> ModelData = new List<string>();

            DataTable dt = new DataTable();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 5;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();
            List<DataRow> objAR = dt.AsEnumerable().ToList();

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            List<DataRow> DataRow = new List<DataRow>();
            int n = 0;
            foreach (DataRow dr in dt.Rows)
            {
                ModelData.Add(dr[0].ToString());
            }

            return Json(ModelData);
        }
        public ActionResult GetTujuanSubdist(string NoMemo)
        {
            List<string> ModelData = new List<string>();

            DataTable dt = new DataTable();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 6;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = NoMemo;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();
            List<DataRow> objAR = dt.AsEnumerable().ToList();

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }
        public ActionResult GetSaldo(string NoSubdist)
        {
            List<string> ModelData = new List<string>();
            DataTable dt = new DataTable();
            string Result;
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 7;

                command.Parameters.Add("@ShipToIDGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@ShipToIDGet"].Value = NoSubdist;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();
            List<DataRow> objAR = dt.AsEnumerable().ToList();

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }
        public ActionResult GetTujuanSubdistBayar(string NoSubdist, string NoMemo)
        {
            List<string> ModelData = new List<string>();

            DataTable dt = new DataTable();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 8;

                command.Parameters.Add("@ShipToIDGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@ShipToIDGet"].Value = NoSubdist;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = NoMemo;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();
            List<DataRow> objAR = dt.AsEnumerable().ToList();

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }


            return Json(rows);
        }
        public ActionResult GetBudgetAvailable(string NoMemo, string LineNumber)
        {
            List<string> ModelData = new List<string>();
            DataTable dt = new DataTable();
           
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 9;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = NoMemo;

                command.Parameters.Add("@LineNumberGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@LineNumberGet"].Value = LineNumber;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();
            List<DataRow> objAR = dt.AsEnumerable().ToList();

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }


            return Json(rows);
        }
        public ActionResult GetItemExpense()
        {
            List<string> ModelData = new List<string>();

            DataTable dt = new DataTable();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 10;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();
            List<DataRow> objAR = dt.AsEnumerable().ToList();

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }
        public ActionResult InsertAddDetail(string NoMemo,int EndBudget, string ShipToID, string LineNumber, int amount, string LineNumberTo, string ListExpense,string NoRekening, string NoteRemarks,string NamaBank,string NamaRekening)
        {
            List<string> ModelData = new List<string>();

            string Result;
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 11;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = NoMemo;

                command.Parameters.Add("@LineNumberGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@LineNumberGet"].Value = LineNumber;

                command.Parameters.Add("@AmountGet", System.Data.SqlDbType.Int);
                command.Parameters["@AmountGet"].Value = amount;

                command.Parameters.Add("@NoRekeningGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoRekeningGet"].Value = NoRekening;

                command.Parameters.Add("@NoteRemarks", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoteRemarks"].Value = NoteRemarks;

                command.Parameters.Add("@ListExpense", System.Data.SqlDbType.NVarChar);
                command.Parameters["@ListExpense"].Value = ListExpense;

                command.Parameters.Add("@LineNumberToGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@LineNumberToGet"].Value = LineNumberTo;

                command.Parameters.Add("@ShipToIDGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@ShipToIDGet"].Value = ShipToID;

                command.Parameters.Add("@EndBudget", System.Data.SqlDbType.Int);
                command.Parameters["@EndBudget"].Value = EndBudget;

                command.Parameters.Add("@NamaBank", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NamaBank"].Value = NamaBank;


                command.Parameters.Add("@NamaRekening", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NamaRekening"].Value = NamaRekening;

                //command.Parameters.Add("@UserNameGet", System.Data.SqlDbType.NVarChar);
                //command.Parameters["@UserNameGet"].Value = Session["Username"];

                Result = (string)command.ExecuteScalar();
            }
            conn.Close();
            ModelData.Add(Result);

            return Json(ModelData);
        }
        public ActionResult GetDetailTO(string NoMemo, string LineNumberTo)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

            DataTable dt = new DataTable();

            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 12;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = NoMemo;

                command.Parameters.Add("@LineNumberToGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@LineNumberToGet"].Value = LineNumberTo;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();
            List<DataRow> objAR = dt.AsEnumerable().ToList();

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return Json(rows);
        }

        public ActionResult CreateToPAExec(string NoSubdist,string LineNumber, string NoMemo)
        {
            List<string> ModelData = new List<string>();

            string Result;
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 15;

                command.Parameters.Add("@ShipToIDGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@ShipToIDGet"].Value = NoSubdist;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = NoMemo;

                command.Parameters.Add("@LineNumberGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@LineNumberGet"].Value = LineNumber;

                Result = (string)command.ExecuteScalar();
            }
            conn.Close();
            ModelData.Add(Result);

            return Json(ModelData);
        }
        public ActionResult CloseExec(string NoMemo, string LineNumber)
        {
            List<string> ModelData = new List<string>();

            string Result;
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 18;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = NoMemo;

                command.Parameters.Add("@LineNumberGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@LineNumberGet"].Value = LineNumber;

                Result = (string)command.ExecuteScalar();
            }
            conn.Close();
            ModelData.Add(Result);

            return Json(ModelData);
        }

        public ActionResult NotKopkarSelected(string ShipToID)
        {
            List<string> ModelData = new List<string>();
            string Result;
            DataTable dt = new DataTable();

            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 20;

                command.Parameters.Add("@ShipToIDGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@ShipToIDGet"].Value = ShipToID;

                Result = (string)command.ExecuteScalar();
            }
            conn.Close();

            ModelData.Add(Result);
            return Json(ModelData);
        }
        public ActionResult DeleteLineTOSLA(string NoMemo, string HeadLineNumber)
        {
            List<string> ModelData = new List<string>();

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

            DataTable dt = new DataTable();

            string Result;
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 20;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = NoMemo;

                command.Parameters.Add("@HeadLNGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@HeadLNGet"].Value = HeadLineNumber;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();
            List<DataRow> objAR = dt.AsEnumerable().ToList();

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return Json(rows);
        }

        //---------------------------------------------------------------------------------------- Update Budget

        public ActionResult GetPerodePA()
        {
            DataTable dt = new DataTable();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 3;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }

        public ActionResult GetNoMemoWithPeriod(string MonthPeriod)
        {
            List<string> ModelData = new List<string>();

            DataTable dt = new DataTable();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 13;

                command.Parameters.Add("@MonthPeriod", System.Data.SqlDbType.NVarChar);
                command.Parameters["@MonthPeriod"].Value = MonthPeriod;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }

        public ActionResult GetSubdistBayarWithPeriod(string NoMemo, string MonthPeriod)
        {
            DataTable dt = new DataTable();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 14;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = NoMemo;

                command.Parameters.Add("@MonthPeriod", System.Data.SqlDbType.NVarChar);
                command.Parameters["@MonthPeriod"].Value = MonthPeriod;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }

        public ActionResult GetBudgetAvailableEdit(string NoMemo, string LineNumber)
        {
            List<string> ModelData = new List<string>();

            string Result;
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 16;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = NoMemo;

                command.Parameters.Add("@LineNumberGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@LineNumberGet"].Value = LineNumber;

                Result = (string)command.ExecuteScalar();
            }
            conn.Close();
            ModelData.Add(Result);

            return Json(ModelData);
        }

        public ActionResult UpdateBudgetAvailable(string NoMemo, string LineNumber, int Amt)
        {
            List<string> ModelData = new List<string>();

            string Result;
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 17;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = NoMemo;

                command.Parameters.Add("@LineNumberGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@LineNumberGet"].Value = LineNumber;

                command.Parameters.Add("@AmtUpdate", System.Data.SqlDbType.Int);
                command.Parameters["@AmtUpdate"].Value = Amt;

                Result = (string)command.ExecuteScalar();
            }
            conn.Close();
            ModelData.Add(Result);

            return Json(ModelData);
        }
        public ActionResult TestVue()
        {
            return View();
        }
        
    }
}