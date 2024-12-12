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
    public partial class EmployeeForm : Form
    {
        private string _username;

        public EmployeeForm(string username)
        {
            InitializeComponent();
            _username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewTransaction viewTransaction = new ViewTransaction();
            viewTransaction.ShowDialog();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            lblName.Text = _username;
            lblDateTime.Text = DateTime.Now.ToString();
        }

        private void addProduct_Click(object sender, EventArgs e)
        {
            productUserControl1.Visible = true;
            posUserControl1.Visible = false;


        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            productUserControl1.Visible = false;
            posUserControl1.Visible = true;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginLogoutClass loginLogoutClass = new LoginLogoutClass();
            int userID = loginLogoutClass.geUserId(_username);

            loginLogoutClass.UserLogout(userID);

            MessageBox.Show("Logged out successfuly!", "Confirm", MessageBoxButtons.OK);

            this.Close();

            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Proceed to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
