<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FireMemEdit.aspx.cs" Inherits="LifeBuildC.Admin.FireClass.FireMemEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            組別
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            小組
            <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
            姓名
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            電話
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            Email
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            姓別
            <asp:RadioButton ID="RadioButton1" runat="server" />男
            <asp:RadioButton ID="RadioButton2" runat="server" />女
            生日
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            衣服尺寸
            <asp:DropDownList ID="DropDownList3" runat="server"></asp:DropDownList>
            下午場講座
            <asp:DropDownList ID="DropDownList4" runat="server"></asp:DropDownList>
            <asp:Button ID="Button1" runat="server" Text="儲存" />
            <asp:Button ID="Button2" runat="server" Text="返回" />
        </div>
    </form>
</body>
</html>
