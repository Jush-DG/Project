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
    public partial class AdminLogHistory : Form
    {
        private string connectionString = @"Data Source=LAPTOP-DVKO92VB\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;";

        public AdminLogHistory()
        {
            InitializeComponent();
            LoadLogHistory();
        }
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            LoadLogHistory();
        }
        private void LoadLogHistory()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM LogHistory";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    dtLogHistory.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transaction history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
