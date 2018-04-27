﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubjectC2Add.aspx.cs" Inherits="LifeBuildC.Admin.SubjectData.SubjectC2Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增C2課程</title>
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
            <h4>新增C2課程</h4>
            <!--Content-->
            <div>
                報名條件：
        <asp:TextBox CssClass="text" Width="100%" ID="txtSUCondition" runat="server"></asp:TextBox>
                <br />
                <br />
                <table width="100%" border="1">
                    <tr>
                        <td colspan="2">上課時間
                        </td>
                    </tr>
                    <tr>
                        <td>C2 一、二課
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
                        <td>C2 三、四課</td>
                        <td>
                            <asp:TextBox CssClass="text" ID="txtSDate34" runat="server"></asp:TextBox>
                            <asp:DropDownList CssClass="text" ID="dropSubTime34" runat="server">
                                <asp:ListItem>上午</asp:ListItem>
                                <asp:ListItem>下午</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox CssClass="text" ID="txtSubTime34" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>C2 五課</td>
                        <td>
                            <asp:TextBox CssClass="text" ID="txtSDate5" runat="server"></asp:TextBox>
                            <asp:DropDownList CssClass="text" ID="dropSubTime5" runat="server">
                                <asp:ListItem>上午</asp:ListItem>
                                <asp:ListItem>下午</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox CssClass="text" ID="txtSubTime5" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
                地點：
        <asp:TextBox CssClass="text" Width="100%" ID="txtSubLocation" runat="server"></asp:TextBox>
                <br />
                <br />
                報名日期：
        <asp:TextBox CssClass="text" ID="txtSubStrDate" runat="server"></asp:TextBox>
                <br />
                報名截止：
        <asp:TextBox CssClass="text" ID="txtSubEndDate" runat="server"></asp:TextBox>
                <br />

            </div>
            <!--Save-->
            <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="儲存" OnClick="btnSave_Click" />
            <asp:Button ID="btnCel" CssClass="btn btn-danger" runat="server" Text="返回" OnClick="btnCel_Click" />
        </div>
    </form>
</body>
</html>
