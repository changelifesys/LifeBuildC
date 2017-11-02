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
    public class SystemSetADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        public void UpdUserScore(string ScoreGoogleLink, string GoogleKey, string ExamCategory, string IsEnable)
        {
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"UPDATE SystemSet
                                            SET ScoreGoogleLink = @ScoreGoogleLink,
                                                    GoogleKey = @GoogleKey,
                                                    IsEnable = @IsEnable
                                            WHERE ExamCategory = @ExamCategory";

                OleDbCommand com = new OleDbCommand(sql, con);
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
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"SELECT * FROM SystemSet";
                OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
                
                sda.Fill(dt);
            }

            return dt;
        }

    }
}
