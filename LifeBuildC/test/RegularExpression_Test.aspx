<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegularExpression_Test.aspx.cs" Inherits="LifeBuildC.test.RegularExpression_Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <font style="font-size: 18px">驗證條件：</font>
            <asp:TextBox ID="txtCondition" Width="100%" runat="server" Font-Size="18px"></asp:TextBox><p />
            <font style="font-size: 18px">資料輸入：</font>
            <asp:TextBox ID="txtRegex" Width="100%" runat="server" Font-Size="18px"></asp:TextBox>
            <p />
            <asp:Button ID="btnRegex" runat="server" Text="驗證" Font-Size="18px" OnClick="btnRegex_Click" />
            <asp:Button ID="btnClear" runat="server" Text="清空" Font-Size="18px" OnClick="btnClear_Click" />
            <p />
            <font style="font-size: 18px">結果：</font>
            <asp:Label ID="lblChk" runat="server" Text=""></asp:Label>
            <br />
            <asp:ListBox ID="lblls" Width="100%" Height="100px" runat="server" Font-Size="18px"></asp:ListBox>
            <p />
            <asp:Button ID="btnRegex_1" runat="server" Text="只能輸入1個數字" Font-Size="18px" OnClick="btnRegex_1_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_2" runat="server" Text="只能輸入n個數字" Font-Size="18px" OnClick="btnRegex_2_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_3" runat="server" Text="只能輸入至少n個數字 " Font-Size="18px" OnClick="btnRegex_3_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_4" runat="server" Text="只能輸入m到n個數字 " Font-Size="18px" OnClick="btnRegex_4_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_5" runat="server" Text="只能輸入數字" Font-Size="18px" OnClick="btnRegex_5_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_6" runat="server" Text="只能輸入某個區間數字" Font-Size="18px" OnClick="btnRegex_6_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_7" runat="server" Text="只能輸入0和非0開頭的數字" Font-Size="18px" OnClick="btnRegex_7_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_8" runat="server" Text="只能輸入實數" Font-Size="18px" OnClick="btnRegex_8_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_9" runat="server" Text="只能輸入n位小數的正實數" Font-Size="18px" OnClick="btnRegex_9_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_10" runat="server" Text="只能輸入mn位小數的正實數" Font-Size="18px" OnClick="btnRegex_10_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_11" runat="server" Text="只能輸入非0的正整數" Font-Size="18px" OnClick="btnRegex_11_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_12" runat="server" Text="只能輸入非0的負整數" Font-Size="18px" OnClick="btnRegex_12_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_13" runat="server" Text="只能輸入n個字符" Font-Size="18px" OnClick="btnRegex_13_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_14" runat="server" Text="只能輸入英文字符" Font-Size="18px" OnClick="btnRegex_14_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_15" runat="server" Text="只能輸入大寫英文字符" Font-Size="18px" OnClick="btnRegex_15_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_16" runat="server" Text="只能輸入小寫英文字符" Font-Size="18px" OnClick="btnRegex_16_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_17" runat="server" Text="只能輸入英文字符+數字" Font-Size="18px" OnClick="btnRegex_17_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_18" runat="server" Text="只能輸入英文字符/數字/下劃線" Font-Size="18px" OnClick="btnRegex_18_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_19" runat="server" Text="密碼舉例" Font-Size="18px" OnClick="btnRegex_19_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_20" runat="server" Text="驗證首字母大寫" Font-Size="18px" OnClick="btnRegex_20_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_21" runat="server" Text="驗證網址（帶?id=中文）VS.NET2005無此功能" Font-Size="18px" OnClick="btnRegex_21_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_22" runat="server" Text="驗證漢字" Font-Size="18px" OnClick="btnRegex_22_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_23" runat="server" Text="驗證QQ號" Font-Size="18px" OnClick="btnRegex_23_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_24" runat="server" Text="驗證電子郵件（驗證MSN號一樣）" Font-Size="18px" OnClick="btnRegex_24_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_25" runat="server" Text="驗證身份證號（粗驗，最好服務器端調類庫再細驗證）" Font-Size="18px" OnClick="btnRegex_25_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_26" runat="server" Text="驗證手機號（包含159，不包含小靈通）" Font-Size="18px" OnClick="btnRegex_26_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_27" runat="server" Text="驗證電話號碼號（很複雜，VS.NET2005給的是錯的）" Font-Size="18px" OnClick="btnRegex_27_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_28" runat="server" Text="驗證護照" Font-Size="18px" OnClick="btnRegex_28_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_29" runat="server" Text="驗證IP" Font-Size="18px" OnClick="btnRegex_29_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_30" runat="server" Text="驗證域" Font-Size="18px" OnClick="btnRegex_30_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_31" runat="server" Text="驗證信用卡" Font-Size="18px" OnClick="btnRegex_31_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_32" runat="server" Text="驗證ISBN 國際標準書號" Font-Size="18px" OnClick="btnRegex_32_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_33" runat="server" Text="驗證GUID 全球唯一標識符" Font-Size="18px" OnClick="btnRegex_33_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_34" runat="server" Text="驗證文件路徑和擴展名" Font-Size="18px" OnClick="btnRegex_34_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_35" runat="server" Text="驗證Html顏色值" Font-Size="18px" OnClick="btnRegex_35_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_36" runat="server" Text="驗證手機" Font-Size="18px" OnClick="btnRegex_36_Click" />&nbsp;|&nbsp;
            <asp:Button ID="btnRegex_37" runat="server" Text="驗證市話" Font-Size="18px" OnClick="btnRegex_37_Click" />&nbsp;|&nbsp;
        </div>
        <div>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 5%; text-align: center;">字元</td>
                    <td>描述</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\</td>
                    <td>將下一個字元標記為一個特殊字元、或一個原義字元、或一個向後引用、或一個八進制轉義符。例如，"n"匹配字元"n"。"\n"匹 配一個分行符號。序列"\\"匹配"\"而"\("則匹配"("。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">^</td>
                    <td><font style="color: red">匹配輸入字串的開始位置。</font>如果設置了RegExp物件的Multiline屬性，^也匹配"\n"或"\r"之後的位置。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">$</td>
                    <td><font style="color: red">匹配輸入字串的結束位置。</font>如果設置了RegExp物件的Multiline屬性，$也匹配"\n"或"\r"之前的位置。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">*</td>
                    <td>匹配前面的子運算式零次或多次。例如，zo*能匹配"z"以及"zoo"。*等價於{0,}。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">+</td>
                    <td>匹配前面的子運算式一次或多次。例如，"zo+"能匹配"zo"以及"zoo"，但不能匹配"z"。+等價於{1,}。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">?</td>
                    <td>匹配前面的子運算式零次或一次。例如，"do(es)?"可以匹配"do"或"does"中的"do"。?等價於{0,1}。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">{n}</td>
                    <td>n是一個非負整數。匹配確定的n次。例如，"o{2}"不能匹配"Bob"中的"o"，但是能匹配"food"中的兩個o。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">{n,}</td>
                    <td>n是一個非負整數。至少匹配n次。例如，"o{2,}"不能匹配"Bob"中的"o"，但能匹配"foooood"中的所有o。"o{1,}"等價於"o+"。"o{0,}"則等價於"o*"。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">{n,m}</td>
                    <td>m和n均為非負整數，其中n<=m。最少匹配n次且最多匹配m次。例如，"o{1,3}"將匹配"fooooood"中的前三個o。"o{0,1}"等價於"o?"。請注意在逗號和兩個數之間不能有空格。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">?</td>
                    <td>當該字元緊跟在任何一個其他限制符(*,+,?,{n},{n,},{n,m})後面時，匹配模式是非貪婪的。非貪婪模式盡可能少的匹配所搜索的字串，而預設的貪婪模式則盡可能多的匹配所搜索的字串。例如，對於字串"oooo"，"o+?"將匹配單個"o"，而"o+"將匹配所有"o"。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">.</td>
                    <td>匹配除"\n"之外的任何單個字元。要匹配包括"\n"在內的任何字元，請使用像"[.\n]"的模式。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">(pattern)</td>
                    <td>匹配pattern並獲取這一匹配。所獲取的匹配可以從產生的Matches集合得到，在VBScript中使用SubMatches集合，在JScript中則使用$0…$9屬性。要匹配圓括號字元，請使用"\("或"\)"。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">(?:pattern)</td>
                    <td>匹配pattern但不獲取匹配結果，也就是說這是一個非獲取匹配，不進行存儲供以後使用。這在使用"或"字元(|)來組合一個模式的各個部分是很有用。例如，"industr(?:y|ies)就是一個比"industry|industries'更簡略的運算式。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">(?=pattern)</td>
                    <td>正向預查，在任何匹配pattern的字串開始處匹配查找字串。這是一個非獲取匹配，也就是說，該匹配不需要獲取供以後使用。例 如，"Windows(?=95|98|NT|2000)"能匹配"Windows2000"中的"Windows"，但不能匹配 "Windows3.1"中的"Windows"。預查不消耗字元，也就是說，在一個匹配發生後，在最後一次匹配之後立即開始下一次匹配的搜索，而不是從 包含預查的字元之後開始。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">(?!pattern)</td>
                    <td>負向預查，在任何不匹配pattern的字串開始處匹配查找字串。這是一個非獲取匹配，也就是說，該匹配不需要獲取供以後使用。例如 "Windows(?!95|98|NT|2000)"能匹配"Windows3.1"中的"Windows"，但不能匹配"Windows2000"中 的"Windows"。預查不消耗字元，也就是說，在一個匹配發生後，在最後一次匹配之後立即開始下一次匹配的搜索，而不是從包含預查的字元之後開始</td>
                </tr>
                <tr>
                    <td style="text-align: center;">x|y</td>
                    <td>匹配x或y。例如，"z|food"能匹配"z"或"food"。"(z|f)ood"則匹配"zood"或"food"。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">[xyz]</td>
                    <td>字元集合。匹配所包含的任意一個字元。例如，"[abc]"可以匹配"plain"中的"a"。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">[^xyz]</td>
                    <td>負值字元集合。匹配未包含的任意字元。例如，"[^abc]"可以匹配"plain"中的"p"。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">[a-z]</td>
                    <td>字元範圍。匹配指定範圍內的任意字元。例如，"[a-z]"可以匹配"a"到"z"範圍內的任意小寫字母字元。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">[^a-z]</td>
                    <td>負值字元範圍。匹配任何不在指定範圍內的任意字元。例如，"[^a-z]"可以匹配任何不在"a"到"z"範圍內的任意字元。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\b</td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: center;">\B</td>
                    <td>匹配非單詞邊界。"er\B"能匹配"verb"中的"er"，但不能匹配"never"中的"er"。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\cx</td>
                    <td>匹配由x指明的控制字元。例如，\cM匹配一個Control-M或回車符。x的值必須為A-Z或a-z之一。否則，將c視為一個原義的"c"字元。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\d</td>
                    <td>匹配一個數位字元。等價於[0-9]。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\D</td>
                    <td>匹配一個非數位字元。等價於[^0-9]。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\f</td>
                    <td>匹配一個換頁符。等價於\x0c和\cL。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\n</td>
                    <td>匹配一個分行符號。等價於\x0a和\cJ。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\r</td>
                    <td>匹配一個回車符。等價於\x0d和\cM。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\s</td>
                    <td>匹配任何空白字元，包括空格、定位字元、換頁符等等。等價於[\f\n\r\t\v]。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\S</td>
                    <td>匹配任何非空白字元。等價於[^\f\n\r\t\v]。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\t</td>
                    <td>匹配一個定位字元。等價於\x09和\cI。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\v</td>
                    <td>匹配一個垂直定位字元。等價於\x0b和\cK。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\w</td>
                    <td>匹配包括底線的任何單詞字元。等價於"[A-Za-z0-9_]"。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\W</td>
                    <td>匹配任何非單詞字元。等價於"[^A-Za-z0-9_]"。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\xn</td>
                    <td>匹配n，其中n為十六進位轉義值。十六進位轉義值必須為確定的兩個數位長。例如，"\x41"匹配"A"。"\x041"則等價於"\x04"&"1"。規則運算式中可以使用ASCII編碼。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\num</td>
                    <td>匹配num，其中num是一個正整數。對所獲取的匹配的引用。例如，"(.)\1"匹配兩個連續的相同字元。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\n</td>
                    <td>標識一個八進制轉義值或一個向後引用。如果\n之前至少n個獲取的子運算式，則n為向後引用。否則，如果n為八進位數字(0-7)，則n為一個八進制轉義值。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\nm</td>
                    <td>標識一個八進制轉義值或一個向後引用。如果\nm之前至少有nm個獲得子運算式，則nm為向後引用。如果\nm之前至少有n個獲取，則n為一個後跟文字m的向後引用。如果前面的條件都不滿足，若n和m均為八進位數字(0-7)，則\nm將匹配八進制轉義值nm。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\nml</td>
                    <td>如果n為八進位數字(0-3)，且m和l均為八進位數字(0-7)，則匹配八進制轉義值nml。</td>
                </tr>
                <tr>
                    <td style="text-align: center;">\un</td>
                    <td>匹配n，其中n是一個用四個十六進位數位表示的Unicode字元。例如，\u00A9匹配版權符號（©）。</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
