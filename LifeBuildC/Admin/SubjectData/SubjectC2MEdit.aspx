<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubjectC2MEdit.aspx.cs" Inherits="LifeBuildC.Admin.SubjectData.SubjectC2MEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>編輯C2 榮耀男人課程</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="http://jqueryui.com/resources/demos/style.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link href="../css/PageStyle.css" rel="stylesheet" />
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/examedit.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
        <script>
        function clickSave() {
            alert('C2 榮耀男人課程儲存成功!');
            $('#btnSave').click();
        };
    </script>
</head>
<body>
<form role="form" runat="server">
        <asp:HiddenField ID="CategoryID" Value="C1" runat="server" />
        <div>
            <center>
                <img alt="" src="../../img/CLC_Logo.gif" />
                <h1>編輯C2 榮耀男人課程</h1>
            </center>

            <!--Content-->
            <div>
                <table id="tableMem" style="width: 500px;" align="center" border="1">
                    <!--開課次數-->
                    <tr style="height: 40px;">
                        <td style="width: 150px; background-color: rgb(204,204,204)">
                            <asp:Label ID="Label8" ForeColor="Black" Font-Bold="true" runat="server" Text="開課次數"></asp:Label>
                        </td>
                        <td style="background-color: rgb(239,239,239); text-align: left;">
                            &nbsp;&nbsp;<asp:TextBox Enabled="false" ID="txtSubCount1" CssClass="inputStyle" runat="server" Width="50px"></asp:TextBox>年&nbsp;/
                            &nbsp;第<asp:TextBox Enabled="false" CssClass="inputStyle" ID="txtSubCount2" runat="server" Width="50px"></asp:TextBox>次開課
                        </td>
                    </tr>
                    <!--報名條件-->
                    <tr style="height: 40px;">
                        <td style="width: 150px; background-color: rgb(204,204,204)">
                            <asp:Label ID="Label2" ForeColor="Black" Font-Bold="true" runat="server" Text="報名條件"></asp:Label>
                        </td>
                        <td style="background-color: rgb(239,239,239); text-align: center;">
                            <asp:TextBox Width="95%" ID="txtSUCondition" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <!--上課日期-->
                    <tr style="height: 40px;">
                        <td style="width: 150px; background-color: rgb(204,204,204)">
                            <asp:Label ID="Label1" ForeColor="Black" Font-Bold="true" runat="server" Text="C2 榮耀男人"></asp:Label>
                        </td>
                        <td style="background-color: rgb(239,239,239); text-align: center;">【C2 榮耀男人】上課時間：
                            <asp:TextBox CssClass="inputStyle" ID="txtSDate" runat="server"></asp:TextBox>
                            <br />
                            <asp:DropDownList CssClass="inputStyle" ID="dropSubTime" runat="server">
                                <asp:ListItem>上午</asp:ListItem>
                                <asp:ListItem>下午</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox CssClass="inputStyle" ID="txtSubTime" runat="server"></asp:TextBox>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <!--地點-->
                    <tr style="height: 40px;">
                        <td style="width: 150px; background-color: rgb(204,204,204)">
                            <asp:Label ID="Label4" ForeColor="Black" Font-Bold="true" runat="server" Text="地點"></asp:Label>
                        </td>
                        <td style="background-color: rgb(239,239,239); text-align: center;">
                            <asp:TextBox Width="95%" ID="txtSubLocation" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <!--報名日期-->
                    <tr style="height: 40px;">
                        <td style="width: 150px; background-color: rgb(204,204,204)">
                            <asp:Label ID="Label5" ForeColor="Black" Font-Bold="true" runat="server" Text="報名日期"></asp:Label>
                        </td>
                        <td style="background-color: rgb(239,239,239); text-align: center;">
                            <asp:TextBox CssClass="inputStyle" ID="txtSubStrDate" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <!--報名截止-->
                    <tr style="height: 40px;">
                        <td style="width: 150px; background-color: rgb(204,204,204)">
                            <asp:Label ID="Label6" ForeColor="Black" Font-Bold="true" runat="server" Text="報名截止"></asp:Label>
                        </td>
                        <td style="background-color: rgb(239,239,239); text-align: center;">
                            <asp:TextBox CssClass="inputStyle" ID="txtSubEndDate" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <!--備註-->
                    <tr style="height: 40px;">
                        <td style="width: 150px; background-color: rgb(204,204,204)">
                            <asp:Label ID="Label7" ForeColor="Black" Font-Bold="true" runat="server" Text="備註"></asp:Label>
                        </td>
                        <td style="background-color: rgb(239,239,239); text-align: center;">
                            <asp:TextBox ID="txtMemo" runat="server" Rows="10" TextMode="MultiLine" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <center>
                <asp:Button ID="btnSave" CssClass="buttonStyle" runat="server" Text="儲存" OnClick="btnSave_Click" />
                            <br />
                <asp:Button ID="btnCel" CssClass="buttonStyle" runat="server" Text="返回" PostBackUrl="~/Admin/SubjectData/SubjectList.aspx" />
            </center>

        </div>
    </form>
</body>
</html>
