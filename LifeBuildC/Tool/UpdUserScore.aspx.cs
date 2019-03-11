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
    public partial class UpdUserScore : System.Web.UI.Page
    {
        AdoInfo Ado_Info = new AdoInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (true && Request.QueryString["CategoryID"] != null &&

                (Request.QueryString["CategoryID"].ToString() == "C1_Score" ||
                 Request.QueryString["CategoryID"].ToString() == "C212_Score" ||
                 Request.QueryString["CategoryID"].ToString() == "C234_Score"
                 )

               )
            {
                string CategoryID = Request.QueryString["CategoryID"].ToString();

                DataTable dtUserScore = Ado_Info.UserScore_ADO.QueryUserScore();
                DataTable dtMem = Ado_Info.ChcMember_ADO.QueryAllByChcMember();
                foreach (DataRow dr in dtUserScore.Rows)
                {
                    //BB202.芬芸牧區-雅雪小組
                    string[] arrg = dr["Egroup"].ToString().Split('.');
                    string GroupCName = arrg[1].Split('-')[0];
                    string GroupName = arrg[1].Split('-')[1];
                    DataRow[] drChcMem = null;

                    //Mode 2. 用小組, 姓名取得資料
                    #region

                    drChcMem = dtMem.Select("GroupCName='" + GroupCName + "' AND GroupName='" + GroupName + "' AND Ename='" + dr["Ename"].ToString() + "'");
                    if (drChcMem.Count() > 0 && ChckEScore(CategoryID, dr, drChcMem))
                    {
                        //寫入Log
                        Ado_Info.ChcMember_Log_ADO.InsChcMember_LogByChcMemberMode("2", drChcMem[0]["MID"].ToString(), GroupCName, GroupName, dr["Emobile"].ToString(), dr["Ename"].ToString());

                        //寫入Data
                        Ado_Info.ChcMember_ADO.UpdChcMemberDataByMode("2", CategoryID, true, drChcMem[0]["MID"].ToString(),
                            GroupCName, GroupName, dr["Emobile"].ToString(), dr["Ename"].ToString(),
                            drChcMem[0]["GroupClass"].ToString(), drChcMem[0]["Gmail"].ToString(), dr["EScore"].ToString());

                    }
                    else
                    {
                        //Mode 3. 若取不到資料就用手機, 姓名取資料
                        #region

                        drChcMem = null;
                        drChcMem = dtMem.Select("Phone='" + dr["Emobile"].ToString() + "' AND Ename='" + dr["Ename"].ToString() + "'");
                        if (drChcMem.Count() > 0 && ChckEScore(CategoryID, dr, drChcMem))
                        {
                            //寫入Log
                            Ado_Info.ChcMember_Log_ADO.InsChcMember_LogByChcMemberMode("3", drChcMem[0]["MID"].ToString(), GroupCName, GroupName, dr["Emobile"].ToString(), dr["Ename"].ToString());

                            //寫入Data
                            Ado_Info.ChcMember_ADO.UpdChcMemberDataByMode("3", CategoryID, true, drChcMem[0]["MID"].ToString(),
                                GroupCName, GroupName, dr["Emobile"].ToString(), dr["Ename"].ToString(),
                                drChcMem[0]["GroupClass"].ToString(), drChcMem[0]["Gmail"].ToString(), dr["EScore"].ToString());
                        }
                        else
                        {
                            //Mode 4. 若取不到資料就用手機, 小組取資料
                            #region

                            drChcMem = null;
                            drChcMem = dtMem.Select("Phone='" + dr["Emobile"].ToString() + "' AND GroupCName='" + GroupCName + "' AND GroupName='" + GroupName + "'");
                            if (drChcMem.Count() > 0 && ChckEScore(CategoryID, dr, drChcMem))
                            {
                                //寫入Log
                                Ado_Info.ChcMember_Log_ADO.InsChcMember_LogByChcMemberMode("4", drChcMem[0]["MID"].ToString(), GroupCName, GroupName, dr["Emobile"].ToString(), dr["Ename"].ToString());

                                //寫入Data
                                Ado_Info.ChcMember_ADO.UpdChcMemberDataByMode("4", CategoryID, true, drChcMem[0]["MID"].ToString(),
                                    GroupCName, GroupName, dr["Emobile"].ToString(), dr["Ename"].ToString(),
                                    drChcMem[0]["GroupClass"].ToString(), drChcMem[0]["Gmail"].ToString(), dr["EScore"].ToString());

                            }
                            else
                            {
                                //Mode 5. 若取不到資料就用姓名取資料
                                #region

                                drChcMem = null;
                                drChcMem = dtMem.Select("Ename='" + dr["Ename"].ToString() + "'");
                                if (drChcMem.Count() == 1)
                                {
                                    if (ChckEScore(CategoryID, dr, drChcMem))
                                    {
                                        //寫入Log
                                        Ado_Info.ChcMember_Log_ADO.InsChcMember_LogByChcMemberMode("5", drChcMem[0]["MID"].ToString(), GroupCName, GroupName, dr["Emobile"].ToString(), dr["Ename"].ToString());

                                        //寫入Data
                                        Ado_Info.ChcMember_ADO.UpdChcMemberDataByMode("5", CategoryID, true, drChcMem[0]["MID"].ToString(),
                                            GroupCName, GroupName, dr["Emobile"].ToString(), dr["Ename"].ToString(),
                                            drChcMem[0]["GroupClass"].ToString(), drChcMem[0]["Gmail"].ToString(), dr["EScore"].ToString());
                                    }

                                }
                                else
                                {
                                    //否則就新增資料

                                    //新增資料
                                    Ado_Info.ChcMember_ADO.InsExcelDataByChcMember(CategoryID, GroupCName, GroupName, "", dr["Ename"].ToString(), true, dr["Emobile"].ToString(), "", dr["EScore"].ToString());

                                    //寫入Log
                                    Ado_Info.ChcMember_Log_ADO.InsChcMemberByChcMember_Log(GroupCName, GroupName, "", dr["Ename"].ToString());

                                }

                                #endregion
                            }

                            #endregion

                        }

                        #endregion


                    }

                    #endregion
                }

                //更新完畢
                //UPDATE uptyn = 1
                Ado_Info.UserScore_ADO.Upduptyn1ByUserScore(CategoryID);

                //更新課程通過狀態
                Ado_Info.ChcMember_ADO.UpdC1C2_StatusByChcMember();
                Ado_Info.ChcMember_ADO.UpdC1_StatusByChcMember();
                Ado_Info.ChcMember_ADO.UpdC2_StatusByChcMember();

                Response.Write("<script>alert('成功匯入');</script>");

            }



        }

        private bool ChckEScore(string CategoryID, DataRow dr, DataRow[] drChcMem)
        {
            switch (CategoryID)
            {
                case "C1_Score":

                    if (int.Parse(dr["EScore"].ToString()) > int.Parse(drChcMem[0]["C1_Score"].ToString()))
                    {
                        return true;
                    }

                    break;

                case "C212_Score":

                    if (int.Parse(dr["EScore"].ToString()) > int.Parse(drChcMem[0]["C212_Score"].ToString()))
                    {
                        return true;
                    }

                    break;

                case "C234_Score":

                    if (int.Parse(dr["EScore"].ToString()) > int.Parse(drChcMem[0]["C234_Score"].ToString()))
                    {
                        return true;
                    }

                    break;
            }

            return false;

        }

    }
}