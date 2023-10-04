using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CondominiumProject.Database
{
    public class DatabaseHelper
    {
        const string server = @"localhost";
        const string database = "CondiminioDB";
        private static string connectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", server, database);

        // Select
        public static DataTable ExecuteQuery(string procedureName, List<SqlParameter> param)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;

                    if (param != null)
                    {
                        foreach (SqlParameter p in param)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }

                    // Utiliza un SqlDataReader para ejecutar el procedimiento almacenado que devuelve un conjunto de resultados
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Update - Delete - Insert
        public static void ExecuteNonQuery(string procedureName, List<SqlParameter> param)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;

                    if (param != null)
                    {
                        foreach (SqlParameter p in param)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }

                    // Ejecuta el comando para procedimientos almacenados que no devuelven resultados
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
