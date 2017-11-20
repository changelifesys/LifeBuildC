using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
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
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"INSERT INTO
            //                               SubSignUpDate(SUID, CategoryID)
            //                               VALUES(@SUID, @CategoryID)";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@SUID", SUID);
            //    com.Parameters.AddWithValue("@CategoryID", CategoryID);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           SubSignUpDate(SUID, CategoryID)
                                           VALUES(@SUID, @CategoryID)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@SUID", SUID);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdDateBySubSignUpDate(int SUID, string CategoryID, string SignDate)
        {
           #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"UPDATE SubSignUpDate
            //                                SET SignDate = @SignDate
            //                                WHERE SUID = @SUID
            //                                AND CategoryID = @CategoryID";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@SignDate", SignDate);
            //    com.Parameters.AddWithValue("@SUID", SUID);
            //    com.Parameters.AddWithValue("@CategoryID", CategoryID);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE SubSignUpDate
                                            SET SignDate = @SignDate
                                            WHERE SUID = @SUID
                                            AND CategoryID = @CategoryID";

                SqlCommand com = new SqlCommand(sql, con);
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
