using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace CLDV_POE.Models
{
    public class LoginModel
    {
        // Connection string to connect to the database
        public static string con_string = "Server=tcp:st10273397server.database.windows.net,1433;Initial Catalog=CLDVPOEDatabase;Persist Security Info=False;User ID=NicWolf;Password=Zombeanie49;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        // Method to select user from the database based on email and password
        public int SelectUser(string email, string password)
        {
            int userID = -1;
            try
            {
                // Create a SqlConnection object within a using statement to ensure disposal
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    // SQL query to select user from userTable based on email and password
                    string sql = "SELECT userID FROM userTable WHERE UserPassword = @Password AND UserEmail = @Email";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Email", email);

                    // Open the SqlConnection
                    con.Open();

                    // Execute the SqlCommand to retrieve data
                    object result = cmd.ExecuteScalar();

                    // Check if a result is returned and convert it to int
                    if (result != null && result != DBNull.Value)
                    {
                        userID = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, return -1 to indicate an error
                Console.WriteLine("An error occurred while selecting user: " + ex.Message);
                userID = -1;
            }

            return userID;
        }
    }
}
