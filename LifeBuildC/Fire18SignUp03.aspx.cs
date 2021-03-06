﻿using ADO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC
{
    public partial class Fire18SignUp03 : System.Web.UI.Page
    {
        FirePassWADO firePass = new FirePassWADO();
        FireMemberADO fireMem = new FireMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["gc"] != null && Request.QueryString["gc"].ToString() != "" &&
                    Request.QueryString["pk"] != null && Request.QueryString["pk"].ToString() != "")
                {
                    string GroupClass = Request.QueryString["gc"].ToString();
                    switch (GroupClass)
                    {
                        case "L": //成人報名
                            lblTitle.Text = "2020 烈火特會成人報名";
                            break;
                        case "S": //學青報名
                            lblTitle.Text = "2020 烈火特會學青報名";
                            break;
                        case "C": //12歲以下報名(含12歲)
                            lblTitle.Text = "2020 烈火特會兒童報名";
                            break;
                    }

                    string PassKey = Request.QueryString["pk"].ToString();

                    bool Is12 = firePass.CheckPassKey(PassKey, GroupClass);

                    DataTable dt = fireMem.GetFireMemberWherePassKey(PassKey);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //小組
                        lblGroupName.Text = dt.Rows[0]["group2"].ToString();
                        //姓名
                        lblEname.Text = dt.Rows[0]["Ename"].ToString();
                        //手機
                        lblPhone.Text = dt.Rows[0]["Phone"].ToString();
                        //信箱
                        lblGmail.Text = dt.Rows[0]["Gmail"].ToString();

                        if (lblGmail.Text.Trim() != "")
                        {
                            lblGmail2.Text = "特會資訊已發送至您的Mail   " + dt.Rows[0]["Gmail"].ToString();
                        }

                        if (GroupClass == "C")
                        {
                            lblgender.Text = "";
                        }
                        else
                        {
                            if (Is12)
                            {
                                if (bool.Parse(dt.Rows[0]["gender"].ToString()))
                                {
                                    lblgender.Text = "男生";
                                }
                                else
                                {
                                    lblgender.Text = "女生";
                                }
                            }
                        }


                        lblBirthday.Text = DateTime.Parse(dt.Rows[0]["Birthday"].ToString()).ToString("yyyy/MM/dd");
                        lblClothesSize.Text = dt.Rows[0]["ClothesSize"].ToString();

                        if (Is12)
                        {
                            //lblCourse.Text = "待大會通知";
                            /*
                            if (bool.Parse(dt.Rows[0]["Course"].ToString()))
                            {
                                lblCourse.Text = "教會突破";
                            }
                            else
                            {
                                lblCourse.Text = "生命突破";
                            }
                            */
                        }



                        
                    }

                }
                else
                {
                    Response.Redirect("Fire18SignUp.aspx");
                }
            }
        }
    }
}