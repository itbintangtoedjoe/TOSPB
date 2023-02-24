using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace WebApplication1.Report_SPB
{
    public partial class Report_SPB_Memo1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack && !IsCallback)
            //{

                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
                string conString = mySetting.ConnectionString;
                SqlConnection conn = new SqlConnection(conString);
                DataTable DT = new DataTable();
                DataTable DT1 = new DataTable();
                DataTable DT2 = new DataTable();
                DataTable DT3 = new DataTable();
                DataTable DT4 = new DataTable();
                string No_Surat = Request.QueryString.Get("No_Surat");
                //var someDataSet = new Report_SPB_Memo();
                var someDataSet = new ReportDocument();
                DataSet data = new DataSet();
               
                try
                {
                    conn.Open();
                    try
                    {
                        using (SqlCommand command = new SqlCommand("SP_PA_SPB", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@NOMSurat", System.Data.SqlDbType.VarChar);
                            command.Parameters["@NOMSurat"].Value = No_Surat;

                            command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                            command.Parameters["@Option"].Value = 15;

                            SqlDataAdapter dataAdapt = new SqlDataAdapter();
                            dataAdapt.SelectCommand = command;
                            dataAdapt.Fill(data,"PA_SPB_Memo");
                        }
                    }
                    catch (Exception ex0)
                    {

                        throw ex0;
                    }

                    try
                    {
                        using (SqlCommand command = new SqlCommand("SP_PA_SPB", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@NOMSurat", System.Data.SqlDbType.VarChar);
                            command.Parameters["@NOMSurat"].Value = No_Surat;

                            command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                            command.Parameters["@Option"].Value = 16;

                            SqlDataAdapter dataAdapt1 = new SqlDataAdapter();
                            dataAdapt1.SelectCommand = command;


                            dataAdapt1.Fill(data, "DETAILSPB");
                        }
                    }
                    catch (Exception ex1)
                    {

                        throw ex1;
                    }

                    try
                    {
                        using (SqlCommand command = new SqlCommand("SP_PA_SPB", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;


                            command.Parameters.Add("@NOMSurat", System.Data.SqlDbType.VarChar);
                            command.Parameters["@NOMSurat"].Value = No_Surat;

                            command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                            command.Parameters["@Option"].Value = 17;


                            SqlDataAdapter dataAdapt2 = new SqlDataAdapter();
                            dataAdapt2.SelectCommand = command;


                            dataAdapt2.Fill(data, "Footer1");
                        }
                    }
                    catch (Exception ex2)
                    {

                        throw ex2;

                    }

                    try
                    {
                        using (SqlCommand command = new SqlCommand("SP_PA_SPB", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@NOMSurat", System.Data.SqlDbType.VarChar);
                            command.Parameters["@NOMSurat"].Value = No_Surat;

                            command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                            command.Parameters["@Option"].Value = 18;


                            SqlDataAdapter dataAdapt3 = new SqlDataAdapter();
                            dataAdapt3.SelectCommand = command;


                            dataAdapt3.Fill(data, "Footer2");
                        }
                    }
                    catch (Exception ex3)
                    {

                        throw ex3;
                    }

                    try
                    {
                        using (SqlCommand command = new SqlCommand("SP_PA_SPB", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@NOMSurat", System.Data.SqlDbType.VarChar);
                            command.Parameters["@NOMSurat"].Value = No_Surat;

                            command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                            command.Parameters["@Option"].Value = 19;


                            SqlDataAdapter dataAdapt4 = new SqlDataAdapter();
                            dataAdapt4.SelectCommand = command;


                            dataAdapt4.Fill(data, "Footer3");
                        }
                    }
                    catch (Exception ex4)
                    {

                        throw ex4;
                    }

                    try
                    {
                        using (SqlCommand command = new SqlCommand("SP_PA_SPB", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;


                            command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                            command.Parameters["@Option"].Value = 29;

                            command.Parameters.Add("@NOMSurat", System.Data.SqlDbType.VarChar);
                            command.Parameters["@NOMSurat"].Value = No_Surat;

                        SqlDataAdapter dataAdapt5 = new SqlDataAdapter();
                            dataAdapt5.SelectCommand = command;


                            dataAdapt5.Fill(data, "Signature");
                        }
                    }
                    catch (Exception ex4)
                    {

                        throw ex4;
                    }

                    try
                    {
                        using (SqlCommand command = new SqlCommand("SP_PA_SPB", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;


                            command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                            command.Parameters["@Option"].Value = 30;

                            command.Parameters.Add("@NOMSurat", System.Data.SqlDbType.VarChar);
                            command.Parameters["@NOMSurat"].Value = No_Surat;

                            SqlDataAdapter dataAdapt5 = new SqlDataAdapter();
                            dataAdapt5.SelectCommand = command;


                            dataAdapt5.Fill(data, "Footer4");
                        }
                    }
                    catch (Exception ex4)
                    {

                        throw ex4;
                    }

                    try
                    {
                        using (SqlCommand command = new SqlCommand("SP_PA_SPB", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@NOMSurat", System.Data.SqlDbType.VarChar);
                            command.Parameters["@NOMSurat"].Value = No_Surat;

                            command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                            command.Parameters["@Option"].Value = 26;

                            SqlDataAdapter dataAdapt = new SqlDataAdapter();
                            dataAdapt.SelectCommand = command;
                            dataAdapt.Fill(data, "Header");
                        }
                    }
                    catch (Exception ex0)
                    {

                        throw ex0;
                    }

                    try
                    {
                        using (SqlCommand command = new SqlCommand("SP_PA_SPB", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@NOMSurat", System.Data.SqlDbType.VarChar);
                            command.Parameters["@NOMSurat"].Value = No_Surat;

                            command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                            command.Parameters["@Option"].Value = 27;

                            SqlDataAdapter dataAdapt1 = new SqlDataAdapter();
                            dataAdapt1.SelectCommand = command;


                            dataAdapt1.Fill(data, "PA_SPB");
                        }
                    }
                    catch (Exception ex1)
                    {

                        throw ex1;
                    }

                    try
                    {
                        using (SqlCommand command = new SqlCommand("SP_PA_SPB", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;


                            command.Parameters.Add("@NOMSurat", System.Data.SqlDbType.VarChar);
                            command.Parameters["@NOMSurat"].Value = No_Surat;

                            command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                            command.Parameters["@Option"].Value = 28;


                            SqlDataAdapter dataAdapt2 = new SqlDataAdapter();
                            dataAdapt2.SelectCommand = command;


                            dataAdapt2.Fill(data, "Approver");
                        }
                    }
                    catch (Exception ex2)
                    {

                        throw ex2;

                    }

                //data.AcceptChanges();
                conn.Close();

                    someDataSet.Load("../Report_SPB/Report_SPB_Memo.rpt");
                    //someDataSet.Load(Server.MapPath("~/Report_SPB/SPB_Report_DETAIL.rpt"));
                    someDataSet.SetDatabaseLogon("sab7", "Welcome123");
                    someDataSet.SetDataSource(data);
                    someDataSet.VerifyDatabase();
                    //someDataSet.SetDatabaseLogon("sab7", "Welcome1234");
                    //someDataSet.Database.Tables[4].SetDataSource(data.Tables["PA_SPB"]);
                    //someDataSet.Database.Tables[2].SetDataSource(data.Tables["DETAILSPB"]);
                    //someDataSet.Database.Tables[1].SetDataSource(data.Tables["Footer1"]);
                    //someDataSet.Database.Tables[2].SetDataSource(data.Tables["Footer2"]);
                    //someDataSet.Database.Tables[3].SetDataSource(data.Tables["Footer3"]);
                    CrystalReportViewer1.ReportSource = someDataSet;
                    CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
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