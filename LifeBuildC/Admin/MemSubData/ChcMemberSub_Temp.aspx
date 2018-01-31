<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChcMemberSub_Temp.aspx.cs" Inherits="LifeBuildC.Admin.MemSubData.ChcMemberSub_Temp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>C1, C2 報名&簽到檔案匯入</h1>
        Excel 格式範例：
        <div style="border-style: solid; border-width: 1px;">
            <table style="width: 100%;" border="1">
                <tr>
                    <td>google 時間戳記</td>
                    <td>姓名</td>
                    <td>所屬牧區/小組</td>
                    <td>電話</td>
                    <td>上課日期</td>
                </tr>
                <tr>
                    <td>1/27/2018 8:31:48</td>
                    <td>流大丹</td>
                    <td>XX001.信豪牧區-彥伯小組</td>
                    <td>2018/12/01</td>
                </tr>
            </table>
        </div>

        <br />
        Excel 檔案：
        <div style="border-style: solid; border-width: 1px;">
            <asp:FileUpload ID="FileUpload1" runat="server" /><br /><br />
            <asp:Button ID="Button1" runat="server" Text="資料轉入" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
