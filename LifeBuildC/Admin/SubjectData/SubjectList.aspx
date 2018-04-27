﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubjectList.aspx.cs" Inherits="LifeBuildC.Admin.SubjectData.SubjectList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>生命建造開課管理</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/menu.js"></script>
    <script src="../js/subjectlist.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnAddC1" runat="server" Text="新增C1課程" OnClick="btnAddC1_Click" />
            <asp:Button ID="btnAddC2" runat="server" Text="新增C2課程" OnClick="btnAddC2_Click" />
        </div>
        <div>
            <asp:RadioButton Font-Size="18px" ForeColor="Blue" AutoPostBack="true" GroupName="C" ID="rdoC1List" runat="server" Text="C1課程" OnCheckedChanged="rdoC1List_CheckedChanged" />
            &nbsp;|&nbsp;
                        <asp:RadioButton Font-Size="18px" ForeColor="Blue" AutoPostBack="true" GroupName="C" ID="rdoC2List" runat="server" Text="C2課程" OnCheckedChanged="rdoC2List_CheckedChanged" />
        </div>
        <div>
            <asp:GridView ID="gvSubject" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSubject_RowDataBound" OnRowDeleting="gvSubject_RowDeleting" Width="60%">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                                                        <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label>
                            <asp:Button ID="btnEdit" runat="server" Text="修改" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="課程">
                        <ItemTemplate>
                            <asp:Label ID="lblCategoryID" CategoryID='<%# Eval("CategoryID") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SDate" HeaderText="日期" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="SubTime" HeaderText="時間" />
                    <asp:TemplateField HeaderText="報名時間">
                        <ItemTemplate>
                            <asp:Label ID="lblSubDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>

                            <%--                            <asp:LinkButton ID="btnEdit" runat="server">修改</asp:LinkButton>--%>
                            
                            <asp:Button ID="btnDel" runat="server" Text="刪除" DELID='<%# Eval("SID") %>' CommandName="Delete"
                                OnClientClick="if (confirm('確定刪除?') == false) return false;" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
