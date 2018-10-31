using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO
{
    public class AnswerItemADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;
        public string DbSchema = ConfigurationManager.AppSettings.Get("DbSchema");

        public void InsAnswerItem(string ExamCategory, string ItemName)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO " +
                                          DbSchema + "AnswerItem(ExamCategory, ItemName) " +
                                          "VALUES(@ExamCategory, @ItemName)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                com.Parameters.AddWithValue("@ItemName", ItemName);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DelAnswerItem(string ExamCategory, string ItemName)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"DELETE FROM " +
                                         DbSchema + "AnswerItem " +
                                         " WHERE ExamCategory = @ExamCategory" +
                                         " AND ItemName = @ItemName";

                SqlCommand com = new SqlCommand(sql, con);
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

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " +
                                         DbSchema + "AnswerItem " +
                                         " WHERE ExamCategory = @ExamCategory" +
                                         " ORDER BY ID";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                sda.Fill(dt);
            }

            return dt;
        }


    }
}
