<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LifeBuildC.Admin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>生命建造 - 查詢系統</title>
    <style>
        .inputStyle {
    width: 20%;
    font-size: 14px;
    height: 30px;
    border-radius: 5px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; margin: 5px;">
            <img alt="" src="../../img/CLC_Logo.gif" />
            <h1>生命建造 - 查詢系統</h1>
        </div>
        <div style="text-align: center; margin: 5px;">
            帳號：<asp:TextBox CssClass="inputStyle" ID="txtAcc" runat="server"></asp:TextBox><br />
            密碼：<asp:TextBox CssClass="inputStyle" ID="txtPass" runat="server" TextMode="Password"></asp:TextBox><br />
            <br />
            <asp:Button CssClass="inputStyle" ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>
