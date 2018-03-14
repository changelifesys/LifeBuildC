<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChcMemberSub_Temp.aspx.cs" Inherits="LifeBuildC.Admin.MemSubData.ChcMemberSub_Temp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script>
        $(function () {

            $('#Button2').click(function () {
                $('#Button2').css('display', 'none');
                $('#imgloading').removeAttr('style');
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <h1>C1, C2 報名&簽到檔案匯入</h1>
        範例格式：
        <a target="_blank" href="https://docs.google.com/spreadsheets/d/1hwdtX6vj9wkg4t_bNKCgQ9bmUEu5muVOshPxKWUPVJw/edit#gid=0">https://docs.google.com/spreadsheets/d/1hwdtX6vj9wkg4t_bNKCgQ9bmUEu5muVOshPxKWUPVJw/edit#gid=0</a>
        <br />
        <br />
        Excel 檔案：
        <div style="border-style: solid; border-width: 1px;">
            <asp:FileUpload ID="FileUpload1" runat="server" /><br /><br />
            <asp:Button ID="Button1" runat="server" Text="資料匯入" OnClick="Button1_Click" />
        </div>
        <br />
        <br />
        <div>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
        <div>
            <br/>
            <asp:Button ID="Button2" Visible="false" runat="server" Text="確認匯入" OnClick="Button2_Click" />
            <img id="imgloading" style="display: none;" src="../../img/loading2.gif" />
            
            <br/>
        </div>
    </form>
</body>
</html>
