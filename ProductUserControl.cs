using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Inventory
{
    public partial class ProductUserControl : UserControl
    {
        private SqlConnection connectionString = new SqlConnection(@"Data Source=LAPTOP-DVKO92VB\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;");
        private int id = 0;

        public string CurrentUsername { get; set; }

        public int UserId {  get; set; }
        public ProductUserControl()
        {
            InitializeComponent();
            DisplayUsersData();

        }
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            DisplayUsersData();
        }

        public void DisplayUsersData()
        {
            AdminAddProductClass adminAddProducts = new AdminAddProductClass();
            List<AdminAddProductClass> listData = adminAddProducts.ShowProductList();

            dtProductList.DataSource = listData;
        }
        public bool EmptyFields()
        {
            if (txtProductName.Text == "" || cbCategory.Text == "" || txtPrice.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ClearFields()
        {
            txtDescription.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            cbCategory.SelectedIndex = -1;
            pictureBox1.Image = null;

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (EmptyFields())
            {
                MessageBox.Show("Complete all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                decimal price;
                if (!decimal.TryParse(txtPrice.Text.Trim(), out price))
                {
                    MessageBox.Show("Please enter a valid numeric value for Price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (connectionString.State == ConnectionState.Closed)
                {
                    try
                    {
                        connectionString.Open();
                        string selectProductName = "SELECT * FROM ProductTable WHERE ProductName = @productName";
                        using (SqlCommand checkProduct = new SqlCommand(selectProductName, connectionString))
                        {
                            checkProduct.Parameters.AddWithValue("@productName", txtProductName.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(checkProduct);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            if (dataTable.Rows.Count >= 1)
                            {
                                MessageBox.Show(txtProductName.Text.Trim() + " already exists in the table!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO ProductTable (productName, Price, Category, ProductImage, Description, userName, userID) " +
                                                    "VALUES (@productName, @price, @category, @image, @description, @username, @userId)";

                                string imagePath = pictureBox1.ImageLocation;
                                string path = null;

                                if (!string.IsNullOrEmpty(imagePath))
                                {
                                    path = Path.Combine(@"C:\Users\Jush\source\repos\Inventory\Inventory\User Directory\"
                                                         + txtProductName.Text.Trim() + ".jpg");

                                    string directoryPath = Path.GetDirectoryName(path);

                                    if (!Directory.Exists(directoryPath))
                                    {
                                        Directory.CreateDirectory(directoryPath);
                                    }

                                    File.Copy(imagePath, path, true);
                                }

                                using (SqlCommand cmd = new SqlCommand(insertData, connectionString))
                                {
                                    cmd.Parameters.AddWithValue("@productName", txtProductName.Text.Trim());
                                    cmd.Parameters.AddWithValue("@price", price); 
                                    cmd.Parameters.AddWithValue("@category", cbCategory.Text.Trim());
                                    cmd.Parameters.AddWithValue("@image", path ?? (object)DBNull.Value);
                                    cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                                    cmd.Parameters.AddWithValue("@username", this.CurrentUsername);
                                    cmd.Parameters.AddWithValue("@userId", this.UserId);



                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Added Successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearFields();
                                    DisplayUsersData();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection Failed" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        connectionString.Close();
                    }
                    finally
                    {
                        connectionString.Close();
                    }
                }
            }
        }

        private void ProductUserControl_Load(object sender, EventArgs e)
        {
            DisplayUsersData();

        }
        private void btnImportImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png";
                string imagePath = "";



                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    pictureBox1.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"5 Error: {ex.Message}", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (EmptyFields())
            {
                MessageBox.Show("Incomplete fields. Please fill all the fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to update " + txtProductName.Text.Trim() + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (connectionString.State == ConnectionState.Closed)
                        {
                            connectionString.Open();
                        }

                        if (id > 0)
                        {
                            string imagePath = "";

                            if (pictureBox1.ImageLocation != null)
                            {
                                imagePath = Path.Combine(@"C:\Users\Jush\source\repos\Inventory\Inventory\User Directory\", txtProductName.Text.Trim() + ".jpg");
                                string directoryPath = Path.GetDirectoryName(imagePath);

                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                File.Copy(pictureBox1.ImageLocation, imagePath, true);
                            }

                            string updateData = "UPDATE ProductTable " +
                                                "SET ProductName = @pName, Price = @pPrice, Category = @pCategory, ProductImage = @pImage, Description = @pDescription " +
                                                "WHERE ProductID = @ID";

                            using (SqlCommand cmd = new SqlCommand(updateData, connectionString))
                            {
                                cmd.Parameters.AddWithValue("@pName", txtProductName.Text.Trim());
                                cmd.Parameters.AddWithValue("@pPrice", txtPrice.Text.Trim());
                                cmd.Parameters.AddWithValue("@pCategory", cbCategory.Text.Trim());
                                cmd.Parameters.AddWithValue("@pImage", imagePath ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@pDescription", txtDescription.Text.Trim());
                                cmd.Parameters.AddWithValue("@ID", id);

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DisplayUsersData();
                                    ClearFields();
                                    id = 0;
                                }
                                else
                                {
                                    MessageBox.Show("No record updated. Please ensure the data is valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid row selected. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while updating: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            ArchiveAndUnarchiveClass Archive = new ArchiveAndUnarchiveClass();
            try
            {
                if (dtProductList.SelectedRows.Count > 0)
                {
                    int productID = Convert.ToInt32(dtProductList.SelectedRows[0].Cells["ProductID"].Value);

                    DialogResult result = MessageBox.Show($"Are you sure you want to archive the product with ID: {productID}?",
                                                          "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Archive.ArchiveProduct(productID); 
                        DisplayUsersData();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a product to archive.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while archiving: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowArchive_Click(object sender, EventArgs e)
        {
            ArchivedProductsForm form = new ArchivedProductsForm();
            form.ShowDialog();

        }

        private void dtProductList_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dtProductList.Rows[e.RowIndex];
                id = (int)row.Cells[0].Value;
                txtProductName.Text = row.Cells[1].Value.ToString();
                txtPrice.Text = row.Cells[3].Value.ToString();
                cbCategory.Text = row.Cells[2].Value.ToString();
                string imagePath = row.Cells[4].Value.ToString();
                txtDescription.Text = row.Cells[5].Value.ToString();


                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("6 Error Message: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            DisplayUsersData();
            ClearFields();
        }
    }
}
