<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="Fire18SignUp02_2.aspx.cs" Inherits="LifeBuildC.Fire18SignUp02_2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>烈火特會線上報到</title>
    <script src="js/jquery-3.1.1.min.js"></script>
    <style>
        .cssA {
            width: 95%;
            font-size: 14px;
            height: 30px;
            border-radius: 5px;
        }

        .cssB {
            font-size: 14px;
            margin: 10px;
        }

        .cssC {
            width: 20%;
            font-size: 14px;
            height: 35px;
            margin: 5px;
            border-radius: 5px;
            background-color: #85325d;
            color: white;
        }

        /*.cssD {
            width: 20%;
            font-size: 14px;
            height: 35px;
            margin: 5px;
            border-radius: 5px;
            background-color: #fff;
            color: #5a5e66;
        }*/

        #mainBg {
            display: block;
            width: 100%;
            height: auto
        }

        .cssField {
            text-align: left;
            margin: 5px;
        }

        .cssField800 {
            display: none;
        }
    </style>
    <script>

        $(function () {

            if ($(window).width() > 800) {
                $(".cssA").css('width', 300);
                $(".cssField800").removeClass();
                $(".cssField").css('display', 'none');
                $("#imgclothSize").css('width', 800);
            }
            else
            {
                $("#imgclothSize").css('width', $(window).width() - 60);
                $("#imgclass").css('width', $(window).width() - 60);
            }

            $("#btnSend,#btnSave,#btnSaveMail").click(function () {

                if ($('#txtGmail').val() != "") {
                    displayDiv('divLoading');
                }

            });

        });

        function closeDiv(divId) {
            document.getElementById(divId).style.display = 'none';
        }

        function displayDiv(divId) {
            document.getElementById(divId).style.display = 'block';
        }

    </script>
</head>
<body>

    <form id="form1" runat="server">
<%--        <img id="mainBg" src="/js/fire/main.jpg?f4ffa9d7f2bec29dabd0859458678983" alt="烈火特會-禱告與復興">--%>
        <div>
            <div style="text-align: center; margin: 5px;">
                <h2 class="center">2019 烈火特會報名</h2>
            </div>

            <!--組別-->
            <div class="cssField">
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">父母組別</font>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">父母組別</font>&nbsp;&nbsp;&nbsp;
                </span>
                <asp:DropDownList CssClass="cssA" ID="dropGroupClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropGroupClass_SelectedIndexChanged">
                    <asp:ListItem Value="" disabled Selected hidden>請選擇組別</asp:ListItem>
                    <asp:ListItem Value="0">家庭組弟兄</asp:ListItem>
                    <asp:ListItem Value="1">家庭組姊妹</asp:ListItem>
                    <asp:ListItem Value="2">社青</asp:ListItem>
                    <asp:ListItem Value="3">學生</asp:ListItem>
                    <asp:ListItem Value="4">傳道人(配偶)</asp:ListItem>
                </asp:DropDownList>
            </div>

            <!--小組-->
            <div class="cssField">
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">父母小組</font>
            </div>
            <div style="text-align: center; margin: 5px;">

                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">父母小組</font>&nbsp;&nbsp;&nbsp;
                </span>

                <asp:DropDownList CssClass="cssA" ID="dropGroupName" runat="server">
                    <asp:ListItem Value="" disabled Selected hidden>請選擇組別</asp:ListItem>
                </asp:DropDownList>
            </div>

            <!--姓名-->
            <div class="cssField">
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">父母姓名(有報名的)</font>
            </div>
            <div style="text-align: center; margin: 5px;">

                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">父母姓名(有報名的)</font>&nbsp;&nbsp;&nbsp;
                </span>

                <asp:TextBox CssClass="cssA" ID="txtEname1" runat="server"></asp:TextBox>
            </div>

            <div class="cssField">
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">小孩姓名</font>
            </div>
            <div style="text-align: center; margin: 5px;">

                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">小孩姓名</font>&nbsp;&nbsp;&nbsp;
                </span>

                <asp:TextBox CssClass="cssA" ID="txtEname2" runat="server"></asp:TextBox>
            </div>

            <!--生日-->
            <div class="cssField">
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">小孩生日</font>&nbsp;
            </div>
            <div style="text-align: center; margin: 5px;">

                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">小孩生日</font>&nbsp;&nbsp;&nbsp;
                </span>

                <asp:TextBox CssClass="cssA" ID="txtBirthday" runat="server" MaxLength="10"></asp:TextBox>
                <br />
                <font style="color: #fa5555;">範例：1984/9/11</font>
            </div>


            <hr />
            <div style="text-align: center; margin: 5px;">
                <asp:Button ID="btnSend" CssClass="cssC" runat="server" Width="100px" Text="報名" OnClick="btnSend_Click" />
            </div>
        </div>
    </form>
</body>
</html>
