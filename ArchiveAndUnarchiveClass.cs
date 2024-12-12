using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    internal class ArchiveAndUnarchiveClass
    {
        private SqlConnection connectionString = new SqlConnection(@"Data Source=LAPTOP-DVKO92VB\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;");
        public void UnarchiveProduct(int productID)
        {
            try
            {
                if (connectionString.State == ConnectionState.Closed)
                {
                    connectionString.Open();
                }

                string query = "UPDATE ProductTable SET ArchiveStatus = 'Active', TimeArchived = NULL WHERE ProductID = @pID";

                using (SqlCommand cmd = new SqlCommand(query, connectionString))
                {
                    cmd.Parameters.AddWithValue("@pID", productID);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product successfully unarchived.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Unarchive failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connectionString.State == ConnectionState.Open)
                {
                    connectionString.Close();
                }
            }
        }
        public void ArchiveProduct(int productID)
        {
            try
            {
                if (connectionString.State == ConnectionState.Closed)
                {
                    connectionString.Open();
                }

                string query = "UPDATE ProductTable SET ArchiveStatus = 'Archived', TimeArchived = GETDATE() WHERE ProductID = @pID";

                using (SqlCommand cmd = new SqlCommand(query, connectionString))
                {
                    cmd.Parameters.AddWithValue("@pID", productID);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product successfully archived.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Archiving failed. Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connectionString.State == ConnectionState.Open)
                {
                    connectionString.Close();
                }
            }
        }
        public void DeleteProduct(int productID)
        {
            try
            {
                if (connectionString.State == ConnectionState.Closed)
                {
                    connectionString.Open();
                }

                string query = "DELETE FROM ProductTable WHERE ProductID = @pID";

                using (SqlCommand cmd = new SqlCommand(query, connectionString))
                {
                    cmd.Parameters.AddWithValue("@pID", productID);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product successfully deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                    else
                    {
                        MessageBox.Show("Deletion failed. Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connectionString.State == ConnectionState.Open)
                {
                    connectionString.Close();
                }
            }
        }

    }
}
