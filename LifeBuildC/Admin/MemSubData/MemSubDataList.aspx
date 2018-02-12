<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemSubDataList.aspx.cs" Inherits="LifeBuildC.Admin.MemSubData.MemSubDataList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>生命建造資料查詢系統</title>
    <link href="../css/PageStyle.css" rel="stylesheet" />
    <script src="../../js/jquery-3.1.1.min.js"></script>
    <script src="../js/MemSubDataList.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hidGroupClass" name="hidGroupClass" type="hidden" />
        <input id="hidGroupName" name="hidGroupName" type="hidden" />
        <input id="hidEname" name="hidEname" type="hidden" />

        <div style="text-align: center; margin: 5px;">
            <img alt="" src="../../img/CLC_Logo.gif" />
            <h1>生命建造資料查詢系統</h1>
        </div>

        <!--組別-->
        <div class="FieldStyle">
            <asp:Label ID="Label2" ForeColor="#5a5e66" runat="server" Text="組別"></asp:Label>
        </div>
        <div style="text-align: center; margin: 5px;">
            <span class="FieldStyle2">
                <asp:Label ID="Label4" ForeColor="#5a5e66" runat="server" Text="組別"></asp:Label>
            </span>
            &nbsp;&nbsp;&nbsp;
                <asp:DropDownList CssClass="inputStyle" ID="dropGroupClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropGroupClass_SelectedIndexChanged">
                    <asp:ListItem Value="" disabled Selected hidden>請選擇組別</asp:ListItem>
                </asp:DropDownList>
        </div>

        <!--小組-->
        <div id="gid1_1" class="FieldStyle" runat="server">
            <asp:Label ID="Label6" ForeColor="#5a5e66" runat="server" Text="小組"></asp:Label>
        </div>
        <div id="gid1_2" style="text-align: center; margin: 5px;" runat="server">
            <span class="FieldStyle2">
                <asp:Label ID="Label8" ForeColor="#5a5e66" runat="server" Text="小組"></asp:Label>
            </span>
            &nbsp;&nbsp;&nbsp;
                <asp:DropDownList CssClass="inputStyle" ID="dropGroupName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropGroupName_SelectedIndexChanged">
                    <asp:ListItem Value="" disabled Selected hidden>請選擇組別</asp:ListItem>
                </asp:DropDownList>
        </div>

        <!--區長-->
        <div id="gid2_1" class="FieldStyle" runat="server">
            <asp:Label ID="Label10" ForeColor="#5a5e66" runat="server" Text="區長"></asp:Label>
        </div>
        <div id="gid2_2" style="text-align: center; margin: 5px;" runat="server">
            <span class="FieldStyle2">
                <asp:Label ID="Label12" ForeColor="#5a5e66" runat="server" Text="區長"></asp:Label>
            </span>
            &nbsp;&nbsp;&nbsp;
                <asp:DropDownList CssClass="inputStyle" ID="dropGroupCName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropGroupCName_SelectedIndexChanged">
                    <asp:ListItem Value="" disabled Selected hidden>請選擇區長</asp:ListItem>
                </asp:DropDownList>
        </div>

        <!--姓名-->
        <div class="FieldStyle">
            <asp:Label ID="Label1" ForeColor="#5a5e66" runat="server" Text="姓名"></asp:Label>
        </div>
        <div style="text-align: center; margin: 5px;">
            <span class="FieldStyle2">
                <asp:Label ID="Label3" ForeColor="#5a5e66" runat="server" Text="姓名"></asp:Label>
            </span>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox CssClass="inputStyle" ID="txtEname" runat="server" OnTextChanged="txtEname_TextChanged"></asp:TextBox>
        </div>

        <div style="text-align: center; margin: 5px;">
            <asp:Label ID="lblDataCnt" ForeColor="#fa5555" runat="server" Text="請選擇組別查詢員組修課資料"></asp:Label>
            <p/>
            <asp:Button CssClass="inputStyle" ID="Button1" runat="server" Text="查詢" />
            <p/>
            <asp:LinkButton ID="blkQ" PostBackUrl="~/Admin/MemSubData/MemSubDataQuestion.aspx" runat="server">問題反應</asp:LinkButton>
        </div>
        <asp:GridView ID="gvChcMember" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvChcMember_RowDataBound" HorizontalAlign="Center">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnView" runat="server" Text="檢視" />
                    </ItemTemplate>
                    <FooterStyle BackColor="Black" />
                    <HeaderStyle BorderColor="Black" ForeColor="#006600" />
                </asp:TemplateField>
                <asp:BoundField DataField="GroupClass" HeaderText="組&nbsp;&nbsp;&nbsp;別">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="小&nbsp;&nbsp;&nbsp;組">
                    <ItemTemplate>
                        <asp:Label ID="lblGroupCName" runat="server" Text=""></asp:Label>-
                            <asp:Label ID="lblGroupName" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField DataField="Ename" HeaderText="姓&nbsp;&nbsp;&nbsp;名">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Church" HeaderText="教&nbsp;&nbsp;&nbsp;會">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Underline="False" ForeColor="#006600" Width="200px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="C1<br/>第一、二課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC112" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C1<br/>第三、四課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC134" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C2<br/>第一、二課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC212" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C2<br/>第三、四課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC234" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C2<br/>第五課">
                    <ItemTemplate>
                        <asp:Label ID="lblIsC25" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField DataField="C1_Score" HeaderText="C1 考試">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="C212_Score" HeaderText="C2 第一、二課考試">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="C234_Score" HeaderText="C2 第三、四課考試">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="交見證">
                    <ItemTemplate>
                        <asp:Label ID="lblIswitness" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:BoundField DataField="C1_Status" HeaderText="C1 通過判定">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="C2_Status" HeaderText="C2 通過判定">

                    <FooterStyle BorderColor="Black" />

                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#006600" Width="100px" BorderColor="Black" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
