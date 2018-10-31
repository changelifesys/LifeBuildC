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
    public class FireMemberADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;
        public string DbSchema = ConfigurationManager.AppSettings.Get("DbSchema");

        public void InsFireMember(string GroupCName, string GroupName, string GroupClass, string Ename, string Phone,
            string Gmail, bool gender, string ClothesSize, bool Course, string PassKey, string Birthday)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           " + DbSchema + @"FireMember(GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, gender, ClothesSize, Course, PassKey, Birthday)
                                           VALUES(@GroupCName, @GroupName, @GroupClass, @Ename, @Phone, @Gmail, @gender, @ClothesSize, @Course, @PassKey, @Birthday)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@gender", gender);
                com.Parameters.AddWithValue("@ClothesSize", ClothesSize);
                com.Parameters.AddWithValue("@Course", Course);
                com.Parameters.AddWithValue("@PassKey", PassKey);
                com.Parameters.AddWithValue("@Birthday", Birthday);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public void UpdFireMember(string Phone,
    string Gmail, bool gender, string ClothesSize, bool Course, string Birthday, string PassKey)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE " + DbSchema + @"FireMember
                                           SET Phone = @Phone, 
                                                  Gmail = @Gmail, 
                                                  gender = @gender,
                                                  ClothesSize = @ClothesSize,
                                                  Course = @Course,
                                                  Birthday = @Birthday
                                           WHERE PassKey = @PassKey";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@gender", gender);
                com.Parameters.AddWithValue("@ClothesSize", ClothesSize);
                com.Parameters.AddWithValue("@Course", Course);
                com.Parameters.AddWithValue("@Birthday", Birthday);
                com.Parameters.AddWithValue("@PassKey", PassKey);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public void UpdAllByFireMember(string GroupCName, string GroupName, string GroupClass, string Ename, string Phone,
    string Gmail, bool gender, string ClothesSize, bool Course, string Birthday, string PassKey)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE " + DbSchema + @"FireMember
                                           SET GroupCName = @GroupCName,
                                                   GroupName = @GroupName,
                                                   GroupClass = @GroupClass,
                                                   Ename = @Ename,
                                                   Phone = @Phone, 
                                                   Gmail = @Gmail, 
                                                   gender = @gender,
                                                   ClothesSize = @ClothesSize,
                                                   Course = @Course,
                                                   Birthday = @Birthday
                                           WHERE PassKey = @PassKey";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@GroupCName", GroupCName);
                com.Parameters.AddWithValue("@GroupName", GroupName);
                com.Parameters.AddWithValue("@GroupClass", GroupClass);
                com.Parameters.AddWithValue("@Ename", Ename);
                com.Parameters.AddWithValue("@Phone", Phone);
                com.Parameters.AddWithValue("@Gmail", Gmail);
                com.Parameters.AddWithValue("@gender", gender);
                com.Parameters.AddWithValue("@ClothesSize", ClothesSize);
                com.Parameters.AddWithValue("@Course", Course);
                com.Parameters.AddWithValue("@Birthday", Birthday);
                com.Parameters.AddWithValue("@PassKey", PassKey);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DelFireMember(string PassKey)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"DELETE FROM " + DbSchema + @"FireMember
                                           WHERE PassKey = @PassKey";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@PassKey", PassKey);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public DataTable QueryFireMember()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @" SELECT *,
	                                            LEFT(Phone, 4)+'-'+RIGHT(Phone, 6) Phone2,
	                                            CASE WHEN gender = 1 THEN '男' ELSE '女' END gender2,
	                                            --CASE WHEN Course = 0 THEN '生命突破' ELSE '教會突破' END Course2,
                                                '待大會通知' AS Course2,

	                                            (SELECT TOP 1 GroupID FROM " + DbSchema + @"ChcGroup 
	                                             WHERE GroupCName = FireMember.GroupCName 
	                                             AND GroupName = FireMember.GroupName)+'.'+GroupCName+'-'+GroupName group2

                                            FROM " + DbSchema + @"FireMember
                                            WHERE CreateTime > '2018-4-8'
                                            --ORDER BY CreateTime DESC
                                            ORDER BY group2
                                         ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable GetFireMemberWherePassKey(string PassKey)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT TOP 1 *,
                                                         CASE WHEN gender = 1 THEN '男' ELSE '女' END gender2,
                                                         CASE WHEN Course = 0 THEN '生命突破' ELSE '教會突破' END Course2,

                                                        (SELECT TOP 1 GroupID FROM " + DbSchema + @"ChcGroup 
	                                                      WHERE GroupCName = FireMember.GroupCName 
	                                                      AND GroupName = FireMember.GroupName)+'.'+GroupCName+'-'+GroupName group2

                                           FROM " + DbSchema + @"FireMember
                                           WHERE PassKey = @PassKey";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@PassKey", PassKey);
                sda.Fill(dt);
            }

            return dt;
        }

    }
}
