<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemSubDataList.aspx.cs" Inherits="LifeBuildC.Admin.MemSubData.MemSubDataList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>生命建造 - 查詢系統</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link href="../css/PageStyle.css" rel="stylesheet" />
    <script src="../../js/jquery-3.1.1.min.js"></script>
    <script src="../js/MemSubDataList.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hidGroupClass" name="hidGroupClass" type="hidden" />
        <input id="hidGroupName" name="hidGroupName" type="hidden" />
        <input id="hidEname" name="hidEname" type="hidden" />

        <div style="text-align: center; margin: 5px;">
            <img alt="" src="../../img/CLC_Logo.gif" />
            <h1>生命建造 - 查詢系統</h1>
        </div>

        <table id="tableMem" style="width: 500px;" align="center" border="1">
            <!--組別-->
            <tr style="height: 40px;">
                <td style="width: 100px; background-color: rgb(204,204,204); text-align: center;">
                    <asp:Label ID="Label2" ForeColor="#5a5e66" runat="server" Text="組別"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:DropDownList CssClass="inputStyle" ID="dropGroupClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropGroupClass_SelectedIndexChanged">
                        <asp:ListItem Value="" disabled Selected hidden>請選擇組別</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <!--小組-->
            <!--區長-->
            <tr style="height: 40px;">
                <td style="width: 100px; background-color: rgb(204,204,204); text-align: center;">
                    <asp:Label ID="lblGName" ForeColor="#5a5e66" runat="server" Text="小組"></asp:Label>
                    <asp:Label ID="lblGCName" ForeColor="#5a5e66" runat="server" Text="區長"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:DropDownList CssClass="inputStyle" ID="dropGroupName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropGroupName_SelectedIndexChanged">
                        <asp:ListItem Value="" disabled Selected hidden>請選擇組別</asp:ListItem>
                    </asp:DropDownList>

                    <asp:DropDownList CssClass="inputStyle" ID="dropGroupCName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropGroupCName_SelectedIndexChanged">
                        <asp:ListItem Value="" disabled Selected hidden>請選擇區長</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox CssClass="inputStyle" ID="txtGroupName" Visible="false" runat="server" OnTextChanged="txtGroupName_TextChanged"></asp:TextBox><br/>
                    <asp:CheckBox ID="ckbGroupName" runat="server" AutoPostBack="true" OnCheckedChanged="ckbGroupName_CheckedChanged"/>
                    <asp:Label ID="lblKeyinGroupName" runat="server" Text="自行輸入小組"></asp:Label>

                </td>
            </tr>
            <!--姓名-->
            <tr style="height: 40px;">
                <td style="width: 100px; background-color: rgb(204,204,204); text-align: center;">
                    <asp:Label ID="Label1" ForeColor="#5a5e66" runat="server" Text="姓名"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:TextBox CssClass="inputStyle" ID="txtEname" runat="server" OnTextChanged="txtEname_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Label ID="lblDataCnt" ForeColor="#fa5555" runat="server" Text="請選擇組別查詢員組修課資料"></asp:Label><br/>
                    <asp:CheckBox ID="chkIsLeave" runat="server" AutoPostBack="true" OnCheckedChanged="chkIsLeave_CheckedChanged" /> 
                    <asp:Label ID="lblIsLeave" runat="server" Text="離開教會"></asp:Label>
                </td>
            </tr>
        </table>
        <br/>
        <div style="text-align: center; margin: 5px;">
            <asp:Button CssClass="inputStyle" ID="btnQuery" runat="server" Text="查詢" />
            <br/><br/>
            <asp:Button CssClass="inputStyle" ID="blkQ" PostBackUrl="~/Admin/MemSubData/MemSubDataQuestion.aspx" runat="server" Text="問題反映" />                    
            <br/><br/>
            <asp:Button CssClass="inputStyle" ID="btnExcel" runat="server" Text="匯出Excel" OnClick="btnExcel_Click" />
            <br/><br/>
            <asp:Button CssClass="inputStyle" ID="btnAllExcel" runat="server" Text="全會友匯出Excel" OnClick="btnAllExcel_Click" />
        </div>
        <br/><br/>
        <div id="divView1" visible="false" style="text-align: center; margin: 5px; font-size: 14pt" runat="server">
            <asp:RadioButton GroupName="View1" Checked="true" ID="rdoAll" runat="server" AutoPostBack="true" OnCheckedChanged="rdoView_CheckedChanged" />&nbsp;課程判定&nbsp;&nbsp;|
            <asp:RadioButton GroupName="View1" ID="rdoC1View" runat="server" AutoPostBack="true" OnCheckedChanged="rdoView_CheckedChanged" />&nbsp;C1 詳細&nbsp;&nbsp;|
            <asp:RadioButton GroupName="View1" ID="rdoC2View" runat="server" AutoPostBack="true" OnCheckedChanged="rdoView_CheckedChanged" />&nbsp;C2 詳細
        </div>

        <!--全部-->
        <asp:GridView ID="gvCStatusAll" runat="server" Visible="False" AutoGenerateColumns="False" OnRowDataBound="gvChcMember_RowDataBound" HorizontalAlign="Center">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnView" runat="server" Text="檢視" />
                    </ItemTemplate>
                    <FooterStyle BackColor="Black" />
                    <HeaderStyle BorderColor="Black" ForeColor="#006600" />
                </asp:TemplateField>
                <asp:BoundField DataField="GroupClass" HeaderText="組&nbsp;&nbsp;&nbsp;別">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="小&nbsp;&nbsp;&nbsp;組">
                    <ItemTemplate>
                        <asp:Label ID="lblGroupCName" runat="server" Text=""></asp:Label>-
                            <asp:Label ID="lblGroupName" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField DataField="Ename" HeaderText="姓&nbsp;&nbsp;&nbsp;名">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="C1 通過判定">
                    <ItemTemplate>
                        <asp:Label ID="lblC1_Status" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C2 通過判定">
                    <ItemTemplate>
                        <asp:Label ID="lblC2_Status" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!--C1-->
        <asp:GridView ID="gvC1All" runat="server" Visible="false" AutoGenerateColumns="False" OnRowDataBound="gvChcMember_RowDataBound" HorizontalAlign="Center">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnView" runat="server" Text="檢視" />
                    </ItemTemplate>
                    <FooterStyle BackColor="Black" />
                    <HeaderStyle BorderColor="Black" ForeColor="#006600" />
                </asp:TemplateField>
                <asp:BoundField DataField="GroupClass" HeaderText="組&nbsp;&nbsp;&nbsp;別">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="小&nbsp;&nbsp;&nbsp;組">
                    <ItemTemplate>
                        <asp:Label ID="lblGroupCName" runat="server" Text=""></asp:Label>-
                            <asp:Label ID="lblGroupName" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField DataField="Ename" HeaderText="姓&nbsp;&nbsp;&nbsp;名">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="C1<br/>第一、二課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC112" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C1<br/>第三、四課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC134" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C1<br/>更深經歷神">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC1God" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
<%--                <asp:BoundField DataField="C1_Status" HeaderText="C1 通過判定">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="C1 通過判定">
                    <ItemTemplate>
                        <asp:Label ID="lblC1_Status" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!--C2-->
        <asp:GridView ID="gvC2All" runat="server" Visible="false" AutoGenerateColumns="False" OnRowDataBound="gvChcMember_RowDataBound" HorizontalAlign="Center">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnView" runat="server" Text="檢視" />
                    </ItemTemplate>
                    <FooterStyle BackColor="Black" />
                    <HeaderStyle BorderColor="Black" ForeColor="#006600" />
                </asp:TemplateField>
                <asp:BoundField DataField="GroupClass" HeaderText="組&nbsp;&nbsp;&nbsp;別">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="小&nbsp;&nbsp;&nbsp;組">
                    <ItemTemplate>
                        <asp:Label ID="lblGroupCName" runat="server" Text=""></asp:Label>-
                            <asp:Label ID="lblGroupName" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField DataField="Ename" HeaderText="姓&nbsp;&nbsp;&nbsp;名">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="C2<br/>第一、二課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC212" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C2<br/>第三、四課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC234" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C2<br/>第五課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC25" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField DataField="C1_Score" HeaderText="C1 考試">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="C212_Score" HeaderText="C2 第一、二課考試">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="C234_Score" HeaderText="C2 第三、四課考試">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="交見證">
                    <ItemTemplate>
                        <asp:Label ID="lblIswitness" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C2<br/>領袖訓練一">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC2L1" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C2 通過判定">
                    <ItemTemplate>
                        <asp:Label ID="lblC2_Status" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!--Old-->
        <asp:GridView Visible="false" ID="gvChcMember" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvChcMember_RowDataBound" HorizontalAlign="Center">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnView" runat="server" Text="檢視" />
                    </ItemTemplate>
                    <FooterStyle BackColor="Black" />
                    <HeaderStyle BorderColor="Black" ForeColor="#006600" />
                </asp:TemplateField>
                <asp:BoundField DataField="GroupClass" HeaderText="組&nbsp;&nbsp;&nbsp;別">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="小&nbsp;&nbsp;&nbsp;組">
                    <ItemTemplate>
                        <asp:Label ID="lblGroupCName" runat="server" Text=""></asp:Label>-
                            <asp:Label ID="lblGroupName" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField DataField="Ename" HeaderText="姓&nbsp;&nbsp;&nbsp;名">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Church" HeaderText="教&nbsp;&nbsp;&nbsp;會">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" Width="200px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="C1<br/>第一、二課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC112" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C1<br/>第三、四課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC134" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C2<br/>第一、二課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC212" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C2<br/>第三、四課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC234" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C2<br/>第五課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC25" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField DataField="C1_Score" HeaderText="C1 考試">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="C212_Score" HeaderText="C2 第一、二課考試">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="C234_Score" HeaderText="C2 第三、四課考試">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="交見證">
                    <ItemTemplate>
                        <asp:Label ID="lblIswitness" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:BoundField DataField="C1_Status" HeaderText="C1 通過判定">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="C2_Status" HeaderText="C2 通過判定">

                    <FooterStyle BorderColor="Black" />

                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

            </Columns>
        </asp:GridView>
        <br/><br/>
    </form>
</body>
</html>
