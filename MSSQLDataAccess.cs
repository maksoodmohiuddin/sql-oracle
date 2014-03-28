using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace MSSQLDataAccess
{
    class MSSQLDataAccess
    {

        public static SqlDataReader ExecuteDataReader(SqlConnection connection, System.Data.CommandType commandType, string commandText)
        {
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                SqlCommand command = new SqlCommand();
                prepareCommand(ref command, connection, commandType, commandText);
                sqlDataReader = command.ExecuteReader();                
            }
            catch (Exception ex)
            {
                // Handle the exception in UI level only
                throw ex;
            }
            return sqlDataReader;
        }

        public static int ExecuteScalar(SqlConnection connection, System.Data.CommandType commandType, string commandText)
        {
            int returnValue;
            try
            {
                SqlCommand command = new SqlCommand();
                prepareCommand(ref command, connection, commandType, commandText);
                returnValue = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                // Handle the exceptiom in UI level only
                throw ex;
            }
            return returnValue;
        }

        public static string ExecuteScalarString(SqlConnection connection, System.Data.CommandType commandType, string commandText)
        {
            string returnValue;
            try
            {
                SqlCommand command = new SqlCommand();
                prepareCommand(ref command, connection, commandType, commandText);
                returnValue = (string)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                // Handle the exceptiom in UI level only
                throw ex;
            }
            return returnValue;
        }


        public static double ExecuteScalarDouble(SqlConnection connection, System.Data.CommandType commandType, string commandText)
        {
            double returnValue = 0.0;
            try
            {
                SqlCommand command = new SqlCommand();
                prepareCommand(ref command, connection, commandType, commandText);
                if (command.ExecuteScalar() != System.DBNull.Value)
                {
                    returnValue = (double)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                // Handle the exceptiom in UI level only
                throw ex;
            }
            return returnValue;
        }


        public static System.Data.DataTable ExecuteDataTable(ref  SqlCommand command, SqlConnection connection, System.Data.CommandType commandType, string commandText)
        {
            System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            System.Data.DataSet dataSet = new System.Data.DataSet();
            System.Data.DataTable dataTable = new System.Data.DataTable();
            try
            {
                prepareCommand(ref command, connection, commandType, commandText);
                sqlDataAdapter.SelectCommand = command;
                sqlDataAdapter.Fill(dataSet);

                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    dataTable = dataSet.Tables[0];
                }
                
            }
            catch (Exception ex)
            {
                //  Handle the exceptiom in UI level only
                throw ex;
            }
            return dataTable;
        }

        public static System.Data.DataSet ExecuteDataSet(ref  SqlCommand command, SqlConnection connection, System.Data.CommandType commandType, string commandText)
        {
            System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            System.Data.DataSet dataSet = new System.Data.DataSet();
            try
            {
                prepareCommand(ref command, connection, commandType, commandText);
                sqlDataAdapter.SelectCommand = command;
                sqlDataAdapter.Fill(dataSet);        

            }
            catch (Exception ex)
            {
                // Handle the exceptiom in UI level only
                throw ex;
            }
            return dataSet;
        }
       
        public static int ExecuteNonQuery(ref SqlCommand command, SqlConnection connection, System.Data.CommandType commandType, string commandText)
        {
            int retval;
            try
            {
                prepareCommand(ref command, connection, commandType, commandText);
                retval = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle the exceptiom in UI level only
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                connection.Close();
            }
            return retval;             
        }

        private static void prepareCommand(ref SqlCommand command, SqlConnection connection, System.Data.CommandType commandType, string commandText)
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                command.Connection = connection;
                command.CommandText = commandText;
                command.CommandType = commandType;

            }
            catch (Exception ex)
            {
                // Handle the exceptiom in UI level only
                throw ex;
            }
        }

        private static void attachParameters(ref SqlCommand command, SqlParameterCollection parameters)
        {
            try
            {
                foreach (SqlParameter p in parameters)
                {
                    command.Parameters.Add(p);
                }
            }
            catch (Exception ex)
            {
                // Handle the exceptiom in UI level only
                throw ex;
            }

        }
    }
    
}
