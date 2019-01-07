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
        public string DbSchema = ConfigurationManager.AppSettings.Get("DbSchema");

        //INSERT

        public void InsSubjectInfo(string SubCount, string CategoryID, string SubName, string SUCondition, string SubLocation, 
                                                          string SubStrDate, string SubEndDate, string Memo, string HtmlSubDesc)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           " + DbSchema + @"SubjectInfo(SubCount, CategoryID, SubName, SUCondition, SubLocation,
                                                                  SubStrDate, SubEndDate, Memo, HtmlSubDesc)
                                           VALUES(@SubCount, @CategoryID, @SubName, @SUCondition, @SubLocation,
                                                                  @SubStrDate, @SubEndDate, @Memo, @HtmlSubDesc)";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@SubCount", SubCount);
                com.Parameters.AddWithValue("@CategoryID", CategoryID);
                com.Parameters.AddWithValue("@SubName", SubName);
                com.Parameters.AddWithValue("@SUCondition", SUCondition);
                com.Parameters.AddWithValue("@SubLocation", SubLocation);
                com.Parameters.AddWithValue("@SubStrDate", SubStrDate);
                com.Parameters.AddWithValue("@SubEndDate", SubEndDate);
                com.Parameters.AddWithValue("@Memo", Memo);
                com.Parameters.AddWithValue("@HtmlSubDesc", HtmlSubDesc);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        //UPDATE

        public void Update_SubjectInfo(string SubCount, string SUCondition, string SubLocation, string SubStrDate, string SubEndDate, int SID, string Memo, string HtmlSubDesc, bool IsCheckOpen)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE " + DbSchema + @"SubjectInfo
                                            SET SubCount = @SubCount,
                                                    SUCondition = @SUCondition,
                                                    SubLocation = @SubLocation,
                                                    SubStrDate = @SubStrDate,
                                                    SubEndDate = @SubEndDate,
                                                    Memo = @Memo,
                                                    HtmlSubDesc = @HtmlSubDesc,
                                                    IsCheckOpen = @IsCheckOpen
                                            WHERE SID = @SID";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@SubCount", SubCount);
                com.Parameters.AddWithValue("@SUCondition", SUCondition);
                com.Parameters.AddWithValue("@SubLocation", SubLocation);
                com.Parameters.AddWithValue("@SubStrDate", SubStrDate);
                com.Parameters.AddWithValue("@SubEndDate", SubEndDate);
                com.Parameters.AddWithValue("@Memo", Memo);
                com.Parameters.AddWithValue("@HtmlSubDesc", HtmlSubDesc);
                com.Parameters.AddWithValue("@IsCheckOpen", IsCheckOpen);
                com.Parameters.AddWithValue("@SID", SID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }


        //Query

        public DataTable QueryMaxSIDBySubjectInfo(string CategoryID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT TOP 1 * FROM " + DbSchema + @"SubjectInfo
                                            WHERE CategoryID = @CategoryID
                                            ORDER BY SID DESC";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.Fill(dt);
            }

            return dt;
        }

        //Get

        public DataTable GetSubjectInfo(string SubDate, string CategoryID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"SubjectInfo SInfo
                                           INNER JOIN " + DbSchema + @"SubjectDate SD ON SInfo.SID = SD.SID
                                           WHERE SInfo.CategoryID = @CategoryID
                                           AND SInfo.SubStrDate <= @SubDate AND SInfo.SubEndDate >= @SubDate
                                           ORDER BY SD.SDate";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.SelectCommand.Parameters.AddWithValue("@SubDate", SubDate);
                sda.Fill(dt);
            }
            return dt;
        }

        public DataTable GetSubjectInfoSDate(string SubDate, string CategoryID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"SubjectInfo SInfo
                                           INNER JOIN " + DbSchema + @"SubjectDate SD ON SInfo.SID = SD.SID
                                           WHERE SInfo.CategoryID = @CategoryID
                                           AND SD.SDate = @SubDate 
                                           AND IsCheckOpen = 1
                                           ORDER BY SD.SDate";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.SelectCommand.Parameters.AddWithValue("@SubDate", SubDate);
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
                                           FROM " + DbSchema + @"SubjectInfo 
                                           INNER JOIN " + DbSchema + @"SubjectDate ON SubjectInfo.SID = SubjectDate.SID
                                           WHERE SubjectInfo.SID = @SID";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@SID", SID);
                sda.Fill(dt);
            }
            return dt;
        }

        public DataTable GetSDateBySubjectJoin(int SID, string SDate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"SubjectInfo 
                                           LEFT JOIN " + DbSchema + @"SubjectDate ON SubjectInfo.SID = SubjectDate.SID
                                           WHERE SubjectInfo.SID = @SID
                                           AND SubjectDate.SDate = @SDate
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@SID", SID);
                sda.SelectCommand.Parameters.AddWithValue("@SDate", SDate);
                sda.Fill(dt);
            }
            return dt;
        }

        public DataTable Get_SubjectInfo_MaxSID_WHERE_CategoryID(string CategoryID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT TOP 1 * FROM " + DbSchema + @"SubjectInfo
                                           WHERE CategoryID = @CategoryID
                                           ORDER BY SID DESC";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.Fill(dt);
            }
            return dt;
        }


        //EXEC

        public void sp_Delete_SubjectInfo(int SID)
        {
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"EXEC " + DbSchema + @"sp_Delete_SubjectInfo @SID";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@SID", SID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }



    }
}
