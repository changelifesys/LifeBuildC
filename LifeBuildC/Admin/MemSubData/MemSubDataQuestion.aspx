﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemSubDataQuestion.aspx.cs" Inherits="LifeBuildC.Admin.MemSubData.MemSubDataQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>生命建造 - 問題反應</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
    <link href="../css/PageStyle.css" rel="stylesheet" />
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/MemSubDataQuestion.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hfGroupClassValue" name="hfGroupClassValue" type="hidden" runat="server" />
        <input id="hfGroupNameValue" name="hfGroupNameValue" type="hidden" runat="server" />
        <asp:HiddenField ID="hfCategoryName" runat="server" />

        <div style="text-align: center; margin: 5px;">
            <img alt="" src="../../img/CLC_Logo.gif" />
            <h1>生命建造 - 問題反應</h1>

            <div style="text-align: center; margin: 5px;">
                <!--組別-->
                <asp:Label ID="lblGroupClass" runat="server" Text=""></asp:Label>
                <br />
                <!--小組-->
                <asp:Label ID="lblGroupName" runat="server" Text=""></asp:Label>
                <br />
                <!--姓名-->
                <asp:Label ID="lblEname" runat="server" Text=""></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <asp:Label ID="Label1" ForeColor="#fa5555" runat="server" Text="「*」為必填欄位"></asp:Label>
            </div>
            <table id="tableMem" style="width: 600px;" align="center" border="1">
                <!--課程-->
                <tr style="height: 40px;">
                    <td style="width: 150px; background-color: rgb(204,204,204)">
                        <asp:Label ID="Label11" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                        <asp:Label ID="Label2" ForeColor="#5a5e66" runat="server" Text="請勾選課程(可複選)"></asp:Label></td>
                    <td style="background-color: rgb(239,239,239);">
                        <asp:CheckBox ID="chkCategoryName1" runat="server" />C1課程<br/>
                        <asp:CheckBox ID="chkCategoryName2" runat="server" />C2課程
                    </td>
                </tr>

                <!--Email-->
                <tr style="height: 40px;">
                    <td style="width: 150px; background-color: rgb(204,204,204)">
                        <asp:Label ID="Label4" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                        <asp:Label ID="Label3" ForeColor="#5a5e66" runat="server" Text="Email"></asp:Label></td>
                    <td style="background-color: rgb(239,239,239);">
                        <asp:TextBox class="inputStyle" ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <!--主旨-->
                <tr style="height: 40px;">
                    <td style="width: 150px; background-color: rgb(204,204,204)">
                        <asp:Label ID="Label6" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                        <asp:Label ID="Label5" ForeColor="#5a5e66" runat="server" Text="主旨"></asp:Label>
                    </td>
                    <td style="background-color: rgb(239,239,239);">
                        <asp:TextBox class="inputStyle" ID="txtSubLine" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <!--問題描述-->
                <tr style="height: 40px;">
                    <td style="width: 150px; background-color: rgb(204,204,204)">
                        <asp:Label ID="Label8" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                        <asp:Label ID="Label7" ForeColor="#5a5e66" runat="server" Text="問題描述"></asp:Label>
                    </td>
                    <td style="background-color: rgb(239,239,239);">
                        <textarea style="width: 98%;" id="txtQuestion" rows="20"></textarea>
                    </td>
                </tr>
            </table>
            <br/>
            <img id="imgloading" style="display: none;" src="../../img/loading2.gif" /><br/>
            <input class="buttonStyle" id="btnSendQ" type="button" value="送出問題" /><br/>
<%--            <input class="buttonStyle" id="btnCel" type="button" value="上一頁" />--%>
            <asp:Button class="buttonStyle" PostBackUrl="~/Admin/MemSubData/MemSubDataList.aspx" ID="btnCel" runat="server" Text="上一頁" />
        </div>
    </form>
</body>
</html>
