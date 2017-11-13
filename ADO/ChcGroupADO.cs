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
    /// <summary>
    /// 小組資訊
    /// </summary>
    public class ChcGroupADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        public void InsChcGroup(int GSort, string GroupName, string GroupClass)
        {
            #region Access
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"INSERT INTO
                                           ChcGroup(GSort, GroupName, GroupClass)
                                           VALUES(@GSort, @GroupName, @GroupClass)";

                OleDbCommand com = new OleDbCommand(sql, con);
                com.Parameters.AddWithValue("@GSort", GSort);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           ChcGroup(GSort, GroupName, GroupClass)
                                           VALUES(@GSort, @GroupName, @GroupClass)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GSort", GSort);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdGSortByChcGroup(int GSort, string ID)
        {
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"UPDATE ChcGroup
            //                                SET GSort = @GSort
            //                                WHERE ID = @ID";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@GSort", GSort);
            //    com.Parameters.AddWithValue("@ID", ID);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE ChcGroup
                                            SET GSort = @GSort
                                            WHERE ID = @ID";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GSort", GSort);
                com.Parameters.AddWithValue("@ID", ID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }







        public DataTable QueryChcGroup()
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM ChcGroup
            //                                ORDER BY GSort";
            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    //sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
            //    sda.Fill(dt);
            //}
            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcGroup
                                            ORDER BY GSort";
                SqlDataAdapter sda = SqlDataAdapter(sql, con);
                //sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                sda.Fill(dt);
            }




            return dt;
        }

        public DataTable QueryGroupByChcGroup()
        {
            DataTable dt = new DataTable();

            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT GroupClass, MAX(GSort) FROM ChcGroup
            //                                WHERE GroupClass <> ''
            //                                GROUP BY GroupClass
            //                                ORDER BY MAX(GSort)
            //                                ";
            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    //sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
            //    sda.Fill(dt);
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT GroupClass, MAX(GSort) FROM ChcGroup
                                           WHERE GroupClass <> ''
                                           GROUP BY GroupClass
                                           ORDER BY MAX(GSort)
                                          ";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryGroupNameByChcGroup(string GroupClass)
        {
            DataTable dt = new DataTable();

            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM ChcGroup
            //                                WHERE GroupClass = @GroupClass
            //                                ORDER BY GSort";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@GroupClass", GroupClass);
            //    sda.Fill(dt);
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcGroup
                                           WHERE GroupClass = @GroupClass
                                           ORDER BY GSort";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupClass", GroupClass);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryGroupClassByChcGroup(string GroupName)
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT TOP 1 * FROM ChcGroup
            //                                WHERE GroupName LIKE '%'+@GroupName+'%'
            //                               ";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
            //    sda.Fill(dt);
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT TOP 1 * FROM ChcGroup
                                            WHERE GroupName LIKE '%'+@GroupName+'%'
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                sda.Fill(dt);
            }


            return dt;
        }

        /// <summary>
        /// 查詢 GroupName 的前兩個字串
        /// </summary>
        public DataTable QueryGroupName12ByChcGroup(string GroupName12)
        {
            DataTable dt = new DataTable();
            #region Access
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                string sql = @"SELECT MAX(GSort), mid(GroupName, 1, 2) 
                                            FROM ChcGroup
                                            GROUP BY mid(GroupName, 1, 2)
                                            HAVING mid(GroupName, 1, 2) = @GroupName12";

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName12", GroupName12);
                sda.Fill(dt);
            }
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT MAX(GSort), mid(GroupName, 1, 2) 
                                            FROM ChcGroup
                                            GROUP BY mid(GroupName, 1, 2)
                                            HAVING mid(GroupName, 1, 2) = @GroupName12";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName12", GroupName12);
                sda.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// 查詢要更新排序的資料
        /// </summary>
        public DataTable QueryGSortByChcGroup(int GSort)
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM ChcGroup
            //                                WHERE GSort > @GSort";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@GSort", GSort);
            //    sda.Fill(dt);
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlDbConnection(condb))
            {
                string sql = @"SELECT * FROM ChcGroup
                                            WHERE GSort > @GSort";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GSort", GSort);
                sda.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// 取得小組ID(GID)
        /// </summary>
        public int GetGIDByChcGroup(string GroupName, string GroupClass)
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    //string sql = @"SELECT ID FROM ChcGroup
            //    //                            WHERE GroupName LIKE '%-'+@GroupName
            //    //                            AND GroupClass = @GroupClass";

            //    string sql = @"SELECT ID FROM ChcGroup
            //                                WHERE GroupName = @GroupName
            //                                AND GroupClass = @GroupClass";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
            //    sda.SelectCommand.Parameters.AddWithValue("@GroupClass", GroupClass);
            //    sda.Fill(dt);
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                //string sql = @"SELECT ID FROM ChcGroup
                //                            WHERE GroupName LIKE '%-'+@GroupName
                //                            AND GroupClass = @GroupClass";

                string sql = @"SELECT ID FROM ChcGroup
                                            WHERE GroupName = @GroupName
                                            AND GroupClass = @GroupClass";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                sda.SelectCommand.Parameters.AddWithValue("@GroupClass", GroupClass);
                sda.Fill(dt);
            }
            if (dt != null && dt.Rows.Count > 0)
                return int.Parse(dt.Rows[0][0].ToString());
            else
                return 0;

        }

    }
}
