﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EasyPay.DatabaseHelper
{
    public class DatabaseHelper
    {
        const string servidor = @"DESKTOP-D0RJ0FV";
        const string baseDatos = "EasyPay";
        const string strConexion = "Data Source=" + servidor + ";Initial Catalog=" + baseDatos + ";Integrated Security=True";

        public static DataTable ExecuteStoreProcedure(string procedure, List<SqlParameter> param)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConexion))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = procedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    if (param != null)
                    {
                        foreach (SqlParameter item in param)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }

                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        public static void ExecStoreProcedure(string procedure, List<SqlParameter> param)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConexion))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = procedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    if (param != null)
                    {
                        foreach (SqlParameter item in param)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
