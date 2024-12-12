using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class AdminForm : Form
    {
        private string _username;
        private int _userId;
       

        public AdminForm(string username, int userId)
        {
            InitializeComponent();
            _username = username;
            _userId = userId;
        }
       

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginLogoutClass loginLogoutClass = new LoginLogoutClass();
            int userID = loginLogoutClass.geUserId(_username);

            loginLogoutClass.UserLogout(userID);

            MessageBox.Show("Logged out successfuly!", "Confirm", MessageBoxButtons.OK);

           ProductUserControl productUserControl = new ProductUserControl();
           productUserControl2.CurrentUsername = _username;

            this.Close();

            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            lblName.Text = _username;
            lblDateTime.Text = DateTime.Now.ToString();

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Proceed to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            dashboardComponent2.Visible = true;
            posUserControl2.Visible = false;
            productUserControl2.Visible = false;
            addUserControl1.Visible = false;

            DashboardComponent dashForm = dashboardComponent2 as DashboardComponent;

            if (dashForm != null)
            {
                dashForm.refreshData();
            }

        }
        private void btnOrders_Click(object sender, EventArgs e)
        {
            dashboardComponent2.Visible = false;
            posUserControl2.Visible = true;
            productUserControl2.Visible = false;
            addUserControl1.Visible = false;
            
            posUserControl2.CurrentUsername = _username;
            posUserControl2.UserId = _userId;

            POSUserControl posForm = posUserControl2 as POSUserControl;

            if (posForm != null)
            {
                posForm.refreshData();
            }

        }
        private void btnAddProd_Click(object sender, EventArgs e)
        {
            dashboardComponent2.Visible = false;
            posUserControl2.Visible = false;
            productUserControl2.Visible = true;
            addUserControl1.Visible = false;

            productUserControl2.CurrentUsername = _username;
            productUserControl2.UserId = _userId; 

            ProductUserControl productForm = productUserControl2 as ProductUserControl;
            if (productForm != null)
            {
                productForm.refreshData();
            }

        }

        private void btnCashier_Click(object sender, EventArgs e)
        {
            dashboardComponent2.Visible = false;
            posUserControl2.Visible = false;
            productUserControl2.Visible = false;
            addUserControl1.Visible = true;


            AddUserControl addForm =  addUserControl1 as AddUserControl;

            if (addForm != null)
            {
                addForm.refreshData();
            }

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            ViewTransaction viewTransaction = new ViewTransaction();
            viewTransaction.ShowDialog();
        }

        private void btnLogHistory_Click(object sender, EventArgs e)
        {
            AdminLogHistory adminLogHistory = new AdminLogHistory();
            adminLogHistory.ShowDialog();
        }
    }
}
