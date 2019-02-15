using ADO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Tool
{
    public partial class ChkMemberID : System.Web.UI.Page
    {
        CLC_FinancialSys _CLC_FinancialSys = new CLC_FinancialSys();

        protected void Page_Load(object sender, EventArgs e)
        {
            string ErrorID1 = string.Empty;
            string ErrorID2 = string.Empty;

            DataTable dt1 = _CLC_FinancialSys.QueryCLC_FinancialSys_ID_Temp_1();
            foreach (DataRow dr in dt1.Rows)
            {
                string id = dr["FieldC"].ToString().Trim();

                try
                {
                    if (!IsValidPersonalId(id))
                    {
                        ErrorID1 += "'" + id + "',";
                    }
                }
                catch
                {
                    ErrorID1 += "'" + id + "',";
                }


            }


            DataTable dt2 = _CLC_FinancialSys.QueryCLC_FinancialSys_ID_Temp_2();
            foreach (DataRow dr in dt2.Rows)
            {
                string id = dr["FieldC"].ToString().Trim();

                try
                {
                    if (!IsValidPersonalId(id))
                    {
                        ErrorID2 += "'" + id + "',";
                    }
                }
                catch
                {
                    ErrorID2 += "'" + id + "',";
                }


            }

            Response.Write(ErrorID1);
            Response.Write("<p/>");
            Response.Write(ErrorID2);
        }

        /// <summary>
        /// 身份證字號格式檢查
        /// </summary>
        /// <param name="id">欲檢查之字串</param>
        /// <returns>true：正確</returns>
        public bool IsValidPersonalId(string id)
        {
            return ChackId(id) == "0";
        }

        /// <summary>
        /// 身份證合法性檢查
        /// </summary>
        /// <param name="vid">欲檢查之字串</param>
        /// <returns>1：字數不到10，2：第二碼非1,2，3：首碼有誤，4：查碼不對</returns>
        private static string ChackId(string vid)
        {
            List<string> FirstEng =
            new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "J",
                               "K", "L", "M", "N", "P", "Q", "R", "S", "T",
                               "U", "V", "X", "Y", "W", "Z", "I", "O" };
            string aa = vid.ToUpper();
            bool chackFirstEnd = false;
            if (aa.Trim().Length == 10)
            {
                byte firstNo = Convert.ToByte(aa.Trim().Substring(1, 1));
                if (firstNo > 2 || firstNo < 1)
                {
                    return "2";
                }
                else
                {
                    int x;
                    for (x = 0; x < FirstEng.Count; x++)
                    {
                        if (aa.Substring(0, 1) == FirstEng[x])
                        {
                            aa = string.Format("{0}{1}", x + 10, aa.Substring(1, 9));
                            chackFirstEnd = true;
                            break;
                        }
                    }
                    if (!chackFirstEnd)
                        return "3";
                    int i = 1;
                    int ss = int.Parse(aa.Substring(0, 1));
                    while (aa.Length > i)
                    {
                        ss = ss + (int.Parse(aa.Substring(i, 1)) * (10 - i));
                        i++;
                    }
                    aa = ss.ToString();
                    if (vid.Substring(9, 1) == "0")
                    {
                        if (aa.Substring(aa.Length - 1, 1) == "0")
                        {
                            return "0";
                        }
                        else
                        {
                            return "4";
                        }
                    }
                    else
                    {
                        if (vid.Substring(9, 1) == (10 - int.Parse(aa.Substring(aa.Length - 1, 1))).ToString())
                        {
                            return "0";
                        }
                        else
                        {
                            return "4";
                        }
                    }
                }
            }
            else
            {
                return "1";
            }
        }


    }
}