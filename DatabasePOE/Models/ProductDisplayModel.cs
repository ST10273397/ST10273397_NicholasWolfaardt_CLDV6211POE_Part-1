using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CLDV_POE.Models
{
    public class ProductDisplayModel
    {
        // Properties to represent product data
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string ProductCategory { get; set; }
        public bool ProductAvailability { get; set; }

        // Default constructor
        public ProductDisplayModel() { }

        // Parameterized constructor
        public ProductDisplayModel(int productID, string productName, decimal productPrice, string productCategory, bool productAvailability)
        {
            ProductID = productID;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductCategory = productCategory;
            ProductAvailability = productAvailability;
        }

        // Method to select all products from the database
        public static List<ProductDisplayModel> SelectProducts()
        {
            List<ProductDisplayModel> products = new List<ProductDisplayModel>();

            try
            {
                // Connection string to connect to the database
                string con_string = "Server=tcp:st10273397server.database.windows.net,1433;Initial Catalog=CLDVPOEDatabase;Persist Security Info=False;User ID=NicWolf;Password=Zombeanie49;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

                // Create a SqlConnection object within a using statement to ensure disposal
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    // SQL query to select all products from the ProductTable
                    string sql = "SELECT ProductID, ProductName, ProductPrice, ProductCategory, ProductAvailability FROM ProductTable";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();

                    // Execute the SqlCommand to retrieve data
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through the SqlDataReader to populate the products list
                    while (reader.Read())
                    {
                        ProductDisplayModel model = new ProductDisplayModel();
                        model.ProductID = Convert.ToInt32(reader["ProductID"]);
                        model.ProductName = Convert.ToString(reader["ProductName"]);
                        model.ProductPrice = Convert.ToDecimal(reader["ProductPrice"]);
                        model.ProductCategory = Convert.ToString(reader["ProductCategory"]);
                        model.ProductAvailability = Convert.ToBoolean(reader["ProductAvailability"]);
                        products.Add(model);
                    }

                    // Close the SqlDataReader
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, return an empty list
                Console.WriteLine("An error occurred while fetching products: " + ex.Message);
            }

            return products;
        }
    }
}
