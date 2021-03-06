﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FireMemEdit.aspx.cs" Inherits="LifeBuildC.Admin.FireClass.FireMemEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title></title>
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
    <style type="text/css">
        .tanchuang_wrap {
            width: 100%;
            height: 400px;
            /*position: absolute;*/
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 100;
            display: none;
        }

        .lightbox {
            width: 100%;
            z-index: 101;
            height: 400px;
            background-color: black;
            filter: alpha(Opacity=20);
            -moz-opacity: 0.2;
            opacity: 0.2;
            /*position: absolute;*/
            position: fixed;
            top: 0px;
            left: 0px;
        }

        .tanchuang_neirong {
            width: 100%;
            height: 153px;
            /*border: solid 1px #f7dd8c;*/
            background-color: #FFF;
            /*position: absolute;*/
            position: fixed;
            z-index: 105;
            left: 30px;
            top: 50px;
        }
    </style>
    <script src="../../js/jquery-3.1.1.min.js"></script>
    <script>
        $(function () {

            if ($(window).width() > 800) {
                $(".cssA").css('width', 300);
                $(".cssField800").removeClass();
                $(".cssField").css('display', 'none');
            }

            $(".tanchuang_wrap").css('height', $(window).height());
            $(".lightbox").css('height', $(window).height());

            $("#btnSaveMail").click(function () {

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
        <div>
            <div style="text-align: center; margin: 5px;">
                <h2 class="center">2018 烈火特會報名-會友資料修改</h2>
            </div>

            <!--組別-->
            <div class="cssField">
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">組別</font>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">組別</font>&nbsp;&nbsp;&nbsp;
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
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">小組</font>
            </div>
            <div style="text-align: center; margin: 5px;">

                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">小組</font>&nbsp;&nbsp;&nbsp;
                </span>

                <asp:DropDownList CssClass="cssA" ID="dropGroupName" runat="server">
                    <asp:ListItem Value="" disabled Selected hidden>請選擇組別</asp:ListItem>
                </asp:DropDownList>
            </div>

            <!--姓名-->
            <div class="cssField">
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">姓名</font>
            </div>
            <div style="text-align: center; margin: 5px;">

                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">姓名</font>&nbsp;&nbsp;&nbsp;
                </span>

                <asp:TextBox CssClass="cssA" ID="txtEname" runat="server"></asp:TextBox>
            </div>

            <!--手機-->
            <div class="cssField">
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">手機</font>
            </div>
            <div style="text-align: center; margin: 5px;">

                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">手機</font>&nbsp;&nbsp;&nbsp;
                </span>

                <asp:TextBox CssClass="cssA" ID="txtPhone" runat="server" MaxLength="10"></asp:TextBox>
                <br />
                <font style="color: #fa5555;">範例：0919123456</font>
            </div>


            <!--Email-->
            <div class="cssField">
                <font style="color: #5a5e66;">Email</font>
            </div>
            <div style="text-align: center; margin: 5px;">

                <span class="cssField800">
                    <font style="color: #5a5e66;">Email</font>&nbsp;&nbsp;&nbsp;
                </span>

                <asp:TextBox CssClass="cssA" ID="txtGmail" runat="server"></asp:TextBox>
                <br />
                <font style="color: #fa5555;">範例：dennis@gmail.com，也可不填</font>
            </div>


            <!--姓別-->
            <div class="cssField">
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">姓別</font>&nbsp;
            </div>
            <div style="text-align: center; margin: 5px;">

                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">姓別</font>&nbsp;&nbsp;&nbsp;
                </span>

                <asp:RadioButton ID="rdogender1" GroupName="Sex" CssClass="cssB" runat="server" />男
            <asp:RadioButton ID="rdogender0" GroupName="Sex" CssClass="cssB" runat="server" />女
            </div>


            <!--生日-->
            <div class="cssField">
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;"> 生日</font>&nbsp;
            </div>
            <div style="text-align: center; margin: 5px;">

                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;"> 生日</font>&nbsp;&nbsp;&nbsp;
                </span>

                <asp:TextBox CssClass="cssA" ID="txtBirthday" runat="server" MaxLength="10"></asp:TextBox>
                <br />
                <font style="color: #fa5555;">範例：1984/9/11</font>
            </div>


            <!--大會T恤尺寸-->
            <div class="cssField">
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">大會T恤尺寸</font>&nbsp;
            </div>
            <div style="text-align: center; margin: 5px;">

                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">大會T恤尺寸</font>&nbsp;&nbsp;&nbsp;
                </span>


                <asp:DropDownList CssClass="cssA" ID="dropClothesSize" runat="server">
                    <asp:ListItem Value="" disabled Selected hidden>請選擇尺寸</asp:ListItem>
                    <asp:ListItem Value="0">S</asp:ListItem>
                    <asp:ListItem Value="1">M</asp:ListItem>
                    <asp:ListItem Value="2">L</asp:ListItem>
                    <asp:ListItem Value="3">XL</asp:ListItem>
                    <asp:ListItem Value="4">XXL</asp:ListItem>
                </asp:DropDownList>
            </div>


            <!--下午場講座-->
<%--            <div class="cssField">
                <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">下午場講座</font>
            </div>--%>
            <div style="text-align: center; margin: 5px;">

<%--                <span class="cssField800">
                    <font style="color: #fa5555;">*</font>&nbsp;<font style="color: #5a5e66;">下午場講座</font>&nbsp;&nbsp;&nbsp;
                </span>--%>

<%--                <asp:DropDownList CssClass="cssA" ID="dropCourse" runat="server">
                    <asp:ListItem Value="" disabled Selected hidden>請選擇講座</asp:ListItem>
                    <asp:ListItem Value="0">生命突破</asp:ListItem>
                    <asp:ListItem Value="1">教會突破</asp:ListItem>
                </asp:DropDownList>--%>

                <div style="text-align: center; margin: 5px;">
                    <asp:Button ID="btnSave" CssClass="cssC" runat="server" Width="100px" Text="儲存" OnClick="btnSave_Click" />
                    <asp:Button ID="btnSaveMail" CssClass="cssC" runat="server" Width="150px" Text="儲存後重寄Mail" OnClick="btnSaveMail_Click" />
                    <asp:Button ID="btnCancel" CssClass="cssC" runat="server" Width="100px" Text="取消" OnClick="btnCancel_Click" />
                </div>



            </div>

        </div>

        <div class="tanchuang_wrap" id="divLoading">
                <div class="lightbox" style="text-align: center;">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <%--                    <font style="color: yellow; font-size: 28px; background-color: black;">Email 寄送中請稍後...</font>--%>
                    <asp:Label Font-Size="28px" ForeColor="Yellow" ID="lblMail" runat="server" Text="Email 寄送中請稍後..."></asp:Label>
                </div>
            </div>

    </form>
</body>
</html>
