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
    /// 課程類別
    /// </summary>
    public class SubCategoryADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        /// <summary>
        /// 檢查補課狀況
        /// </summary>
        public DataTable QueryCategoryID_DataBySubCategory(int MID, string CategoryID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM chclife.SubCategory
                                            WHERE CategoryID NOT IN
                                            (
                                              SELECT A.CategoryID FROM (
                                                SELECT SubSignInfo.SUID, SubSignInfo.SID, SubSignInfo.MID, SubSignInfo.SUDate, SubSignUpDate.CategoryID, SubSignUpDate.SignDate
                                                FROM SubSignInfo LEFT JOIN SubSignUpDate ON SubSignInfo.SUID = SubSignUpDate.SUID
                                              ) A
                                              LEFT JOIN SubjectDate ON SubjectDate.SID = A.SID 
                                              AND SubjectDate.SDate = A.SignDate
                                              AND SubjectDate.CategoryID = A.CategoryID
                                              WHERE SubjectDate.SDate IS NOT NULL
                                              AND LEFT(A.CategoryID, 2) = @CategoryID
                                              AND A.MID = @MID
                                              GROUP BY A.CategoryID
                                            )
                                            AND LEFT(CategoryID, 2) = @CategoryID";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.SelectCommand.Parameters.AddWithValue("@MID", MID);
                sda.Fill(dt);
            }
            return dt;
        }

    }
}
