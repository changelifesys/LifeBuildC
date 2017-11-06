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
    public class ExamQuestionsADO
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;

        public void InsExamQuestions(string ExamCategory,
                                                                  int FieldCnt, bool IsField1, string Field1,
                                                                  bool IsField2, string Field2,
                                                                  bool IsField3, string Field3,
                                                                  bool IsField4, string Field4,
                                                                  bool IsField5, string Field5,
                                                                  bool IsField6, string Field6,
                                                                  bool IsField7, string Field7,
                                                                  bool IsField8, string Field8,
                                                                  bool IsField9, string Field9,
                                                                  bool IsField10, string Field10)
        {

            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"INSERT INTO
            //                               ExamQuestions(ExamCategory, FieldCnt, IsField1, Field1,
            //                                                              IsField2, Field2,
            //                                                              IsField3, Field3,
            //                                                              IsField4, Field4,
            //                                                              IsField5, Field5,
            //                                                              IsField6, Field6,
            //                                                              IsField7, Field7,
            //                                                              IsField8, Field8,
            //                                                              IsField9, Field9,
            //                                                              IsField10, Field10,
            //                                                              CreateDate)
            //                               VALUES(@ExamCategory, @FieldCnt, @IsField1, @Field1,
            //                                               @IsField2, @Field2,
            //                                               @IsField3, @Field3,
            //                                               @IsField4, @Field4,
            //                                               @IsField5, @Field5,
            //                                               @IsField6, @Field6,
            //                                               @IsField7, @Field7,
            //                                               @IsField8, @Field8,
            //                                               @IsField9, @Field9,
            //                                               @IsField10, @Field10,
            //                                               @CreateDate)";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@ExamCategory", ExamCategory);
            //    com.Parameters.AddWithValue("@FieldCnt", FieldCnt);
            //    com.Parameters.AddWithValue("@IsField1", IsField1);
            //    com.Parameters.AddWithValue("@Field1", Field1);
            //    com.Parameters.AddWithValue("@IsField2", IsField2);
            //    com.Parameters.AddWithValue("@Field2", Field2);
            //    com.Parameters.AddWithValue("@IsField3", IsField3);
            //    com.Parameters.AddWithValue("@Field3", Field3);
            //    com.Parameters.AddWithValue("@IsField4", IsField4);
            //    com.Parameters.AddWithValue("@Field4", Field4);
            //    com.Parameters.AddWithValue("@IsField5", IsField5);
            //    com.Parameters.AddWithValue("@Field5", Field5);
            //    com.Parameters.AddWithValue("@IsField6", IsField6);
            //    com.Parameters.AddWithValue("@Field6", Field6);
            //    com.Parameters.AddWithValue("@IsField7", IsField7);
            //    com.Parameters.AddWithValue("@Field7", Field7);
            //    com.Parameters.AddWithValue("@IsField8", IsField8);
            //    com.Parameters.AddWithValue("@Field8", Field8);
            //    com.Parameters.AddWithValue("@IsField9", IsField9);
            //    com.Parameters.AddWithValue("@Field9", Field9);
            //    com.Parameters.AddWithValue("@IsField10", IsField10);
            //    com.Parameters.AddWithValue("@Field10", Field10);
            //    com.Parameters.AddWithValue("@CreateDate", CreateDate);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"INSERT INTO
                                           ExamQuestions(ExamCategory, FieldCnt, IsField1, Field1,
                                                                          IsField2, Field2,
                                                                          IsField3, Field3,
                                                                          IsField4, Field4,
                                                                          IsField5, Field5,
                                                                          IsField6, Field6,
                                                                          IsField7, Field7,
                                                                          IsField8, Field8,
                                                                          IsField9, Field9,
                                                                          IsField10, Field10,
                                                                          CreateDate)
                                           VALUES(@ExamCategory, @FieldCnt, @IsField1, @Field1,
                                                           @IsField2, @Field2,
                                                           @IsField3, @Field3,
                                                           @IsField4, @Field4,
                                                           @IsField5, @Field5,
                                                           @IsField6, @Field6,
                                                           @IsField7, @Field7,
                                                           @IsField8, @Field8,
                                                           @IsField9, @Field9,
                                                           @IsField10, @Field10,
                                                           GETDATE())";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                com.Parameters.AddWithValue("@FieldCnt", FieldCnt);
                com.Parameters.AddWithValue("@IsField1", IsField1);
                com.Parameters.AddWithValue("@Field1", Field1);
                com.Parameters.AddWithValue("@IsField2", IsField2);
                com.Parameters.AddWithValue("@Field2", Field2);
                com.Parameters.AddWithValue("@IsField3", IsField3);
                com.Parameters.AddWithValue("@Field3", Field3);
                com.Parameters.AddWithValue("@IsField4", IsField4);
                com.Parameters.AddWithValue("@Field4", Field4);
                com.Parameters.AddWithValue("@IsField5", IsField5);
                com.Parameters.AddWithValue("@Field5", Field5);
                com.Parameters.AddWithValue("@IsField6", IsField6);
                com.Parameters.AddWithValue("@Field6", Field6);
                com.Parameters.AddWithValue("@IsField7", IsField7);
                com.Parameters.AddWithValue("@Field7", Field7);
                com.Parameters.AddWithValue("@IsField8", IsField8);
                com.Parameters.AddWithValue("@Field8", Field8);
                com.Parameters.AddWithValue("@IsField9", IsField9);
                com.Parameters.AddWithValue("@Field9", Field9);
                com.Parameters.AddWithValue("@IsField10", IsField10);
                com.Parameters.AddWithValue("@Field10", Field10);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public void UpdExamQuestions(string ID, string ExamCategory,
                                                                  int FieldCnt, bool IsField1, string Field1,
                                                                  bool IsField2, string Field2,
                                                                  bool IsField3, string Field3,
                                                                  bool IsField4, string Field4,
                                                                  bool IsField5, string Field5,
                                                                  bool IsField6, string Field6,
                                                                  bool IsField7, string Field7,
                                                                  bool IsField8, string Field8,
                                                                  bool IsField9, string Field9,
                                                                  bool IsField10, string Field10, string UpdateDate)
        {

            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"UPDATE ExamQuestions
            //                                SET ExamCategory = @ExamCategory, FieldCnt = @FieldCnt,
            //                                        IsField1 = @IsField1, Field1 = @Field1,  
            //                                        IsField2 = @IsField2, Field2 = @Field2,
            //                                        IsField3 = @IsField3, Field3 = @Field3,
            //                                        IsField4 = @IsField4, Field4 = @Field4,
            //                                        IsField5 = @IsField5, Field5 = @Field5,
            //                                        IsField6 = @IsField6, Field6 = @Field6,
            //                                        IsField7 = @IsField7, Field7 = @Field7,
            //                                        IsField8 = @IsField8, Field8 = @Field8,
            //                                        IsField9 = @IsField9, Field9 = @Field9,
            //                                        IsField10 = @IsField10, Field10 = @Field10,
            //                                        UpdateDate = @UpdateDate
            //                                WHERE ID = @ID";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@ExamCategory", ExamCategory);
            //    com.Parameters.AddWithValue("@FieldCnt", FieldCnt);
            //    com.Parameters.AddWithValue("@IsField1", IsField1);
            //    com.Parameters.AddWithValue("@Field1", Field1);
            //    com.Parameters.AddWithValue("@IsField2", IsField2);
            //    com.Parameters.AddWithValue("@Field2", Field2);
            //    com.Parameters.AddWithValue("@IsField3", IsField3);
            //    com.Parameters.AddWithValue("@Field3", Field3);
            //    com.Parameters.AddWithValue("@IsField4", IsField4);
            //    com.Parameters.AddWithValue("@Field4", Field4);
            //    com.Parameters.AddWithValue("@IsField5", IsField5);
            //    com.Parameters.AddWithValue("@Field5", Field5);
            //    com.Parameters.AddWithValue("@IsField6", IsField6);
            //    com.Parameters.AddWithValue("@Field6", Field6);
            //    com.Parameters.AddWithValue("@IsField7", IsField7);
            //    com.Parameters.AddWithValue("@Field7", Field7);
            //    com.Parameters.AddWithValue("@IsField8", IsField8);
            //    com.Parameters.AddWithValue("@Field8", Field8);
            //    com.Parameters.AddWithValue("@IsField9", IsField9);
            //    com.Parameters.AddWithValue("@Field9", Field9);
            //    com.Parameters.AddWithValue("@IsField10", IsField10);
            //    com.Parameters.AddWithValue("@Field10", Field10);
            //    com.Parameters.AddWithValue("@UpdateDate", UpdateDate);
            //    com.Parameters.AddWithValue("@ID", ID);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"UPDATE ExamQuestions
                                            SET ExamCategory = @ExamCategory, FieldCnt = @FieldCnt,
                                                    IsField1 = @IsField1, Field1 = @Field1,  
                                                    IsField2 = @IsField2, Field2 = @Field2,
                                                    IsField3 = @IsField3, Field3 = @Field3,
                                                    IsField4 = @IsField4, Field4 = @Field4,
                                                    IsField5 = @IsField5, Field5 = @Field5,
                                                    IsField6 = @IsField6, Field6 = @Field6,
                                                    IsField7 = @IsField7, Field7 = @Field7,
                                                    IsField8 = @IsField8, Field8 = @Field8,
                                                    IsField9 = @IsField9, Field9 = @Field9,
                                                    IsField10 = @IsField10, Field10 = @Field10,
                                                    UpdateDate = @UpdateDate
                                            WHERE ID = @ID";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                com.Parameters.AddWithValue("@FieldCnt", FieldCnt);
                com.Parameters.AddWithValue("@IsField1", IsField1);
                com.Parameters.AddWithValue("@Field1", Field1);
                com.Parameters.AddWithValue("@IsField2", IsField2);
                com.Parameters.AddWithValue("@Field2", Field2);
                com.Parameters.AddWithValue("@IsField3", IsField3);
                com.Parameters.AddWithValue("@Field3", Field3);
                com.Parameters.AddWithValue("@IsField4", IsField4);
                com.Parameters.AddWithValue("@Field4", Field4);
                com.Parameters.AddWithValue("@IsField5", IsField5);
                com.Parameters.AddWithValue("@Field5", Field5);
                com.Parameters.AddWithValue("@IsField6", IsField6);
                com.Parameters.AddWithValue("@Field6", Field6);
                com.Parameters.AddWithValue("@IsField7", IsField7);
                com.Parameters.AddWithValue("@Field7", Field7);
                com.Parameters.AddWithValue("@IsField8", IsField8);
                com.Parameters.AddWithValue("@Field8", Field8);
                com.Parameters.AddWithValue("@IsField9", IsField9);
                com.Parameters.AddWithValue("@Field9", Field9);
                com.Parameters.AddWithValue("@IsField10", IsField10);
                com.Parameters.AddWithValue("@Field10", Field10);
                com.Parameters.AddWithValue("@UpdateDate", UpdateDate);
                com.Parameters.AddWithValue("@ID", ID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }


        public void DelExamQuestions(string ID)
        {
            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"DELETE FROM ExamQuestions
            //                                WHERE ID = @ID";

            //    OleDbCommand com = new OleDbCommand(sql, con);
            //    com.Parameters.AddWithValue("@ID", ID);

            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"DELETE FROM ExamQuestions
                                            WHERE ID = @ID";

                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@ID", ID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public DataTable QueryExamQuestions(string ExamCategory)
        {
            DataTable dt = new DataTable();

            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM ExamQuestions
            //                                WHERE ExamCategory = @ExamCategory";
            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
            //    sda.Fill(dt);
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ExamQuestions
                                            WHERE ExamCategory = @ExamCategory";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryExamQuestionsByIsEnable(string ExamCategory)
        {
            DataTable dt = new DataTable();

            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT EQ.*, S.IsEnable FROM ExamQuestions EQ
            //                                LEFT JOIN SystemSet S ON S.ExamCategory = EQ.ExamCategory
            //                                WHERE EQ.ExamCategory = @ExamCategory
            //                                AND IsEnable = 'True'";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
            //    sda.Fill(dt);
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT EQ.*, S.IsEnable FROM ExamQuestions EQ
                                            LEFT JOIN SystemSet S ON S.ExamCategory = EQ.ExamCategory
                                            WHERE EQ.ExamCategory = @ExamCategory
                                            AND IsEnable = 'True'";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryExamQuestionsByExamToTrue(string ExamCategory)
        {
            DataTable dt = new DataTable();

            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM ExamQuestions
            //                                WHERE ExamCategory = @ExamCategory
            //                              ";

            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
            //    sda.Fill(dt);
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ExamQuestions
                                            WHERE ExamCategory = @ExamCategory
                                          ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@ExamCategory", ExamCategory);
                sda.Fill(dt);
            }

            return dt;
        }

        public DataTable QueryExamQuestionsByID(string ID)
        {
            DataTable dt = new DataTable();

            #region Access

            //using (OleDbConnection con = new OleDbConnection(condb))
            //{
            //    string sql = @"SELECT * FROM ExamQuestions
            //                                WHERE ID = @ID";
            //    OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
            //    sda.SelectCommand.Parameters.AddWithValue("@ID", ID);
            //    sda.Fill(dt);
            //}

            #endregion

            //MS SQL
            using (SqlConnection con = new SqlConnection(condb))
            {
                string sql = @"SELECT * FROM ExamQuestions
                                            WHERE ID = @ID";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.SelectCommand.Parameters.AddWithValue("@ID", ID);
                sda.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// 測試Access SQL語法
        /// </summary>
        public DataTable TestSql(string sql)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(condb))
            {
                OleDbDataAdapter sda = new OleDbDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;
        }

    }
}
