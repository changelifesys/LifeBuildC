<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubjectList.aspx.cs" Inherits="LifeBuildC.Admin.SubjectList" %>

<%@ Register Src="~/Admin/UserControl/menu.ascx" TagPrefix="uc1" TagName="menu" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>生命建造開課管理</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <style>

    </style>
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="../css/login.css" rel="stylesheet" />
    <script src="js/menu.js"></script>
    <script src="js/subjectlist.js"></script>
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
                    <div>
                        <asp:Button ID="btnAddC1" class="btn btn-success" runat="server" Text="新增C1課程" OnClick="btnAddC1_Click" />
                        <asp:Button ID="btnAddC2" class="btn btn-success" runat="server" Text="新增C2課程" OnClick="btnAddC2_Click" />
                    </div>
                    <hr/>
                    <div>
                        <asp:RadioButton Font-Size="18px" ForeColor="Blue" AutoPostBack="true" GroupName="C" ID="rdoC1List" runat="server" Text="C1課程" OnCheckedChanged="rdoC1List_CheckedChanged" />
                        &nbsp;|&nbsp;
                        <asp:RadioButton Font-Size="18px" ForeColor="Blue" AutoPostBack="true" GroupName="C" ID="rdoC2List" runat="server" Text="C2課程" OnCheckedChanged="rdoC2List_CheckedChanged" />
                    </div>
                    <div class="container">
                        <asp:GridView ID="gvSubject" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSubject_RowDataBound" OnRowDeleting="gvSubject_RowDeleting" OnRowCommand="gvSubject_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="課程">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryID" CategoryID='<%# Eval("CategoryID") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SDate" HeaderText="日期" />
                                <asp:BoundField DataField="SubTime" HeaderText="時間" />
                                <asp:TemplateField HeaderText="報名時間">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubDate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox placeholder="工作表名稱" ID="txtSheet" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton CssClass="btn btn-info" CommandName="GoogleExport" SID='<%# Eval("SID") %>' ID="btnExport" runat="server">上課名單匯出</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:LinkButton CssClass="btn btn-warning" ID="btnEdit" runat="server">修改</asp:LinkButton>
                                        <asp:Button ID="btnDel" CssClass="btn btn-danger" runat="server" Text="刪除" DELID='<%# Eval("SID") %>' CommandName="Delete"
                                            OnClientClick="if (confirm('確定刪除?') == false) return false;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <script>
                            //格式化GridView
                            $('#gvSubject').attr('class', 'table table-condensed');
                            $('#gvSubject').removeAttr('cellspacing');
                            $('#gvSubject').removeAttr('rules');
                            $('#gvSubject').removeAttr('border');
                            $('#gvSubject').removeAttr('style');
                        </script>
                    </div>
                </form>
                <br />
                <br />
            </div>
        </div>
    </div>

    <!--footer-->
    <footer class="container-fluid">
        <p>Copyright© 2017 Change Life Church</p>
    </footer>

</body>
</html>
