<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupAdd.aspx.cs" Inherits="LifeBuildC.Admin.GroupAdd" %>

<%@ Register Src="~/Admin/UserControl/menu.ascx" TagPrefix="uc1" TagName="menu" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <title>牧區小組編輯</title>
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
        <asp:HiddenField ID="hdenGroupClass" runat="server" />
        <div class="container-fluid">
            <div class="row content">
                <!--menu-->
                <uc1:menu runat="server" ID="menu" />
                <!--content-->
                <div class="col-sm-9">

                    <h4><small>牧區小組編輯</small></h4>
                    <hr>

                    <!--Save-->
                    <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="儲存" OnClick="btnSave_Click" />
                    <hr>
                    <!--編輯設定-->
                    <!--SystemSet1-->
                    <div>
                        <label>
                            組別設定
                        </label>
                    </div>
                    <div class="dropdown">
                        <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown" style="width: 100%">
                            家庭組弟兄
    <span class="caret"></span>
                        </button>
                        <ul id="examdate" class="dropdown-menu" style="width: 100%">
                            <li><a>家庭組弟兄</a></li>
                            <li><a>家庭組姊妹</a></li>
                            <li><a>社青</a></li>
                            <li><a>學生</a></li>
                        </ul>
                    </div>
                    <br />
                    <!--SystemSet2-->
                    <div>
                        <label>
                            編號設定
                        </label>
                    </div>
                    <div class="form-group">
                        <textarea id="txtaGroupID" class="form-control" rows="3" runat="server" placeholder="【填寫範例】 AA101"></textarea>
                    </div>
                    <!--SystemSet3-->
                    <div>
                        <label>
                            選項排序設定(要填寫數字)
                        </label>
                    </div>
                    <div class="form-group">
                        <textarea id="txtaGSort" class="form-control" rows="3" runat="server" placeholder="【填寫範例】 1"></textarea>
                    </div>
                    <!--SystemSet4-->
                    <div>
                        <label>
                            牧區設定
                        </label>
                    </div>
                    <div class="form-group">
                        <textarea id="txtaGroup" class="form-control" rows="3" runat="server" placeholder="【填寫範例】 永健"></textarea>
                    </div>
                    <!--SystemSet5-->
                    <div>
                        <label>
                            小組設定
                        </label>
                    </div>
                    <div class="form-group">
                        <textarea id="txtaGroupName" class="form-control" rows="3" runat="server" placeholder="【填寫範例】 永健"></textarea>
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
