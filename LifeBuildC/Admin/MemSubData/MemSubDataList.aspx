<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemSubDataList.aspx.cs" Inherits="LifeBuildC.Admin.MemSubData.MemSubDataList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>生命建造程序修課資料</title>
    <link href="../css/PageStyle.css" rel="stylesheet" />
    <script src="../../js/jquery-3.1.1.min.js"></script>
    <script src="../js/MemSubDataList.js"></script>
</head>
<body>
    <form id="form1" runat="server">

        <div style="text-align: center; margin: 5px;">
            <h1>生命建造程序修課資料</h1>
        </div>

                    <!--組別-->
            <div class="FieldStyle">
                <asp:Label ID="Label1" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label2" ForeColor="#5a5e66" runat="server" Text="組別"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label3" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                    <asp:Label ID="Label4" ForeColor="#5a5e66" runat="server" Text="組別"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </span>
                <select class="inputStyle" id="dropGroupClass" runat="server">
                </select>
            </div>

            <!--小組-->
            <div class="FieldStyle">
                <asp:Label ID="Label5" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                &nbsp;
                <asp:Label ID="Label6" ForeColor="#5a5e66" runat="server" Text="小組"></asp:Label>
            </div>
            <div style="text-align: center; margin: 5px;">
                <span class="FieldStyle2">
                    <asp:Label ID="Label7" ForeColor="#fa5555" runat="server" Text="*"></asp:Label>
                    &nbsp;
                    <asp:Label ID="Label8" ForeColor="#5a5e66" runat="server" Text="小組"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </span>
                <select class="inputStyle" id="dropGroupName" runat="server">
                </select>
            </div>

        <div>
            <asp:GridView ID="gvChcMember" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvChcMember_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnView" runat="server" Text="檢視" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="GroupClass" HeaderText="組別">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="小組">
                        <ItemTemplate>
                            <asp:Label ID="lblGroupCName" runat="server" Text=""></asp:Label>-
                            <asp:Label ID="lblGroupName" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Ename" HeaderText="姓名">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Church" HeaderText="教會">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="C1 第一、二課">
                        <ItemTemplate>
                            <asp:Label ID="lblIsC112" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C1 第三、四課">
                        <ItemTemplate>
                            <asp:Label ID="lblIsC134" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C2 第一、二課">
                        <ItemTemplate>
                            <asp:Label ID="lblIsC212" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C2 第三、四課">
                        <ItemTemplate>
                            <asp:Label ID="lblIsC234" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C2 第五課">
                        <ItemTemplate>
                            <asp:Label ID="lblIsC25" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="C1_Score" HeaderText="C1 考試">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="C212_Score" HeaderText="C2 第一、二課考試">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="C234_Score" HeaderText="C2 第三、四課考試">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="交見證">
                        <ItemTemplate>
                            <asp:Label ID="lblIswitness" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="C1_Status" HeaderText="C1 通過判定">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="C2_Status" HeaderText="C2 通過判定">

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>

                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
