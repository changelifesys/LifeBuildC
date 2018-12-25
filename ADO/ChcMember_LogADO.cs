using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO
{
    public class ChcMember_LogADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;
        public string DbSchema = ConfigurationManager.AppSettings.Get("DbSchema");

        //INSERT

        public void InsChcMemberByChcMember_Log(string GroupCName, string GroupName, string GroupClass, string Ename)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           " + DbSchema + @"ChcMember_Log(MID, GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, Church, C1_Status, C2_Status,
                                                                            IsC112, IsC134, IsC212, IsC234, IsC25, C1_Score, C212_Score, C234_Score, witness, Iswitness, Memo)
                                           SELECT TOP 1 
                                                         MID, GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, Church, C1_Status, C2_Status,
                                                                                                                    IsC112, IsC134, IsC212, IsC234, IsC25, C1_Score, C212_Score, C234_Score, witness, Iswitness, Memo
                                           FROM " + DbSchema + @"ChcMember
                                           WHERE GroupCName = @GroupCName
                                           AND GroupName = @GroupName
                                           AND GroupClass = @GroupClass
                                           AND Ename = @Ename";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void InsChcMember_LogByChcMemberMode(string Mode, string MID, string GroupCName, string GroupName, string Phone, string Ename)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           " + DbSchema + @"ChcMember_Log(MID, GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, Church, C1_Status, C2_Status,
                                                                            IsC112, IsC134, IsC212, IsC234, IsC25, C1_Score, C212_Score, C234_Score, witness, Iswitness, Memo)
                                           SELECT TOP 1 
                                                         MID, GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, Church, C1_Status, C2_Status,
                                                                                                                    IsC112, IsC134, IsC212, IsC234, IsC25, C1_Score, C212_Score, C234_Score, witness, Iswitness, Memo
                                           FROM " + DbSchema + @"ChcMember
                                           ";

                switch (Mode)
                {
                    case "1":
                        sql += @" WHERE MID = @MID";
                        break;
                    case "2":
                        sql += @" WHERE GroupCName = @GroupCName
                                            AND GroupName = @GroupName
                                            AND Ename = @Ename";
                        break;
                    case "3":
                        sql += @" WHERE Phone = @Phone
                                            AND Ename = @Ename";
                        break;
                    case "4":
                        sql += @" WHERE Phone = @Phone
                                            AND GroupCName = @GroupCName
                                            AND GroupName = @GroupName";
                        break;
                    case "5":
                        sql += @" WHERE Ename = @Ename";
                        break;
                }

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@MID", MID);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Ename", Ename);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void InsChcMember2ByChcMember_Log(string GroupCName, string GroupName, string Ename)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           " + DbSchema + @"ChcMember_Log(MID, GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, Church, C1_Status, C2_Status,
                                                                            IsC112, IsC134, IsC212, IsC234, IsC25, C1_Score, C212_Score, C234_Score, witness, Iswitness, Memo)
                                           SELECT TOP 1 
                                                         MID, GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, Church, C1_Status, C2_Status,
                                                                                                                    IsC112, IsC134, IsC212, IsC234, IsC25, C1_Score, C212_Score, C234_Score, witness, Iswitness, Memo
                                           FROM " + DbSchema + @"ChcMember
                                           WHERE GroupCName = @GroupCName
                                           AND GroupName = @GroupName
                                           AND Ename = @Ename";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@Ename", Ename);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void InsLogDataByChcMember_Log(string MID, string GroupCName, string GroupName, string GroupClass, string Ename, string Phone, string Gmail, string Church, string C1_Status, string C2_Status,
                                                                           bool IsC112, bool IsC134, bool IsC212, bool IsC234, bool IsC25, int C1_Score, int C212_Score, int C234_Score, string witness, bool Iswitness, string Memo)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           " + DbSchema + @"ChcMember_Log(MID, GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, Church, C1_Status, C2_Status,
                                                                           IsC112, IsC134, IsC212, IsC234, IsC25, C1_Score, C212_Score, C234_Score, witness, Iswitness, Memo)
                                           VALUES(@MID, @GroupCName, @GroupName, @GroupClass, @Ename, @Phone, @Gmail, @Church, @C1_Status, @C2_Status,
                                                                           @IsC112, @IsC134, @IsC212, @IsC234, @IsC25, @C1_Score, @C212_Score, @C234_Score, @witness, @Iswitness, @Memo)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@MID", MID);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Gmail", Gmail);
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
                com.Parameters.AddWithValue("@Memo", Memo);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
