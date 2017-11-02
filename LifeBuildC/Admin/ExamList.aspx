<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamList.aspx.cs" Inherits="LifeBuildC.Admin.ExamList" %>

<%@ Register Src="~/Admin/UserControl/menu.ascx" TagPrefix="uc1" TagName="menu" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <title>Bootstrap Example</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="../css/login.css" rel="stylesheet" />
    <script src="js/menu.js"></script>
</head>
<body>

    <div class="container-fluid">
        <div class="row content">
            <!--menu-->
            <uc1:menu runat="server" ID="menu" />
            <!--content-->
            <div class="col-sm-9">

                <h4><small id="h4title"></small></h4>
                <br />
                <form role="form" runat="server">
                    <input id="iptAdd2" class="btn btn-success" type="button" value="新增考題" onclick="location.href='ExamAdd.aspx?ec='" />
                    <div class="container">
                        <asp:GridView ID="gvExam" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvExam_RowDataBound" OnRowDeleting="gvExam_RowDeleting">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        No.<%#Container.DataItemIndex + 1%>&nbsp;
                                        <asp:Label ID="lblField1" runat="server" Text='<%# Eval("Field1") %>'></asp:Label>
                                        <asp:Label ID="lblField2" runat="server" Text='<%# Eval("Field2") %>'></asp:Label>
                                        <asp:Label ID="lblField3" runat="server" Text='<%# Eval("Field3") %>'></asp:Label>
                                        <asp:Label ID="lblField4" runat="server" Text='<%# Eval("Field4") %>'></asp:Label>
                                        <asp:Label ID="lblField5" runat="server" Text='<%# Eval("Field5") %>'></asp:Label>
                                        <asp:Label ID="lblField6" runat="server" Text='<%# Eval("Field6") %>'></asp:Label>
                                        <asp:Label ID="lblField7" runat="server" Text='<%# Eval("Field7") %>'></asp:Label>
                                        <asp:Label ID="lblField8" runat="server" Text='<%# Eval("Field8") %>'></asp:Label>
                                        <asp:Label ID="lblField9" runat="server" Text='<%# Eval("Field9") %>'></asp:Label>
                                        <asp:Label ID="lblField10" runat="server" Text='<%# Eval("Field10") %>'></asp:Label>
                                        <%--<asp:Button ID="btnEdit" CssClass="btn btn-warning" runat="server" Text="修改" />--%>
                                        <asp:LinkButton ID="btnEdit" CssClass="btn btn-warning" runat="server">修改</asp:LinkButton>
                                        <asp:Button ID="btnDel" CssClass="btn btn-danger" runat="server" Text="刪除" DELID='<%# Eval("ID") %>' CommandName="Delete"
                                            OnClientClick="if (confirm('確定刪除?') == false) return false;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <script>
                            //格式化GridView
                            $('#gvExam').attr('class', 'table table-condensed');
                            $('#gvExam').removeAttr('cellspacing');
                            $('#gvExam').removeAttr('rules');
                            $('#gvExam').removeAttr('border');
                            $('#gvExam').removeAttr('style');
                            $("tr:eq(0) th:eq(0)").html('<input id="iptAdd" class="btn btn-success" type="button" value="新增考題" onclick="location.href=\'ExamAdd.aspx?ec=\'" />');

                            if ($("tr:eq(0) th:eq(0)").html() != null) {
                                $('#iptAdd2').remove();
                            }

                            //Url 取得參數

                            var strUrl = location.search; //取得:  ?ec=C234
                            var getPara, ParaVal;
                            var aryPara = [];

                            if (strUrl.indexOf("?") != -1) {

                                var getSearch = strUrl.split("?");
                                getPara = getSearch[1].split("&");

                                for (i = 0; i < getPara.length; i++) {
                                    ParaVal = getPara[i].split("=");
                                    aryPara.push(ParaVal[0]);
                                    aryPara[ParaVal[0]] = ParaVal[1];
                                }

                            }

                            if (aryPara["ec"] == "C1")
                                $('#h4title').val('C1 所有考題');
                            if (aryPara["ec"] == "C212")
                                $('#h4title').val('C2 一、二課所有考題');
                            if (aryPara["ec"] == "C234")
                                $('#h4title').val('C2 三、四課所有考題');

                            $('#iptAdd').attr('onclick', "location.href=\'ExamAdd.aspx?ec=" + aryPara["ec"] + "'");
                            $('#iptAdd2').attr('onclick', "location.href=\'ExamAdd.aspx?ec=" + aryPara["ec"] + "'");

                        </script>
                    </div>

                </form>
                <br>
                <br>
            </div>
        </div>
    </div>

    <!--footer-->
    <footer class="container-fluid">
        <p>Copyright© 2017 Change Life Church</p>
    </footer>

</body>
</html>
