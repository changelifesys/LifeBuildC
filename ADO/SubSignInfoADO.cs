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
    /// <summary>
    /// 報名資訊
    /// </summary>
    public class SubSignInfoADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        /// <summary>
        /// 新增報名資訊
        /// </summary>
        public void InsSubSignInfo(int SID, int MID, string SUDate)
        {

            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"INSERT INTO
                                           SubSignInfo(SID, MID, SUDate)
                                           VALUES(@SID, @MID, @SUDate)";

                OleDbCommand com = new OleDbCommand(sql, con);
                com.Parameters.AddWithValue("@SID", SID);
                com.Parameters.AddWithValue("@MID", MID);
                com.Parameters.AddWithValue("@SUDate", SUDate);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        /// <summary>
        /// 查詢上課編號SUID
        /// </summary>
        public int QuerySUIDBySubSignInfo(int MID)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"SELECT TOP 1 SUID FROM SubSignInfo
                                            WHERE MID = @MID
                                            ORDER BY SUID DESC";

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@MID", MID);
                sda.Fill(dt);
            }

            return int.Parse(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// 查詢是否為第一次上課
        /// </summary>
        public bool QueryFirstBySubSignInfo(int MID, string CategoryID)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"SELECT *
                                            FROM SubSignInfo LEFT JOIN SubSignUpDate ON SubSignInfo.SUID = SubSignUpDate.SUID
                                            WHERE SubSignInfo.MID = @MID
                                            AND LEFT(CategoryID, 2) = @CategoryID
                                            AND SignDate IS NOT NULL";

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@MID", MID);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.Fill(dt);
            }

            if (dt.Rows.Count > 0)
                return false; //不是第一次上課
            else
                return true; //第一次上課

        }

        /// <summary>
        /// 查詢報到課程
        /// </summary>
        public DataTable QuerySubSignInfo(int MID, int SID)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"SELECT TOP 1 * FROM SubSignInfo
                                            WHERE MID = @MID
                                            AND SID = @SID
                                            ORDER BY SUDate DESC";

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@MID", MID);
                sda.SelectCommand.Parameters.AddWithValue("@SID", SID);

                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QuerySIDBySubSignInfo(int SID)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"SELECT TOP 1 * FROM SubSignInfo
                                            WHERE SID = @SID
                                          ";

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@SID", SID);

                sda.Fill(dt);
            }

            return dt;
        }

    }
}
