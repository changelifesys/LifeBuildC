<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="LifeBuildC.Admin.UserControl.menu" %>

<div class="col-sm-3 sidenav">
    <h4>生命建造程序系統</h4>
    <ul class="nav nav-pills nav-stacked">
        <li id="li_1"><a href="ExamList.aspx?ec=C1">C1 題庫</a></li>
        <li id="li_2"><a href="ExamList.aspx?ec=C212">C2 一、二課題庫</a></li>
        <li id="li_3"><a href="ExamList.aspx?ec=C234">C2 三、四課題庫</a></li>
        <li id="li_5"><a href="SubjectList.aspx">課程管理</a></li>
        <%--<li id="li_5"><a href="ExamScore.aspx?ec=Score">成績查詢</a></li>--%>
        <li id="li_4"><a href="SystemSet.aspx?ec=Sys">系統設定</a></li>
    </ul>
    <br>
</div>

