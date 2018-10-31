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
    /// 上課時間
    /// </summary>
    public class SubjectDateADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;
        public string DbSchema = ConfigurationManager.AppSettings.Get("DbSchema");

        //INSERT

        public void InsSubjectDate(int SID, string CategoryID,  string SDate, string SubTime)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           " + DbSchema + @"SubjectDate(SID, CategoryID, SDate, SubTime)
                                           VALUES(@SID, @CategoryID, @SDate, @SubTime)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@SID", SID);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);
                com.Parameters.AddWithValue("@SDate", SDate);
                com.Parameters.AddWithValue("@SubTime", SubTime);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        //UPDATE

        public void DelSubjectDate(int SID, string CategoryID)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"DELETE FROM " + DbSchema + @"SubjectDate
                                            WHERE SID = @SID
                                            AND CategoryID = @CategoryID
                                          ";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@SID", SID);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        //QUERY

        public DataTable QuerySIDBySubjectDate(string CategoryID, string SDate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT TOP 1 * FROM " + DbSchema + @"SubjectDate
                                            WHERE LEFT(CategoryID, 2) = @CategoryID
                                            AND SDate = @SDate";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.SelectCommand.Parameters.AddWithValue("@SDate", SDate);
                sda.Fill(dt);
            }
            return dt;
        }

        public DataTable QueryAllBySubjectDate(string CategoryID)
        {

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT SubjectInfo.SubStrDate, SubjectInfo.SubEndDate, SubjectInfo.SubCount,
                                                          SubjectDate.*
                                            FROM " + DbSchema + @"SubjectInfo INNER JOIN " + DbSchema + @"SubjectDate ON SubjectInfo.SID = SubjectDate.SID
                                            WHERE LEFT(SubjectDate.CategoryID, 2) = @CategoryID
                                            ORDER BY SubjectDate.SID DESC, SubjectDate.SDate";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.Fill(dt);
            }
            return dt;
        }

        //GET

        public DataTable GetCategoryIDBySubjectDate(int SID)
        {

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"SubjectDate
                                           WHERE SID = @SID
                                           AND SDate = CONVERT(varchar(100), GETDATE(), 23)";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@SID", SID);
                sda.Fill(dt);
            }
            return dt;
        }

    }
}
