using ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin
{
    public partial class ExamAdd : System.Web.UI.Page
    {
        AnswerItemADO ansitem = new AnswerItemADO();
        ExamQuestionsADO eques = new ExamQuestionsADO();

        public string Title;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CheckStringByRequest("ec") != "")
                {
                    try
                    {
                        switch (Request.QueryString["ec"].Substring(0, 2))
                        {
                            case "C1":
                                Title = "C1 考題編輯";
                                break;
                            case "C2":

                                Title = "C2 " + Request.QueryString["ec"].Substring(2, 2) + "課考題編輯";
                                break;
                        }

                    } catch { }
                    
                }
                else
                {
                    Response.Redirect("SystemSet.aspx");
                }
            }

        }

        private string CheckStringByRequest(string QueryString_Name)
        {
            string _value = string.Empty;
            if (Request.QueryString[QueryString_Name] != null && Request.QueryString[QueryString_Name].ToString().Length > 0)
                _value = Request.QueryString[QueryString_Name].ToString();

            return _value;
        }

        /// <summary>
        /// 儲存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string ec = Request.QueryString["ec"];

            #region 取得題目段落

            string _Field1 = txtField_1.Value.Trim();
            string _Field2 = txtField_2.Value.Trim();
            string _Field3 = txtField_3.Value.Trim();
            string _Field4 = txtField_4.Value.Trim();
            string _Field5 = txtField_5.Value.Trim();
            string _Field6 = txtField_6.Value.Trim();
            string _Field7 = txtField_7.Value.Trim();
            string _Field8 = txtField_8.Value.Trim();
            string _Field9 = txtField_9.Value.Trim();
            string _Field10 = txtField_10.Value.Trim();

            #endregion

            #region 取得該格子是否為答案

            bool _IsField1 = ckbIsField_1.Checked;
            bool _IsField2 = ckbIsField_2.Checked;
            bool _IsField3 = ckbIsField_3.Checked;
            bool _IsField4 = ckbIsField_4.Checked;
            bool _IsField5 = ckbIsField_5.Checked;
            bool _IsField6 = ckbIsField_6.Checked;
            bool _IsField7 = ckbIsField_7.Checked;
            bool _IsField8 = ckbIsField_8.Checked;
            bool _IsField9 = ckbIsField_9.Checked;
            bool _IsField10 = ckbIsField_10.Checked;

            #endregion

            #region 取得答題數

            int _FieldCnt = 0; //答題數
            for (int i = 1; i <= 10; i++)
            {
                CheckBox ckb = (CheckBox)Page.FindControl("ckbIsField_" + i);

                if (ckb.Checked)
                {
                    ansitem.DelAnswerItem(ec, GetField(i));
                    ansitem.InsAnswerItem(ec, GetField(i));
                    _FieldCnt++;
                }
                    

            }

            #endregion

            eques.InsExamQuestions(ec, _FieldCnt, _IsField1, _Field1,
                                                            _IsField2, _Field2,
                                                            _IsField3, _Field3,
                                                            _IsField4, _Field4,
                                                            _IsField5, _Field5,
                                                            _IsField6, _Field6,
                                                            _IsField7, _Field7,
                                                            _IsField8, _Field8,
                                                            _IsField9, _Field9,
                                                            _IsField10, _Field10);

            Response.Write("<script>alert('儲存成功');location.href='ExamList.aspx?ec=" + ec + "'</script>");

        }

        /// <summary>
        /// 取得取得題目段落的值
        /// </summary>
        protected string GetField(int i)
        {
            switch (i)
            {
                case 1:
                    return txtField_1.Value.Trim();
                case 2:
                    return txtField_2.Value.Trim();
                case 3:
                    return txtField_3.Value.Trim();
                case 4:
                    return txtField_4.Value.Trim();
                case 5:
                    return txtField_5.Value.Trim();
                case 6:
                    return txtField_6.Value.Trim();
                case 7:
                    return txtField_7.Value.Trim();
                case 8:
                    return txtField_8.Value.Trim();
                case 9:
                    return txtField_9.Value.Trim();
                case 10:
                    return txtField_10.Value.Trim();
                default:
                    return "";
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        protected void btnCel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamList.aspx?ec=" + Request.QueryString["ec"]);
        }
    }
}