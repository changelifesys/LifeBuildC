using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO
{
    public class FireMemberADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        public void InsFireMember(string GroupCName, string GroupName, string GroupClass, string Ename, string Phone,
            string Gmail, bool gender, string ClothesSize, bool Course)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           FireMember(GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, gender, ClothesSize, Course)
                                           VALUES(@GroupCName, @GroupName, @GroupClass, @Ename, @Phone, @Gmail, @gender, @ClothesSize, @Course)";

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

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

    }
}
