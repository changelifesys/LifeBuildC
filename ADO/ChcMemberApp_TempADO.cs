using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO
{
    public class ChcMemberApp_TempADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;
        public string DbSchema = ConfigurationManager.AppSettings.Get("DbSchema");

        //INSERT

        public void InsChcMemberApp_Temp(string UUID, string MID, string GroupCName, string GroupName, string GroupClass,
                                                                             string Ename, string Phone, string Gmail, string TithingNo, string Memo)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           " + DbSchema + @"ChcMemberApp_Temp(UUID, MID, GroupCName, GroupName, GroupClass, Ename,
                                                                                                                              Phone, Gmail, TithingNo, Memo)
                                           VALUES(@UUID, @MID, @GroupCName, @GroupName, @GroupClass, @Ename,
                                                            @Phone, @Gmail, @TithingNo, @Memo)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@UUID", UUID);
                com.Parameters.AddWithValue("@MID", MID);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@TithingNo", TithingNo);
                com.Parameters.AddWithValue("@Memo", Memo);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }



        //UPDATE

        public void UpdChcMemberApp_Temp(string MID, string GroupCName, string GroupName, string GroupClass, string Ename,
                                                                                   string Phone, string Gmail, string TithingNo, string Memo, bool IsTemp, string UUID)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE " + DbSchema + @"ChcMemberApp_Temp
                                            SET MID = @MID,
                                                    GroupCName = @GroupCName,
                                                    GroupName = @GroupName,
                                                    GroupClass = @GroupClass,
                                                    Ename = @Ename,
                                                    Phone = @Phone,
                                                    Gmail = @Gmail,
                                                    TithingNo = @TithingNo,
                                                    Memo = @Memo,
                                                    uptyn = 0,
                                                    UpdateTime = GETDATE(),
                                                    IsTemp = @IsTemp
                                            WHERE UUID = @UUID
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@MID", MID);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@TithingNo", TithingNo);
                com.Parameters.AddWithValue("@Memo", Memo);
                com.Parameters.AddWithValue("@IsTemp", IsTemp);
                com.Parameters.AddWithValue("@UUID", UUID);


                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        //QUERY

        public DataTable QueryDataWhereUUID_uptyn(string UUID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {

                string sql = @"SELECT TOP 1 *
                                            FROM " + DbSchema + @"ChcMemberApp_Temp
                                            WHERE UUID = @UUID
                                            AND (uptyn = 0 OR IsTemp = 1)
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@UUID", UUID);
                sda.Fill(dt);
            }

            return dt;

        }

        public DataTable QueryAllDataWhereuptyn()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {

                string sql = @"SELECT TOP 1 *
                                            FROM " + DbSchema + @"ChcMemberApp_Temp
                                            WHERE (uptyn = 0 OR IsTemp = 1)
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;

        }

    }
}
