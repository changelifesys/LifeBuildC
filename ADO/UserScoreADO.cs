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
    public class UserScoreADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        public void InsUserScore(string ExamCategory, string Egroup, string Ename, string Emobile, string EScore)
        {

            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"INSERT INTO
            //                               UserScore(ExamCategory, Egroup, Ename, Emobile, EScore, CreateDate)
            //                               VALUES(@ExamCategory, @Egroup, @Ename, @Emobile, @EScore, @CreateDate)";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@ExamCategory", ExamCategory);
            //    com.Parameters.AddWithValue("@Egroup", Egroup);
            //    com.Parameters.AddWithValue("@Ename", Ename);
            //    com.Parameters.AddWithValue("@Emobile", Emobile);
            //    com.Parameters.AddWithValue("@EScore", EScore);
            //    com.Parameters.AddWithValue("@CreateDate", CreateDate);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           UserScore(ExamCategory, Egroup, Ename, Emobile, EScore, CreateDate)
                                           VALUES(@ExamCategory, @Egroup, @Ename, @Emobile, @EScore, GETDATE())";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                com.Parameters.AddWithValue("@Egroup", Egroup);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Emobile", Emobile);
                com.Parameters.AddWithValue("@EScore", EScore);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DelUserScore(string ExamCategory, string Egroup, string Ename, string CreateDate)
        {
            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"DELETE FROM UserScore
            //                                WHERE ExamCategory = @ExamCategory
            //                                AND Egroup = @Egroup
            //                                AND Ename = @Ename
            //                                AND (CreateDate LIKE @CreateDate + '%')";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@ExamCategory", ExamCategory);
            //    com.Parameters.AddWithValue("@Egroup", Egroup);
            //    com.Parameters.AddWithValue("@Ename", Ename);
            //    com.Parameters.AddWithValue("@CreateDate", CreateDate);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"DELETE FROM UserScore
                                            WHERE ExamCategory = @ExamCategory
                                            AND Egroup = @Egroup
                                            AND Ename = @Ename
                                            AND CONVERT(varchar(100), CreateDate, 111) = @CreateDate";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                com.Parameters.AddWithValue("@Egroup", Egroup);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@CreateDate", CreateDate);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DelUserScoreByUSID(string USID)
        {
           #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"DELETE FROM UserScore
            //                                WHERE USID = @USID";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@USID", USID);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"DELETE FROM UserScore
                                            WHERE USID = @USID";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@USID", USID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public DataTable QueryUserScore(string ExamCategory, string Egroup, string Ename, string CreateDate)
        {

            DataTable dt = new DataTable();

            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM UserScore
            //                                WHERE ExamCategory = @ExamCategory
            //                                AND Egroup = @Egroup
            //                                AND Ename = @Ename
            //                                AND (CreateDate LIKE @CreateDate + '%')";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
            //    sda.SelectCommand.Parameters.AddWithValue("@Egroup", Egroup);
            //    sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
            //    sda.SelectCommand.Parameters.AddWithValue("@CreateDate", CreateDate);
            //    sda.Fill(dt);
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM UserScore
                                            WHERE ExamCategory = @ExamCategory
                                            AND Egroup = @Egroup
                                            AND Ename = @Ename
                                            AND CONVERT(varchar(100), CreateDate, 111) = @CreateDate";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                sda.SelectCommand.Parameters.AddWithValue("@Egroup", Egroup);
                sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
                sda.SelectCommand.Parameters.AddWithValue("@CreateDate", CreateDate);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryUserScoreNoneExamCategory(string Egroup, string Ename, string CreateDate)
        {
            DataTable dt = new DataTable();

            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM UserScore
            //                                WHERE Egroup = @Egroup
            //                                AND Ename = @Ename
            //                                AND (CreateDate LIKE @CreateDate + '%')";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@Egroup", Egroup);
            //    sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
            //    sda.SelectCommand.Parameters.AddWithValue("@CreateDate", CreateDate);
            //    sda.Fill(dt);
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM UserScore
                                            WHERE Egroup = @Egroup
                                            AND Ename = @Ename
                                            AND CONVERT(varchar(100), CreateDate, 111) = @CreateDate";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@Egroup", Egroup);
                sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
                sda.SelectCommand.Parameters.AddWithValue("@CreateDate", CreateDate);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryAllUserScore()
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM UserScore
            //                                ORDER BY CreateDate, ExamCategory, Egroup DESC";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.Fill(dt);
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM UserScore
                                            ORDER BY CreateDate, ExamCategory, Egroup DESC";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }
            return dt;
        }

        public DataTable Getexamdate()
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT Left(CreateDate, 10) AS CreateDate FROM UserScore
            //                                GROUP BY Left(CreateDate, 10)
            //                                ORDER BY Left(CreateDate, 10) DESC";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.Fill(dt);
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT Left(CreateDate, 10) AS CreateDate FROM UserScore
                                            GROUP BY Left(CreateDate, 10)
                                            ORDER BY Left(CreateDate, 10) DESC";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }
            return dt;
        }

        public DataTable ChkUserScoreByUSID(string USID)
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM UserScore
            //                                WHERE USID = @USID";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@USID", USID);
            //    sda.Fill(dt);
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM UserScore
                                            WHERE USID = @USID";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@USID", USID);
                sda.Fill(dt);
            }

            

            return dt;
        }

    }
}
