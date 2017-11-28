<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemSubDataEdit.aspx.cs" Inherits="LifeBuildC.Admin.MemSubData.MemSubDataEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            組別
            <asp:DropDownList ID="dropGroupClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropGroupClass_SelectedIndexChanged">
                <asp:ListItem>家庭組弟兄</asp:ListItem>
                <asp:ListItem>家庭組姊妹</asp:ListItem>
                <asp:ListItem>社青</asp:ListItem>
                <asp:ListItem>學生</asp:ListItem>
            </asp:DropDownList>
            <br />
            小組
            <asp:DropDownList ID="dropGroupName" runat="server"></asp:DropDownList>
            <br />
            姓名
            <asp:TextBox ID="txtEname" runat="server"></asp:TextBox>
            <br />
            教會
            <asp:DropDownList ID="dropChurch" runat="server">
                <asp:ListItem>江子翠行道會會友</asp:ListItem>
                <asp:ListItem>新莊幸福行道會會友</asp:ListItem>
            </asp:DropDownList>
            <br />
            C1 第一、二課
            <asp:CheckBox ID="chkIsC112" runat="server" />Pass
            <br />
            C1 第三、四課
            <asp:CheckBox ID="chkIsC134" runat="server" />Pass
            <br />
            C2 第一、二課
            <asp:CheckBox ID="chkIsC212" runat="server" />Pass
            <br />
            C2 第三、四課
            <asp:CheckBox ID="chkIsC234" runat="server" />Pass
            <br />
            C2 第五課
            <asp:CheckBox ID="chkIsC25" runat="server" />Pass
            <br />
            C1 考試
            <asp:TextBox ID="txtC1_Score" Width="50px" runat="server"></asp:TextBox>分
            <br />
            C2 第一、二課考試
            <asp:TextBox ID="txtC212_Score" Width="50px" runat="server"></asp:TextBox>分
            <br />
            C2 第三、四課考試
            <asp:TextBox ID="txtC234_Score" Width="50px" runat="server"></asp:TextBox>分
            <br />
            交見證
            <asp:CheckBox ID="chkIswitness" runat="server" />Pass
            <br />
            <asp:TextBox ID="txtwitness" runat="server"></asp:TextBox>
            <br />
            C1 通過判定
            <asp:Label ID="lblC1_Status" runat="server" Text=""></asp:Label>
            <br />
            C2 通過判定
            <asp:Label ID="lblC2_Status" runat="server" Text=""></asp:Label>
            <br />

            <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnCel" runat="server" Text="取消" OnClick="btnCel_Click" />
        </div>
    </form>
</body>
</html>
