using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO
{
    /// <summary>
    /// 會友簽到記錄
    /// </summary>
    public class SubSignUpDateADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        /// <summary>
        /// 新增簽到表
        /// </summary>
        public void InsSubSignUpDate(int SUID, string CategoryID)
        {

            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"INSERT INTO
                                           SubSignUpDate(SUID, CategoryID)
                                           VALUES(@SUID, @CategoryID)";

                OleDbCommand com = new OleDbCommand(sql, con);
                com.Parameters.AddWithValue("@SUID", SUID);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public void UpdDateBySubSignUpDate(int SUID, string CategoryID, string SignDate)
        {
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"UPDATE SubSignUpDate
                                            SET SignDate = @SignDate
                                            WHERE SUID = @SUID
                                            AND CategoryID = @CategoryID";

                OleDbCommand com = new OleDbCommand(sql, con);
                com.Parameters.AddWithValue("@SignDate", SignDate);
                com.Parameters.AddWithValue("@SUID", SUID);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }


    }
}
