<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemSet.aspx.cs" Inherits="LifeBuildC.Admin.SystemSet" %>

<%@ Register Src="~/Admin/UserControl/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>系統設定</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/login.css" rel="stylesheet" />
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/menu.js"></script>
</head>
<body>
    <form role="form" runat="server">
        <div class="container-fluid">
            <div class="row content">
                <!--menu-->
                <uc1:menu runat="server" ID="menu" />
                <!--content-->
                <div class="col-sm-9">

                    <h4><small>系統設定</small></h4>
                    <hr>

                    <!--Save-->
                    <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="儲存" OnClick="btnSave_Click" />
                    <hr>
                    <!--編輯設定-->
                    <!--SystemSet1-->
                    <div class="checkbox">
                        <label>
                            Google表單連結 (C1 分數)
                        </label>
                        <br />
                        <br />
                        <label>
                            <asp:CheckBox ID="ckbIsEnable_C1" runat="server" />開啓 C1 考題
                        </label>

                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtC1" class="form-control" rows="3" runat="server" placeholder="填寫 google 表單網旨"></textarea>
                    </div>
                    <!--SystemSet2-->
                    <div class="checkbox">
                        <label>
                            Google表單連結 (C2 一、二課分數)
                        </label>
                        <br />
                        <br />
                        <label>
                            <asp:CheckBox ID="ckbIsEnable_C212" runat="server" />開啓 C2 一、二課 考題
                        </label>
                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtC212" class="form-control" rows="3" runat="server" placeholder="填寫 google 表單網旨"></textarea>
                    </div>
                    <!--SystemSet3-->
                    <div class="checkbox">
                        <label>
                            Google表單連結 (C2 三、四課分數)
                        </label>
                        <br />
                        <br />
                        <label>
                            <asp:CheckBox ID="ckbIsEnable_C234" runat="server" />開啓 C2 三、四課 考題
                        </label>
                        &nbsp;&nbsp;<a onclick="window.scrollTo(0, 1);">(Top)</a>
                    </div>
                    <div class="form-group">
                        <textarea id="txtC234" class="form-control" rows="3" runat="server" placeholder="填寫 google 表單網旨"></textarea>
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
