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

        //INSERT

        public void InsChcMemberByChcMember_Log(string GroupCName, string GroupName, string GroupClass, string Ename)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           ChcMember_Log(MID, GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, Church, C1_Status, C2_Status,
                                                                            IsC112, IsC134, IsC212, IsC234, IsC25, C1_Score, C212_Score, C234_Score, witness, Iswitness, Memo)
                                           SELECT TOP 1 
                                                         MID, GroupCName, GroupName, GroupClass, Ename, Phone, Gmail, Church, C1_Status, C2_Status,
                                                                                                                    IsC112, IsC134, IsC212, IsC234, IsC25, C1_Score, C212_Score, C234_Score, witness, Iswitness, Memo
                                           FROM ChcMember
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

    }
}
