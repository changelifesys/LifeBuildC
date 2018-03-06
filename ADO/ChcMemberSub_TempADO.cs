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

    }
}
