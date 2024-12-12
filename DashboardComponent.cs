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
    public partial class DashboardComponent : UserControl
    {
        readonly SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-DVKO92VB\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;");

        public DashboardComponent()
        {
            InitializeComponent();
            DisplayTotalCashier();
            DisplayTransactions();
            DisplayTodayIncome();
            DisplayTotalIncome();
        }
        public void refreshData()
        {
            if(InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            DisplayTotalCashier();
            DisplayTransactions();
            DisplayTodayIncome();
            DisplayTotalIncome();
        }

        public void DisplayTotalCashier()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                try
                {
                    Connection.Open();

                    string selectData = "SELECT COUNT(*) FROM UserAccounts WHERE userRole = @role AND userStatus = @status";

                    using (SqlCommand cmd = new SqlCommand(selectData, Connection))
                    {
                        cmd.Parameters.AddWithValue("role", "Cashier");
                        cmd.Parameters.AddWithValue("@status", "Active");

                        object result = cmd.ExecuteScalar();
                        int count = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        lblTotalEmployee.Text = count.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("11 Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { Connection.Close(); }
            }
        }

        public void DisplayTransactions()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                try
                {
                    Connection.Open();

                    string selectData = "SELECT COUNT (CustomerID) FROM TransactionTable WHERE CAST(Date AS DATE) = @date";

                    using (SqlCommand cmd = new SqlCommand(selectData, Connection))
                    {
                        DateTime date = DateTime.Today;
                        cmd.Parameters.AddWithValue("@date", date);

                        object result = cmd.ExecuteScalar();
                        int count = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        lblTransactions.Text = count.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("12 Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { Connection.Close(); }
            }
        }

        public void DisplayTodayIncome()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                try
                {
                    Connection.Open();

                    string selectData = "SELECT SUM(TotalAmount) FROM TransactionTable WHERE CAST(Date AS DATE) = @date ";

                    using (SqlCommand cmd = new SqlCommand(selectData, Connection))
                    {
                        DateTime date = DateTime.Today;
                        cmd.Parameters.AddWithValue("@date", date);

                        object result = cmd.ExecuteScalar();
                        decimal income = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                        lblTodayIncome.Text = "₱" + income.ToString("0.00");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("13 Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { Connection.Close(); }
            }
        }

        public void DisplayTotalIncome()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                try
                {
                    Connection.Open();

                    string selectData = "SELECT SUM(TotalAmount) FROM TransactionTable";

                    using (SqlCommand cmd = new SqlCommand(selectData, Connection))
                    {
                        object result = cmd.ExecuteScalar();
                        decimal income = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                        lblTotalIncome.Text = "₱" + income.ToString("0.00");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("14 Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { Connection.Close(); }
            }
        }

    }
}

