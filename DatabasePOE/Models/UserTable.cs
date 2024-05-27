using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace CLDV_POE.Models
{
    public class UserTable
    {
        // Connection string to connect to the database
        public static string con_string = "Server=tcp:st10273397server.database.windows.net,1433;Initial Catalog=CLDVPOEDatabase;Persist Security Info=False;User ID=NicWolf;Password=Zombeanie49;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        // SqlConnection object to manage database connection
        public static SqlConnection con = new SqlConnection(con_string);

        // Properties to represent user data
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        // Method to insert user data into the database
        public int insert_User(UserTable table)
        {
            try
            {
                // SQL query to insert user data into the UserTable
                string sql = "INSERT INTO UserTable (UserName, UserSurname, UserPassword, UserEmail) VALUES (@Name, @Surname, @Password, @Email)";

                // Create a SqlCommand object with the SQL query and SqlConnection
                SqlCommand cmd = new SqlCommand(sql, con);

                // Add parameters to the SqlCommand for user data
                cmd.Parameters.AddWithValue("@Name", table.Name);
                cmd.Parameters.AddWithValue("@Surname", table.Surname);
                cmd.Parameters.AddWithValue("@Password", table.Password);
                cmd.Parameters.AddWithValue("@Email", table.Email);

                // Open the SqlConnection
                con.Open();

                // Execute the SqlCommand to insert the record into the UserTable
                int rowsAffected = cmd.ExecuteNonQuery();

                // Close the SqlConnection
                con.Close();

                // Return the number of rows affected by the insert operation
                return rowsAffected;
            }
            catch (SqlException ex)
            {
                // Log the exception or handle it appropriately
                // For now, return an error code or message based on the exception
                if (ex.Number == 2601 || ex.Number == 2627) // Unique constraint violation
                {
                    return -2; // Return a custom error code indicating duplicate entry
                }
                else
                {
                    return -1; // Return a generic error code for other SQL exceptions
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, return a generic error code for other exceptions
                return -1;
            }
            finally
            {
                // Ensure the SqlConnection is closed in case of an exception
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}
