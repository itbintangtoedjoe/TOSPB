using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using CrystalDecisions.Shared;
using PAOnline.Models;
using RestSharp;
using WebApplication1.Models;
using ZXing;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace WebApplication1.Controllers
{
    public class CMSController : Controller
    {
        readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];

        readonly DataTable DT = new DataTable();

        // GET: CMS
        public ActionResult CMS_EO()
        {
            return View();
        }

        public ActionResult CMS_Signature()
        {
            return View();
        }

        public ActionResult CMS_SPBList()
        {
            return View();
        }

        public ActionResult CMS_SPBApproval()
        {
            return View();
        }

        public ActionResult CMS_TOApproval()
        {
            return View();
        }

        public ActionResult MappingApprover()
        {
            return View();
        }

        public ActionResult CMS_TOList()
        {
            return View();
        }

        public ActionResult CMS_GetNamaEmployee(SignatureModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_Signature]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Get Nama DDL";

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(DT);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in DT.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in DT.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }

                rows.Add(row);
            }

            return Json(rows);
        }

        public ActionResult CMS_UploadFile()
        {
         
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            var path = "";
            string FileNameForDB;
            List<string> List = new List<string>();

            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    string UniqueID = Guid.NewGuid().ToString("N");

                    var fileName = Path.GetFileName(UniqueID + "_" + file.FileName);

                    //path = Path.Combine(@"\\b7-drive.bintang7.com\File Upload Intranet\SPB\", fileName);

                    //path = Path.Combine(@"D:\Documents\Internship\B7\Signatures\", fileName);
                    //string folderPath = Server.MapPath("../Signatures");

                    string folderPath = @"C:\FileUpload\Signatures\";
                    path = folderPath + fileName;

                    file.SaveAs(path);
                    List.Add(path);

                    FileNameForDB = fileName;
                }
            }

            catch (Exception ex)
            {
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbITSupport"];
                string conString = mySetting.ConnectionString;
                string Result;

                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(conString);
                conn.Open();

                string query = "INSERT INTO T_ErrorLog VALUES (" + ex + ", "+ DateTime.Now + ")";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dt);
                conn.Close();
                da.Dispose();
                conn.Close();
                //result = ex.ToString();
                throw ex;
            }
           
            return Json(path);
        }

        public ActionResult Generate(LibraryModel qrcode)
        {
            try
            {
                if (qrcode.Flag == "SPB")
                {
                    qrcode.QRCodeImagePath = GenerateQRCodeSPB(qrcode.QRCodeText, qrcode.NoSurat);
                }
                if (qrcode.Flag == "TO")
                {
                    qrcode.QRCodeImagePath = GenerateQRCodeTO(qrcode.QRCodeText, qrcode.NoTO);
                }
                ViewBag.Message = "QR Code Created successfully";
            }
            catch (Exception ex)
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
                    dataAdapter.Fill(DT);
                }

                conn.Close();
                //result = ex.ToString();
                throw ex;
            }
            return Json(qrcode.QRCodeImagePath);
        }

        private string GenerateQRCodeSPB(string qrcodeText, string NoSurat)
        {
            //string folderPath = @"\\b7-drive.bintang7.com\File Upload Intranet\DFISQRCode\SPB";
            //var path = Server.MapPath("~/App_Data");
            //var fullpath = Path.Combine(path, "myfile.txt");
            //string folderPath = @"D:\Documents\Internship\B7\Signatures\SPB";
            //string imagePath = @"\\b7-drive.bintang7.com\File Upload Intranet\DFISQRCode\SPB\" + NoSurat + ".png";

            //string imagePath = @"D:\Documents\Internship\B7\Signatures\SPB\" + NoSurat + ".png";
            try 
            {
                //string folderPath = Server.MapPath("../Signatures/SPB");
                //string imagePath = Path.Combine(folderPath, NoSurat + ".png");

                string folderPath = @"C:\FileUpload\Signatures\DFISQRCode\SPB\";
                string imagePath = folderPath + NoSurat + ".png";

                // If the directory doesn't exist then create it.
                //if (!Directory.Exists(folderPath))
                //{
                //    Directory.CreateDirectory(folderPath);
                //}

                var barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.QR_CODE;
                var result = barcodeWriter.Write(qrcodeText);

                //string barcodePath = Server.MapPath(imagePath);
                string barcodePath = imagePath;
                var barcodeBitmap = new Bitmap(result);
                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }

                return imagePath;
            }
            catch (Exception ex)
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
                    dataAdapter.Fill(DT);
                }

                conn.Close();
                //result = ex.ToString();
                throw ex;
            }

        }

                    


        private string GenerateQRCodeTO(string qrcodeText, string NoTO)
        {
            //string folderPath = @"\\b7-drive.bintang7.com\File Upload Intranet\DFISQRCode\TO";
            //string imagePath = @"\\b7-drive.bintang7.com\File Upload Intranet\DFISQRCode\TO\" + NoTO + ".png";

            // string folderPath = @"D:\Documents\Internship\B7\Signatures\TO";
            //string imagePath = @"D:\Documents\Internship\B7\Signatures\TO\" + NoTO + ".png";
            try
            {
                //string folderPath = Server.MapPath("../Signatures/TO");
                //string imagePath = Path.Combine(folderPath, NoTO + ".png");

                string folderPath = @"C:\FileUpload\Signatures\DFISQRCode\TO\";
                string imagePath = folderPath + NoTO + ".png";

                // If the directory doesn't exist then create it.
                //if (!Directory.Exists(folderPath))
                //{
                //    Directory.CreateDirectory(folderPath);
                //}

                var barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.QR_CODE;
                var result = barcodeWriter.Write(qrcodeText);

                //string barcodePath = Server.MapPath(imagePath);
                string barcodePath = imagePath;
                var barcodeBitmap = new Bitmap(result);
                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }

                return imagePath;
            }
            catch (Exception ex)
            {
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbITSupport"];
                string conString = mySetting.ConnectionString;
                string Result;

                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(conString);
                conn.Open();

                string query = "INSERT INTO T_ErrorLog VALUES (" + ex + ", " + DateTime.Now + ")";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dt);
                conn.Close();
                da.Dispose();
                conn.Close();
                //result = ex.ToString();
                throw ex;
            }
        }

        //EMAIL CONTROLLERS
        public ActionResult SubmitMappingSPB(SPBTOModel Model)
        {
            string result = "";
            List<string> ModelData = new List<string>();   
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
           
                try
                {
                    Conn.Open();
                    using (SqlCommand command = new SqlCommand("SP_Signature", Conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Option", SqlDbType.NVarChar);
                        command.Parameters["@Option"].Value = Model.Option;

                        command.Parameters.Add("@NoSPB", SqlDbType.NVarChar);
                        command.Parameters["@NoSPB"].Value = Model.NoSPB;

                        command.Parameters.Add("@Approver1", SqlDbType.NVarChar);
                        command.Parameters["@Approver1"].Value = Model.Approver1;

                        command.Parameters.Add("@Approver2", SqlDbType.NVarChar);
                        command.Parameters["@Approver2"].Value = Model.Approver2;

                        command.Parameters.Add("@Approver3", SqlDbType.NVarChar);
                        command.Parameters["@Approver3"].Value = Model.Approver3;

                        command.Parameters.Add("@Approver4", SqlDbType.NVarChar);
                        command.Parameters["@Approver4"].Value = Model.Approver4;

                        command.Parameters.Add("@ApproverDetail1", SqlDbType.NVarChar);
                        command.Parameters["@ApproverDetail1"].Value = Model.ApproverDetail1;

                        command.Parameters.Add("@ApproverDetail2", SqlDbType.NVarChar);
                        command.Parameters["@ApproverDetail2"].Value = Model.ApproverDetail2;

                        command.Parameters.Add("@ApproverDetail3", SqlDbType.NVarChar);
                        command.Parameters["@ApproverDetail3"].Value = Model.ApproverDetail3;

                        command.Parameters.Add("@ApproverDetail4", SqlDbType.NVarChar);
                        command.Parameters["@ApproverDetail4"].Value = Model.ApproverDetail4;

                        command.Parameters.Add("@Uploader", SqlDbType.NVarChar);
                        command.Parameters["@Uploader"].Value = Model.Uploader;


                        command.CommandType = CommandType.StoredProcedure;
                            result = (string)command.ExecuteScalar();
                    }
                    Conn.Close();


                    MailModel objsend = new MailModel();
                    objsend.mailPriority = "High";
                    objsend.isHtml = true;
                    objsend.mailSubject = "SPB Need Your Approval!";
                    objsend.mailBody = "<html> <body><label style='color: black'> Dear " + Model.Approver1 + ", </label> <br/>  " +
                                       "<label style='color: black'> Dengan E-mail ini kami menginformasikan bahwa adanya SPB yang perlu anda approve. </label><br/>  <br/>" +
                                       "<table style ='float:left'><tr>" +
                                       "<td> No.SPB </td>" + "<td>:</td><td><b> " + Model.NoSPB + " </b>" +
                                       "</td>" +
                                       "</tr>" +
                                       "<tr><td> Waktu Pembuatan SPB </td><td>:</td><td><b>" + Model.SubmitDate + " </b>" +
                                       "</td>" +
                                       "<tr><td> Access </td><td>:</td><td><b> <a href = 'https://portal.bintang7.com/ClaimOnlineDF/Home/Login'> Klik disini untuk melakukan approval </a> </b>" + "</td>" +
                                       "</tr>" +
                                       "</table>" +
                                       "</body>" +
                                       "</html>";
                    //objsend.mailTo = Model.Email1;
                    objsend.mailTo = "michaelken117@gmail.com";
                    objsend.mailCC = null;
                    objsend.mailBCC = null;

                    //SendMail(objsend);

                }
                catch (Exception ex)
                {
                    //result = ex.ToString();
                    throw ex;
                }

                return Json(rows);
        }

        public ActionResult SubmitMappingTO(SPBTOModel Model)
        {
            string result ;
            List<string> ModelData = new List<string>();
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Signature", Conn))
                {

                    command.Parameters.Add("@Option", SqlDbType.NVarChar);
                    command.Parameters["@Option"].Value = Model.Option;

                    command.Parameters.Add("@NoTO", SqlDbType.NVarChar);
                    command.Parameters["@NoTO"].Value = Model.NoTO;

                    command.Parameters.Add("@Approver1", SqlDbType.NVarChar);
                    command.Parameters["@Approver1"].Value = Model.Approver1;

                    command.Parameters.Add("@Approver2", SqlDbType.NVarChar);
                    command.Parameters["@Approver2"].Value = Model.Approver2;

                    command.Parameters.Add("@Approver3", SqlDbType.NVarChar);
                    command.Parameters["@Approver3"].Value = Model.Approver3;

                    command.Parameters.Add("@Approver4", SqlDbType.NVarChar);
                    command.Parameters["@Approver4"].Value = Model.Approver4;

                    command.Parameters.Add("@ApproverDetail1", SqlDbType.NVarChar);
                    command.Parameters["@ApproverDetail1"].Value = Model.ApproverDetail1;

                    command.Parameters.Add("@ApproverDetail2", SqlDbType.NVarChar);
                    command.Parameters["@ApproverDetail2"].Value = Model.ApproverDetail2;

                    command.Parameters.Add("@ApproverDetail3", SqlDbType.NVarChar);
                    command.Parameters["@ApproverDetail3"].Value = Model.ApproverDetail3;

                    command.Parameters.Add("@ApproverDetail4", SqlDbType.NVarChar);
                    command.Parameters["@ApproverDetail4"].Value = Model.ApproverDetail4;

                    command.Parameters.Add("@Uploader", SqlDbType.NVarChar);
                    command.Parameters["@Uploader"].Value = Model.Uploader;

                    command.Parameters.Add("@Region", SqlDbType.NVarChar);
                    command.Parameters["@Region"].Value = Model.Region;

                    command.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                    command.Parameters["@Remarks"].Value = Model.Remarks;

                    command.CommandType = CommandType.StoredProcedure;
                    result = (string)command.ExecuteScalar();
                    ModelData.Add(result);
                }
                Conn.Close();


                MailModel objsend = new MailModel();
                objsend.mailPriority = "High";
                objsend.isHtml = true;
                objsend.mailSubject = "TO Need Your Approval!";
                objsend.mailBody = "<html> <body><label style='color: black'> Dear " + Model.Approver1 + ", </label> <br/>  " +
                                   "<label style='color: black'> Dengan E-mail ini kami menginformasikan bahwa adanya TO yang perlu anda approve. </label><br/>  <br/>" +
                                   "<table style ='float:left'><tr>" +
                                   "<td> No.TO </td>" + "<td>:</td><td><b> " + Model.NoTO + " </b>" +
                                   "</td>" +
                                   "</tr>" +
                                   "<tr><td> Waktu Pembuatan TO </td><td>:</td><td><b>" + Model.SubmitDate + " </b>" +
                                   "</td>" +
                                   "<tr><td> Access </td><td>:</td><td><b> <a href = 'https://portal.bintang7.com/ClaimOnlineDF/Home/Login'> Klik disini untuk melakukan approval </a> </b>" + "</td>" +
                                   "</tr>" +
                                   "</table>" +
                                   "</body>" +
                                   "</html>";
                var email = Model.Email1;
                objsend.mailTo = "michaelken117@gmail.com";
                objsend.mailCC = null;
                objsend.mailBCC = null;

                //SendMail(objsend);

            }
            catch (Exception ex)
            {
                //result = ex.ToString();
                throw ex;
            }

            return Json(rows);
        }

        public ActionResult GetEmailDataSPB(SPBTOModel Model)
        {
            string result = "";
            List<string> ModelData = new List<string>();
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            if (Model.FlagStatus == "approve")
            {
                try
                {
                    Conn.Open();
                    using (SqlCommand command = new SqlCommand("SP_Signature", Conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Option", SqlDbType.VarChar);
                        command.Parameters["@Option"].Value = "Get Email Data SPB";

                        command.Parameters.Add("@NoSPB", SqlDbType.VarChar);
                        command.Parameters["@NoSPB"].Value = Model.NoSPB;

                        command.Parameters.Add("@LineNumber", SqlDbType.VarChar);
                        command.Parameters["@LineNumber"].Value = Model.LineNumber;

                        command.Parameters.Add("@Uploader", SqlDbType.VarChar);
                        command.Parameters["@Uploader"].Value = Model.Uploader;

                        SqlDataAdapter dataAdap = new SqlDataAdapter();
                        dataAdap.SelectCommand = command;
                        dataAdap.Fill(DT);

                    }


                    foreach (DataRow dr in DT.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in DT.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }

                        rows.Add(row);
                    }
                    Conn.Close();


                    MailModel objsend = new MailModel();
                    objsend.mailPriority = "High";
                    objsend.isHtml = true;
                    objsend.mailSubject = "SPB Need Your Approval!";
                    objsend.mailBody = "<html> <body><label style='color: black'> Dear " + rows[0]["NamaEmployee"] + ", </label> <br/>  " +
                                       "<label style='color: black'> Dengan E-mail ini kami menginformasikan bahwa adanya SPB yang perlu anda approve. </label><br/>  <br/>" +
                                       "<table style ='float:left'><tr>" +
                                       "<td> No.SPB </td>" + "<td>:</td><td><b> " + Model.NoSPB + " </b>" +
                                       "</td>" +
                                       "</tr>" +
                                       "<tr><td> Waktu Pembuatan SPB </td><td>:</td><td><b>" + rows[0]["CreationDate"] + " </b>" +
                                       "</td>" +
                                       "<tr><td> Access </td><td>:</td><td><b> <a href = 'https://portal.bintang7.com/ClaimOnlineDF/Home/Login'> Klik disini untuk melakukan approval </a> </b>" + "</td>" +
                                       "</tr>" +
                                       "</table>" +
                                       "</body>" +
                                       "</html>";
                    //objsend.mailTo = (string)rows[0]["EmailNext"];
                    //var emailnext = (string)rows[0]["EmailNext"];
                    objsend.mailTo = "michaelken117@gmail.com";
                    objsend.mailCC = null;
                    objsend.mailBCC = null;

                    //SendMail(objsend);

                }
                catch (Exception ex)
                {
                    //result = ex.ToString();
                    throw ex;
                }
            }
            else if (Model.FlagStatus == "rejected")
            {
                try
                {
                    Conn.Open();
                    using (SqlCommand command = new SqlCommand("SP_Signature", Conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Option", SqlDbType.VarChar);
                        command.Parameters["@Option"].Value = "Get Email Data Reject SPB";

                        command.Parameters.Add("@NoSPB", SqlDbType.VarChar);
                        command.Parameters["@NoSPB"].Value = Model.NoSPB;

                        command.Parameters.Add("@Uploader", SqlDbType.VarChar);
                        command.Parameters["@Uploader"].Value = Model.Uploader;

                        command.Parameters.Add("@AlasanReject", SqlDbType.VarChar);
                        command.Parameters["@AlasanReject"].Value = Model.AlasanReject;

                        SqlDataAdapter dataAdap = new SqlDataAdapter();
                        dataAdap.SelectCommand = command;
                        dataAdap.Fill(DT);

                    }


                    foreach (DataRow dr in DT.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in DT.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }

                        rows.Add(row);
                    }
                    Conn.Close();

                    for (int i = 0; i < rows.Count; i++)
                    {
                        MailModel objsend = new MailModel();
                        objsend.mailPriority = "High";
                        objsend.isHtml = true;
                        objsend.mailSubject = "SPB Telah Di Reject! ";
                        objsend.mailBody = "<html> <body><label style='color: black'> Dear " + rows[i]["NamaEmployee"] + ", </label> <br/>  " +
                                           "<label style='color: black'> Dengan E-mail ini kami menginformasikan bahwa No. SPB: <b>" + rows[i]["NoSPB"] + "</b> </label><br/> " +
                                           "<label style='color: black'> telah di reject oleh: <b>" + rows[i]["Rejecter"] + "</b> </label><br/>  <br/>" +
                                           "<table style ='float:left'>" +
                                           "<tr>" +
                                           "<td> Alasan Reject </td><td>:</td><td><b style='color:red'>" + Model.AlasanReject + " </b>" +
                                           "</td>" +
                                           "</tr>" +
                                           "<tr>" +
                                           "<td> Waktu Reject </td><td>:</td><td><b> " + Model.WaktuReject + " </b>" +
                                           "</td>" +
                                           "</tr>" +
                                           "<tr>" +
                                           "<td> Access </td><td>:</td><td><b> <a href = 'https://portal.bintang7.com/ClaimDFIS/Home/Login'> Klik disini untuk membuka aplikasi SPB </a> </b>" + "</td>" +
                                           "</tr>" +
                                           "</table>" +
                                           "</body>" +
                                           "</html>";
                        objsend.mailTo = "michaelken117@gmail.com";
                        //objsend.mailTo = (string)rows[i]["Email"];
                        objsend.mailCC = null;
                        objsend.mailBCC = null;

                        //SendMail(objsend);
                    }

                }
                catch (Exception ex)
                {
                    //result = ex.ToString();
                    throw ex;
                }
            }

            return Json(rows);
        }

        public ActionResult GetEmailDataTO(SPBTOModel Model)
        {
            string result = "";
            List<string> ModelData = new List<string>();
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            if (Model.FlagStatus == "approve")
            {
                try
                {
                    Conn.Open();
                    using (SqlCommand command = new SqlCommand("SP_Signature", Conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Option", SqlDbType.VarChar);
                        command.Parameters["@Option"].Value = "Get Email Data TO";

                        command.Parameters.Add("@NoTO", SqlDbType.VarChar);
                        command.Parameters["@NoTO"].Value = Model.NoTO;

                        command.Parameters.Add("@LineNumber", SqlDbType.VarChar);
                        command.Parameters["@LineNumber"].Value = Model.LineNumber;

                        command.Parameters.Add("@Uploader", SqlDbType.VarChar);
                        command.Parameters["@Uploader"].Value = Model.Uploader;

                        SqlDataAdapter dataAdap = new SqlDataAdapter();
                        dataAdap.SelectCommand = command;
                        dataAdap.Fill(DT);

                    }


                    foreach (DataRow dr in DT.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in DT.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }

                        rows.Add(row);
                    }
                    Conn.Close();


                    MailModel objsend = new MailModel();
                    objsend.mailPriority = "High";
                    objsend.isHtml = true;
                    objsend.mailSubject = "TO Need Your Approval!";
                    objsend.mailBody = "<html> <body><label style='color: black'> Dear " + rows[0]["NamaEmployee"] + ", </label> <br/>  " +
                                       "<label style='color: black'> Dengan E-mail ini kami menginformasikan bahwa adanya TO yang perlu anda approve. </label><br/>  <br/>" +
                                       "<table style ='float:left'><tr>" +
                                       "<td> No.TO </td>" + "<td>:</td><td><b> " + Model.NoTO + " </b>" +
                                       "</td>" +
                                       "</tr>" +
                                       "<tr><td> Waktu Pembuatan TO </td><td>:</td><td><b>" + rows[0]["CreationDate"] + " </b>" +
                                       "</td>" +
                                       "<tr><td> Access </td><td>:</td><td><b> <a href = 'https://portal.bintang7.com/ClaimOnlineDF/Home/Login'> Klik disini untuk melakukan approval </a> </b>" + "</td>" +
                                       "</tr>" +
                                       "</table>" +
                                       "</body>" +
                                       "</html>";
                    //objsend.mailTo = (string)rows[0]["EmailNext"];
                    var emailnext = (string)rows[0]["EmailNext"];
                    objsend.mailTo = "michaelken117@gmail.com";
                    objsend.mailCC = null;
                    objsend.mailBCC = null;

                    //SendMail(objsend);

                }
                catch (Exception ex)
                {
                    //result = ex.ToString();
                    throw ex;
                }
            }
            else if (Model.FlagStatus == "rejected")
            {
                try
                {
                    Conn.Open();
                    using (SqlCommand command = new SqlCommand("SP_Signature", Conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Option", SqlDbType.VarChar);
                        command.Parameters["@Option"].Value = "Get Email Data Reject TO";

                        command.Parameters.Add("@NoTO", SqlDbType.VarChar);
                        command.Parameters["@NoTO"].Value = Model.NoTO;

                        command.Parameters.Add("@Uploader", SqlDbType.VarChar);
                        command.Parameters["@Uploader"].Value = Model.Uploader;

                        command.Parameters.Add("@AlasanReject", SqlDbType.VarChar);
                        command.Parameters["@AlasanReject"].Value = Model.AlasanReject;

                        SqlDataAdapter dataAdap = new SqlDataAdapter();
                        dataAdap.SelectCommand = command;
                        dataAdap.Fill(DT);

                    }


                    foreach (DataRow dr in DT.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in DT.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }

                        rows.Add(row);
                    }
                    Conn.Close();

                    for (int i = 0; i < rows.Count; i++)
                    {
                        MailModel objsend = new MailModel();
                        objsend.mailPriority = "High";
                        objsend.isHtml = true;
                        objsend.mailSubject = "TO Telah Di Reject! ";
                        objsend.mailBody = "<html> <body><label style='color: black'> Dear " + rows[i]["NamaEmployee"] + ", </label> <br/>  " +
                                           "<label style='color: black'> Dengan E-mail ini kami menginformasikan bahwa No. TO: <b>" + rows[i]["NoTO"] + "</b> </label><br/> " +
                                           "<label style='color: black'> telah di reject oleh: <b>" + rows[i]["Rejecter"] + "</b> </label><br/>  <br/>" +
                                           "<table style ='float:left'>" +
                                           "<tr>" +
                                           "<td> Alasan Reject </td><td>:</td><td><b style='color:red'>" + Model.AlasanReject + " </b>" +
                                           "</td>" +
                                           "</tr>" +
                                           "<tr>" +
                                           "<td> Waktu Reject </td><td>:</td><td><b> " + Model.WaktuReject + " </b>" +
                                           "</td>" +
                                           "</tr>" +
                                           "<tr>" +
                                           "<td> Access </td><td>:</td><td><b> <a href = 'https://portal.bintang7.com/ClaimDFIS/Home/Login'> Klik disini untuk membuka aplikasi TO </a> </b>" + "</td>" +
                                           "</tr>" +
                                           "</table>" +
                                           "</body>" +
                                           "</html>";
                        objsend.mailTo = "michaelken117@gmail.com";
                        //objsend.mailTo = (string)rows[i]["Email"];
                        objsend.mailCC = null;
                        objsend.mailBCC = null;

                        //SendMail(objsend);
                    }

                }
                catch (Exception ex)
                {
                    //result = ex.ToString();
                    throw ex;
                }
            }

            return Json(rows);
        }

        //KOMEN DULU BIAR GA KE KIRIM EMAIL
        private string SendMail(MailModel value)
        {
            string result = "";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            var client = new RestClient("https://portal.bintang7.com/MailSendingServices/API/GetMailData");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");

            var BodyJson = new
            {
                mailPriority = value.mailPriority,
                isHtml = value.isHtml,
                mailSubject = value.mailSubject,
                mailBody = value.mailBody,
                mailTo = value.mailTo,
                mailCC = value.mailCC,
                mailBCC = value.mailBCC
            };
            request.AddBody(BodyJson);
            var response = client.Post(request);
            var content = response.Content; // Raw content as string


            return result;
        }
    }
}