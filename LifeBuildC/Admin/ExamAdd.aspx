<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamAdd.aspx.cs" Inherits="LifeBuildC.Admin.ExamAdd" %>

<%@ Register Src="~/Admin/UserControl/menu.ascx" TagPrefix="uc1" TagName="menu" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <title><%=Title %></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/login.css" rel="stylesheet" />
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/examedit.js"></script>
</head>
<body>
    <form role="form" runat="server">
        <div class="container-fluid">
            <div class="row content">
                <!--menu-->
                <uc1:menu runat="server" ID="menu" />
                <!--content-->
                <div class="col-sm-9">

                    <h4><small>新增考題</small></h4>
                    <hr>
                    <h2>題目：</h2>
                    <br>
                    <p id="pshow">
                        <label id="pshow_1"></label>
                        <label id="pshow_2"></label>
                        <label id="pshow_3"></label>
                        <label id="pshow_4"></label>
                        <label id="pshow_5"></label>
                        <label id="pshow_6"></label>
                        <label id="pshow_7"></label>
                        <label id="pshow_8"></label>
                        <label id="pshow_9"></label>
                        <label id="pshow_10"></label>
                    </p>
                    <!--Save-->
                    <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="儲存" OnClick="btnSave_Click" />
                    <%--<asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="儲存" />--%>
                    <asp:Button ID="btnCel" CssClass="btn btn-danger" runat="server" Text="返回" OnClick="btnCel_Click"/>
                    <input id="btnView" class="btn btn-info" type="button" value="預覽" />
                    <hr>
                    <!--編輯題目-->
                    <!--Field1-->
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="ckbIsField_1" runat="server" />設定此格需填空答案
                        </label>
                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtField_1" class="form-control" rows="3" runat="server" placeholder="編輯題目段落1"></textarea>
                    </div>
                    <!--Field2-->
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="ckbIsField_2" runat="server" />設定此格需填空答案</label>
                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtField_2" class="form-control" rows="3" runat="server" placeholder="編輯題目段落2"></textarea>
                    </div>
                    <!--Field3-->
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="ckbIsField_3" runat="server" />設定此格需填空答案</label>
                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtField_3" class="form-control" rows="3" runat="server" placeholder="編輯題目段落3"></textarea>
                    </div>
                    <!--Field4-->
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="ckbIsField_4" runat="server" />設定此格需填空答案</label>
                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtField_4" class="form-control" rows="3" runat="server" placeholder="編輯題目段落4"></textarea>
                    </div>
                    <!--Field5-->
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="ckbIsField_5" runat="server" />設定此格需填空答案</label>
                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtField_5" class="form-control" rows="3" runat="server" placeholder="編輯題目段落5"></textarea>
                    </div>
                    <!--Field6-->
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="ckbIsField_6" runat="server" />設定此格需填空答案</label>
                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtField_6" class="form-control" rows="3" runat="server" placeholder="編輯題目段落6"></textarea>
                    </div>
                    <!--Field7-->
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="ckbIsField_7" runat="server" />設定此格需填空答案</label>
                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtField_7" class="form-control" rows="3" runat="server" placeholder="編輯題目段落7"></textarea>
                    </div>
                    <!--Field8-->
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="ckbIsField_8" runat="server" />設定此格需填空答案</label>
                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtField_8" class="form-control" rows="3" runat="server" placeholder="編輯題目段落8"></textarea>
                    </div>
                    <!--Field9-->
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="ckbIsField_9" runat="server" />設定此格需填空答案</label>
                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtField_9" class="form-control" rows="3" runat="server" placeholder="編輯題目段落9"></textarea>
                    </div>
                    <!--Field10-->
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="ckbIsField_10" runat="server" />設定此格需填空答案</label>
                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtField_10" class="form-control" rows="3" runat="server" placeholder="編輯題目段落10"></textarea>
                    </div>
                </div>
            </div>
        </div>

        <!--footer-->
        <footer class="container-fluid">
            <p>Copyright© 2017 Change Life Church</p>
        </footer>
    </form>
</body>
</html>
