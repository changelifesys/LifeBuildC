<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubjectC1Edit.aspx.cs" Inherits="LifeBuildC.Admin.SubjectData.SubjectC1Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>編輯C1課程</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        .text {
            background-color: #ffd800;
        }
    </style>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="http://jqueryui.com/resources/demos/style.css" />
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/examedit.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>

</head>
<body>
    <form role="form" runat="server">
        <div>
            <h4>編輯C1課程</h4>
            <hr />
            <!--Content-->
            <div>
                報名條件：<br />
                <asp:TextBox CssClass="text" Width="50%" ID="txtSUCondition" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />
                上課日期：<br />
                <table width="100%" border="1">
                    <tr>
                        <td>
                            <asp:CheckBox ID="ckbIsSub12" runat="server" Checked="True" />
                            有開課
                        </td>
                        <td>C1 一、二課
                        </td>
                        <td>
                            <asp:TextBox CssClass="text" ID="txtSDate12" runat="server"></asp:TextBox>
                            <asp:DropDownList CssClass="text" ID="dropSubTime12" runat="server">
                                <asp:ListItem>上午</asp:ListItem>
                                <asp:ListItem>下午</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox CssClass="text" ID="txtSubTime12" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="ckbIsSub34" runat="server" Checked="True" />
                            有開課
                        </td>
                        <td>C1 三、四課</td>
                        <td>
                            <asp:TextBox CssClass="text" ID="txtSDate34" runat="server"></asp:TextBox>
                            <asp:DropDownList CssClass="text" ID="dropSubTime34" runat="server">
                                <asp:ListItem>上午</asp:ListItem>
                                <asp:ListItem>下午</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox CssClass="text" ID="txtSubTime34" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
                <br />
                地點：<br />
                <asp:TextBox CssClass="text" Width="50%" ID="txtSubLocation" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />
                報名日期：
        <asp:TextBox CssClass="text" ID="txtSubStrDate" runat="server"></asp:TextBox>
                <br />
                報名截止：
        <asp:TextBox CssClass="text" ID="txtSubEndDate" runat="server"></asp:TextBox>
                <br />
                <br />
                備註：<br />
                <asp:TextBox CssClass="text" ID="txtMemo" runat="server" Rows="10" TextMode="MultiLine" Width="50%"></asp:TextBox>
            </div>
            <br />
            <br />
            <!--Save-->
            <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="儲存" OnClick="btnSave_Click" />
            <asp:Button ID="btnCel" CssClass="btn btn-danger" runat="server" Text="返回" OnClick="btnCel_Click" />
        </div>
    </form>
</body>
</html>
