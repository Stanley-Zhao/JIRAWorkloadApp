<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JIRAWorkLogAPP.Default" %>

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
            <telerik:RadLabel ID="lbl" runat="server" ></telerik:RadLabel>
                 From date:
            <telerik:RadDatePicker ID="fromDate" runat="server"></telerik:RadDatePicker>
                 To date:
            <telerik:RadDatePicker ID="toDate" runat="server"></telerik:RadDatePicker>
                 <telerik:RadComboBox ID="region" runat="server" AutoPostBack="true" Width="300px" OnSelectedIndexChanged="region_SelectedIndexChanged">
                     <Items>                         
                         <telerik:RadComboBoxItem Text="Beijing Time"  Value="BJ"/>
                         <telerik:RadComboBoxItem Text="Sanfrancisco Time" Value="SF" />
                          <telerik:RadComboBoxItem Text="Oslo Time(UTC+1 Central Europe Standard Time)" Value="OS" />
                          </Items>
                 </telerik:RadComboBox>
            <telerik:RadButton ID="DoSearch" Text="Search" runat="server" OnClick="DoSearch_Click"></telerik:RadButton>
           <asp:HyperLink ID="jump" NavigateUrl="~/Overview.aspx" Text="Jump" runat="server"></asp:HyperLink>
                <telerik:RadGrid ID="RadGrid1" runat="server"
                    EnableRangeFiltering="true"
                    EnableTimeIndependentFiltering ="true"
                    EnableHeaderContextMenu="true"
                    EnableHeaderContextFilterMenu="true"
                    OnNeedDataSource="RadGrid1_NeedDataSource1"
                    ClientSettings-AllowDragToGroup="true"
                    FilterMenu-Enabled="true"
                    ShowGroupPanel="true"
                    ShowHeader="true"
                    ShowStatusBar="true"
                    ClientSettings-AllowColumnsReorder="true"
                    AllowFilteringByColumn="true"
                    AllowPaging="true"
                    AllowSorting="true"
                    AllowMultiRowSelection="true"
                    RenderMode="Lightweight"
                    ActiveItemStyle-Wrap="false"
                    AlternatingItemStyle-Wrap="false"
                    CommandItemStyle-Wrap="false"
                    EditItemStyle-Wrap="false"
                    FilterItemStyle-Wrap="false"
                    HeaderStyle-Wrap="false"
                    AutoGenerateColumns="false"
                    
                   PageSize="100"
                    ShowFooter="True">
                    <GroupingSettings ShowUnGroupButton="true" />
                    <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" CellSelectionMode="SingleCell"></Selecting>
                    </ClientSettings>
                    <MasterTableView Width="100%" ShowGroupFooter="true" UseAllDataFields="true" CommandItemDisplay="None" EnableHeaderContextFilterMenu="true" EnableHeaderContextMenu="true">
                        <Columns>
                            <telerik:GridBoundColumn DataField="UserName" HeaderText="UserName"
                                SortExpression="UserName" UniqueName="UserName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Aggregate="Sum" DataField="LogHour" HeaderText="LogHour"
                                SortExpression="LogHour" UniqueName="LogHour">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn EnableRangeFiltering="true" DataType="System.DateTime" DataField="LogDate" HeaderText="LogDate"
                                SortExpression="LogDate" UniqueName="LogDate2">
                                </telerik:GridDateTimeColumn>
                          <%--  <telerik:GridBoundColumn DataType="System.DateTime" DataField="LogDate" HeaderText="LogDate"
                                SortExpression="LogDate" UniqueName="LogDate">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="JIRAID" HeaderText="JIRAID"
                                SortExpression="JIRAID" UniqueName="JIRAID">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="JIRASummary" HeaderText="JIRASummary"
                                SortExpression="JIRASummary" UniqueName="JIRASummary">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <CommandItemSettings ShowExportToWordButton="true" SaveChangesText="Save" ShowSaveChangesButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true" />
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>
        </div>
    </form>
</body>
</html>
