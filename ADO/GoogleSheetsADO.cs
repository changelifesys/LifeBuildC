using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.CData.GoogleSheets;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO
{
    public class GoogleSheetsADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["GoogleSheetsConnectionString"].ConnectionString;

        public void InsGoogleSheets(string SheetsName, List<string> SheetsColumn, List<string> SheetsValues)
        {
            using (GoogleSheetsConnection con = new GoogleSheetsConnection(condb))
            {
                string sql = "INSERT INTO " + SheetsName + "(";

                foreach (string s in SheetsColumn)
                {
                    sql += s + ",";
                }

                sql = sql.Substring(0, sql.Length - 1);
                sql += ")";

                sql += " VALUES(";

                foreach (string s in SheetsValues)
                {
                    sql += "'" + s + "',";
                }

                sql = sql.Substring(0, sql.Length - 1);
                sql += ")";

                //string sql = @"INSERT INTO
                //                           工作表1(標題1, 標題2)
                //                           VALUES('AAAA123', 'BBBB456')";

                GoogleSheetsCommand com = new GoogleSheetsCommand(sql, con);
                //com.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                //com.Parameters.AddWithValue("@ItemName", ItemName);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
