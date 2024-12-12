using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Inventory
{
    internal class AdminAddProductClass
    {
        private readonly string connectionString = @"Data Source=LAPTOP-DVKO92VB\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;";

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public float ProductPrice { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public List<AdminAddProductClass> ShowProductList()
        {
            List<AdminAddProductClass> productList = new List<AdminAddProductClass>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT ProductID, ProductName, Category, Price, ProductImage, Description FROM ProductTable WHERE ArchiveStatus = 'Active'";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                              
                                AdminAddProductClass product = new AdminAddProductClass
                                {
                                    ProductID = Convert.ToInt32(reader["ProductID"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    ProductCategory = reader["Category"].ToString(),
                                    ProductPrice = Convert.ToSingle(reader["Price"]),
                                    Image = reader["ProductImage"].ToString(),
                                    Description = reader["Description"].ToString()
                                };

                                productList.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching product list: {ex.Message}\n\nStackTrace: {ex.StackTrace}",
                                "Database Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            return productList;
        }
    }
}
