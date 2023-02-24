using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.DAL;
using WebApplication1.Models;
using static WebApplication1.Models.DapperModel;

namespace WebApplication1.Controllers
{
    public class UploadMemoController : Controller
    {

        DataAccess DAL = new DataAccess();
        // GET: UploadMemo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadMemoProcess()
        {
            return View();
        }
        public ActionResult List_Subdist()
        {
            return View();
        }
        public ActionResult UploadAttachment(string Periode,string TypePembayaran)
        {
            try
            {
                List<string> ModelData = new List<string>();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                string Result;
                var path = "";
                string FileNameForDB;
                string URLAttachment;
                DataTable dt = new DataTable();
                string conString = ConfigurationManager.ConnectionStrings["dbReserveDiscount"].ConnectionString;
                SqlConnection conn = new SqlConnection(conString);

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    var fileName = Path.GetFileName(TypePembayaran + "_" + Periode + "_" + file.FileName);
                    if(TypePembayaran == "TO")
                    {
                        //path = Path.Combine(@"\\kalbox-b7.bintang7.com\Intranetportal\Intranet Attachment\TO\", fileName);
                        //path = Path.Combine(@"C:\FileUpload\Intranetportal\Intranet Attachment\TO\", fileName);

                        string folderPath = @"C:\FileUpload\Intranetportal\Intranet Attachment\TO\";
                        path = folderPath + fileName;

                        // If the directory doesn't exist then create it.
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                    }


                    if (TypePembayaran == "SPB")
                    {
                        //    path = Path.Combine(@"C:\FileUpload\Intranetportal\Intranet Attachment\SPB\", fileName);
                        string folderPath = @"C:\FileUpload\Intranetportal\Intranet Attachment\SPB\";
                        path = folderPath + fileName;

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                    }
                        //var path = Path.Combine("//10.167.1.78/File Sharing B7/Intranetportal/Intranet Attachment/CCC/AttachmentCC/CAPA/", fileName);

                        file.SaveAs(path);

                    FileNameForDB = fileName;
                }

                List<string> List = new List<string>();

                String excelConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 8.0\"", path);
                //Create Connection to Excel work book 
                string sql;
                if (TypePembayaran == "TO")
                {
                     sql = string.Format("Select [No],[Reg],[Cabang],[ShipToID],[Base],[Nama],[Subdist],[KlaimTO],[NoPA],'','','" + Periode + "' from [{0}]", "Sheet1$");
                }
                else 
                {
                     //SPB
                     sql = string.Format("Select * from [Sheet1$] where [REG] is not null");
                }
                OleDbConnection oledbconn = new OleDbConnection(excelConnString);
                OleDbCommand oledbcmd = new OleDbCommand(sql, oledbconn);

                string ResultETL = "";

                if (TypePembayaran == "TO")
                {
                    using (oledbconn)
                    {
                        oledbconn.Open();
                        DbDataReader dr2 = oledbcmd.ExecuteReader();
                        SqlBulkCopy bulkInsert = new SqlBulkCopy(conString);
                        bulkInsert.DestinationTableName = "tblR_tempUploadTO";
                        bulkInsert.WriteToServer(dr2);
                        oledbconn.Close();
                    }
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                        command.Parameters["@Option"].Value = 1;

                        command.Parameters.Add("@UserNameGet", System.Data.SqlDbType.NVarChar);
                        command.Parameters["@UserNameGet"].Value = "Admin";

                        command.Parameters.Add("@TypePembayaran", System.Data.SqlDbType.NVarChar);
                        command.Parameters["@TypePembayaran"].Value = TypePembayaran;

                        Result = (string)command.ExecuteScalar();
                    }
                    conn.Close();

                    if(Result != "UNVALID")
                    {
                        Session["NoMemo"] = Result;
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                            command.Parameters["@Option"].Value = 2;

                            command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                            command.Parameters["@NoMemoGet"].Value = Result;

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
                }
                else
                {
                    var dictionary1 = new Dictionary<string, object>
                    {
                        {"Option", 4},
                        {"Period", Periode}
                    };
                    DynamicParameters parameters2 = new DynamicParameters(dictionary1); 
                    var truncate = Json(DAL.StoredProcedure(parameters2, "SP_SPB"));

                    using (oledbconn)
                    {
                        oledbconn.Open();
                        DbDataReader dr3 = oledbcmd.ExecuteReader();
                        SqlBulkCopy bulkInserts = new SqlBulkCopy(conString);
                        bulkInserts.DestinationTableName = "temp_SPB";
                        bulkInserts.WriteToServer(dr3);
                        oledbconn.Close();
                    }
                    var dictionary = new Dictionary<string, object>
                    {
                        {"Option", 3},
                        {"Period", Periode}
                    };
                    DynamicParameters parameters = new DynamicParameters(dictionary);
                    return Json(DAL.StoredProcedure(parameters, "SP_SPB"));
                }

                return Json(rows);

            }
            catch(Exception ex)
            {
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbITSupport"];
                string conString = mySetting.ConnectionString;

                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(conString);
                string error = ex.Message;

                string log = error.Replace("'", "");

                string query = @"INSERT INTO T_ErrorLog VALUES ('" + log + "', '" + DateTime.Now + "')";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(dt);
                }

                conn.Close();
                //result = ex.ToString();
                throw ex;
            }

        }

        public ActionResult KonfirmasiUpload(string NoMemo)
        {
            List<string> ModelData = new List<string>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            string Result;
            var path = "";
            string FileNameForDB;
            string URLAttachment;
            DataTable dt = new DataTable();
            string conString = ConfigurationManager.ConnectionStrings["dbReserveDiscount"].ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 4;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = Session["NoMemo"].ToString();

                command.Parameters.Add("@UserNameGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@UserNameGet"].Value = "Admin";

                Result = (string)command.ExecuteScalar();
            }
            conn.Close();

            return Json(ModelData);
        }

        public ActionResult GetPeriod()
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
                command.Parameters["@Option"].Value = 3;

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

        public ActionResult DynamicController(DynamicModel Models, string spname)
        {
            var parameters = new DynamicParameters(Models.Model);
            return Json(DAL.StoredProcedure(parameters, spname), JsonRequestBehavior.AllowGet);

        }

        public ActionResult InsertPaymentDet(InsertPaymentDet paymentDet,DynamicModel Models)
        {
            var pjg = paymentDet.PaymentDetail.Count;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("PERIODSPB");
            dt.Columns.Add("REG");
            dt.Columns.Add("BANK");
            dt.Columns.Add("NOKTP");
            dt.Columns.Add("NOMORREKENING");
            dt.Columns.Add("NAMATEAM");
            dt.Columns.Add("NOSURAT");
            dt.Columns.Add("TOTAL_INCOME"); 
            int trav;
            for (trav = 0; trav < pjg; trav++)
            {
                DataRow rowstype = dt.NewRow();
                rowstype["ID"] = paymentDet.PaymentDetail[trav].id;
                rowstype["PERIODSPB"] = paymentDet.PaymentDetail[trav].PERIODSPB;
                rowstype["REG"] = paymentDet.PaymentDetail[trav].REG;
                rowstype["BANK"] = paymentDet.PaymentDetail[trav].BANK;
                rowstype["NOKTP"] = paymentDet.PaymentDetail[trav].NOKTP;
                rowstype["NOMORREKENING"] = paymentDet.PaymentDetail[trav].NOMORREKENING;
                rowstype["NAMATEAM"] = paymentDet.PaymentDetail[trav].NAMATEAM;
                rowstype["NOSURAT"] = paymentDet.PaymentDetail[trav].NOSURAT;
                rowstype["TOTAL_INCOME"] = paymentDet.PaymentDetail[trav].TOTAL_INCOME;
                dt.Rows.Add(rowstype);
            }
            var parameters = new DynamicParameters(Models.Model);
            parameters.Add("PaymentTable", dt.AsTableValuedParameter("[dbo].[SPB_PaymentDetail]"));

            var spname = "SP_PA_SPB";

            return Json(DAL.StoredProcedure(parameters, spname));
        }
    }
}