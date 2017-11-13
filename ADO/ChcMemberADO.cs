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
    /// 會友資訊
    /// </summary>
    public class ChcMemberADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        public void InsChcMember(int GID, string Ename, string Gmail, string Church, string C1_Status, string C2_Status, string Phone)
        {
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"INSERT INTO
            //                               ChcMember(GID, Ename, Gmail, Church, C1_Status, C2_Status, Phone)
            //                               VALUES(@GID, @Ename, @Gmail, @Church, @C1_Status, @C2_Status, @Phone)";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@GID", GID);
            //    com.Parameters.AddWithValue("@Ename", Ename);
            //    com.Parameters.AddWithValue("@Gmail", Gmail);
            //    com.Parameters.AddWithValue("@Church", Church);
            //    com.Parameters.AddWithValue("@C1_Status", C1_Status);
            //    com.Parameters.AddWithValue("@C2_Status", C2_Status);
            //    com.Parameters.AddWithValue("@Phone", Phone);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           ChcMember(GID, Ename, Gmail, Church, C1_Status, C2_Status, Phone)
                                           VALUES(@GID, @Ename, @Gmail, @Church, @C1_Status, @C2_Status, @Phone)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GID", GID);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@Church", Church);
                com.Parameters.AddWithValue("@C1_Status", C1_Status);
                com.Parameters.AddWithValue("@C2_Status", C2_Status);
                com.Parameters.AddWithValue("@Phone", Phone);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void InsChcMember2(string GroupName, string Ename, string Gmail, string Church, string C1_Status, string C2_Status, string Phone)
        {
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"INSERT INTO
            //                               ChcMember(GroupName, Ename, Gmail, Church, C1_Status, C2_Status, Phone)
            //                               VALUES(@GroupName, @Ename, @Gmail, @Church, @C1_Status, @C2_Status, @Phone)";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@GroupName", GroupName);
            //    com.Parameters.AddWithValue("@Ename", Ename);
            //    com.Parameters.AddWithValue("@Gmail", Gmail);
            //    com.Parameters.AddWithValue("@Church", Church);
            //    com.Parameters.AddWithValue("@C1_Status", C1_Status);
            //    com.Parameters.AddWithValue("@C2_Status", C2_Status);
            //    com.Parameters.AddWithValue("@Phone", Phone);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           ChcMember(GroupName, Ename, Gmail, Church, C1_Status, C2_Status, Phone)
                                           VALUES(@GroupName, @Ename, @Gmail, @Church, @C1_Status, @C2_Status, @Phone)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@Church", Church);
                com.Parameters.AddWithValue("@C1_Status", C1_Status);
                com.Parameters.AddWithValue("@C2_Status", C2_Status);
                com.Parameters.AddWithValue("@Phone", Phone);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }


        public void UpdChcMember(string Phone, string Gmail, string Church, int MID)
        {
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"UPDATE ChcMember
            //                                SET Phone = @Phone,
            //                                        Gmail = @Gmail,
            //                                        Church = @Church
            //                                WHERE MID = @MID
            //                              ";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@Phone", Phone);
            //    com.Parameters.AddWithValue("@Gmail", Gmail);
            //    com.Parameters.AddWithValue("@Church", Church);
            //    com.Parameters.AddWithValue("@MID", MID);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE ChcMember
                                            SET Phone = @Phone,
                                                    Gmail = @Gmail,
                                                    Church = @Church
                                            WHERE MID = @MID
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@Church", Church);
                com.Parameters.AddWithValue("@MID", MID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// 更新手機號碼
        /// </summary>
        public void UpdPhoneByChcMember(string Ename, string GroupName, string Phone)
        {
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"UPDATE ChcMember
            //                                SET Phone = @Phone
            //                                WHERE GroupName = @GroupName
            //                                AND Ename = @Ename
            //                              ";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@Phone", Phone);
            //    com.Parameters.AddWithValue("@GroupName", GroupName);
            //    com.Parameters.AddWithValue("@Ename", Ename);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE ChcMember
                                            SET Phone = @Phone
                                            WHERE GroupName = @GroupName
                                            AND Ename = @Ename
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@Ename", Ename);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// 更新小組資料
        /// </summary>
        public void UpdGroupNameByChcMember(string Ename, string GroupName, string Phone)
        {
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"UPDATE ChcMember
            //                                SET GroupName = @GroupName
            //                                WHERE Ename = @Ename
            //                                AND Phone = @Phone
            //                              ";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@GroupName", GroupName);
            //    com.Parameters.AddWithValue("@Ename", Ename);
            //    com.Parameters.AddWithValue("@Phone", Phone);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE ChcMember
                                            SET GroupName = @GroupName
                                            WHERE Ename = @Ename
                                            AND Phone = @Phone
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Phone", Phone);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// 更新會友姓名
        /// </summary>
        public void UpdEnameByChcMember(string Ename, string GroupName, string Phone)
        {
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"UPDATE ChcMember
            //                                SET Ename = @Ename
            //                                WHERE GroupName = @GroupName
            //                                AND Phone = @Phone
            //                              ";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@Ename", Ename);
            //    com.Parameters.AddWithValue("@GroupName", GroupName);
            //    com.Parameters.AddWithValue("@Phone", Phone);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE ChcMember
                                            SET Ename = @Ename
                                            WHERE GroupName = @GroupName
                                            AND Phone = @Phone
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@Phone", Phone);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }


        /// <summary>
        /// 更新會友C1補課狀況
        /// </summary>
        public void UpdC1_StatusByChcMember(string C1_Status, int MID)
        {
         #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{

            //    string sql = @"UPDATE ChcMember 
            //                                SET C1_Status = @C1_Status
            //                                WHERE MID = @MID
            //                              ";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@C1_Status", C1_Status);
            //    com.Parameters.AddWithValue("@MID", MID);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {

                string sql = @"UPDATE ChcMember 
                                            SET C1_Status = @C1_Status
                                            WHERE MID = @MID
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@C1_Status", C1_Status);
                com.Parameters.AddWithValue("@MID", MID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// 更新會友C2補課狀況
        /// </summary>
        public void UpdC2_StatusByChcMember(string C2_Status, int MID)
        {
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"UPDATE ChcMember 
            //                                SET C2_Status = @C2_Status
            //                                WHERE MID = @MID
            //                              ";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@C2_Status", C2_Status);
            //    com.Parameters.AddWithValue("@MID", MID);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE ChcMember 
                                            SET C2_Status = @C2_Status
                                            WHERE MID = @MID
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@C2_Status", C2_Status);
                com.Parameters.AddWithValue("@MID", MID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// 查詢會友資料
        /// </summary>
        public DataTable QueryByChcMember(string Ename, int GID)
        {
            #region Access
            //DataTable dt = new DataTable();
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM ChcMember
            //                                WHERE Ename = @Ename
            //                                AND GID = @GID";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
            //    sda.SelectCommand.Parameters.AddWithValue("@GID", GID);
            //    sda.Fill(dt);
            //}

            //return dt;
            #endregion
            //MYSQL
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcMember
                                            WHERE Ename = @Ename
                                            AND GID = @GID";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
                sda.SelectCommand.Parameters.AddWithValue("@GID", GID);
                sda.Fill(dt);
            }

            return dt;

        }

        /// <summary>
        /// 查詢會友資料
        /// </summary>
        public DataTable QueryNotPhoneByChcMember(string GroupName, string Ename)
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM ChcMember
            //                                WHERE GroupName = @GroupName
            //                                AND Ename = @Ename";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
            //    sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
            //    sda.Fill(dt);
            //}

            //return dt;
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcMember
                                            WHERE GroupName = @GroupName
                                            AND Ename = @Ename";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
                sda.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// 查詢會友資料
        /// </summary>
        public DataTable QueryNotGroupNameByChcMember(string Phone, string Ename)
        {
            DataTable dt = new DataTable();
           #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM ChcMember
            //                                WHERE Phone = @Phone
            //                                AND Ename = @Ename";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@Phone", Phone);
            //    sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
            //    sda.Fill(dt);
            //}

            //return dt;
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcMember
                                            WHERE Phone = @Phone
                                            AND Ename = @Ename";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@Phone", Phone);
                sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
                sda.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// 查詢會友資料
        /// </summary>
       
        public DataTable QueryNotEnameByChcMember(string Phone, string GroupName)
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM ChcMember
            //                                WHERE Phone = @Phone
            //                                AND GroupName = @GroupName";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@Phone", Phone);
            //    sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
            //    sda.Fill(dt);
            //}

            //return dt;
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcMember
                                            WHERE Phone = @Phone
                                            AND GroupName = @GroupName";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@Phone", Phone);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                sda.Fill(dt);
            }

            return dt;


        }

        public DataTable QueryPhoneByChcMember(string Phone)
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    //string sql = @"SELECT TOP 1 M.*, G.GroupName, G.GroupClass
            //    //                            FROM ChcMember M
            //    //                            LEFT JOIN ChcGroup G ON M.GID = G.ID
            //    //                            WHERE M.Phone = @Phone
            //    //                           ";

            //    string sql = @"SELECT TOP 1 *
            //                                FROM ChcMember
            //                                WHERE Phone = @Phone
            //                               ";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@Phone", Phone);
            //    sda.Fill(dt);
            //}
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                //string sql = @"SELECT TOP 1 M.*, G.GroupName, G.GroupClass
                //                            FROM ChcMember M
                //                            LEFT JOIN ChcGroup G ON M.GID = G.ID
                //                            WHERE M.Phone = @Phone
                //                           ";

                string sql = @"SELECT TOP 1 *
                                            FROM ChcMember
                                            WHERE Phone = @Phone
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@Phone", Phone);
                sda.Fill(dt);
            }

            return dt;

        }

        public DataTable QueryEnameByChcMember(string Ename)
        {
            DataTable dt = new DataTable();
           #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT TOP 1 *
            //                                FROM ChcMember
            //                                WHERE Ename = @Ename
            //                               ";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
            //    sda.Fill(dt);
            //}

            //return dt;
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT TOP 1 *
                                            FROM ChcMember
                                            WHERE Ename = @Ename
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryAllByChcMember()
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT *
            //                                FROM ChcMember M
            //                               ";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.Fill(dt);
            //}

            //return dt;
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT *
                                            FROM ChcMember M
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;

        }

        public DataTable QueryMIDByChcMember(int MID)
        {
            DataTable dt = new DataTable();
            #region Access
            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT *
            //                                FROM ChcMember M
            //                                WHERE MID=@MID
            //                               ";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@MID", MID);
            //    sda.Fill(dt);
            //}

            //return dt;
            #endregion
            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT *
                                            FROM ChcMember M
                                            WHERE MID=@MID
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@MID", MID);
                sda.Fill(dt);
            }

            return dt;
        }

    }
}
