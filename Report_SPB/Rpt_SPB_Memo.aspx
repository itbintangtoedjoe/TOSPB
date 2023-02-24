<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rpt_SPB_Memo.aspx.cs" Inherits="WebApplication1.Report_SPB.Report_SPB_Memo1" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"  Height="50px" ReportSourceID="CrystalReportSource1"  ToolPanelWidth="200px" Width="350px" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="../Report_SPB/Report_SPB_Memo.rpt">
                </Report>
            </CR:CrystalReportSource>
        </div>
    </form>
</body>
</html>