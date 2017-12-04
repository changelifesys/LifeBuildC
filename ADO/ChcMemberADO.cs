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
    /// 會友資訊
    /// </summary>
    public class ChcMemberADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        public void InsChcMember2(string GroupCName, string GroupName, string GroupClass, string Ename, string Gmail, string Church, string C1_Status, string C2_Status, string Phone)
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
                                           ChcMember(GroupCName, GroupName, GroupClass, Ename, Gmail, Church, C1_Status, C2_Status, Phone)
                                           VALUES(@GroupCName, @GroupName, @GroupClass, @Ename, @Gmail, @Church, @C1_Status, @C2_Status, @Phone)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
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

        #region Update Member


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

        public void UpdChcMember2(int MID, string GroupCName, string GroupName, string GroupClass,
            string Ename, string Church, string C1_Status, string C2_Status,
            bool IsC112, bool IsC134, bool IsC212, bool IsC234, bool IsC25, int C1_Score, int C212_Score, int C234_Score,
            string witness, bool Iswitness)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE ChcMember
                                            SET GroupCName = @GroupCName,
                                                    GroupName = @GroupName,
                                                    GroupClass = @GroupClass,
                                                    Ename = @Ename,
                                                    Church = @Church,
                                                    C1_Status = @C1_Status,
                                                    C2_Status = @C2_Status,
                                                    IsC112 = @IsC112,
                                                    IsC134 = @IsC134,
                                                    IsC212 = @IsC212,
                                                    IsC234 = @IsC234,
                                                    IsC25 = @IsC25,
                                                    C1_Score = @C1_Score,
                                                    C212_Score = @C212_Score,
                                                    C234_Score = @C234_Score,
                                                    witness = @witness,
                                                    Iswitness = @Iswitness
                                            WHERE MID = @MID
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Church", Church);
                com.Parameters.AddWithValue("@C1_Status", C1_Status);
                com.Parameters.AddWithValue("@C2_Status", C2_Status);
                com.Parameters.AddWithValue("@IsC112", IsC112);
                com.Parameters.AddWithValue("@IsC134", IsC134);
                com.Parameters.AddWithValue("@IsC212", IsC212);
                com.Parameters.AddWithValue("@IsC234", IsC234);
                com.Parameters.AddWithValue("@IsC25", IsC25);
                com.Parameters.AddWithValue("@C1_Score", C1_Score);
                com.Parameters.AddWithValue("@C212_Score", C212_Score);
                com.Parameters.AddWithValue("@C234_Score", C234_Score);
                com.Parameters.AddWithValue("@witness", witness);
                com.Parameters.AddWithValue("@Iswitness", Iswitness);
                com.Parameters.AddWithValue("@MID", MID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// 更新手機號碼
        /// </summary>
        public void UpdPhoneByChcMember(string Phone, string GroupName, string GroupCName, string GroupClass, string Ename)
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
                                            AND GroupCName = @GroupCName
                                            AND GroupClass = @GroupClass
                                            AND Ename = @Ename
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// 更新小組資料
        /// </summary>
        public void UpdGroupNameByChcMember(string GroupName, string GroupCName, string GroupClass,
                                                                                       string Ename, string Phone)
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
                                            SET GroupName = @GroupName, 
                                                   GroupCName = @GroupCName,
                                                   GroupClass = @GroupClass
                                            WHERE Ename = @Ename
                                            AND Phone = @Phone
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
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
        public void UpdEnameByChcMember(string Ename, string GroupName, string GroupCName, string GroupClass, string Phone)
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
                                            AND GroupCName = @GroupCName
                                            AND GroupClass = @GroupClass
                                            AND Phone = @Phone
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
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


        #endregion

        #region 會友條件查詢

        public DataTable QueryByChcMember(string GroupCName, string GroupName, string Ename)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcMember
                                           WHERE GroupCName = @GroupCName
                                           AND GroupName = @GroupName
                                           AND Ename = @Ename";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupCName", GroupCName);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
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

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcMember M
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;

        }

        public DataTable QueryMIDByChcMember(int MID)
        {
            DataTable dt = new DataTable();
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

        #endregion



    }
}
