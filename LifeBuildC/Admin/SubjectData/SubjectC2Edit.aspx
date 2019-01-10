<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubjectC2Edit.aspx.cs" Inherits="LifeBuildC.Admin.SubjectData.SubjectC2Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>編輯C2課程</title>
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
            alert('C2 課程儲存成功!');
            $('#btnSave').click();
        };
    </script>
</head>
<body>
    <form role="form" runat="server">
        <asp:HiddenField ID="CategoryID" Value="C2" runat="server" />
        <div>
            <center>
                <img alt="" src="../../img/CLC_Logo.gif" />
                <h1>編輯C2課程</h1>
            </center>

            <!--Content-->
            <div>
                <table id="tableMem" style="width: 500px;" align="center" border="1">
                    <!--開課次數-->
                    <tr style="height: 40px;">
                        <td style="width: 150px; background-color: rgb(204,204,204)">
                            <asp:Label ID="Label9" ForeColor="Black" Font-Bold="true" runat="server" Text="開課次數"></asp:Label>
                        </td>
                        <td style="background-color: rgb(239,239,239); text-align: left;">
                            &nbsp;&nbsp;<asp:TextBox Enabled="false" ID="txtSubCount1" CssClass="inputStyle" runat="server" Width="50px"></asp:TextBox>年&nbsp;/
                            &nbsp;第<asp:TextBox CssClass="inputStyle" ID="txtSubCount2" runat="server" Width="50px"></asp:TextBox>次開課
                        </td>
                    </tr>
                    <!--開始簽到-->
                    <tr style="height: 40px;">
                        <td style="width: 150px; background-color: rgb(204,204,204)">
                            <asp:Label ID="Label10" ForeColor="Black" Font-Bold="true" runat="server" Text="開始簽到"></asp:Label>
                        </td>
                        <td style="background-color: rgb(239,239,239); text-align: left;">
                            <asp:CheckBox ID="ckbIsCheckOpen" runat="server" />&nbsp;&nbsp;打開
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
                            <asp:Label ID="Label1" ForeColor="Black" Font-Bold="true" runat="server" Text="C1 一、二課"></asp:Label>
                            <asp:CheckBox ID="ckbIsSub12" runat="server" Checked="True" AutoPostBack="True" OnCheckedChanged="ckbIsSub12_CheckedChanged" />
                            有開課
                        </td>
                        <td style="background-color: rgb(239,239,239); text-align: center;">【C1 一、二課】上課時間：
                            <asp:TextBox CssClass="inputStyle" ID="txtSDate12" runat="server"></asp:TextBox>
                            <br />
                            <asp:DropDownList CssClass="inputStyle" ID="dropSubTime12" runat="server">
                                <asp:ListItem>上午</asp:ListItem>
                                <asp:ListItem>下午</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox CssClass="inputStyle" ID="txtSubTime12" runat="server"></asp:TextBox>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr style="height: 40px;">
                        <td style="width: 150px; background-color: rgb(204,204,204)">
                            <asp:Label ID="Label3" ForeColor="Black" Font-Bold="true" runat="server" Text="C1 三、四課"></asp:Label>
                            <asp:CheckBox ID="ckbIsSub34" runat="server" Checked="True" AutoPostBack="True" OnCheckedChanged="ckbIsSub34_CheckedChanged" />
                            有開課
                        </td>
                        <td style="background-color: rgb(239,239,239); text-align: center;">【C1 三、四課】上課時間：
                            <asp:TextBox CssClass="inputStyle" ID="txtSDate34" runat="server"></asp:TextBox>
                            <br />
                            <asp:DropDownList CssClass="inputStyle" ID="dropSubTime34" runat="server">
                                <asp:ListItem>上午</asp:ListItem>
                                <asp:ListItem>下午</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox CssClass="inputStyle" ID="txtSubTime34" runat="server"></asp:TextBox>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr style="height: 40px;">
                        <td style="width: 150px; background-color: rgb(204,204,204)">
                            <asp:Label ID="Label8" ForeColor="Black" Font-Bold="true" runat="server" Text="C2 五課"></asp:Label>
                            <asp:CheckBox ID="ckbIsSub5" runat="server" Checked="True" AutoPostBack="True" OnCheckedChanged="ckbIsSub5_CheckedChanged" />
                            有開課
                        </td>
                        <td style="background-color: rgb(239,239,239); text-align: center;">【C2 五課】上課時間：
                            <asp:TextBox CssClass="inputStyle" ID="txtSDate5" runat="server"></asp:TextBox>
                            <br />
                            <asp:DropDownList CssClass="inputStyle" ID="dropSubTime5" runat="server">
                                <asp:ListItem>上午</asp:ListItem>
                                <asp:ListItem>下午</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox CssClass="inputStyle" ID="txtSubTime5" runat="server"></asp:TextBox>
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
            <!--Save-->
            <center>
                            <asp:Button ID="btnSave" CssClass="buttonStyle" runat="server" Text="儲存" OnClick="btnSave_Click" />
                                        <br />
            <asp:Button ID="btnCel" CssClass="buttonStyle" runat="server" Text="返回" PostBackUrl="~/Admin/SubjectData/SubjectList.aspx" />
            </center>

        </div>
    </form>
</body>
</html>
