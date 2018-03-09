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

        public void InsChcMemberSub_Temp(string CategoryID, string Ename, string Egroup, string Phone, string EStatus, string SubDate, string Memo)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           ChcMemberSub_Temp(CategoryID, Ename, Egroup, Phone, EStatus, SubDate, Memo)
                                           VALUES(@CategoryID, @Ename, @Egroup, @Phone, @EStatus, @SubDate, @Memo)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Egroup", Egroup);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@EStatus", EStatus);
                com.Parameters.AddWithValue("@SubDate", SubDate);
                com.Parameters.AddWithValue("@Memo", Memo);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void InsChcMemberSub_Temp_2(int SID, string CategoryID, string GroupCName, string GroupName, string GroupClass,
            string Ename, string Phone, string Gmail, string Church, string EStatus, DateTime SubDate, string Memo)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"EXEC sp_ChcMemberSub_Temp_ADD_Data @SID, @CategoryID, @GroupCName, @GroupName, @GroupClass,
                                            @Ename, @Phone, @Gmail, @Church, @EStatus, @SubDate, @Memo";

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

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

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

        public DataTable GetChcMemberSub_TempByGroup(int SID, string GroupCName, string GroupName, string GroupClass,
            DateTime SubDate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcMemberSub_Temp
                                           WHERE SID = @SID
                                           AND GroupCName = @GroupCName
                                           AND GroupName = @GroupName
                                           AND GroupClass = @GroupClass
                                           AND SubDate = @SubDate";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@SID", SID);
                sda.SelectCommand.Parameters.AddWithValue("@GroupCName", GroupCName);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                sda.SelectCommand.Parameters.AddWithValue("@GroupClass", GroupClass);
                sda.SelectCommand.Parameters.AddWithValue("@SubDate", SubDate);

                sda.Fill(dt);
            }

            return dt;
        }

    }
}
