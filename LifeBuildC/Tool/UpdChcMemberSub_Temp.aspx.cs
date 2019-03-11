using libLifeBuildC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Tool
{
    public partial class UpdChcMemberSub_Temp : System.Web.UI.Page
    {
        AdoInfo Ado_Info = new AdoInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (true && Request.QueryString["CategoryID"] != null && 

                (Request.QueryString["CategoryID"].ToString() == "C112" ||
                 Request.QueryString["CategoryID"].ToString() == "C134" ||
                 Request.QueryString["CategoryID"].ToString() == "C212" ||
                 Request.QueryString["CategoryID"].ToString() == "C234" ||
                 Request.QueryString["CategoryID"].ToString() == "C25" ||
                 Request.QueryString["CategoryID"].ToString() == "C2QT" ||
                 Request.QueryString["CategoryID"].ToString() == "C2MW" ||
                 Request.QueryString["CategoryID"].ToString() == "C2InTo" ||
                 Request.QueryString["CategoryID"].ToString() == "C3N" ||
                 Request.QueryString["CategoryID"].ToString() == "C3P"
                 )

               )
            {
                string CategoryID = Request.QueryString["CategoryID"].ToString();

                DataTable dtMemTemp = Ado_Info.ChcMemberSub_Temp_ADO.QueryEStatus1ByChcMemberSub_Temp(CategoryID.ToUpper());
                DataTable dtMem = Ado_Info.ChcMember_ADO.QueryAllByChcMember();
                foreach (DataRow dr in dtMemTemp.Rows)
                {
                    DataRow[] drChcMem = null;

                    //1. 先用MID取得資料
                    if (dr["MID"].ToString() != "")
                    {
                        drChcMem = dtMem.Select("MID='" + dr["MID"].ToString() + "'");
                    }

                    if (drChcMem != null && drChcMem.Count() > 0)
                    {
                        //更新上課資料
                        #region

                        //寫入Log
                        Ado_Info.ChcMember_Log_ADO.InsChcMember_LogByChcMemberMode("1", dr["MID"].ToString(), dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["Phone"].ToString(), dr["Ename"].ToString());

                        //寫入Data
                        Ado_Info.ChcMember_ADO.UpdChcMemberDataByMode("1", dr["CategoryID"].ToString(), true, dr["MID"].ToString(),
                            dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["Phone"].ToString(), dr["Ename"].ToString(),
                            dr["GroupClass"].ToString(), dr["Gmail"].ToString(), "");

                        #endregion
                    }
                    else
                    {
                        //2. 若取不到資料就用小組, 姓名取得資料
                        #region

                        drChcMem = null;
                        drChcMem = dtMem.Select("GroupCName='" + dr["GroupCName"].ToString() + "' AND GroupName='" + dr["GroupName"].ToString() + "' AND Ename='" + dr["Ename"].ToString() + "'");
                        if (drChcMem.Count() > 0)
                        {
                            //寫入Log
                            Ado_Info.ChcMember_Log_ADO.InsChcMember_LogByChcMemberMode("2", dr["MID"].ToString(), dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["Phone"].ToString(), dr["Ename"].ToString());

                            //寫入Data
                            Ado_Info.ChcMember_ADO.UpdChcMemberDataByMode("2", dr["CategoryID"].ToString(), true, dr["MID"].ToString(),
                                dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["Phone"].ToString(), dr["Ename"].ToString(),
                                dr["GroupClass"].ToString(), dr["Gmail"].ToString(), "");
                        }
                        else
                        {
                            //3. 若取不到資料就用手機, 姓名取資料
                            #region

                            drChcMem = null;
                            drChcMem = dtMem.Select("Phone='" + dr["Phone"].ToString() + "' AND Ename='" + dr["Ename"].ToString() + "'");
                            if (drChcMem.Count() > 0)
                            {
                                //寫入Log
                                Ado_Info.ChcMember_Log_ADO.InsChcMember_LogByChcMemberMode("3", dr["MID"].ToString(), dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["Phone"].ToString(), dr["Ename"].ToString());

                                //寫入Data
                                Ado_Info.ChcMember_ADO.UpdChcMemberDataByMode("3", dr["CategoryID"].ToString(), true, dr["MID"].ToString(),
                                    dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["Phone"].ToString(), dr["Ename"].ToString(),
                                    dr["GroupClass"].ToString(), dr["Gmail"].ToString(), "");
                            }
                            else
                            {
                                //4. 若取不到資料就用手機, 小組取資料
                                #region

                                drChcMem = null;
                                drChcMem = dtMem.Select("Phone='" + dr["Phone"].ToString() + "' AND GroupCName='" + dr["GroupCName"].ToString() + "' AND GroupName='" + dr["GroupName"].ToString() + "'");
                                if (drChcMem.Count() > 0)
                                {
                                    //寫入Log
                                    Ado_Info.ChcMember_Log_ADO.InsChcMember_LogByChcMemberMode("4", dr["MID"].ToString(), dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["Phone"].ToString(), dr["Ename"].ToString());

                                    //寫入Data
                                    Ado_Info.ChcMember_ADO.UpdChcMemberDataByMode("4", dr["CategoryID"].ToString(), true, dr["MID"].ToString(),
                                        dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["Phone"].ToString(), dr["Ename"].ToString(),
                                        dr["GroupClass"].ToString(), dr["Gmail"].ToString(), "");
                                }
                                else
                                {
                                    //5. 若取不到資料就用姓名取資料
                                    #region

                                    drChcMem = null;
                                    drChcMem = dtMem.Select("Ename='" + dr["Ename"].ToString() + "'");
                                    if (drChcMem.Count() == 1)
                                    {
                                        //寫入Log
                                        Ado_Info.ChcMember_Log_ADO.InsChcMember_LogByChcMemberMode("5", dr["MID"].ToString(), dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["Phone"].ToString(), dr["Ename"].ToString());

                                        //寫入Data
                                        Ado_Info.ChcMember_ADO.UpdChcMemberDataByMode("5", dr["CategoryID"].ToString(), true, dr["MID"].ToString(),
                                            dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["Phone"].ToString(), dr["Ename"].ToString(),
                                            dr["GroupClass"].ToString(), dr["Gmail"].ToString(), "");
                                    }
                                    else
                                    {
                                        //否則就新增資料
                                        #region

                                        //新增資料
                                        Ado_Info.ChcMember_ADO.InsExcelDataByChcMember(dr["CategoryID"].ToString(), dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["GroupClass"].ToString(), dr["Ename"].ToString(), bool.Parse(dr["EStatus"].ToString()), dr["Phone"].ToString(), dr["Gmail"].ToString(), "");

                                        //寫入Log
                                        Ado_Info.ChcMember_Log_ADO.InsChcMemberByChcMember_Log(dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["GroupClass"].ToString(), dr["Ename"].ToString());

                                        #endregion

                                    }

                                    #endregion

                                }

                                #endregion


                            }

                            #endregion

                        }

                        #endregion
                    }

                }

                //更新完畢
                //UPDATE uptyn = 1
                Ado_Info.ChcMemberSub_Temp_ADO.Upduptyn1ByChcMemberSub_Temp(CategoryID);

                if (CategoryID != "C2QT")
                {
                    //更新課程通過狀態
                    Ado_Info.ChcMember_ADO.UpdC1C2_StatusByChcMember();
                    Ado_Info.ChcMember_ADO.UpdC1_StatusByChcMember();
                    Ado_Info.ChcMember_ADO.UpdC2_StatusByChcMember();
                }

                Response.Write("<script>alert('成功匯入');</script>");
            }


        }
    }
}