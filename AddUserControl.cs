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
using System.IO;
namespace Inventory
{
    public partial class AddUserControl : UserControl
    {
        private readonly SqlConnection connectionString = new SqlConnection(@"Data Source=LAPTOP-DVKO92VB\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;");


        public AddUserControl()
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
            AdminAddUserClass adminAddUserClass = new AdminAddUserClass();
            List<AdminAddUserClass> listData = adminAddUserClass.UserListData();

            dtDataUsers.DataSource = listData;
        }

        public bool EmptyFields()
        { 
            if (txtPassword.Text == "" || txtUserName.Text == "" || cbRole.Text == "" || cbStatus.Text == "")
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
            txtUserName.Clear();     
            txtPassword.Clear();     
            cbRole.SelectedIndex = -1; 
            cbStatus.SelectedIndex = -1; 
            pictureBox1.Image = null;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (EmptyFields())
            {
                MessageBox.Show("Complete all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connectionString.State == ConnectionState.Closed )
                {
                    try
                    {
                        connectionString.Open();
                        string selectUsername = "SELECT * FROM UserAccounts WHERE userName = @username";
                        using (SqlCommand checkUser = new SqlCommand(selectUsername, connectionString))
                        {
                            checkUser.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(checkUser);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            if (dataTable.Rows.Count >= 1)
                            {
                                MessageBox.Show(txtUserName.Text.Trim() + " already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO userAccounts (userName, userPassword, userRole, userStatus, dateRegistered, userPhoto) " +
                                    "VALUES (@username, @password, @role, @status, @registeredDate, @image)";
                                DateTime date = DateTime.Now;

                                string imagePath = pictureBox1.ImageLocation;
                                string path = null;

                                if (!string.IsNullOrEmpty(imagePath)) 
                                {
                                    path = Path.Combine(@"C:\Users\Jush\source\repos\Inventory\Inventory\User Directory\"
                                                         + txtUserName.Text.Trim() + ".jpg");

                                    string directoryPath = Path.GetDirectoryName(path);

                                    if (!Directory.Exists(directoryPath))
                                    {
                                        Directory.CreateDirectory(directoryPath);
                                    }

                                    File.Copy(imagePath, path, true); 
                                }
                               
                                using (SqlCommand cmd = new SqlCommand(insertData, connectionString))
                                {
                                    cmd.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
                                    cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                                    cmd.Parameters.AddWithValue("@role", cbRole.Text.Trim());
                                    cmd.Parameters.AddWithValue("@status", cbStatus.Text.Trim());
                                    cmd.Parameters.AddWithValue("@registeredDate", date);
                                    cmd.Parameters.AddWithValue("@image", path ?? (object)DBNull.Value); 

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

        private void btnImport_Click(object sender, EventArgs e)
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
                MessageBox.Show($" Error: {ex.Message}", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int id = 0;
        private void dtDataUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtDataUsers.Rows[e.RowIndex];
            id = (int)row.Cells[0].Value;
            txtUserName.Text = row.Cells[1].Value.ToString();
            txtPassword.Text = row.Cells[2].Value.ToString();
            cbRole.Text = row.Cells[3].Value.ToString();
            cbStatus.Text = row.Cells[4].Value.ToString();

            string imagePath = row.Cells[6].Value.ToString();

           
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                pictureBox1.Image = Image.FromFile(imagePath);
            }
            else
            {
                pictureBox1.Image = null; 
            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (EmptyFields())
            {
                MessageBox.Show("Incomplete fields. Please fill all the fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to proceed?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                                imagePath = Path.Combine(@"C:\Users\Jush\source\repos\Inventory\Inventory\User Directory\", txtUserName.Text.Trim() + ".jpg");
                                string directoryPath = Path.GetDirectoryName(imagePath);

                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                File.Copy(pictureBox1.ImageLocation, imagePath, true);
                            }

                            string updateData = "UPDATE UserAccounts " +
                                                "SET userName = @username, userPassword = @password, userRole = @role, userStatus = @status, userPhoto = @photo " +
                                                "WHERE userID = @ID";

                            using (SqlCommand cmd = new SqlCommand(updateData, connectionString))
                            {
                                cmd.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
                                cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                                cmd.Parameters.AddWithValue("@role", cbRole.Text.Trim());
                                cmd.Parameters.AddWithValue("@status", cbStatus.Text.Trim());
                                cmd.Parameters.AddWithValue("@photo", imagePath ?? (object)DBNull.Value);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            DisplayUsersData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (id == 0) 
            {
                MessageBox.Show("No user selected. Please select a user to delete.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete " + txtUserName.Text.Trim() + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (connectionString.State == ConnectionState.Closed)
                    {
                        connectionString.Open();
                    }

                    string deleteData = "DELETE FROM UserAccounts WHERE userID = @ID";

                    using (SqlCommand cmd = new SqlCommand(deleteData, connectionString))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Deleted Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DisplayUsersData();
                            ClearFields();
                            id = 0; 
                        }
                        else
                        {
                            MessageBox.Show("Deletion failed. Please ensure a valid user is selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
}

