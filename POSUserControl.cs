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
using System.Drawing.Printing;
namespace Inventory
{
    public partial class POSUserControl : UserControl
    {
        public string CurrentUsername { get; set; }

        public int UserId { get; set; }
        public POSUserControl()
        {
            InitializeComponent();

            LoadCategories();
            ShowAvailableProducts();
            DisplayTotalPrice();

        }
        public static int getCustomerID;
        readonly SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-DVKO92VB\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;");

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            LoadCategories();
            ShowAvailableProducts();
            DisplayTotalPrice();
        }
        public void ShowAvailableProducts()
        {
            CashierOrderForm showAvailable = new CashierOrderForm();
            List<CashierOrderForm> listData = showAvailable.ShowMenu();
            dtMenu.DataSource = listData;
            
        }
        private void DisplayOrder()
        {
            CashierOrdersClass Orders = new CashierOrdersClass();

            List<CashierOrdersClass> listData = Orders.OrderListData();

            dtCart.DataSource = listData;
        }

        private float TotalPrice;
        private void DisplayTotalPrice()
        {
            TotalPrice = 0; 

            foreach (DataGridViewRow row in dtCart.Rows)
            {
                if (row.Cells["Price"].Value != null && row.Cells["Quantity"].Value != null)
                {
                    float price = Convert.ToSingle(row.Cells["Price"].Value);
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    TotalPrice += price * quantity;
                }
            }

            lblTotalPrice.Text = TotalPrice.ToString("F2"); 
        }


        private int generateID = 0;
        private void IdGenerator()
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }

                string selectID = "SELECT MAX(CustomerID) FROM TransactionTable";

                using (SqlCommand cmd = new SqlCommand(selectID, Connection))
                {
                    object result = cmd.ExecuteScalar();

                    
                    if (result == null || result == DBNull.Value)
                    {
                        generateID = 1; 
                    }
                    else
                    {
                        generateID = Convert.ToInt32(result) + 1;
                    }
                    getCustomerID = generateID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                IdGenerator();

                if (string.IsNullOrEmpty(lblProdID.Text) ||
                    string.IsNullOrEmpty(lblProductName.Text) ||
                    string.IsNullOrEmpty(lblPrice.Text) ||
                     txtQuanity.Value == 0)
                {
                    MessageBox.Show("Please select a product and enter a valid quantity", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }


                string insertOrder = "INSERT INTO Orders (CustomerID, ProductID, ProductName, Category, Price, OrderQuantity, OrderDate, userName) " +
                                      "VALUES (@customerID, @prodID, @prodName, @category, @price, @quantity, @orderDate, @username)";

                DateTime today = DateTime.Now;

                using (SqlCommand cmd = new SqlCommand(insertOrder, Connection))
                {
                    cmd.Parameters.AddWithValue("@customerID", generateID);
                    cmd.Parameters.AddWithValue("@prodID", lblProdID.Text.Trim());
                    cmd.Parameters.AddWithValue("@prodName", lblProductName.Text.Trim());
                    cmd.Parameters.AddWithValue("@category", cbCategory.Text.Trim());
                    cmd.Parameters.AddWithValue("@price", lblPrice.Text.Trim());
                    cmd.Parameters.AddWithValue("@quantity", txtQuanity.Value);
                    cmd.Parameters.AddWithValue("@orderDate", today);
                    cmd.Parameters.AddWithValue("@username", CurrentUsername);


                    cmd.ExecuteNonQuery();

                    
                    DisplayOrder();
                    DisplayTotalPrice();
                }


                MessageBox.Show("Order added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbProductName.Items.Clear();

            string selectedValue = cbCategory.SelectedItem as string;

            try
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }

                if (!string.IsNullOrEmpty(selectedValue))
                {
                    string selectData = "SELECT ProductName FROM ProductTable WHERE Category = @Category AND ArchiveStatus = 'Active' ORDER BY ProductName";
                    using (SqlCommand cmd = new SqlCommand(selectData, Connection))
                    {
                        cmd.Parameters.AddWithValue("@Category", selectedValue);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string productName = reader["ProductName"].ToString();
                                cbProductName.Items.Add(productName);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }
        private void cbProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedProductName = cbProductName.SelectedItem as string;

            try
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }

                if (!string.IsNullOrEmpty(selectedProductName))
                {
                    string selectData = "SELECT ProductID, Price FROM ProductTable WHERE ProductName = @ProductName AND ArchiveStatus = 'Active'";
                    using (SqlCommand cmd = new SqlCommand(selectData, Connection))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", selectedProductName);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblProdID.Text = reader["ProductID"].ToString(); 
                                lblPrice.Text = reader["Price"].ToString();      
                                lblProductName.Text = selectedProductName.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        private void LoadCategories()
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string query = "SELECT DISTINCT Category FROM ProductTable WHERE ArchiveStatus = 'Active'";
                using (SqlCommand cmd = new SqlCommand(query, Connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cbCategory.Items.Clear(); 

                        while (reader.Read())
                        {
                            string category = reader["Category"].ToString();
                            cbCategory.Items.Add(category);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }
       
        private void txtAmountPaid_TextChanged(object sender, EventArgs e)
        {
            IdGenerator();

           
        }

        private void txtAmountPaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    float getAmount = Convert.ToSingle(txtAmountPaid.Text);

                    float getChange = (getAmount - TotalPrice);

                    if (getChange <= -1)
                    {
                        MessageBox.Show("Input right amount!","Invalid Input",MessageBoxButtons.OK);
                    }
                    else
                    {
                        lblChange.Text = getChange.ToString() + ".00";

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;

            if (string.IsNullOrWhiteSpace(txtAmountPaid.Text) || dtCart.Rows.Count <= 0)
            {
                MessageBox.Show("Please enter the amount paid and ensure the cart is not empty.",
                                "Error Message",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Do you want to proceed?",
                                    "Confirmation",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (Connection.State == ConnectionState.Closed)
                        {
                            Connection.Open();
                        }

                        transaction = Connection.BeginTransaction();

                        string insertData = "INSERT INTO TransactionTable (CustomerID, TotalPrice, TotalAmount, CustomerChange, Date, userName, userID) " +
                                            "VALUES (@custID, @totalPrice, @totalAmount, @custChange, @date, @username, @userId)";

                        using (SqlCommand cmd = new SqlCommand(insertData, Connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@custID", generateID);
                            cmd.Parameters.AddWithValue("@totalPrice", TotalPrice);
                            cmd.Parameters.AddWithValue("@totalAmount", txtAmountPaid.Text);
                            cmd.Parameters.AddWithValue("@custChange", lblChange.Text);
                            cmd.Parameters.AddWithValue("@date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@username",CurrentUsername);
                            cmd.Parameters.AddWithValue("@userId", this.UserId);



                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        MessageBox.Show("Paid Successfully!",
                                        "Information Message",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        dtCart.DataSource = null;
                        lblTotalPrice.Text = "0.00";
                        txtAmountPaid.Text = "";
                        lblChange.Text = "0.00";
                    }
                    catch (Exception ex)
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                        }

                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (Connection.State == ConnectionState.Open)
                        {
                            Connection.Close();
                        }
                    }
                }
            }
        }

        private int rowIndex = 0;


        private void btnReceipt_Click(object sender, EventArgs e)
        {
            PrintReceipt.PrintPage += new PrintPageEventHandler(PrintReceipt_PrintPage);
            PrintReceipt.BeginPrint += new PrintEventHandler(PrintReceipt_BeginPrint);

            printPreviewDialog1.Document = PrintReceipt;
            printPreviewDialog1.ShowDialog();
        }

        private void PrintReceipt_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            rowIndex = 0;


        }


        private void PrintReceipt_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            DisplayTotalPrice();

            Font titleFont = new Font("Arial", 18, FontStyle.Bold);
            Font sectionHeaderFont = new Font("Arial", 14, FontStyle.Bold);
            Font textFont = new Font("Arial", 12);
            Font labelFont = new Font("Arial", 12, FontStyle.Bold);

            float y = e.MarginBounds.Top;
            float x = e.MarginBounds.Left;

            // Alignments
            StringFormat alignCenter = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat alignLeft = new StringFormat { Alignment = StringAlignment.Near };

            // Header
            string shopName = "                             Been Cafe";
            float shopNameWidth = e.Graphics.MeasureString(shopName, titleFont).Width;
            e.Graphics.DrawString(shopName, titleFont, Brushes.Black, (e.MarginBounds.Width / 2) - (shopNameWidth / 2), y);
            y += titleFont.GetHeight(e.Graphics) + 10;

            // Date and Time
            DateTime currentDateTime = DateTime.Now;
            e.Graphics.DrawString($"Date: {currentDateTime.ToShortDateString()}   Time: {currentDateTime.ToShortTimeString()}",
                                  textFont, Brushes.Black, x, y);
            y += textFont.GetHeight(e.Graphics) + 10;

            // Current Username 
            e.Graphics.DrawString($"Transaction By: {CurrentUsername}", textFont, Brushes.Black, x, y);
            y += textFont.GetHeight(e.Graphics) + 20;

            // Table Header
            string[] headers = { "CustomerID", "ProductName", "Category", "Quantity", "Price" };
            float[] columnWidths = { 125, 150, 125, 100, 100 };
            float currentX = x;

            for (int i = 0; i < headers.Length; i++)
            {
                e.Graphics.DrawString(headers[i], sectionHeaderFont, Brushes.Black, currentX, y, alignLeft);
                currentX += columnWidths[i] + 10;
            }
            y += sectionHeaderFont.GetHeight(e.Graphics) + 10;

            // Table Rows
            while (rowIndex < dtCart.Rows.Count)
            {
                DataGridViewRow row = dtCart.Rows[rowIndex];
                currentX = x;

                for (int i = 0; i < headers.Length; i++)
                {
                    object cellValue = row.Cells[i + (i >= 1 ? 1 : 0)].Value;
                    string cellText = cellValue != null ? cellValue.ToString() : string.Empty;
                    e.Graphics.DrawString(cellText, textFont, Brushes.Black, currentX, y, alignLeft);
                    currentX += columnWidths[i] + 10;
                }

                y += textFont.GetHeight(e.Graphics);
                rowIndex++;

                if (y + textFont.GetHeight(e.Graphics) > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            y += 20;

            // Total Price and Summary
            e.Graphics.DrawString("___________________________________________________________\n\n" + $"Total Price: ${TotalPrice:F2}", labelFont, Brushes.Black, x, y);
            y += labelFont.GetHeight(e.Graphics) + 10;

            e.Graphics.DrawString("\n\n" + $"Amount Paid: ${txtAmountPaid.Text}", labelFont, Brushes.Black, x, y);
            y += labelFont.GetHeight(e.Graphics) + 10;

            e.Graphics.DrawString("\n\n" + $"Change: ${lblChange.Text}", labelFont, Brushes.Black, x, y);
            y += labelFont.GetHeight(e.Graphics) + 30;

            // Footer
            string footerText = "\n\nThank you for dining with us!";
            float footerTextWidth = e.Graphics.MeasureString(footerText, textFont).Width;
            e.Graphics.DrawString(footerText, textFont, Brushes.Black, (e.MarginBounds.Width / 2) - (footerTextWidth / 2), y);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtCart.SelectedRows.Count > 0)
                {
                    int productID = Convert.ToInt32(dtCart.SelectedRows[0].Cells["ProductID"].Value);
                    int orderID = Convert.ToInt32(dtCart.SelectedRows[0].Cells["CustomerID"].Value); 

                    DialogResult result = MessageBox.Show($"Are you sure you want to delete the product with ID: {productID} from this order?",
                                                          "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Connection.Open();
                        string query = "DELETE FROM Orders WHERE ProductID = @pID AND CustomerID = @cID";

                        using (SqlCommand cmd = new SqlCommand(query, Connection))
                        {
                            cmd.Parameters.AddWithValue("@pID", productID);
                            cmd.Parameters.AddWithValue("@cID", orderID); 
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Product successfully deleted from this order.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DisplayOrder();
                                DisplayTotalPrice();
                            }
                            else
                            {
                                MessageBox.Show("Deletion failed. Product not found in the current order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a product to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        private void dtMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dtMenu.Rows[e.RowIndex];

                    lblProdID.Text = row.Cells["ProductID"].Value.ToString();
                    lblProductName.Text = row.Cells["ProductName"].Value.ToString();
                    lblPrice.Text = row.Cells["ProductPrice"].Value.ToString();
                    cbProductName.Text = row.Cells["ProductName"].Value.ToString();  
                    cbCategory.Text = row.Cells["ProductCategory"].Value.ToString();        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       }
   }
    
