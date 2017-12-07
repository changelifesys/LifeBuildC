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
    public class FirePassWADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        public bool CheckPassKey(string PassKey)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM FirePassW
                                           WHERE PassKey = @PassKey";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@PassKey", PassKey);
                sda.Fill(dt);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                //密碼輸入正確
                return true;
            }

            //密碼輸入錯誤
            return false;
        }

    }
}
