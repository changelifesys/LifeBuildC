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
    /// 會友資料
    /// </summary>
    public class ChcMemberADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;
        public string DbSchema = ConfigurationManager.AppSettings.Get("DbSchema");

        //INSERT

        public void InsChcMember2(string GroupCName, string GroupName, string GroupClass, string Ename, string Gmail, string Church, string C1_Status, string C2_Status, string Phone)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           " + DbSchema + @"ChcMember(GroupCName, GroupName, GroupClass, Ename, Gmail, Church, C1_Status, C2_Status, Phone)
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

        public void InsExcelDataByChcMember(string CategoryID, string GroupCName, string GroupName, string GroupClass, string Ename, bool IsPass)
        {
            string sql = "INSERT INTO " + DbSchema + @"ChcMember(GroupCName, GroupName, GroupClass, Ename,";

            switch (CategoryID.ToUpper())
            {
                case "C112":
                    sql += " IsC112)";
                    break;
                case "C134":
                    sql += " IsC134)";
                    break;
                case "C212":
                    sql += " IsC212)";
                    break;
                case "C234":
                    sql += " IsC234)";
                    break;
                case "C25":
                    sql += " IsC25)";
                    break;
            }

            sql += " VALUES(@GroupCName, @GroupName, @GroupClass, @Ename,";

            switch (CategoryID.ToUpper())
            {
                case "C112":
                    sql += " @IsPass)";
                    break;
                case "C134":
                    sql += " @IsPass)";
                    break;
                case "C212":
                    sql += " @IsPass)";
                    break;
                case "C234":
                    sql += " @IsPass)";
                    break;
                case "C25":
                    sql += " @IsPass)";
                    break;
            }

            using (SqlConnection con = new SqlConnection(condb))
            {
                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@IsPass", IsPass);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        //UPDATE

        public void UpdChcMember(string Phone, string Gmail, string Church, int MID)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE " + DbSchema + @"ChcMember
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
            string witness, bool Iswitness, bool IsLeave,
            bool IsC1God, bool IsC2L1)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE " + DbSchema + @"ChcMember
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
                                                    Iswitness = @Iswitness,
                                                    IsLeave = @IsLeave,
                                                    IsC1God = @IsC1God,
                                                    IsC2L1 = @IsC2L1
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
                com.Parameters.AddWithValue("@IsLeave", IsLeave);
                com.Parameters.AddWithValue("@IsC1God", IsC1God);
                com.Parameters.AddWithValue("@IsC2L1", IsC2L1);
                com.Parameters.AddWithValue("@MID", MID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdPhoneByChcMember(string Phone, string GroupName, string GroupCName, string GroupClass, string Ename)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE " + DbSchema + @"ChcMember
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

        public void UpdGroupNameByChcMember(string GroupName, string GroupCName, string GroupClass,
                                                                                       string Ename, string Phone)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE " + DbSchema + @"ChcMember
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

        public void UpdEnameByChcMember(string Ename, string GroupName, string GroupCName, string GroupClass, string Phone)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE " + DbSchema + @"ChcMember
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

        public void UpdC1C2_StatusByChcMember()
        {
            using (SqlConnection con = new SqlConnection(condb))
            {

                string sql = @"UPDATE " + DbSchema + @"ChcMember 
                                            SET C1_Status = '不通過', C2_Status = '不通過'
                                          ";

                SqlCommand com = new SqlCommand(sql, con);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdC1_StatusByChcMember()
        {
            using (SqlConnection con = new SqlConnection(condb))
            {

                string sql = @"UPDATE " + DbSchema + @"ChcMember 
                                            SET C1_Status = '通過'
                                            WHERE (IsC112 = 1 AND IsC134 = 1) OR IsC1God = 1
                                          ";

                SqlCommand com = new SqlCommand(sql, con);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdC2_StatusByChcMember()
        {
            using (SqlConnection con = new SqlConnection(condb))
            {

                string sql = @"UPDATE " + DbSchema + @"ChcMember 
                                            SET C2_Status = '通過'
                                            WHERE (IsC212 = 1 AND IsC234 = 1 AND IsC25 = 1
                                            AND IsC112 = 1 AND IsC134 = 1
                                            AND C1_Score >= 70 AND C212_Score >= 70 AND C234_Score >= 70
                                            AND Iswitness = 1) OR IsC2L1 = 1
                                          ";

                SqlCommand com = new SqlCommand(sql, con);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdPassDataByChcMember(string CategoryID, string GroupName, string Ename, bool IsPass)
        {
            string sql = "UPDATE " + DbSchema + @"ChcMember SET";

            switch(CategoryID.ToUpper())
            {
                case "C112":
                    sql += " IsC112 = @IsPass";
                    break;
                case "C134":
                    sql += " IsC134 = @IsPass";
                    break;
                case "C212":
                    sql += " IsC212 = @IsPass";
                    break;
                case "C234":
                    sql += " IsC234 = @IsPass";
                    break;
                case "C25":
                    sql += " IsC25 = @IsPass";
                    break;
            }

            using (SqlConnection con = new SqlConnection(condb))
            {
                sql += @" WHERE GroupName = @GroupName AND Ename = @Ename";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@IsPass", IsPass);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdWitness(string GroupCName, string GroupName, string Ename, string witness)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE " + DbSchema + @"ChcMember 
                                            SET witness = @witness
                                            WHERE GroupCName = @GroupCName
                                            AND GroupName = @GroupName
                                            AND Ename = @Ename
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@witness", witness);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@Ename", Ename);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdScoreByChcMember(string CategoryID, string Phone, string GroupCName, string GroupName, string Ename, int Score)
        {

            string sql = "UPDATE " + DbSchema + @"ChcMember SET Phone = @Phone,";

            switch (CategoryID.ToUpper())
            {
                case "C1":
                    sql += " C1_Score = @Score";
                    break;
                case "C212":
                    sql += " C212_Score = @Score";
                    break;
                case "C234":
                    sql += " C234_Score = @Score";
                    break;
            }

            using (SqlConnection con = new SqlConnection(condb))
            {
                sql += @" WHERE GroupCName = @GroupCName
                                   AND GroupName = @GroupName
                                   AND Ename = @Ename";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Score", Score);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        //QUERY

        public DataTable QueryPhoneByChcMember(string Phone)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {

                string sql = @"SELECT TOP 1 *
                                            FROM " + DbSchema + @"ChcMember
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

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT TOP 1 *
                                            FROM " + DbSchema + @"ChcMember
                                            WHERE Ename = @Ename
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryEnameByChcMember_1(string Ename)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT *
                                            FROM " + DbSchema + @"ChcMember
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
                string sql = @"SELECT *, '' AS [No] FROM " + DbSchema + @"ChcMember M
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;

        }

        public DataTable QueryLikeEnameByChcMember(string Ename)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"ChcMember M
                                           WHERE Ename LIKE @Ename+'%'
                                           AND IsLeave = 0
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
                sda.Fill(dt);
            }

            return dt;

        }

        public DataTable QueryMIDByChcMember(int MID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT *,

    (SELECT TOP 1 GroupID FROM " + DbSchema + @"ChcGroup 
	WHERE GroupCName = M.GroupCName 
	AND GroupName = M.GroupName)+'.'+GroupCName+'-'+GroupName group2

                                            FROM " + DbSchema + @"ChcMember M
                                            WHERE MID=@MID
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@MID", MID);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryGroupCNameByChcMember(string GroupCName)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"ChcMember
                                           WHERE GroupCName = @GroupCName
                                           AND IsLeave = 0
                                           ORDER BY GroupName, MID
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupCName", GroupCName);
                sda.Fill(dt);
            }

            return dt;

        }

        public DataTable QueryChcMemberByGroupNameAndIsLeave(string GroupName)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"ChcMember
                                           WHERE GroupName = @GroupName
                                           AND IsLeave = 0
                                           ORDER BY GroupName, MID
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                sda.Fill(dt);
            }

            return dt;

        }

        public DataTable QueryChcMemberByGroupName(string GroupName)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"ChcMember
                                           WHERE GroupName = @GroupName
                                           ORDER BY GroupName, MID
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                sda.Fill(dt);
            }

            return dt;

        }

        public DataTable QueryMemDataByChcMember(string GroupCName, string GroupName, string Ename)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"ChcMember
                                           WHERE GroupCName = @GroupCName
                                           AND GroupName = @GroupName
                                           AND Ename = @Ename
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupCName", GroupCName);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                sda.SelectCommand.Parameters.AddWithValue("@Ename", Ename);
                sda.Fill(dt);
            }

            return dt;

        }

        public DataTable QueryIsLeaveByChcMember()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"ChcMember
                                           WHERE IsLeave = 1
                                           ORDER BY GroupName, MID
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;

        }

        public DataTable QueryEnameByGroupNameAndGroupClass(string GroupName)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            using (SqlConnection con = new SqlConnection(condb))
            {
                if (GroupName == "")
                {
                    sql = @"SELECT * FROM " + DbSchema + @"ChcMember
                                    WHERE GroupName = ''
                                  ";
                }
                else
                {
                    sql = @"SELECT * FROM " + DbSchema + @"ChcMember
                                WHERE GroupName LIKE @GroupName+'%'
                                --AND (GroupClass IS NULL OR GroupClass = '' OR GroupClass = @GroupClass)
                              ";
                }



                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                //sda.SelectCommand.Parameters.AddWithValue("@GroupClass", GroupClass);
                sda.Fill(dt);
            }

            return dt;

        }


        public DataTable QueryChcMemberByMID(string MID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"ChcMember
                                           WHERE MID = @MID
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@MID", MID);
                sda.Fill(dt);
            }

            return dt;

        }

        //GET

        public DataTable GetChcMemberByGroup(string GroupCName, string GroupName, string Ename)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"ChcMember
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



    }
}
