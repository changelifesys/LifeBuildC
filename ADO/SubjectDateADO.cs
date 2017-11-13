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
    /// 上課日期
    /// </summary>
    public class SubjectDateADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        public void InsSubjectDate(int SID, string CategoryID,  string SDate, string SubTime)
        {
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"INSERT INTO
            //                               SubjectDate(SID, CategoryID, SDate, SubTime)
            //                               VALUES(@SID, @CategoryID, @SDate, @SubTime)";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@SID", SID);
            //    com.Parameters.AddWithValue("@CategoryID", CategoryID);
            //    com.Parameters.AddWithValue("@SDate", SDate);
            //    com.Parameters.AddWithValue("@SubTime", SubTime);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           SubjectDate(SID, CategoryID, SDate, SubTime)
                                           VALUES(@SID, @CategoryID, @SDate, @SubTime)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@SID", SID);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);
                com.Parameters.AddWithValue("@SDate", SDate);
                com.Parameters.AddWithValue("@SubTime", SubTime);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdSubjectDate(string SDate, string SubTime, int SID, string CategoryID)
        {
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"UPDATE SubjectDate
            //                                SET SDate = @SDate,
            //                                        SubTime = @SubTime
            //                                WHERE SID = @SID
            //                                AND CategoryID = @CategoryID
            //                              ";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@SDate", SDate);
            //    com.Parameters.AddWithValue("@SubTime", SubTime);
            //    com.Parameters.AddWithValue("@SID", SID);
            //    com.Parameters.AddWithValue("@CategoryID", CategoryID);
                
            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE SubjectDate
                                            SET SDate = @SDate,
                                                    SubTime = @SubTime
                                            WHERE SID = @SID
                                            AND CategoryID = @CategoryID
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@SDate", SDate);
                com.Parameters.AddWithValue("@SubTime", SubTime);
                com.Parameters.AddWithValue("@SID", SID);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DelSubjectDate(int SID, string CategoryID)
        {
            #region Acces
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"DELETE FROM SubjectDate
            //                                WHERE SID = @SID
            //                                AND CategoryID = @CategoryID
            //                              ";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@SID", SID);
            //    com.Parameters.AddWithValue("@CategoryID", CategoryID);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"DELETE FROM SubjectDate
                                            WHERE SID = @SID
                                            AND CategoryID = @CategoryID
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@SID", SID);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// 查詢預新增的簽到表
        /// </summary>
        public DataTable QueryCategoryIDBySubjectDate(int MID)
        {
            DataTable dt = new DataTable();
            #region Aceess
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM SubjectDate
            //                                WHERE SID IN
            //                                (
            //                                SELECT TOP 1 SID FROM SubSignInfo
            //                                WHERE MID = @MID
            //                                ORDER BY SUID DESC
            //                                )";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@MID", MID);
            //    sda.Fill(dt);
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM SubjectDate
                                            WHERE SID IN
                                            (
                                            SELECT TOP 1 SID FROM SubSignInfo
                                            WHERE MID = @MID
                                            ORDER BY SUID DESC
                                            )";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@MID", MID);
                sda.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// 查詢CategoryID用
        /// </summary>
        public DataTable QueryCategoryIDBySubjectDate(int SID, string SDate)
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT TOP 1 * FROM SubjectDate
            //                                WHERE SID = @SID
            //                                AND SDate = @SDate";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@SID", SID);
            //    sda.SelectCommand.Parameters.AddWithValue("@SDate", SDate);
            //    sda.Fill(dt);
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT TOP 1 * FROM SubjectDate
                                            WHERE SID = @SID
                                            AND SDate = @SDate";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@SID", SID);
                sda.SelectCommand.Parameters.AddWithValue("@SDate", SDate);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QuerySIDBySubjectDate(string CategoryID, string SDate)
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT TOP 1 * FROM SubjectDate
            //                                WHERE LEFT(CategoryID, 2) = @CategoryID
            //                                AND SDate = @SDate";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
            //    sda.SelectCommand.Parameters.AddWithValue("@SDate", SDate);
            //    sda.Fill(dt);
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT TOP 1 * FROM SubjectDate
                                            WHERE LEFT(CategoryID, 2) = @CategoryID
                                            AND SDate = @SDate";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.SelectCommand.Parameters.AddWithValue("@SDate", SDate);
                sda.Fill(dt);
            }
            return dt;
        }

        public DataTable QueryAllBySubjectDate(string CategoryID)
        {

            DataTable dt = new DataTable();
           #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    //string sql = @"SELECT SubjectInfo.SubStrDate, SubjectInfo.SubEndDate,
            //    //                                          SubjectDate.*
            //    //                            FROM SubjectInfo INNER JOIN SubjectDate ON SubjectInfo.SID = SubjectDate.SID
            //    //                            WHERE SubjectDate.SDate >= @SDate
            //    //                            AND LEFT(SubjectDate.CategoryID, 2) = @CategoryID
            //    //                            ORDER BY SubjectDate.SID DESC, SubjectDate.SDate";

            //    string sql = @"SELECT SubjectInfo.SubStrDate, SubjectInfo.SubEndDate,
            //                                              SubjectDate.*
            //                                FROM SubjectInfo INNER JOIN SubjectDate ON SubjectInfo.SID = SubjectDate.SID
            //                                WHERE LEFT(SubjectDate.CategoryID, 2) = @CategoryID
            //                                ORDER BY SubjectDate.SID DESC, SubjectDate.SDate";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
            //    sda.Fill(dt);
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                //string sql = @"SELECT SubjectInfo.SubStrDate, SubjectInfo.SubEndDate,
                //                                          SubjectDate.*
                //                            FROM SubjectInfo INNER JOIN SubjectDate ON SubjectInfo.SID = SubjectDate.SID
                //                            WHERE SubjectDate.SDate >= @SDate
                //                            AND LEFT(SubjectDate.CategoryID, 2) = @CategoryID
                //                            ORDER BY SubjectDate.SID DESC, SubjectDate.SDate";

                string sql = @"SELECT SubjectInfo.SubStrDate, SubjectInfo.SubEndDate,
                                                          SubjectDate.*
                                            FROM SubjectInfo INNER JOIN SubjectDate ON SubjectInfo.SID = SubjectDate.SID
                                            WHERE LEFT(SubjectDate.CategoryID, 2) = @CategoryID
                                            ORDER BY SubjectDate.SID DESC, SubjectDate.SDate";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.Fill(dt);
            }
            return dt;
        }

    }
}
