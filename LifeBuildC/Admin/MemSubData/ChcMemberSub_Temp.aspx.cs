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
        ChcMemberSub_TempADO memSub = new ChcMemberSub_TempADO();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            #region DataTable

            DataTable table = new DataTable();
            DataColumn column;
            DataRow row;

            //google 時間戳記
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "CreateTime";
            table.Columns.Add(column);

            //上課類別
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "CategoryID";
            table.Columns.Add(column);

            //姓名
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Ename";
            table.Columns.Add(column);

            //所屬牧區/小組
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Egroup";
            table.Columns.Add(column);

            //手機
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Phone";
            table.Columns.Add(column);

            //報名&簽到
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "EStatus";
            table.Columns.Add(column);

            //上課日期
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "SubDate";
            table.Columns.Add(column);



            #endregion

            XSSFWorkbook workbook = new XSSFWorkbook(FileUpload1.FileContent);

            //讀取Sheet1 工作表
            var sheet = workbook.GetSheetAt(0);

            for (int r = 1; r <= sheet.LastRowNum; r++)
            {
                row = table.NewRow();

                if (sheet.GetRow(r) != null)
                {
                    int ri = 0;
                    foreach (var c in sheet.GetRow(r).Cells)
                    {
                        if (c.CellType == CellType.Numeric)
                        {
                            DateTime _CreateTime = c.DateCellValue;
                            row[ri] = _CreateTime.ToString("yyyy/MM/dd HH:mm:ss");
                        }

                        if (c.CellType == CellType.String && c.StringCellValue.ToString() != "")
                        {
                            row[ri] = c.StringCellValue.ToString().Replace(" ", "");
                        }

                        ri++;
                    }

                }

                table.Rows.Add(row);
            }

            foreach (DataRow dr in table.Rows)
            {
                string CategoryID = dr["CategoryID"].ToString();
                string Ename = dr["Ename"].ToString();
                string Egroup = dr["Egroup"].ToString();
                string Phone = dr["Phone"].ToString();
                string EStatus = dr["EStatus"].ToString();
                string SubDate = dr["SubDate"].ToString();
                string Memo = dr["Memo"].ToString();

                memSub.InsChcMemberSub_Temp(CategoryID, Ename, Egroup, Phone, EStatus, SubDate, Memo);
            }

        }
    }
}