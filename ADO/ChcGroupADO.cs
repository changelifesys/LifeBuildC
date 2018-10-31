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
    /// 小組群組
    /// </summary>
    public class ChcGroupADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;
        public string DbSchema = ConfigurationManager.AppSettings.Get("DbSchema");

        //Query

        public DataTable QueryGroupByChcGroup()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT GroupClass, MAX(GSort) FROM " +
                                           DbSchema + "ChcGroup " +
                                     @" WHERE GroupClass <> ''
                                           GROUP BY GroupClass
                                           ORDER BY MAX(GSort)
                                          ";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable Query_ChcGroup_GroupClass()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT GroupClass, MAX(GSort) FROM " +
                                           DbSchema + "ChcGroup " +
                                     @" WHERE GroupClass <> ''
                                           AND GSort > 0
                                           GROUP BY GroupClass
                                           ORDER BY MAX(GSort)
                                         ";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;

            //return data
            //家庭組弟兄 |   25
            //家庭組姊妹 |   95
            //社青 |  127
            //學生 |  153
            //其他 |  154


        }

        public DataTable Query_ChcGroup_CategoryID_GSort(string CategoryID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT GroupClass, MAX(GSort) FROM " +
                                           DbSchema + "ChcGroup " +
                                     @" WHERE CategoryID IN ('None', @CategoryID)
                                           GROUP BY GroupClass
                                           ORDER BY MAX(GSort)
                                         ";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.Fill(dt);
            }

            return dt;

            //return data
            //其他	|  0
            //家庭組弟兄 |   25
            //家庭組姊妹 |   95
            //社青 |  127
            //學生 |  153
            //其他 |  154


        }

        public DataTable Query_ChcGroup_CategoryID(string CategoryID)
        {

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " +
                                          DbSchema + "ChcGroup " +
                                      @" WHERE CategoryID IN ('None', @CategoryID)
                                          ";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryGroupClassByMemSubData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT GroupClass, MAX(GSort) FROM " +
                                         DbSchema + "ChcGroup " +
                                     @" WHERE GroupClass <> ''
                                           AND GSort <> 0
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
                string sql = @"SELECT GroupCName FROM " +
                                         DbSchema + "ChcGroup " +
                                     @" WHERE GroupClass = @GroupClass
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
                string sql = @"SELECT * FROM " +
                                           DbSchema + "ChcGroup " +
                                     @" WHERE GroupClass = @GroupClass
                                           ORDER BY GSort";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupClass", GroupClass);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable Query_ChcGroup_GSort_GroupClass(string GroupClass)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " +
                                         DbSchema + "ChcGroup " +
                                     @" WHERE GroupClass = @GroupClass
                                           AND GSort > 0
                                           ORDER BY GSort";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupClass", GroupClass);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable Query_ChcGroup_GSort()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " +
                                          DbSchema + "ChcGroup " +
                                      @" WHERE GSort > 0
                                            ORDER BY GSort";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryGroupClassByChcGroup(string GroupName)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT TOP 1 * FROM " + DbSchema + @"ChcGroup
                                            WHERE GroupName = @GroupName
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                sda.Fill(dt);
            }


            return dt;
        }

        public DataTable QueryChcGroupByCondition(string GroupCName, string GroupName, string GroupClass)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"ChcGroup
                                           WHERE GroupCName = @GroupCName
                                           AND GroupName = @GroupName
                                           AND GroupClass = @GroupClass
                                          ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupCName", GroupCName);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName", GroupName);
                sda.SelectCommand.Parameters.AddWithValue("@GroupClass", GroupClass);
                sda.Fill(dt);
            }


            return dt;
        }

        public DataTable QueryGroupName12ByChcGroup(string GroupName12)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT MAX(GSort), mid(GroupName, 1, 2) 
                                            FROM " + DbSchema + @"ChcGroup
                                            GROUP BY mid(GroupName, 1, 2)
                                            HAVING mid(GroupName, 1, 2) = @GroupName12";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GroupName12", GroupName12);
                sda.Fill(dt);
            }
            return dt;
        }

        public DataTable QueryGSortByChcGroup(int GSort)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM " + DbSchema + @"ChcGroup
                                            WHERE GSort > @GSort";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@GSort", GSort);
                sda.Fill(dt);
            }
            return dt;
        }

    }
}
