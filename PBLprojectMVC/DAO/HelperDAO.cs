﻿using System.Data.SqlClient;
using System.Data;

namespace PBLprojectMVC.DAO
{
    public class HelperDAO
    {
        public static void ExecuteProc(string NameProc, SqlParameter[] parameters)
        {
            if (value == null)
                return DBNull.Value;
            else
                return value;
        }

        public static void ExecuteProc(string nameProc,
                                      SqlParameter[] parameters)
        {
            using (SqlConnection connection = ConnectionDB.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(NameProc, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    command.ExecuteNonQuery();
                }
            }
        }
      
        public static DataTable ExecuteProcSelect(string NameProc, SqlParameter[] parameters)
        {
            using (SqlConnection conn = ConnectionDAO.GetConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(NameProc, conn))
                {
                if (parameters != null)
                    adapter.SelectCommand.Parameters.AddRange(parameters);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable table = new DataTable();
                adapter.Fill(table);

        public static DataTable ExecuteProcSelect(string nameProc, SqlParameter[] parameters)
        {
            using (SqlConnection connection = ConnectionDB.GetConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(nameProc, connection))
                {
                    if (parameters != null)
                        adapter.SelectCommand.Parameters.AddRange(parameters);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }
    }
}