<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FireMem.aspx.cs" Inherits="LifeBuildC.Admin.FireClass.FireMem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>2018 烈火特會報名</title>
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script>
        $(function () {
            
            $('#btnExcel').click(function () {
                $('#btnExcel').css('display', 'none');
                $('#lblSend').removeAttr('style');
                $('#lblSend').css('color', 'red');
            });

        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnExcel" runat="server" Visible="true" Text="匯出到google雲端" OnClick="btnExcel_Click" />
            <label id="lblSend" style="display:none;">資料匯出到雲端中，請稍後...</label>
            <asp:GridView ID="gvExcel" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="CreateTime" HeaderText="報名時間" />
                    <asp:BoundField DataField="GroupClass" HeaderText="組別" />
                    <asp:BoundField DataField="group2" HeaderText="小組" />
                    <asp:BoundField DataField="Ename" HeaderText="姓名" />
                    <asp:BoundField DataField="Phone2" HeaderText="電話" />
                    <asp:BoundField DataField="Gmail" HeaderText="Email" />
                    <asp:BoundField DataField="gender2" HeaderText="姓別" />
                    <asp:BoundField DataField="Birthday" HeaderText="生日" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="ClothesSize" HeaderText="衣服尺寸" />
                    <asp:BoundField DataField="Course2" HeaderText="下午場講座" />
                    <asp:BoundField DataField="PassKey" HeaderText="密碼" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
