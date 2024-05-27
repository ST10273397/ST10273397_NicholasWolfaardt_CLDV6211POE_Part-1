using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CLDV_POE.Models
{
    public class ProductTable
    {
        // Connection string to connect to the database
        public static string con_string = "Server=tcp:st10273397server.database.windows.net,1433;Initial Catalog=CLDVPOEDatabase;Persist Security Info=False;User ID=NicWolf;Password=Zombeanie49;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        // Properties to represent product data
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductCategory { get; set; }
        public int ProductAvailability { get; set; }

        // Method to insert product data into the database
        public int insert_product(ProductTable p)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    string sql = "INSERT INTO ProductTable (ProductName, ProductPrice, ProductCategory, ProductAvailability) VALUES (@ProductName, @ProductPrice, @ProductCategory, @ProductAvailability)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
                    cmd.Parameters.AddWithValue("@ProductPrice", p.ProductPrice);
                    cmd.Parameters.AddWithValue("@ProductCategory", p.ProductCategory);
                    cmd.Parameters.AddWithValue("@ProductAvailability", p.ProductAvailability);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or handle it appropriately
                // For now, rethrow the exception
                if (ex.Number == 2601 || ex.Number == 2627) // Unique constraint violation
                {
                    // Handle duplicate entry error
                    return -2;
                }
                else
                {
                    // Handle other SQL exceptions
                    return -1;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, return a generic error code for other exceptions
                return -1;
            }
        }

        // Method to retrieve all products from the database
        public static List<ProductTable> GetAllProducts()
        {
            List<ProductTable> products = new List<ProductTable>();

            try
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    string sql = "SELECT * FROM ProductTable";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        ProductTable product = new ProductTable();
                        product.ProductID = Convert.ToInt32(rdr["ProductId"]);
                        product.ProductName = rdr["ProductName"].ToString();
                        product.ProductPrice = Convert.ToInt32(rdr["ProductPrice"]);
                        product.ProductCategory = rdr["ProductCategory"].ToString();
                        product.ProductAvailability = Convert.ToInt32(rdr["ProductAvailability"]);
                        products.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, return an empty list
                // You may modify this to throw an exception or return an error message
                Console.WriteLine("An error occurred while fetching products: " + ex.Message);
            }

            return products;
        }
    }
}
