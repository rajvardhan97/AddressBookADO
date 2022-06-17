using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AddressBookADO
{
    public class AddressBook
    {
        public static string connection = @"Data Source = RAJVARDHAN; Initial Catalog = AddressBook_Service; Integrated Security=SSPI";
        SqlConnection sqlConnection = new SqlConnection(connection);
        
        public void SetConnection()
        {
            if (sqlConnection != null && sqlConnection.State.Equals(ConnectionState.Closed))
            {
                try
                {
                    sqlConnection.Open();
                }
                catch (Exception)
                {
                    throw new CustomException(CustomException.ExceptionType.Connection_Failed, "Connection Failed");
                }
            }
        }
        public void Close()
        {
            if (sqlConnection != null && !sqlConnection.State.Equals(ConnectionState.Open))
            {
                try
                {
                    sqlConnection.Close();
                }
                catch (Exception)
                {
                    throw new CustomException(CustomException.ExceptionType.Connection_Failed, "Connection Failed");
                }
            }
        }

        
        public int InsertIntoTable(Contact contact)
        {
            SetConnection();
            int result = 0;
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertData", this.sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@FirstName", contact.Firstname);
                    sqlCommand.Parameters.AddWithValue("@LastName", contact.Lastname);
                    sqlCommand.Parameters.AddWithValue("@Address", contact.Address);
                    sqlCommand.Parameters.AddWithValue("@City", contact.City);
                    sqlCommand.Parameters.AddWithValue("@State", contact.State);
                    sqlCommand.Parameters.AddWithValue("@zip", contact.Zip);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Email", contact.Email);
                    sqlCommand.Parameters.AddWithValue("@Type", contact.Type);
                    

                    result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("Updated");
                    }
                    else
                    {
                        Console.WriteLine("Not Updated");
                    }   
                }
            }
            catch (Exception)
            {
                throw new CustomException(CustomException.ExceptionType.No_data_found, "No data found");
            }
            sqlConnection.Close();
            return result;
        }

    }
}
