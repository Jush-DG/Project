using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class ArchivedProductsForm : Form
    {
        public ArchivedProductsForm()
        {
            InitializeComponent();
        }
        private SqlConnection connectionString = new SqlConnection(@"Data Source=LAPTOP-DVKO92VB\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;");
       
        private void DisplayArchivedItems()
        {
            try
            {
                if (connectionString.State == ConnectionState.Closed)
                {
                    connectionString.Open();
                }

                string query = "SELECT * FROM ProductTable WHERE ArchiveStatus = 'Archived'";

                using (SqlCommand cmd = new SqlCommand(query, connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dtArchivedProducts.DataSource = dataTable;
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
        private void ArchivedProductsForm_Load(object sender, EventArgs e)
        {
           DisplayArchivedItems();
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            ArchiveAndUnarchiveClass Unarchive = new ArchiveAndUnarchiveClass();
            try
            {
                if (dtArchivedProducts.SelectedRows.Count > 0)
                {
                    int productID = Convert.ToInt32(dtArchivedProducts.SelectedRows[0].Cells["ProductID"].Value);

                    DialogResult result = MessageBox.Show($"Are you sure you want to unarchive the product with ID: {productID}?",
                                                          "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Unarchive.UnarchiveProduct(productID);
                        DisplayArchivedItems();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a product to unarchive.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while unarchiving: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ArchiveAndUnarchiveClass Delete = new ArchiveAndUnarchiveClass();
            try
            {
                if (dtArchivedProducts.SelectedRows.Count > 0)
                {
                    int productID = Convert.ToInt32(dtArchivedProducts.SelectedRows[0].Cells["ProductID"].Value);

                    DialogResult result = MessageBox.Show($"Are you sure you want todelete the product with ID: {productID}?",
                                                          "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Delete.DeleteProduct(productID);
                        DisplayArchivedItems();

                    }
                }
                else
                {
                    MessageBox.Show("Please select a product to unarchive.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while unarchiving: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
