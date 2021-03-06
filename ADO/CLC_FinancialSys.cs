﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO
{
    public class CLC_FinancialSys
    {
        public string condb = ConfigurationManager.ConnectionStrings["LifeDBConnectionString"].ConnectionString;
        public string DbSchema = ConfigurationManager.AppSettings.Get("DbSchema");

        public DataTable QueryCLC_FinancialSys_ID_Temp_1()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {

                string sql = @"SELECT *
                                            FROM " + DbSchema + @"CLC_FinancialSys_ID_Temp_1
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;

        }

        public DataTable QueryCLC_FinancialSys_ID_Temp_2()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(condb))
            {

                string sql = @"SELECT *
                                            FROM " + DbSchema + @"CLC_FinancialSys_ID_Temp_2
                                           ";

                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }

            return dt;

        }



    }
}
