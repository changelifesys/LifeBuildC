<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemSubDataEdit.aspx.cs" Inherits="LifeBuildC.Admin.MemSubData.MemSubDataEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>生命建造 - 資料維護</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link href="../css/PageStyle.css" rel="stylesheet" />
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/MemSubDataEdit.js"></script>
    <script type="text/javascript">
        function clickSave() {
            alert('儲存成功');
            $('#btnSave').click();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!--JSON Data-->
        <asp:HiddenField ID="hfChcGroup" runat="server" />
        <!--組別-->
        <asp:HiddenField ID="hfGroupClass" runat="server" />
        <asp:HiddenField ID="hfGroupClassValue" runat="server" />
        <!--小組-->
        <asp:HiddenField ID="hfGroupName" runat="server" />
        <asp:HiddenField ID="hfGroupNameValue" runat="server" />

        <div style="text-align: center; margin: 5px;">
            <img alt="" src="../../img/CLC_Logo.gif" />
            <h1>生命建造 - 資料維護</h1>
            <asp:Label ID="Label61" ForeColor="#fa5555" runat="server" Text="「*」為必填欄位"></asp:Label>
        </div>

        <table id="tableMem" style="width: 500px;" align="center" border="1">
            <!--組別-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label1" ForeColor="Red" Font-Bold="true" runat="server" Text="*"></asp:Label>
                    &nbsp;
                         <asp:Label ID="Label2" ForeColor="Black" Font-Bold="true" runat="server" Text="組別"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <select class="inputStyle" id="dropGroupClass" runat="server">
                    </select>
                </td>
            </tr>

            <!--小組-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label3" ForeColor="Red" Font-Bold="true" runat="server" Text="*"></asp:Label>
                    &nbsp;
                         <asp:Label ID="Label4" ForeColor="Black" Font-Bold="true" runat="server" Text="小組"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <select class="inputStyle" id="dropGroupName" runat="server">
                    </select>
                </td>
            </tr>

            <!--姓名-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label5" ForeColor="Red" Font-Bold="true" runat="server" Text="*"></asp:Label>
                    &nbsp;
                         <asp:Label ID="Label6" ForeColor="Black" Font-Bold="true" runat="server" Text="姓名"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:TextBox class="inputStyle" ID="txtEname" runat="server"></asp:TextBox>
                </td>
            </tr>

            <!--教會-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label7" ForeColor="Red" Font-Bold="true" runat="server" Text="*"></asp:Label>
                    &nbsp;
                         <asp:Label ID="Label8" ForeColor="Black" Font-Bold="true" runat="server" Text="教會"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:DropDownList class="inputStyle" ID="dropChurch" runat="server">
                        <asp:ListItem>江子翠行道會會友</asp:ListItem>
                        <asp:ListItem>新莊幸福行道會會友</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <!--C1 第一、二課-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label10" ForeColor="Black" Font-Bold="true" runat="server" Text="C1 第一、二課"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:CheckBox ID="chkIsC112" runat="server" />通過
                </td>
            </tr>

            <!--C1 第三、四課-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label9" ForeColor="Black" Font-Bold="true" runat="server" Text="C1 第三、四課"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:CheckBox ID="chkIsC134" runat="server" />通過
                </td>
            </tr>

            <!--C2 第一、二課-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label11" ForeColor="Black" Font-Bold="true" runat="server" Text="C2 第一、二課"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:CheckBox ID="chkIsC212" runat="server" />通過
                </td>
            </tr>

            <!--C2 第三、四課-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label12" ForeColor="Black" Font-Bold="true" runat="server" Text="C2 第三、四課"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:CheckBox ID="chkIsC234" runat="server" />通過
                </td>
            </tr>

            <!--C2 第五課-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label13" ForeColor="Black" Font-Bold="true" runat="server" Text="C2 第五課"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:CheckBox ID="chkIsC25" runat="server" />通過
                </td>
            </tr>

            <!--C1 考試-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label14" ForeColor="Black" Font-Bold="true" runat="server" Text="C1 考試"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:TextBox class="inputStyle" ID="txtC1_Score" Width="50px" runat="server"></asp:TextBox>分
                </td>
            </tr>

            <!--C2 第一、二課考試-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label15" ForeColor="Black" Font-Bold="true" runat="server" Text="C2 第一、二課考試"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:TextBox class="inputStyle" ID="txtC212_Score" Width="50px" runat="server"></asp:TextBox>分
                </td>
            </tr>

            <!--C2 第三、四課考試-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label16" ForeColor="Black" Font-Bold="true" runat="server" Text="C2 第三、四課考試"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:TextBox class="inputStyle" ID="txtC234_Score" Width="50px" runat="server"></asp:TextBox>分
                </td>
            </tr>

            <!--見證繳交-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label17" ForeColor="Black" Font-Bold="true" runat="server" Text="見證繳交"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:CheckBox ID="chkIswitness" runat="server" />已交見證
                </td>
            </tr>
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label18" ForeColor="Black" Font-Bold="true" runat="server" Text="見證內容"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:TextBox ID="txtwitness" runat="server" Rows="20" TextMode="MultiLine" Width="95%"></asp:TextBox>
                </td>
            </tr>

            <!--C1 通過判定-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label19" ForeColor="Black" Font-Bold="true" runat="server" Text="C1 通過判定"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:Label ID="lblC1_Status" runat="server" Text=""></asp:Label>
                </td>
            </tr>

            <!--C2 通過判定-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label20" ForeColor="Black" Font-Bold="true" runat="server" Text="C2 通過判定"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:Label ID="lblC2_Status" runat="server" Text=""></asp:Label>
                </td>
            </tr>

            <!--離開教會-->
            <tr style="height: 40px;">
                <td style="width: 150px; background-color: rgb(204,204,204)">
                    <asp:Label ID="Label21" ForeColor="Black" Font-Bold="true" runat="server" Text="是否離開教會"></asp:Label>
                </td>
                <td style="background-color: rgb(239,239,239); text-align: center;">
                    <asp:CheckBox ID="chkIsLeave" runat="server" />已經離開
                </td>
            </tr>

        </table>

        <div style="text-align: center; margin: 5px;">
            <asp:Button CssClass="buttonStyle" ID="btnSave" runat="server" Text="儲存資料" OnClick="btnSave_Click" />
            <br />
            <asp:Button CssClass="buttonStyle" ID="btnCel" runat="server" Text="上一頁" PostBackUrl="~/Admin/MemSubData/MemSubDataList.aspx" OnClick="btnCel_Click"/>
        </div>
    </form>
</body>
</html>
