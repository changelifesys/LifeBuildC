<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemSubDataEdit.aspx.cs" Inherits="LifeBuildC.Admin.MemSubData.MemSubDataEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title></title>
    <link href="../css/PageStyle.css" rel="stylesheet" />
    <script src="../js/jquery-3.1.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div class="FieldStyle">
                <asp:Label ID="Label1" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label2" ForeColor="#5a5e66" runat="server" Text="組別"></asp:Label>
            </div>

            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label3" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                    <asp:Label ID="Label4" ForeColor="#5a5e66" runat="server" Text="組別"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </span>
                <asp:DropDownList ID="dropGroupClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropGroupClass_SelectedIndexChanged">
                    <asp:ListItem>家庭組弟兄</asp:ListItem>
                    <asp:ListItem>家庭組姊妹</asp:ListItem>
                    <asp:ListItem>社青</asp:ListItem>
                    <asp:ListItem>學生</asp:ListItem>
                </asp:DropDownList>
            </div>


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
