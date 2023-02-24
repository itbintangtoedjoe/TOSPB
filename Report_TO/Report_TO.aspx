<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_TO.aspx.cs" Inherits="WebApplication1.Report_TO.Report_TO1" %>
<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server" AutoDataBind="True"  Height="50px" ReportSourceID="CrystalReportSource2"  ToolPanelWidth="200px" Width="350px" />
        <CR:CrystalReportSource ID="CrystalReportSource2" runat="server">
            <Report FileName="../Report_TO/Report_TO_CR.rpt">
            </Report>
        </CR:CrystalReportSource>
    </div>
</form>
</body>
</html>
