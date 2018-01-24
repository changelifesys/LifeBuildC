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
    /// 小組資訊
    /// </summary>
    public class ChcGroupADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        public DataTable QueryGroupByChcGroup()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT GroupClass, MAX(GSort) FROM ChcGroup
                                           WHERE GroupClass <> ''
                                           GROUP BY GroupClass
                                           ORDER BY MAX(GSort)
                                          ";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryGroupCNameByChcGroup(string GroupClass)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT GroupCName FROM ChcGroup
                                            WHERE GroupClass = @GroupClass
                                            AND GSort > 0
                                            GROUP BY GroupCName
                                            ORDER BY MAX(GSort)
                                          ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupClass", GroupClass);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryGroupNameByChcGroup(string GroupClass)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcGroup
                                           WHERE GroupClass = @GroupClass
                                           ORDER BY GSort";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupClass", GroupClass);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryGroupClassByChcGroup(string GroupName)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT TOP 1 * FROM ChcGroup
                                            WHERE GroupName = @GroupName
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                sda.Fill(dt);
            }


            return dt;
        }

        /// <summary>
        /// 查詢 GroupName 的前兩個字串
        /// </summary>
        public DataTable QueryGroupName12ByChcGroup(string GroupName12)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT MAX(GSort), mid(GroupName, 1, 2) 
                                            FROM ChcGroup
                                            GROUP BY mid(GroupName, 1, 2)
                                            HAVING mid(GroupName, 1, 2) = @GroupName12";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName12", GroupName12);
                sda.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// 查詢要更新排序的資料
        /// </summary>
        public DataTable QueryGSortByChcGroup(int GSort)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ChcGroup
                                            WHERE GSort > @GSort";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GSort", GSort);
                sda.Fill(dt);
            }
            return dt;
        }

    }
}
