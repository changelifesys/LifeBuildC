<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdMemSubData.aspx.cs" Inherits="LifeBuildC.Admin.MemSubData.UpdMemSubData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            <asp:Button ID="btnUpd" runat="server" Text="更新資料" OnClick="btnUpd_Click" />
        </div>
    </form>
</body>
</html>
