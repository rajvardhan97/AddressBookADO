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
        public Contact InsertIntoTable(Contact contact)
        {
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertData", sqlConnection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    
                    sqlCommand.Parameters.AddWithValue("@FirstName", contact.Firstname);
                    sqlCommand.Parameters.AddWithValue("@LastName", contact.Lastname);
                    sqlCommand.Parameters.AddWithValue("@Address", contact.Address);
                    sqlCommand.Parameters.AddWithValue("@City", contact.City);
                    sqlCommand.Parameters.AddWithValue("@State", contact.State);
                    sqlCommand.Parameters.AddWithValue("@zip", contact.Zip);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Email", contact.Email);
                    sqlCommand.Parameters.AddWithValue("@Type", contact.Type);
                    
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception)
            {
                throw new CustomException(CustomException.ExceptionType.No_data_found, "No data found");
            }
            
            return contact;
        }

        public Contact UpdateByName(Contact contact)
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.UpdateData", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Firstname", contact.Firstname);
                    sqlCommand.Parameters.AddWithValue("@Lastname", contact.Lastname);
                    sqlCommand.Parameters.AddWithValue("PhoneNumber", contact.PhoneNumber);
                    contact = new Contact();
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            contact.Firstname = (string)sqlDataReader["Firstname"];
                        }
                    }
                    sqlConnection.Close();
                    return contact;
                }
            }
            catch(Exception)
            {
                throw new CustomException(CustomException.ExceptionType.No_data_found, "No data found");
            }
        }

        public bool Delete(Contact contact)
        {
            SqlCommand sqlCommand = new SqlCommand("dbo.DeleteData", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlConnection.Open();
                using(sqlConnection)
                {
                    sqlCommand.Parameters.AddWithValue("@Firstname", contact.Firstname);
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            catch(Exception)
            {
                throw new CustomException(CustomException.ExceptionType.No_data_found, "No data found");
            }
            return true;
        }

        public string PrintDataBasedOnCity(string City, string State)
        {
            string nameList = "";
            string query = @"select * from AddressBook_Table where City =" +  City + " or State=" + State;
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    DisplayEmployeeDetails(sqlDataReader);
                    nameList += sqlDataReader["FirstName"].ToString() + " ";
                }
            }
            return nameList;
        }
        Contact Contact = new Contact();
        public void DisplayEmployeeDetails(SqlDataReader sqlDataReader)
        {

            Contact.Firstname = Convert.ToString(sqlDataReader["FirstName"]);
            Contact.Lastname = Convert.ToString(sqlDataReader["LastName"]);
            Contact.Address = Convert.ToString(sqlDataReader["Address"] + " " + sqlDataReader["City"] + " " + sqlDataReader["State"] + " " + sqlDataReader["zip"]);
            Contact.PhoneNumber = Convert.ToInt64(sqlDataReader["PhoneNumber"]);
            Contact.Email = Convert.ToString(sqlDataReader["email"]);
            Contact.Zip = Convert.ToInt64(sqlDataReader["Zip"]);
            Contact.Type = Convert.ToString(sqlDataReader["Type"]);
            Console.WriteLine("{0} \n {1} \n {2} \n {3} \n {4} \n {5} \n {6}", Contact.Firstname, Contact.Lastname, Contact.Address, Contact.PhoneNumber, Contact.Email, Contact.Zip, Contact.Type);

        }

        public string PrintCountBasedOnCityandState()
        {
            string nameList = "";
            string query = @"Select Count(*),State,City from AddressBook_Table Group by State,City";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Console.WriteLine("{0} \t {1} \t {2}", sqlDataReader[0], sqlDataReader[1], sqlDataReader[2]);
                    nameList += sqlDataReader[0].ToString() + " ";
                }
            }
            return nameList;
        }
    }
}
