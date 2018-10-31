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
    public class SystemSetADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;
        public string DbSchema = ConfigurationManager.AppSettings.Get("DbSchema");

        public void UpdUserScore(string ScoreGoogleLink, string GoogleKey, string ExamCategory, string IsEnable)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE " + DbSchema + @"SystemSet
                                            SET ScoreGoogleLink = @ScoreGoogleLink,
                                                    GoogleKey = @GoogleKey,
                                                    IsEnable = @IsEnable
                                            WHERE ExamCategory = @ExamCategory";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@ScoreGoogleLink", ScoreGoogleLink);
                com.Parameters.AddWithValue("@GoogleKey", GoogleKey);
                com.Parameters.AddWithValue("@IsEnable", IsEnable);
                com.Parameters.AddWithValue("@ExamCategory", ExamCategory);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public DataTable QuerySystemSet()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"SystemSet";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);

                sda.Fill(dt);
            }

            return dt;
        }

    }
}
