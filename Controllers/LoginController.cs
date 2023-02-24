using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = false)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        [DllImport("kernel32.dll")]
        public static extern int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId, ref string lpBuffer, int nSize, ref IntPtr Arguments);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
       
        public static extern bool CloseHandle(IntPtr handle);
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Session_Error()
        {
            return View();
        }
        public ActionResult TestLokasi()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> LoginExec(string Username, string Password, string ADFlag)
        {
            List<string> ModelData = new List<string>();
            IntPtr tokenHandle = new IntPtr(0);
            DataTable dt = new DataTable();
            string conString = ConfigurationManager.ConnectionStrings["dbReserveDiscount"].ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            using (var client = new HttpClient())
            {
                string LoginApiBasePath = ConfigurationManager.AppSettings["LoginApiBasePath"];
                client.DefaultRequestHeaders.Clear();

                List<string> List = new List<string>();
                try
                 {
                    string UserName, MachineName, Pwd = null;
                    var json = JsonConvert.SerializeObject(new
                    {
                        Username = Username,
                        Password = Password.ToString(),
                    });
                    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                    HttpResponseMessage Res = await client.PostAsync(LoginApiBasePath + "/Login", new StringContent(json, UnicodeEncoding.UTF8, "application/json"));

                    dynamic response = JObject.Parse(await Res.Content.ReadAsStringAsync());
                    string responseMessage = response.message.ToString().ToLower();
                    //Session["LoginSuccess"] = "True";
                    //Session["UserName"] = Username;

                    if (ADFlag == "isAD")
                    {
                        //bool returnValue = LogonUser(UserName, MachineName, Pwd, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);
                        /*if (returnValue == false) //matiin dulu fungsi ini biar bisa login pake AD
                         {
                             ModelData.Add("Wrong Credentials");
                         }
                         else
                         {*/
                        if (Res.IsSuccessStatusCode && responseMessage == "success")
                        {
                            conn.Open();
                            using (SqlCommand command = new SqlCommand("[dbo].[SP_Signature]", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@Option", System.Data.SqlDbType.NVarChar);
                                command.Parameters["@Option"].Value = "Get Existing Data AD";

                                command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
                                command.Parameters["@Username"].Value = Username;

                                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                                dataAdapt.SelectCommand = command;

                                dataAdapt.Fill(dt);
                            }
                            conn.Close();
                            //This function returns the error code that the last unmanaged function returned.

                            Session["LoginSuccess"] = "True";
                            Session["UserName"] = Username;
                            Session["Role"] = dt.Rows[0]["Role"].ToString();
                            Session["NamaEmployee"] = dt.Rows[0]["NamaEmployee"].ToString();
                            ModelData.Add(dt.Rows[0][0].ToString());
                        }
                        else
                        {
                            Session["LoginSuccess"] = "False";
                            ModelData.Add("Wrong Credentials");
                        }
                    }
                    else if(ADFlag == "isNotAD")
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("[dbo].[SP_Signature]", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@Option", System.Data.SqlDbType.NVarChar);
                            command.Parameters["@Option"].Value = "Get Existing Data Not AD";

                            command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
                            command.Parameters["@Username"].Value = Username;

                            command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar);
                            command.Parameters["@Password"].Value = Password;

                            SqlDataAdapter dataAdapt = new SqlDataAdapter();
                            dataAdapt.SelectCommand = command;

                            dataAdapt.Fill(dt);
                        }
                        conn.Close();

                        Session["LoginSuccess"] = "True";
                        Session["UserName"] = Username;
                        Session["Role"] = dt.Rows[0]["Role"].ToString();
                        Session["NamaEmployee"] = dt.Rows[0]["NamaEmployee"].ToString();
                        ModelData.Add(dt.Rows[0][0].ToString());
                    
                }
            }
            catch (Exception ex)
            {
                Session["LoginSuccess"] = "False";
            }

                return Json(ModelData);
            }
        }
        
    }
}