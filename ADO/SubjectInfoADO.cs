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
    /// 課程資訊
    /// </summary>
    public class SubjectInfoADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        public void InsSubjectInfo(string CategoryID, string SubName, string SUCondition, string SubLocation, 
                                                          string SubStrDate, string SubEndDate)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           SubjectInfo(CategoryID, SubName, SUCondition, SubLocation,
                                                                  SubStrDate, SubEndDate)
                                           VALUES(@CategoryID, @SubName, @SUCondition, @SubLocation,
                                                                  @SubStrDate, @SubEndDate)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);
                com.Parameters.AddWithValue("@SubName", SubName);
                com.Parameters.AddWithValue("@SUCondition", SUCondition);
                com.Parameters.AddWithValue("@SubLocation", SubLocation);
                com.Parameters.AddWithValue("@SubStrDate", SubStrDate);
                com.Parameters.AddWithValue("@SubEndDate", SubEndDate);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public void UpdSubjectInfo(string SUCondition, string SubLocation, string SubStrDate, string SubEndDate, int SID)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE SubjectInfo
                                            SET SUCondition = @SUCondition,
                                                    SubLocation = @SubLocation,
                                                    SubStrDate = @SubStrDate,
                                                    SubEndDate = @SubEndDate
                                            WHERE SID = @SID";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@SUCondition", SUCondition);
                com.Parameters.AddWithValue("@SubLocation", SubLocation);
                com.Parameters.AddWithValue("@SubStrDate", SubStrDate);
                com.Parameters.AddWithValue("@SubEndDate", SubEndDate);
                com.Parameters.AddWithValue("@SID", SID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }


        public DataTable QueryMaxSIDBySubjectInfo(string CategoryID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT TOP 1 * FROM SubjectInfo
                                            WHERE CategoryID = @CategoryID
                                            ORDER BY SID DESC";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QuerySIDBySubjectInfo(int SID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT SubjectDate.SubTime, SubjectDate.CategoryID, SubjectDate.SDate,

                                            (SELECT CategoryName  FROM SubCategory
                                             WHERE CategoryID = SubjectDate.CategoryID) AS CategoryName

                                            FROM SubjectInfo 
                                            LEFT JOIN SubjectDate ON SubjectInfo.SID = SubjectDate.SID
                                            WHERE SubjectInfo.SID = @SID
                                            ORDER BY SubjectDate.SDate";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@SID", SID);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable GetSubjectInfo(string SubStrDate, string CategoryID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT *
                                           FROM SubjectInfo
                                           INNER JOIN SubjectDate ON SubjectInfo.SID = SubjectDate.SID
                                           WHERE SubjectInfo.CategoryID = @CategoryID
                                           AND SubjectInfo.SubStrDate <= @SubStrDate
                                           AND SubjectInfo.SubEndDate >= @SubStrDate
                                           ORDER BY SubjectDate.SDate";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.SelectCommand.Parameters.AddWithValue("@SubStrDate", SubStrDate);
                sda.Fill(dt);
            }
            return dt;
        }

        public DataTable GetSubjectDateBySubjectInfo(int SID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT *, SubjectDate.CategoryID AS CategoryID2 
                                           FROM SubjectInfo 
                                           INNER JOIN SubjectDate ON SubjectInfo.SID = SubjectDate.SID
                                           WHERE SubjectInfo.SID = @SID";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@SID", SID);
                sda.Fill(dt);
            }
            return dt;
        }

    }
}
