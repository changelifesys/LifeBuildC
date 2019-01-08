using ADO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.MemSubData
{
    public partial class ChcMemberSub_Temp : System.Web.UI.Page
    {
        ChcMember_LogADO chcmemlog = new ChcMember_LogADO();
        ChcMemberSub_TempADO memSub = new ChcMemberSub_TempADO();
        ChcMemberADO chcmember = new ChcMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            #region DataTable
            DataTable table = new DataTable();
            DataColumn column;
            DataRow row;

            //0(A)
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "課程編號";
            table.Columns.Add(column);

            //1(B)
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "是否簽到";
            table.Columns.Add(column);

            //2(C)
            column = new DataColumn();
            column.DataType = Type.GetType("System.DateTime");
            column.ColumnName = "報名or簽到時間";
            table.Columns.Add(column);

            //3(D)
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "姓名";
            table.Columns.Add(column);

            //4(E)
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "上課註記";
            table.Columns.Add(column);

            //5(F)
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "所屬牧區/小組：家庭弟兄";
            table.Columns.Add(column);

            //6(G)
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "所屬牧區/小組：家庭姊妹";
            table.Columns.Add(column);

            //6(H)
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "所屬牧區/小組：社青";
            table.Columns.Add(column);

            //7(I)
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "所屬牧區/小組：學青";
            table.Columns.Add(column);

            #endregion

            XSSFWorkbook workbook = new XSSFWorkbook(FileUpload1.FileContent);

            //讀取Sheet1 工作表
            var sheet = workbook.GetSheetAt(0);

            for (int r = 2; r <= sheet.LastRowNum; r++)
            {

                if (sheet.GetRow(r) != null && sheet.GetRow(r).GetCell(0) != null)
                {
                    row = table.NewRow();

                    for (int c = 0; c < 9; c++)
                    {
                        if (sheet.GetRow(r).GetCell(c) != null)
                        {
                            row[c] = sheet.GetRow(r).GetCell(c).ToString();
                        }
                        else
                        {
                            row[c] = "";
                        }

                    }

                    table.Rows.Add(row);
                }
            }

            GridView1.DataSource = table;
            GridView1.DataBind();
            Button2.Visible = true;

            Response.Write("<script>alert('要匯入的資料已顯示在網頁上，請確認資料是否正確，再點選「確認匯入」');</script>");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string CategoryID = GridView1.Rows[i].Cells[0].Text; //課程編號

                //0: 報名 1:簽到
                bool EStatus = true;
                if (GridView1.Rows[i].Cells[1].Text == "0")
                {
                    EStatus = false;
                }

                DateTime SubDate = DateTime.Parse(GridView1.Rows[i].Cells[2].Text); //報名or簽到時間
                string Ename = GridView1.Rows[i].Cells[3].Text; //姓名

                string GroupCName = string.Empty;
                string GroupName = string.Empty;
                string GroupClass = string.Empty;
                if (GridView1.Rows[i].Cells[5].Text.Replace(" ", "").Replace("&nbsp;", "") != "")
                {
                    //小組
                    string[] arrg = GridView1.Rows[i].Cells[5].Text.Split('.');
                    GroupCName = arrg[1].Split('-')[0];
                    GroupName = arrg[1].Split('-')[1];
                    GroupClass = "家庭組弟兄";
                }
                else if (GridView1.Rows[i].Cells[6].Text.Replace(" ", "").Replace("&nbsp;", "") != "")
                {
                    //小組
                    string[] arrg = GridView1.Rows[i].Cells[6].Text.Split('.');
                    GroupCName = arrg[1].Split('-')[0];
                    GroupName = arrg[1].Split('-')[1];
                    GroupClass = "家庭組姊妹";
                }
                else if (GridView1.Rows[i].Cells[7].Text.Replace(" ", "").Replace("&nbsp;", "") != "")
                {
                    //小組
                    string[] arrg = GridView1.Rows[i].Cells[7].Text.Split('.');
                    GroupCName = arrg[1].Split('-')[0];
                    GroupName = arrg[1].Split('-')[1];
                    GroupClass = "社青";
                }
                else if (GridView1.Rows[i].Cells[8].Text.Replace(" ", "").Replace("&nbsp;", "") != "")
                {
                    //小組
                    string[] arrg = GridView1.Rows[i].Cells[8].Text.Split('.');
                    GroupCName = arrg[1].Split('-')[0];
                    GroupName = arrg[1].Split('-')[1];
                    GroupClass = "學生";
                }


                string Phone = "";
                string Gmail = "";
                string Church = "";

                memSub.InsExcelDataByChcMemberSub_Temp(CategoryID, GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, Church, EStatus, SubDate);
            }

            DataTable dtMemTemp = memSub.QueryuptynByChcMemberSub_Temp();
            DataTable dtMem = chcmember.QueryAllByChcMember();
            foreach (DataRow dr in dtMemTemp.Rows)
            {
                DataRow[] drChcMem = dtMem.Select("GroupName='" + dr["GroupName"].ToString() + "' AND Ename='" + dr["Ename"].ToString() + "'");
                if (drChcMem.Count() > 0)
                {
                    //寫入Log
                    chcmemlog.InsChcMemberByChcMember_Log(dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["GroupClass"].ToString(), dr["Ename"].ToString());

                    //小組&姓名若填寫正確
                    //更新上課資料
                    chcmember.UpdPassDataByChcMember(dr["CategoryID"].ToString(), dr["GroupName"].ToString(), dr["Ename"].ToString(), bool.Parse(dr["EStatus"].ToString()));
                }
                else
                { //否則就新增資料

                    //新增資料
                    chcmember.InsExcelDataByChcMember(dr["CategoryID"].ToString(), dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["GroupClass"].ToString(), dr["Ename"].ToString(), bool.Parse(dr["EStatus"].ToString()), dr["Phone"].ToString(), dr["Gmail"].ToString());

                    //寫入Log
                    chcmemlog.InsChcMemberByChcMember_Log(dr["GroupCName"].ToString(), dr["GroupName"].ToString(), dr["GroupClass"].ToString(), dr["Ename"].ToString());
                }
            }

            //更新完畢
            //UPDATE uptyn = 1
            memSub.Upduptyn1ByChcMemberSub_Temp();

            //更新課程通過狀態
            chcmember.UpdC1C2_StatusByChcMember();
            chcmember.UpdC1_StatusByChcMember();
            chcmember.UpdC2_StatusByChcMember();
            Response.Write("<script>alert('成功匯入');</script>");


            

        }



    }
}