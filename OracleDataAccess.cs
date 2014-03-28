using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

namespace OracleDataAccess
{
    class OracleDataAccess
    {

        #region ExecuteDataReader
        public static OracleDataReader ExecuteDataReader(OracleConnection connection, System.Data.CommandType commandType, string commandText)
        {
            OracleDataReader oracleDataReader = null;           
            try
            {
                OracleCommand command = new OracleCommand();
                prepareCommand(ref command, connection, commandType, commandText);
                oracleDataReader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                // Handle the exception in UI level only
                throw ex;
            }
            return oracleDataReader;
        }
        #endregion

        #region ExecuteScalar
        public static int ExecuteScalar(OracleConnection connection, System.Data.CommandType commandType, string commandText)
        {
            int returnValue;
            try
            {
                OracleCommand command = new OracleCommand();
                prepareCommand(ref command, connection, commandType, commandText);
                returnValue = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                // Handle the exception in UI level only
                throw ex;
            }
            return returnValue;
        }
        #endregion

        #region ExecuteScalarString
        public static string ExecuteScalarString(OracleConnection connection, System.Data.CommandType commandType, string commandText)
        {
            string returnValue;
            try
            {
                OracleCommand command = new OracleCommand();
                prepareCommand(ref command, connection, commandType, commandText);
                returnValue = (string)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                // Handle the exception in UI level only
                throw ex;
            }
            return returnValue;
        }
        #endregion

        #region ExecuteDataTable
        public static System.Data.DataTable ExecuteDataTable(ref  System.Data.OracleClient.OracleCommand command, System.Data.OracleClient.OracleConnection connection, System.Data.CommandType commandType, string commandText)
        {
            System.Data.OracleClient.OracleDataAdapter oracleDataAdapter = new System.Data.OracleClient.OracleDataAdapter();
            System.Data.DataSet dataSet = new System.Data.DataSet();
            System.Data.DataTable dataTable = new System.Data.DataTable();
            try
            {
                prepareCommand(ref command, connection, commandType, commandText);
                oracleDataAdapter.SelectCommand = command;
                oracleDataAdapter.Fill(dataSet);

                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    dataTable = dataSet.Tables[0];
                }

            }
            catch (Exception ex)
            {
                // Handle the exception in UI level only
                throw ex;
            }
            return dataTable;
        }
        #endregion
           
        #region ExecuteDataSet
        public static System.Data.DataSet ExecuteDataSet(ref OracleCommand command, OracleConnection connection, System.Data.CommandType commandType, string commandText)
        {
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter();
            System.Data.DataSet dataSet = new System.Data.DataSet();
            try
            {
                prepareCommand(ref command, connection, commandType, commandText);
                oracleDataAdapter.SelectCommand = command;
                oracleDataAdapter.Fill(dataSet);

            }
            catch (Exception ex)
            {
                // Handle the exception in UI level only
                throw ex;
            }
            return dataSet;
        }
        #endregion

        #region ExecuteNonQuery
        public static int ExecuteNonQuery(ref OracleCommand command, OracleConnection connection, System.Data.CommandType commandType, string commandText)
        {
            int retval;
            try
            {
                prepareCommand(ref command, connection, commandType, commandText);
                retval = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle the exception in UI level only
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                connection.Close();
            }
            return retval;
        }
        #endregion

        #region prepareCommand
        private static void prepareCommand(ref System.Data.OracleClient.OracleCommand command, System.Data.OracleClient.OracleConnection connection, System.Data.CommandType commandType, string commandText)
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
                // Handle the exception in UI level only
                throw ex;
            }
        }
        #endregion              

        #region attachParameters
        private static void attachParameters(ref OracleCommand command, OracleParameterCollection parameters)
        {
            try
            {
                foreach (OracleParameter p in parameters)
                {
                    command.Parameters.Add(p);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception in UI level only
                throw ex;
            }
        }
        #endregion
    }
}