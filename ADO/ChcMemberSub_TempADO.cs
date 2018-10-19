﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO
{
    /// <summary>
    /// 會員報名&簽到資料Temp
    /// </summary>
    public class ChcMemberSub_TempADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        //INSERT

        public void InsExcelDataByChcMemberSub_Temp(string CategoryID, string GroupCName, string GroupName, string GroupClass, string Ename, string Phone, string Gmail, string Church, bool EStatus, DateTime SubDate)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           ChcMemberSub_Temp(CategoryID, GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, Church, EStatus, SubDate)
                                           VALUES(@CategoryID, @GroupCName, @GroupName, @GroupClass, @Ename, @Phone, @Gmail, @Church, @EStatus, @SubDate)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@Church", Church);
                com.Parameters.AddWithValue("@EStatus", EStatus);
                com.Parameters.AddWithValue("@SubDate", SubDate);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void InsChcMemberSub_TempByUpdSubSignToC1(int SID, string CategoryID, string GroupCName, string GroupName, string GroupClass,
string Ename, string Phone, string Gmail, string Church, string EStatus, string SubDate, string Memo, string MID)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO 
                                           chclife.ChcMemberSub_Temp([SID], CategoryID, GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, Church, EStatus, SubDate, Memo, MID) 
		                                   VALUES (@SID, @CategoryID, @GroupCName, @GroupName, @GroupClass, @Ename, @Phone, @Gmail, @Church, @EStatus, @SubDate, @Memo, @MID);";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@SID", SID);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@Church", Church);
                com.Parameters.AddWithValue("@EStatus", EStatus);
                com.Parameters.AddWithValue("@SubDate", SubDate);
                com.Parameters.AddWithValue("@Memo", Memo);
                com.Parameters.AddWithValue("@MID", MID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }


        //UPDATE

        public void Upduptyn1ByChcMemberSub_Temp()
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE ChcMemberSub_Temp
                                           SET uptyn = 1";

                SqlCommand com = new SqlCommand(sql, con);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Upd_EStatus(int EStatus, int SID, string CategoryID, string GroupCName, string GroupName, string GroupClass, string Ename)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE ChcMemberSub_Temp
                                           SET EStatus = @EStatus
                                           WHERE SID = @SID
                                           AND CategoryID = @CategoryID
                                           AND GroupCName = @GroupCName
                                           AND GroupName = @GroupName
                                           AND GroupClass = @GroupClass
                                           AND Ename = @Ename";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@EStatus", EStatus);
                com.Parameters.AddWithValue("@SID", SID);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdChcMemberSub_TempByNo(string GroupCName, string GroupName, string GroupClass, string Ename, string Phone,
                                                                                         string Gmail, string Memo, string No)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE ChcMemberSub_Temp
                                           SET GroupCName = @GroupCName,
                                                  GroupName = @GroupName,
                                                  GroupClass = @GroupClass,
                                                  Ename = @Ename,
                                                  Phone = @Phone,
                                                  Gmail = @Gmail,
                                                  Memo = Memo + @Memo,
                                                  UpdateTime = GETDATE()
                                           WHERE [No] = @No";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@Memo", Memo);
                com.Parameters.AddWithValue("@No", No);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdChcMemberSub_TempByUpdSubSign(
            string GroupCName, string GroupName, string GroupClass, string Ename, string Phone,
                                                                                 string Gmail, string Memo, string No, int IsPass, string Make)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE ChcMemberSub_Temp
                                           SET GroupCName = @GroupCName,
                                                  GroupName = @GroupName,
                                                  GroupClass = @GroupClass,
                                                  Ename = @Ename,
                                                  Phone = @Phone,
                                                  Gmail = @Gmail,
                                                  Memo = Memo + @Memo,
                                                  UpdateTime = GETDATE(),
                                                  EStatus = 1,
                                                  IsPass = @IsPass,
                                                  Make = @Make
                                           WHERE [No] = @No
                                           AND SubDate = CONVERT(varchar(100), GETDATE(), 23)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@Memo", Memo);
                com.Parameters.AddWithValue("@No", No);
                com.Parameters.AddWithValue("@IsPass", IsPass);
                com.Parameters.AddWithValue("@Make", Make);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }


        //QUREY

        public DataTable QueryuptynByChcMemberSub_Temp()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcMemberSub_Temp
                                           WHERE uptyn = 0";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable Query_ChcMemberSub_Temp_SID(int SID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcMemberSub_Temp
                                           WHERE SID = @SID";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@SID", SID);
                sda.Fill(dt);
            }

            return dt;
        }

        //Check

        public DataTable CheckMakeByChcMemberSub_Temp(string MID, int SID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcMemberSub_Temp
                                           WHERE MID = @MID
                                           AND CategoryID = (
                                                SELECT TOP 1 CategoryID FROM SubjectDate
                                                WHERE SID = @SID
                                                AND SDate = CONVERT(varchar(100), GETDATE(), 23)
                                           )
                                           AND SubDate <> CONVERT(varchar(100), GETDATE(), 23)";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@MID", MID);
                sda.SelectCommand.Parameters.AddWithValue("@SID", SID);
                sda.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// true: 該名會友有報名資料 ; false: 該名會友沒有報名資料
        /// </summary>
        public bool ChkChcMemberSub_TempByMID(int MID, int SID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcMemberSub_Temp
                                           WHERE MID = @MID
                                           AND SID = @SID";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@SID", SID);
                sda.SelectCommand.Parameters.AddWithValue("@MID", MID);
                sda.Fill(dt);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                return true; //有報名資料
            }

            return false; //沒有報名資料

        }

        //EXEC

        public void InsChcMemberSub_Temp_2(int SID, string CategoryID, string GroupCName, string GroupName, string GroupClass,
    string Ename, string Phone, string Gmail, string Church, string EStatus, DateTime SubDate, string Memo, string MID, string No)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"EXEC sp_ChcMemberSub_Temp_ADD_Data @SID, @CategoryID, @GroupCName, @GroupName, @GroupClass,
                                            @Ename, @Phone, @Gmail, @Church, @EStatus, @SubDate, @Memo, @MID, @No";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@SID", SID);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@Church", Church);
                com.Parameters.AddWithValue("@EStatus", EStatus);
                com.Parameters.AddWithValue("@SubDate", SubDate);
                com.Parameters.AddWithValue("@Memo", Memo);
                com.Parameters.AddWithValue("@MID", MID);
                com.Parameters.AddWithValue("@No", No);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }



    }
}
