using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DataManager
{
    public class DataUtilities
    {
        #region SqlConnection

        /// <summary>
        /// This method gets the connection string.
        /// </summary>
        /// <returns>Connection String</returns>
        public string GetConnectionString()
        {
            /* This code takes connection string from the web.config file.*/
			
            return "Data Source=WAHEED\\SQLEXPRESS;Database=invoice_db_local;User Id=invoice_xxxx;Password=xxxxxx;"; 
        }


        /// <summary>
        /// This method returns SqlConnection object.
        /// </summary>
        /// <returns>SqlConnection</returns>
        public SqlConnection GetSqlConnection()
        {
            string strConnection = GetConnectionString();
            if (strConnection == null)
                return null;
            var objSqlConnection = new SqlConnection(strConnection);
            return objSqlConnection;
        }

        #endregion


        #region EXECUTE DATASET

        /// <summary>
        /// This method returns the data in dataset form. 
        /// </summary>
        /// <param name="commandText">Command text</param>
        /// <returns>Data in the form of Dataset.</returns>
        public DataSet ExecuteDataSet(string commandText)
        {
            
                var dsData = new DataSet();
                var objMyDataAdapter = new SqlDataAdapter();
                SqlConnection objMySqlConn = GetSqlConnection();
            try
            {
                if (objMySqlConn == null)
                {
                    return null;
                }
                var objMyCommand = new SqlCommand
                {
                    Connection = objMySqlConn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandText
                };
                objMyDataAdapter.SelectCommand = objMyCommand;

                objMyDataAdapter.Fill(dsData);
                //objMySqlConn.Close();
                //objMySqlConn.Dispose();

                return dsData;
            }
            catch
            {
                return null; 
            }
            finally
            {
                objMySqlConn.Close();
            }
        }


        /// <summary>
        /// This method returns the data in dataset form. 
        /// </summary>
        /// <param name="commandText">Command Text</param>
        /// <param name="htParameters">Hash Table</param>
        /// <returns>Data in the form of Dataset</returns>
        public DataSet ExecuteDataSet(string commandText, Hashtable htParameters)
        {
            var dsData = new DataSet();
            var objMyDataAdapter = new SqlDataAdapter();
            SqlConnection objMySqlConn = GetSqlConnection();
            try
            {
                if (objMySqlConn == null)
                {
                    return null;
                }
                var objSqlCommand = new SqlCommand
                {
                    Connection = objMySqlConn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandText,
                    CommandTimeout = 0
                };

                foreach (DictionaryEntry parameter in htParameters)
                {
                    objSqlCommand.Parameters.AddWithValue(parameter.Key.ToString(), parameter.Value);
                }

                objMyDataAdapter.SelectCommand = objSqlCommand;
                objMyDataAdapter.Fill(dsData);

                return dsData;
            }
            catch
            {
                return null; 
            }
            finally
            {
                objMySqlConn.Close();
            }
        }


        /// <summary>
        /// This method returns the data in dataset form. 
        /// </summary>
        /// <param name="commandText">Command Text</param>
        /// <param name="htParameters">Hash Table</param>
        /// <param name="strVitualTable">VirtualTable</param>
        /// <returns>Data in the form of Dataset</returns>
        public DataSet ExecuteDataSetVirtualTable(string commandText, Hashtable htParameters, string strVitualTable)
        {
            var dsData = new DataSet();
            var objMyDataAdapter = new SqlDataAdapter();
            SqlConnection objMySqlConn = GetSqlConnection();
            if (objMySqlConn == null)
            {
                return null;
            }
            var objSqlCommand = new SqlCommand
            {
                Connection = objMySqlConn,
                CommandType = CommandType.StoredProcedure,
                CommandText = commandText
            };

            foreach (DictionaryEntry parameter in htParameters)
            {
                objSqlCommand.Parameters.AddWithValue(parameter.Key.ToString(), parameter.Value);
            }

            objMyDataAdapter.SelectCommand = objSqlCommand;
            objMyDataAdapter.Fill(dsData, strVitualTable);

            return dsData;
        }




        #endregion

        #region EXECUTE SCALAR

        /// <summary>
        /// <param name="commandText"></param>       
        /// </summary>

        public int ExecuteScalar(string commandText, string commandType)
        {

            int nRValue = 0;
            SqlTransaction objSqlTransaction = null;
            SqlConnection objMySqlConn = GetSqlConnection();
            if (objMySqlConn != null)
            {


                try
                {
                    var objMySqlCmd = new SqlCommand
                    {
                        Connection = objMySqlConn,
                        CommandType = CommandType.Text,
                        CommandText = commandText
                    };
                    objMySqlConn.Open();
                    objSqlTransaction = objMySqlConn.BeginTransaction();
                    object objValue = objMySqlCmd.ExecuteScalar();
                    objSqlTransaction.Commit();
                    nRValue = Convert.ToInt32(objValue);
                }
                catch (Exception)
                {
                    if (objSqlTransaction != null)
                        objSqlTransaction.Rollback();
                    throw;
                    // LogError(this, ex, "Unable to execute the ExecuteScalar() ");
                }
                finally
                {
                    objMySqlConn.Close();
                }

            }
            return nRValue;
        }

        /// <summary>
        /// This method returns object typecast the object to int or string depending upon return type.
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns>Object</returns>
        public object ExecuteScalar(string commandText)
        {
            object objValue;
            SqlTransaction objSqlTransaction = null;
            SqlConnection objMySqlConn = GetSqlConnection();
            if (objMySqlConn == null)
            {
                return null;
            }
            try
            {
                var objMySqlCmd = new SqlCommand
                {
                    Connection = objMySqlConn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandText
                };
                objMySqlConn.Open();
                objSqlTransaction = objMySqlConn.BeginTransaction();
                objValue = objMySqlCmd.ExecuteScalar();
                objSqlTransaction.Commit();
            }
            catch (Exception)
            {
                if (objSqlTransaction != null)
                    objSqlTransaction.Rollback();
                throw;
                // LogError(this, ex, "Unable to execute the ExecuteScalar() ");
            }
            finally
            {
                objMySqlConn.Close();
            }
            return objValue;
        }


        /// <summary>
        ///  This method returns object typecast the object to int or string depending upon return type.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="htParameters"></param>
        /// <returns>Object</returns>
        public object ExecuteScalar(string commandText, Hashtable htParameters)
        {
            object objValue;
            //SqlTransaction objSqlTransaction = null;
            SqlConnection objMySqlConn = GetSqlConnection();
            if (objMySqlConn == null)
            {
                return null;
            }
            try
            {
                var objSqlCommand = new SqlCommand
                {
                    Connection = objMySqlConn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandText
                };
                foreach (DictionaryEntry parameter in htParameters)
                {
                    objSqlCommand.Parameters.AddWithValue(parameter.Key.ToString(), parameter.Value);
                }
                objMySqlConn.Open();
                //objSqlTransaction = objMySqlConn.BeginTransaction();
                objValue = objSqlCommand.ExecuteScalar();
                //objSqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                //if (objSqlTransaction != null)
                //    objSqlTransaction.Rollback();
                throw ex;
                // LogError(this, ex, "Unable to execute the parameterized ExecuteScalar() ");
            }
            finally
            {
                objMySqlConn.Close();
            }
            return objValue;
        }

        #endregion

        #region EXECUTE NONQUERY




        /// <summary>
        /// This methods returns no of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns>Returns Integer</returns>
        public int ExecuteNonQuery(string commandText)
        {
            int nRetVal = 0;
            SqlTransaction objSqlTransaction = null;
            SqlConnection objMySqlConn = GetSqlConnection();
            if (objMySqlConn == null)
            {
                return nRetVal;
            }
            try
            {
                var objSqlCommand = new SqlCommand
                {
                    Connection = objMySqlConn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandText
                };
                objMySqlConn.Open();
                objSqlTransaction = objMySqlConn.BeginTransaction();
                nRetVal = objSqlCommand.ExecuteNonQuery();
                objSqlTransaction.Commit();
            }
            catch (Exception)
            {
                if (objSqlTransaction != null)
                    objSqlTransaction.Rollback();
                throw;
                //LogError(this, ex, "Unable to execute the ExcuteNonQuery() !");
            }
            finally
            {
                objMySqlConn.Close();
            }
            return nRetVal;
        }


        /// <summary>
        /// This methods returns no of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="htParameters"></param>
        /// <returns>Returns Integer</returns>
        public int ExecuteNonQuery(string commandText, Hashtable htParameters)
        {
            int nRetVal = 0;
            SqlTransaction objSqlTransaction = null;
            SqlConnection objMySqlConn = GetSqlConnection();
            if (objMySqlConn == null)
            {
                //LogError(this, null, "Unable to create SqlConnection object ! ");
                return nRetVal;
            }
            try
            {
                var objSqlCommand = new SqlCommand
                {
                    Connection = objMySqlConn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandText,
                    CommandTimeout = 0
                };
                foreach (DictionaryEntry parameter in htParameters)
                {
                    objSqlCommand.Parameters.AddWithValue(parameter.Key.ToString(), parameter.Value);
                }

                objMySqlConn.Open();

                objSqlTransaction = objMySqlConn.BeginTransaction();
                objSqlCommand.Transaction = objSqlTransaction;
                nRetVal = objSqlCommand.ExecuteNonQuery();
                objSqlTransaction.Commit();
            }
            catch (Exception)
            {
                if (objSqlTransaction != null)
                    objSqlTransaction.Rollback();
                throw;
                //LogError(this, ex, "Unable to execute the ExcuteNonQuery() !");
            }
            finally
            {
                objMySqlConn.Close();
            }
            return nRetVal;
        }


        /// <summary>
        /// This methods returns no of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="htParameters"></param>
        /// <param name="retunParameter"></param>
        /// <returns>Returns Integer</returns>
        public int ExecuteNonQuery(string commandText, Hashtable htParameters, string retunParameter)
        {
            int nRetVal = 0;
            //SqlTransaction objSqlTransaction = null;
            SqlConnection objMySqlConn = GetSqlConnection();
            if (objMySqlConn == null)
            {
                //LogError(this, null, "Unable to create SqlConnection object ! ");
                return nRetVal;
            }
            try
            {
                var objSqlCommand = new SqlCommand
                {
                    Connection = objMySqlConn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandText,
                    CommandTimeout = 0

                };
                foreach (DictionaryEntry parameter in htParameters)
                {
                    objSqlCommand.Parameters.AddWithValue(parameter.Key.ToString(), parameter.Value);
                }
                objSqlCommand.Parameters.Add(retunParameter, SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                objMySqlConn.Open();
                objSqlCommand.ExecuteNonQuery();
                nRetVal = Convert.ToInt32(objSqlCommand.Parameters[retunParameter].Value);

            }
            finally
            {
                objMySqlConn.Close();
            }
            return nRetVal;
        }

        public Hashtable GetParameters(string strStoreProcedure, string strDBName, string strFields)
        {
            Hashtable htParams = new Hashtable();
            try
            {
                htParams.Add("@dbname", strDBName);
                htParams.Add("@spname", strStoreProcedure);
                htParams.Add("@fields", strFields.TrimEnd(','));
                return htParams;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ExecusteReader
        /// <summary>
        /// This methods returns no of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="htParameters"></param>
        /// <returns>Returns Integer</returns>
        public SqlDataReader ExecuteReader(string commandText, Hashtable htParameters)
        {
            SqlTransaction objSqlTransaction = null;
            SqlConnection objMySqlConn = GetSqlConnection();
            SqlDataReader reader;
            if (objMySqlConn == null)
            {
                //LogError(this, null, "Unable to create SqlConnection object ! ");
                return null;
            }
            try
            {
                var objSqlCommand = new SqlCommand
                {
                    Connection = objMySqlConn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandText,
                    CommandTimeout = 0
                };
                foreach (DictionaryEntry parameter in htParameters)
                {
                    objSqlCommand.Parameters.AddWithValue(parameter.Key.ToString(), parameter.Value);
                }

                objMySqlConn.Open();
                reader = objSqlCommand.ExecuteReader();
            }
            catch (Exception)
            {
                if (objSqlTransaction != null)
                    objSqlTransaction.Rollback();
                throw;
                //LogError(this, ex, "Unable to execute the ExcuteNonQuery() !");
            }
            finally
            {
               // objMySqlConn.Close();
            }
            return reader;
        }
        public SqlDataReader ExecuteReader(string commandText)
        {
            SqlTransaction objSqlTransaction = null;
            SqlConnection objMySqlConn = GetSqlConnection();
            SqlDataReader reader;
            if (objMySqlConn == null)
            {
                //LogError(this, null, "Unable to create SqlConnection object ! ");
                return null;
            }
            try
            {
                var objSqlCommand = new SqlCommand
                {
                    Connection = objMySqlConn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandText,
                    CommandTimeout = 0
                };
                objMySqlConn.Open();
                reader = objSqlCommand.ExecuteReader();
            }
            catch (Exception)
            {
                if (objSqlTransaction != null)
                    objSqlTransaction.Rollback();
                throw;
                //LogError(this, ex, "Unable to execute the ExcuteNonQuery() !");
            }
            finally
            {
                // objMySqlConn.Close();
            }
            return reader;
        }
        #endregion ExecuteReader

    }
}