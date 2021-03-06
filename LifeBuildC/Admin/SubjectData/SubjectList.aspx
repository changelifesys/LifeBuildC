﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubjectList.aspx.cs" Inherits="LifeBuildC.Admin.SubjectData.SubjectList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>生命建造開課管理</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/menu.js"></script>
    <%--<script src="../js/subjectlist.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <center>
        <img alt="" src="../../img/CLC_Logo.gif" />
        <h1>生命建造-課程管理</h1>
        <div>
            課程類別：<asp:DropDownList ID="dropSubject" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropSubject_SelectedIndexChanged" Font-Size="14pt">
                <asp:ListItem Value="C1">C1 課程</asp:ListItem>
                <asp:ListItem Value="C2">C2 課程</asp:ListItem>
                <asp:ListItem Value="C2QT">C2 QT研習營課程</asp:ListItem>
                <asp:ListItem Value="C2M">C2 榮耀男人課程</asp:ListItem>
                <asp:ListItem Value="C2W">C2 幸福女人課程</asp:ListItem>
                <asp:ListItem Value="C3N">C3 九型人格課程</asp:ListItem>
                <asp:ListItem Value="C3P">C3 人際關係課程</asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAddSubject" runat="server" Text="" OnClick="btnAddSubject_Click" Font-Size="14pt" />
        </div>
        <p/>
        <div>
            <asp:GridView ID="gvSubject" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSubject_RowDataBound" OnRowDeleting="gvSubject_RowDeleting" Width="60%" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                            <asp:Label ID="lblSubCount" runat="server" Text=""></asp:Label> - 
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
                            <asp:Button ID="btnDel" runat="server" Text="刪除" DELID='<%# Eval("SID") %>' CommandName="Delete"
                                OnClientClick="if (confirm('確定刪除?') == false) return false;" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        </center>

    </form>
</body>
</html>
