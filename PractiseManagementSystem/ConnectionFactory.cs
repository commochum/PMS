using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiseManagementSystem
{
    class ConnectionFactory
    {
        SqlConnection con;
        String dbMessage;
        SqlCommand sqlCommand;

        public void ConnectDB()
        {
            try { 
            String conString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            con = new SqlConnection(conString);
                con.Open();
            } catch(SqlException se)
            {
                Console.WriteLine("Encountered SQL Exception while connecting to Database!", se.Message);
            } catch(Exception e)
            {
                Console.WriteLine("Exception occured while connecting to Database!", e.Message);
            }
        }

        public string createDataIntoDB(string sqlQuery)
        {

            ConnectDB();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, con);
                //sqlCommand.Connection.Open();
                int rowCount = sqlCommand.ExecuteNonQuery();
                if(rowCount > 0)
                {
                    dbMessage = " record has been successfully created!";
                } else
                {
                    dbMessage = " record creation failed! Please verify patient details.";
                }
                
            }
            catch(SqlException se)
            {
                dbMessage = " encountered SQL Exception while creating data into Database! " + se.Message;

            } catch(Exception e)
            {
                dbMessage = " exception occurred while creating data! " + e.Message;
            }
            finally {
                con.Close();
            }
            return dbMessage;
        }

        internal string executeTransactionIntoDB(string sqlQuery1, string sqlQuery2)
        {
                ConnectDB();
                SqlCommand command = con.CreateCommand();
                SqlTransaction transaction;
                // Start a local transaction.
                transaction = con.BeginTransaction();

                // Assign both transaction object and connection to Command object for a pending local transaction
                command.Connection = con;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                        sqlQuery1;
                    command.ExecuteNonQuery();
                    command.CommandText =
                        sqlQuery2;
                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    dbMessage = " has been successful!";
                }
                catch (Exception ex)
                {
                    dbMessage = "Commit Exception Type: " +  ex.GetType() + "  Message: " + ex.Message;

                    // Attempting to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                    //Performed rollback when failed
                    dbMessage = "Rollback Exception Type: "+ ex2.GetType() +"  Message: " + ex2.Message;
                    }
                finally
                {
                    
                    con.Close();
                }
            }
            return dbMessage;
        }

        public string updateDataIntoDB(string sqlQuery)
        {
            try
            {
                ConnectDB();
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, con);
                //sqlCommand.Connection.Open();
                int rowCount = sqlCommand.ExecuteNonQuery();
                if (rowCount > 0)
                {
                    dbMessage = " has been successfully updated!";
                }
                else
                {
                    dbMessage = " update failed! Please verify details.";
                }
                
            }
            catch (SqlException se)
            {
                dbMessage = " encountered SQL Exception while updating data into Database! " + se.Message;

            }
            catch (Exception e)
            {
                dbMessage = " exception occurred while updating data! " + e.Message;
            }
            finally
            {
                con.Close();
            }
            return dbMessage;
        }

        public Boolean isReadDataFromDB(string sqlQuery)
        {
            SqlDataReader dr = null;
            Boolean hasRows = false;

            ConnectDB();
            sqlCommand = new SqlCommand(sqlQuery, con);
            
            try {
               // sqlCommand.Connection.Open();
                dr = sqlCommand.ExecuteReader();
                if(dr.HasRows)
                {
                    hasRows = true;
                }
                
            }
            catch (SqlException se)
            {
                Console.WriteLine("Encountered SQL Exception while reading the data into Database!", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while reading the data!", e.Message);
            }
            finally
            {
                con.Close();
            }
            return hasRows;
        }

        public DataSet retrieveDataFromDB(string sqlQuery)
        {
            DataSet dt = new DataSet();

            ConnectDB();
           // sqlCommand = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, con);

                try
                {
                    da.Fill(dt);
                // dsStudents.Tables[0].TableName = "Students";
                //sqlCommand.Connection.Open();
                //dr = sqlCommand.ExecuteReader();
                //if (dr.HasRows)
                //{
                //    dbMessage = "Record successfully retrieved!";
                //}
                //else
                //{
                //    dbMessage = "Failed to retrieve records! Please try again!";
                //}

                }
            catch (SqlException se)
            {
                Console.WriteLine("Encountered SQL Exception while retrieving the data into Database!", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while retrieving the data!", e.Message);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable populateDataFromDB(string sqlQuery)
        {
            DataTable dataTable = new DataTable();

            try
            {
                ConnectDB();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, con);
                sqlDataAdapter.Fill(dataTable);

                sqlDataAdapter.Dispose();

            }
            catch (SqlException se)
            {
                Console.WriteLine("Encountered SQL Exception while populating data into Database!", se.Message);
            }
            catch(InvalidOperationException ioe)
            {
                Console.WriteLine("Encountered Invalid Operation Exception while populating data into Database!", ioe.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while populating data!", e.Message);
            }
            finally
            {   
                con.Close();
            }
            return dataTable;
        }

        public string deleteDataFromDB(string sqlQuery, string name)
        {
            ConnectDB();
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, con);
            try {
                //sqlCommand.Connection.Open();
                int rowCount = Convert.ToInt32(sqlCommand.ExecuteNonQuery());
                Console.WriteLine("Exception occurred while populating data!", +rowCount);
                if (rowCount == 1)
                {
                    dbMessage = name + " record has been successfully deleted!";
                }
                else
                {
                    dbMessage = "Failed to delete " + name +"! Please try again!";
                }
                
            }
            catch(SqlException sqlex)
            {
                Console.WriteLine("Encountered SQL Exception while deleting data!", sqlex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while deleting data", e.Message);
            }
            finally
            {
                con.Close();
            }
            return dbMessage;
        }

        public int countDataFromDB(string sqlQuery)
        {
            Int32 count = 0;
            ConnectDB();
            sqlCommand = new SqlCommand(sqlQuery, con);
            try
            {
               // sqlCommand.Connection.Open();
                count = (Int32)sqlCommand.ExecuteScalar();
                
            }
            catch (SqlException se)
            {
                Console.WriteLine("Encountered SQL Exception while reading data into Database!", se.Message);
            }
            catch (SystemException sysex)
            {
                Console.WriteLine("Encountered SQL Exception while reading data into Database!", sysex.Message);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while reading data!", e.StackTrace);
            }
            finally
            {

                con.Close();
            }
            return (int)count;
        }

        //public List<string> PopulateList(string sqlQuery)
        //{
        //    List<string> list = new List<string>();
        //    ConnectDB();
        //    sqlCommand = new SqlCommand(sqlQuery, con);

        //    try
        //    {
        //        sqlCommand.Connection.Open();
        //        SqlDataReader dr = sqlCommand.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                list.Add(dr[0].ToString());
        //            }
        //        }
        //        
        //    }
        //    catch (SqlException se)
        //    {
        //        Console.WriteLine("Encountered SQL Exception while populating list into Database!", se.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Exception occurred while populating list!", e.StackTrace);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return list;
        //}

        public Dictionary<string, string> SelectFromDB(string queryStr)
        {
            ConnectDB();
            SqlCommand sqlCommand = new SqlCommand(queryStr, con);
            List<string> patientNameList = new List<string>();
            Dictionary<string, string> newMap = new Dictionary<string, string>();

            string name = null;

            using (IDataReader dr = sqlCommand.ExecuteReader())
            {
                List<string> values = new List<string>();

                while (dr.Read())
                {
                    name = dr[1].ToString().Trim().Substring(9, (dr[1].ToString().Length-9));
                    newMap.Add(dr[0].ToString(), name);
                }
                return newMap;
            }
        }

        public Dictionary<string, List<string>> SelectFromDBValues(string queryStr, Dictionary<string, List<string>> dMap)
        {
            ConnectDB();
            SqlCommand sqlCommand = new SqlCommand(queryStr, con);
            sqlCommand.Connection.Open();

            Dictionary<string, List<string>> newMap = new Dictionary<string, List<string>>();
            using (IDataReader dr = sqlCommand.ExecuteReader())
            {
                List<string> values = new List<string>();
                List<string> patientNameList = new List<string>();

                while (dr.Read())
                {
                   
                    //List<string> patientValues = new List<string>();

                    string patientNameValue = null;
                    string patientMedicareNoValue = null;
                    string patientContactValue = null;
                    string patientEmailAddValue = null;


                    values.Add(dr[1].ToString());
                    values.Add(dr[2].ToString());
                    values.Add(dr[3].ToString());
                    values.Add(dr[4].ToString());
                    values.Add(dr[5].ToString());
                    dMap.Add(dr[0].ToString(), values);

                    foreach (KeyValuePair<string, List<string>> keyValuePair in dMap)
                    {
                        int index = 0;
                        patientNameList.Clear();
                        patientNameValue = null;

                        foreach (string value in keyValuePair.Value)
                        {
                            ++index;
                            if (index <= 2)
                            {
                                patientNameValue += " " + value;
                                // MessageBox.Show("value index ------>" +index);
                            }
                            Console.WriteLine("index " + index);
                            if (index == 3)
                            {
                                patientMedicareNoValue = value;
                                Console.WriteLine("3  " + patientMedicareNoValue);
                            }
                            if (index == 4)
                            {
                                patientContactValue = value;
                                Console.WriteLine("4  " + patientContactValue);
                            }
                            if (index == 5)
                            {
                                patientEmailAddValue = value;
                                Console.WriteLine("5  " + patientEmailAddValue);
                                // index = 0;
                                break;
                            }
                            patientNameList.Add(patientNameValue);
                            patientNameList.Add(patientMedicareNoValue);
                            patientNameList.Add(patientContactValue);
                            patientNameList.Add(patientEmailAddValue);
                        }

                        newMap.Add(patientNameValue, patientNameList);
                    }
                  }
            }
            
            return dMap;

        }
        

    }
}
