using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.test
{
    public partial class RegularExpression_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnRegex_Click(object sender, EventArgs e)
        {
            lblls.Items.Clear();

            List<string> ls = new List<string>();
            ls = RegularExpressionByCondition(txtCondition.Text, txtRegex.Text);
            foreach (string s in ls)
            {
                lblls.Items.Add(s);
            }

            lblChk.Text = "驗證失敗!!";
            lblChk.ForeColor = System.Drawing.Color.Red;
            if (lblls.Items.Count == 1)
            {
                lblChk.Text = "驗證通過!!";
                lblChk.ForeColor = System.Drawing.Color.Blue;
            }

        }

        /// <summary>
        /// 條件測試驗證
        /// </summary>
        /// <param name="RegexCondition"></param>
        /// <param name="EmailText"></param>
        /// <returns></returns>
        public List<string> RegularExpressionByCondition(string RegexCondition, string EmailText)
        {
            Regex regImg = new Regex(RegexCondition, RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(EmailText);

            List<string> ls = new List<string>();
            foreach (Match match in matches)
            {
                ls.Add(match.Value);
            }

            return ls;
        }

        //^[a-zA-Z0-9_\-]{3,15}$
        //長度為3-15的字元
        //

        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblls.Items.Clear();
            txtRegex.Text = "";
        }

        /// <summary>
        /// 只能輸入1個數字 
        /// </summary>
        protected void btnRegex_1_Click(object sender, EventArgs e)
        {
            //只能輸入1個數字 
            //表達式 	^\d$
            //描述 	匹配一個數字
            //匹配的例子 	0,1,2,3
            //不匹配的例子 	 

            txtCondition.Text = @"^\d$";
        }

        /// <summary>
        /// 只能輸入n個數字
        /// </summary>
        protected void btnRegex_2_Click(object sender, EventArgs e)
        {
            //只能輸入n個數字  
            //表達式 	^\d{n}$ 例如^\d{8}$
            //描述 	匹配8個數字
            //匹配的例子 	12345678,22223334,12344321
            //不匹配的例子 	 

            txtCondition.Text = @"^\d{n}$";
        }

        /// <summary>
        /// 只能輸入至少n個數字
        /// </summary>
        protected void btnRegex_3_Click(object sender, EventArgs e)
        {
            //只能輸入至少n個數字 
            //表達式 	^/d{n,}$ 例如^/d{8,}$
            //描述 	匹配最少n個數字
            //匹配的例子 	12345678,123456789,12344321
            //不匹配的例子 	 

            txtCondition.Text = @"^\d{n,}$";
        }

        /// <summary>
        /// 只能輸入m到n個數字
        /// </summary>
        protected void btnRegex_4_Click(object sender, EventArgs e)
        {
            // 只能輸入m到n個數字 
            //表達式 	^/d{m,n}$ 例如^/d{7,8}$
            //描述 	匹配m到n個數字
            //匹配的例子 	12345678,1234567
            //不匹配的例子 	123456,123456789

            txtCondition.Text = @"^\d{m,n}$";
        }

        /// <summary>
        /// 只能輸入數字
        /// </summary>
        protected void btnRegex_5_Click(object sender, EventArgs e)
        {
            //只能輸入數字  
            //表達式 	^[0-9]*$
            //描述 	匹配任意個數字
            //匹配的例子 	12345678,1234567
            //不匹配的例子 	E,

            txtCondition.Text = @"^[0-9]*$";
        }

        /// <summary>
        /// 只能輸入某個區間數字
        /// </summary>
        protected void btnRegex_6_Click(object sender, EventArgs e)
        {
            //只能輸入某個區間數字  
            //表達式 	^[12-15]$
            //描述 	匹配某個區間的數字
            //匹配的例子 	12,13,14,15
            //不匹配的例子 	 

            txtCondition.Text = @"^[12-15]$";
        }

        /// <summary>
        /// 只能輸入0或非0開頭的數字
        /// </summary>
        protected void btnRegex_7_Click(object sender, EventArgs e)
        {
            //只能輸入0或非0開頭的數字  
            //表達式 	^(0|[1-9][0-9]*)$
            //描述 	可以為0，第一個數字不能為0，數字中可以有0
            //匹配的例子 	12,10,101,100
            //不匹配的例子 	01,

            txtCondition.Text = @"^(0|[1-9][0-9]*)$";
        }

        /// <summary>
        /// 只能輸入實數
        /// </summary>
        protected void btnRegex_8_Click(object sender, EventArgs e)
        {
            //            只能輸入實數  
            //表達式 	^[-+]?/d+(/./d+)?$
            //描述 	匹配實數
            //匹配的例子 	18,+3.14,-9.90
            //不匹配的例子 	.6,33s,67-99

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 只能輸入n位小數的正實數
        /// </summary>
        protected void btnRegex_9_Click(object sender, EventArgs e)
        {
            //            只能輸入n位小數的正實數  
            //表達式 	^[0-9]+(.[0-9]{n})?$以^[0-9]+(.[0-9]{2})?$為例
            //描述 	匹配n位小數的正實數
            //匹配的例子 	2.22
            //不匹配的例子 	2.222,-2.22,

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 只能輸入mn位小數的正實數
        /// </summary>
        protected void btnRegex_10_Click(object sender, EventArgs e)
        {
            //            只能輸入mn位小數的正實數  
            //表達式 	^[0-9]+(.[0-9]{m,n})?$以^[0-9]+(.[0-9]{1,2})?$為例
            //描述 	匹配m到n位小數的正實數
            //匹配的例子 	2.22,2.2
            //不匹配的例子 	2.222,-2.2222,

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 只能輸入非0的正整數
        /// </summary>
        protected void btnRegex_11_Click(object sender, EventArgs e)
        {
            //            只能輸入非0的正整數  
            //表達式 	^/+?[1-9][0-9]*$
            //描述 	匹配非0的正整數
            //匹配的例子 	2,23,234
            //不匹配的例子 	0,-4,

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 只能輸入非0的負整數
        /// </summary>
        protected void btnRegex_12_Click(object sender, EventArgs e)
        {
            //            只能輸入非0的負整數 
            //表達式 	^/-[1-9][0-9]*$
            //描述 	匹配非0的負整數
            //匹配的例子 	-2,-23,-234
            //不匹配的例子 	0,4,

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 只能輸入n個字符
        /// </summary>
        protected void btnRegex_13_Click(object sender, EventArgs e)
        {
            //            只能輸入n個字符 
            //表達式 	^.{n}$ 以^.{4}$為例
            //描述 	匹配n個字符，注意漢字只算1個字符
            //匹配的例子 	1234,12we,123清,清清月兒
            //不匹配的例子 	0,123,123www,

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 只能輸入英文字符
        /// </summary>
        protected void btnRegex_14_Click(object sender, EventArgs e)
        {
            //只能輸入英文字符 
            //表達式 	^.[A-Za-z]+$為例
            //描述 	匹配英文字符，大小寫任意
            //匹配的例子 	Asp,WWW,
            //不匹配的例子 	0,123,123www,

            txtCondition.Text = @"^.[A-Za-z]+$";
        }

        /// <summary>
        /// 只能輸入大寫英文字符
        /// </summary>
        protected void btnRegex_15_Click(object sender, EventArgs e)
        {
            //            只能輸入大寫英文字符 
            //表達式 	^.[AZ]+$為例
            //描述 	匹配英文大寫字符
            //匹配的例子 	NET,WWW,
            //不匹配的例子 	0,123,123www,

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 只能輸入小寫英文字符
        /// </summary>
        protected void btnRegex_16_Click(object sender, EventArgs e)
        {
            //            只能輸入小寫英文字符 
            //表達式 	^.[az]+$為例
            //描述 	匹配英文大寫字符
            //匹配的例子 	asp,csdn
            //不匹配的例子 	0,NET,WWW,

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 只能輸入英文字符+數字
        /// </summary>
        protected void btnRegex_17_Click(object sender, EventArgs e)
        {
            //            只能輸入英文字符+數字 
            //表達式 	^.[A-Za-z0-9]+$為例
            //描述 	匹配英文字符+數字
            //匹配的例子 	1Asp,W1W1W,
            //不匹配的例子 	0,123,123,www,

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 只能輸入英文字符/數字/下劃線
        /// </summary>
        protected void btnRegex_18_Click(object sender, EventArgs e)
        {
            //只能輸入英文字符/數字/下劃線 
            //表達式 	^\w+$為例
            //描述 	匹配英文字符或數字或下劃線
            //匹配的例子 	1Asp,WWW,12,1_w
            //不匹配的例子 	3#,2-4,w#$,

            txtCondition.Text = @"^\w+$";
        }

        /// <summary>
        /// 密碼舉例
        /// </summary>
        protected void btnRegex_19_Click(object sender, EventArgs e)
        {
            //            密碼舉例 
            //表達式 	^.[a-zA-Z] /w{m,n}$
            //描述 	匹配英文字符開頭的mn位字符且只能數字字母或下劃線
            //匹配的例子 	 
            //不匹配的例子 	 

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證首字母大寫
        /// </summary>
        protected void btnRegex_20_Click(object sender, EventArgs e)
        {
            //            驗證首字母大寫 
            //表達式 	/b[^/Wa-z0-9_][^/WA-Z0-9_]*/b
            //描述 	首字母只能大寫
            //匹配的例子 	Asp,Net
            //不匹配的例子 	 

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證網址（帶?id=中文）VS.NET2005無此功能
        /// </summary>
        protected void btnRegex_21_Click(object sender, EventArgs e)
        {
            //            驗證網址（帶?id=中文）VS.NET2005無此功能 
            //表達式 	

            //^http:////([/w-]+(/.[/w-]+)+(//[/w- .///?%&=/u4e00-/u9fa5]*)?) ?$
            //描述 	驗證帶?id=中文
            //匹配的例子 	, 
            //http://blog.csdn.net?id=清清月兒
            //不匹配的例子 	 

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證漢字
        /// </summary>
        protected void btnRegex_22_Click(object sender, EventArgs e)
        {
            //            驗證漢字 
            //表達式 	^[/u4e00-/u9fa5]{0,}$
            //描述 	只能漢字
            //匹配的例子 	清清月兒
            //不匹配的例子 	 

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證QQ號
        /// </summary>
        protected void btnRegex_23_Click(object sender, EventArgs e)
        {
            //            驗證QQ號 
            //表達式 	[0-9]{5,9}
            //描述 	5-9位的QQ號
            //匹配的例子 	10000,123456
            //不匹配的例子 	10000w,

            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證電子郵件（驗證MSN號一樣）
        /// </summary>
        protected void btnRegex_24_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證身份證號（粗驗，最好服務器端調類庫再細驗證）
        /// </summary>
        protected void btnRegex_25_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證手機號（包含159，不包含小靈通）
        /// </summary>
        protected void btnRegex_26_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證電話號碼號（很複雜，VS.NET2005給的是錯的）
        /// </summary>
        protected void btnRegex_27_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證護照
        /// </summary>
        protected void btnRegex_28_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證IP
        /// </summary>
        protected void btnRegex_29_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證域
        /// </summary>
        protected void btnRegex_30_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證信用卡
        /// </summary>
        protected void btnRegex_31_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證ISBN 國際標準書號
        /// </summary>
        protected void btnRegex_32_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證GUID 全球唯一標識符
        /// </summary>
        protected void btnRegex_33_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證文件路徑和擴展名
        /// </summary>
        protected void btnRegex_34_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證Html顏色值
        /// </summary>
        protected void btnRegex_35_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"";
        }

        /// <summary>
        /// 驗證手機
        /// </summary>
        protected void btnRegex_36_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"/^(13[0-9]{9})|(15[89][0-9]{8})$/";
        }

        /// <summary>
        /// 驗證市話
        /// </summary>
        protected void btnRegex_37_Click(object sender, EventArgs e)
        {
            txtCondition.Text = @"\d{3}-\d{8}|\d{4}-\d{7}";
        }

    }
}