using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace WebApplication1.Report_TO
{
    public partial class Report_TO1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conString); DataTable DT = new DataTable();
            DataTable DT1 = new DataTable();
            DataTable DT2 = new DataTable();
            DataTable DT3 = new DataTable();
            DataTable DT4 = new DataTable();
            string NoTO = Request.QueryString.Get("No_TO");
            //var someDataSet = new Report_SPB_Memo();
            var someDataSet = new ReportDocument();
            DataSet data = new DataSet();
            try
            {
                conn.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_CrystalReport_TO", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NO_TO", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NO_TO"].Value = NoTO;

                        command.Parameters.Add("@Option", System.Data.SqlDbType.NVarChar);
                        command.Parameters["@Option"].Value = "Header Data";
                         
                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;
                        dataAdapt.Fill(data, "HeaderData");
                    }
                }
                catch (Exception ex0)
                {

                    throw ex0;
                }

                try
                {
                    using (SqlCommand command = new SqlCommand("SP_CrystalReport_TO", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NO_TO", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NO_TO"].Value = NoTO;

                        command.Parameters.Add("@Option", System.Data.SqlDbType.NVarChar);
                        command.Parameters["@Option"].Value = "Detail Data";

                        SqlDataAdapter dataAdapt1 = new SqlDataAdapter();
                        dataAdapt1.SelectCommand = command;


                        dataAdapt1.Fill(data, "DetailData");
                    }
                }
                catch (Exception ex1)
                {

                    throw ex1;
                }

                try
                {
                    using (SqlCommand command = new SqlCommand("SP_CrystalReport_TO", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NO_TO", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NO_TO"].Value = NoTO;

                        command.Parameters.Add("@Option", System.Data.SqlDbType.NVarChar);
                        command.Parameters["@Option"].Value = "Approver Data";


                        SqlDataAdapter dataAdapt2 = new SqlDataAdapter();
                        dataAdapt2.SelectCommand = command;


                        dataAdapt2.Fill(data, "ApproverData");
                    }
                }
                catch (Exception ex2)
                {

                    throw ex2;

                }

                try
                {
                    using (SqlCommand command = new SqlCommand("SP_CrystalReport_TO", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NO_TO", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NO_TO"].Value = NoTO;

                        command.Parameters.Add("@Option", System.Data.SqlDbType.NVarChar);
                        command.Parameters["@Option"].Value = "Detail Table Data";


                        SqlDataAdapter dataAdapt2 = new SqlDataAdapter();
                        dataAdapt2.SelectCommand = command;


                        dataAdapt2.Fill(data, "DetailTable");
                    }
                }
                catch (Exception ex2)
                {

                    throw ex2;

                }


                //data.AcceptChanges();
                conn.Close();
                someDataSet.Load(Server.MapPath("~/Report_TO/Report_TO_CR.rpt"));
                someDataSet.SetDatabaseLogon("sab7", "Welcome123");
                someDataSet.SetDataSource(data);
                someDataSet.VerifyDatabase();
                //someDataSet.SetDatabaseLogon("sab7", "Welcome1234");
                //someDataSet.Database.Tables[4].SetDataSource(data.Tables["PA_SPB"]);
                //someDataSet.Database.Tables[2].SetDataSource(data.Tables["DETAILSPB"]);
                //someDataSet.Database.Tables[1].SetDataSource(data.Tables["Footer1"]);
                //someDataSet.Database.Tables[2].SetDataSource(data.Tables["Footer2"]);
                //someDataSet.Database.Tables[3].SetDataSource(data.Tables["Footer3"]);
                CrystalReportViewer2.ReportSource = someDataSet;
                CrystalReportViewer2.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
                // CrystalReportViewer1.RefreshReport();
                Session["myReport"] = someDataSet;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //}s
        }
    }
}