using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO
{
    public class AnswerItemADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        public void InsAnswerItem(string ExamCategory, string ItemName)
        {
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"INSERT INTO
                                           AnswerItem(ExamCategory, ItemName)
                                           VALUES(@ExamCategory, @ItemName)";

                OleDbCommand com = new OleDbCommand(sql, con);
                com.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                com.Parameters.AddWithValue("@ItemName", ItemName);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DelAnswerItem(string ExamCategory, string ItemName)
        {
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"DELETE FROM AnswerItem
                                            WHERE ExamCategory = @ExamCategory
                                            AND ItemName = @ItemName";

                OleDbCommand com = new OleDbCommand(sql, con);
                com.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                com.Parameters.AddWithValue("@ItemName", ItemName);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public DataTable QueryAnswerItem(string ExamCategory)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"SELECT * FROM AnswerItem
                                            WHERE ExamCategory = @ExamCategory
                                            ORDER BY ID";
                OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                sda.Fill(dt);
            }

            return dt;
        }


    }
}
