<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="JIRAWorkLogAPP.Overview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <script type="text/javascript">
            //Put your JavaScript code here.
        </script>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
        <div>
            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" CssClass="grid_wrapper">
                <telerik:RadLabel ID="lbl" runat="server"></telerik:RadLabel>
                <telerik:RadDatePicker ID="fromDate" runat="server"></telerik:RadDatePicker>
                <telerik:RadDatePicker ID="toDate" runat="server"></telerik:RadDatePicker>
                <telerik:RadComboBox ID="region" runat="server" AutoPostBack="true" Width="300px" OnSelectedIndexChanged="region_SelectedIndexChanged">
                    <Items>
                        <telerik:RadComboBoxItem Text="Beijing Time" Value="BJ" />
                        <telerik:RadComboBoxItem Text="Sanfrancisco Time" Value="SF" />
                        <telerik:RadComboBoxItem Text="Oslo Time(UTC+1 Central Europe Standard Time)" Value="OS" />
                    </Items>
                </telerik:RadComboBox>
                <telerik:RadButton ID="DoSearch" Text="Search" runat="server" OnClick="DoSearch_Click"></telerik:RadButton>

                <telerik:RadHtmlChart runat="server" Width="800px" Height="500px" ID="RadHtmlChart1">
                    <ChartTitle Text="Pie chart of work log">
                        <Appearance Align="Center" Position="Top">
                        </Appearance>
                    </ChartTitle>
                    <Legend>
                        <Appearance Position="Right" Visible="true">
                        </Appearance>
                    </Legend>
                    <PlotArea>
                        <Series>
                           <telerik:PieSeries DataFieldY="LogHours" NameField="UserName" ExplodeField="IsExploded">
                            <LabelsAppearance DataFormatString="{0}{1}Hours">
                            </LabelsAppearance>
                            <TooltipsAppearance Color="White" DataFormatString="{0}Hour"></TooltipsAppearance>
                        </telerik:PieSeries>
                        </Series>
                    </PlotArea>
                </telerik:RadHtmlChart>

            </telerik:RadAjaxPanel>
        </div>
    </form>
</body>
</html>
