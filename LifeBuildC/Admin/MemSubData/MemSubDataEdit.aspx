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
    <script src="../js/MemSubDataEdit.js"></script>
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
            <h1>組員上課資料維護</h1>
        </div>
        <div>

            <!--組別-->
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
                <select class="inputStyle" id="dropGroupClass" runat="server">
                </select>
            </div>

            <!--小組-->
            <div class="FieldStyle">
                <asp:Label ID="Label5" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label6" ForeColor="#5a5e66" runat="server" Text="小組"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label7" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                    <asp:Label ID="Label8" ForeColor="#5a5e66" runat="server" Text="小組"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </span>
                <select class="inputStyle" id="dropGroupName" runat="server">
                </select>
            </div>

            <!--姓名-->
            <div class="FieldStyle">
                <asp:Label ID="Label9" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label10" ForeColor="#5a5e66" runat="server" Text="姓名"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label11" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                    <asp:Label ID="Label12" ForeColor="#5a5e66" runat="server" Text="姓名"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </span>
                <asp:TextBox class="inputStyle" ID="txtEname" runat="server"></asp:TextBox>
            </div>

            <!--教會-->
            <div class="FieldStyle">
                <asp:Label ID="Label13" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label14" ForeColor="#5a5e66" runat="server" Text="教會"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label15" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                    <asp:Label ID="Label16" ForeColor="#5a5e66" runat="server" Text="教會"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </span>
                <asp:DropDownList class="inputStyle" ID="dropChurch" runat="server">
                    <asp:ListItem>江子翠行道會會友</asp:ListItem>
                    <asp:ListItem>新莊幸福行道會會友</asp:ListItem>
                </asp:DropDownList>
                <br />
            </div>

            <!--C1 第一、二課-->
            <div class="FieldStyle">
                <asp:Label ID="Label17" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label18" ForeColor="#5a5e66" runat="server" Text="C1 第一、二課"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">

                <span class="FieldStyle2">
                    <asp:Label ID="Label39" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                    <asp:Label ID="Label40" ForeColor="#5a5e66" runat="server" Text="C1 第一、二課"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </span>

                <asp:CheckBox ID="chkIsC112" runat="server" />通過
            </div>

            <!--C1 第三、四課-->
            <div class="FieldStyle">
                <asp:Label ID="Label19" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label20" ForeColor="#5a5e66" runat="server" Text="C1 第三、四課"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label41" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                <asp:Label ID="Label42" ForeColor="#5a5e66" runat="server" Text="C1 第三、四課"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </span>
                <asp:CheckBox ID="chkIsC134" runat="server" />通過
            </div>


            <!--C2 第一、二課-->
            <div class="FieldStyle">
                <asp:Label ID="Label21" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label22" ForeColor="#5a5e66" runat="server" Text="C2 第一、二課"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label43" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                <asp:Label ID="Label44" ForeColor="#5a5e66" runat="server" Text="C2 第一、二課"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </span>
                <asp:CheckBox ID="chkIsC212" runat="server" />通過
            </div>



            <!--C2 第三、四課-->
            <div class="FieldStyle">
                <asp:Label ID="Label23" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label24" ForeColor="#5a5e66" runat="server" Text="C2 第三、四課"></asp:Label>

            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label45" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                <asp:Label ID="Label46" ForeColor="#5a5e66" runat="server" Text="C2 第三、四課"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </span>
                <asp:CheckBox ID="chkIsC234" runat="server" />通過
            </div>



            <!--C2 第五課-->
            <div class="FieldStyle">
                <asp:Label ID="Label25" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label26" ForeColor="#5a5e66" runat="server" Text="C2 第五課"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label47" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                <asp:Label ID="Label48" ForeColor="#5a5e66" runat="server" Text="C2 第五課"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </span>
                <asp:CheckBox ID="chkIsC25" runat="server" />通過
            </div>

            <!--C1 考試-->
            <div class="FieldStyle">
                <asp:Label ID="Label27" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label28" ForeColor="#5a5e66" runat="server" Text="C1 考試"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label49" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                <asp:Label ID="Label50" ForeColor="#5a5e66" runat="server" Text="C1 考試"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                </span>
                <asp:TextBox class="inputStyle" ID="txtC1_Score" Width="50px" runat="server"></asp:TextBox>分
            </div>

            <!--C2 第一、二課考試-->
            <div class="FieldStyle">
                <asp:Label ID="Label29" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label30" ForeColor="#5a5e66" runat="server" Text="C2 第一、二課考試"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label51" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                <asp:Label ID="Label52" ForeColor="#5a5e66" runat="server" Text="C2 第一、二課考試"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                </span>
                <asp:TextBox class="inputStyle" ID="txtC212_Score" Width="50px" runat="server"></asp:TextBox>分
            </div>

            <!--C2 第三、四課考試-->
            <div class="FieldStyle">
                <asp:Label ID="Label31" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label32" ForeColor="#5a5e66" runat="server" Text="C2 第三、四課考試"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label53" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                <asp:Label ID="Label54" ForeColor="#5a5e66" runat="server" Text="C2 第三、四課考試"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                </span>
                <asp:TextBox class="inputStyle" ID="txtC234_Score" Width="50px" runat="server"></asp:TextBox>分
            </div>

            <!--交見證-->
            <div style="background-color: rgb(220,230,241);margin: 5px;">
                            <div class="FieldStyle">
                <asp:Label ID="Label33" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label34" ForeColor="#5a5e66" runat="server" Text="交見證"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label55" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                <asp:Label ID="Label56" ForeColor="#5a5e66" runat="server" Text="交見證"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                </span>
                <asp:CheckBox ID="chkIswitness" runat="server" />已交見證
            <br />
                <asp:TextBox ID="txtwitness" runat="server" Rows="50" TextMode="MultiLine" Height="50px" Width="400px"></asp:TextBox>
            </div>
            </div>
            
            <div style="background-color: rgb(253,233,217);margin: 5px;">
                            <!--C1 通過判定-->
            <div class="FieldStyle">
                <asp:Label ID="Label35" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label36" ForeColor="#5a5e66" runat="server" Text="C1 通過判定"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label57" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                <asp:Label ID="Label58" ForeColor="#5a5e66" runat="server" Text="C1 通過判定"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                </span>
                <asp:Label ID="lblC1_Status" runat="server" Text=""></asp:Label>
            </div>

            <!--C2 通過判定-->
            <div class="FieldStyle">
                <asp:Label ID="Label37" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label38" ForeColor="#5a5e66" runat="server" Text="C2 通過判定"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label59" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                <asp:Label ID="Label60" ForeColor="#5a5e66" runat="server" Text="C2 通過判定"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                </span>
                <asp:Label ID="lblC2_Status" runat="server" Text=""></asp:Label>
            </div>
            </div>


            <div style="text-align: center; margin: 5px;">
                <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnCel" runat="server" Text="取消" OnClick="btnCel_Click" />
            </div>

        </div>
    </form>
</body>
</html>
