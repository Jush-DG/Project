namespace Inventory
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogHistory = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnCashier = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOrders = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnAddProd = new System.Windows.Forms.Button();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.exitBtn = new System.Windows.Forms.PictureBox();
            this.dashboardComponent2 = new Inventory.DashboardComponent();
            this.posUserControl2 = new Inventory.POSUserControl();
            this.productUserControl2 = new Inventory.ProductUserControl();
            this.addUserControl1 = new Inventory.AddUserControl();
            this.dashboardComponent1 = new Inventory.DashboardComponent();
            this.posUserControl1 = new Inventory.POSUserControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.btnLogHistory);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnDashboard);
            this.panel1.Controls.Add(this.btnCashier);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnOrders);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.btnAddProd);
            this.panel1.Location = new System.Drawing.Point(21, 45);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(145, 436);
            this.panel1.TabIndex = 1;
            // 
            // btnLogHistory
            // 
            this.btnLogHistory.BackColor = System.Drawing.Color.White;
            this.btnLogHistory.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogHistory.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogHistory.Location = new System.Drawing.Point(0, 364);
            this.btnLogHistory.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogHistory.Name = "btnLogHistory";
            this.btnLogHistory.Size = new System.Drawing.Size(145, 37);
            this.btnLogHistory.TabIndex = 16;
            this.btnLogHistory.Text = "Log History";
            this.btnLogHistory.UseVisualStyleBackColor = false;
            this.btnLogHistory.Click += new System.EventHandler(this.btnLogHistory_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(0, 328);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 37);
            this.button1.TabIndex = 15;
            this.button1.Text = "View Transaction";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.White;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDashboard.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.Location = new System.Drawing.Point(0, 184);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(2);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(145, 37);
            this.btnDashboard.TabIndex = 6;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnCashier
            // 
            this.btnCashier.BackColor = System.Drawing.Color.White;
            this.btnCashier.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCashier.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCashier.Location = new System.Drawing.Point(0, 292);
            this.btnCashier.Margin = new System.Windows.Forms.Padding(2);
            this.btnCashier.Name = "btnCashier";
            this.btnCashier.Size = new System.Drawing.Size(145, 37);
            this.btnCashier.TabIndex = 7;
            this.btnCashier.Text = "Add Cashier";
            this.btnCashier.UseVisualStyleBackColor = false;
            this.btnCashier.Click += new System.EventHandler(this.btnCashier_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Inventory.Properties.Resources.manager;
            this.pictureBox1.Location = new System.Drawing.Point(34, 26);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnOrders
            // 
            this.btnOrders.BackColor = System.Drawing.Color.White;
            this.btnOrders.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOrders.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrders.Location = new System.Drawing.Point(0, 220);
            this.btnOrders.Margin = new System.Windows.Forms.Padding(2);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(145, 37);
            this.btnOrders.TabIndex = 5;
            this.btnOrders.Text = "Orders";
            this.btnOrders.UseVisualStyleBackColor = false;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tw Cen MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblName.Location = new System.Drawing.Point(82, 142);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 17);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tw Cen MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(9, 142);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Welcome,";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(45, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Admin";
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(105)))), ((int)(((byte)(97)))));
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogout.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Location = new System.Drawing.Point(0, 400);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(145, 36);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnAddProd
            // 
            this.btnAddProd.BackColor = System.Drawing.Color.White;
            this.btnAddProd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddProd.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProd.Location = new System.Drawing.Point(0, 256);
            this.btnAddProd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddProd.Name = "btnAddProd";
            this.btnAddProd.Size = new System.Drawing.Size(145, 37);
            this.btnAddProd.TabIndex = 2;
            this.btnAddProd.Text = "Add Products";
            this.btnAddProd.UseVisualStyleBackColor = false;
            this.btnAddProd.Click += new System.EventHandler(this.btnAddProd_Click);
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.Location = new System.Drawing.Point(18, 14);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(45, 19);
            this.lblDateTime.TabIndex = 8;
            this.lblDateTime.Text = "label3";
            // 
            // exitBtn
            // 
            this.exitBtn.BackColor = System.Drawing.Color.Wheat;
            this.exitBtn.Image = global::Inventory.Properties.Resources.close;
            this.exitBtn.Location = new System.Drawing.Point(1264, 11);
            this.exitBtn.Margin = new System.Windows.Forms.Padding(2);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(16, 20);
            this.exitBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.exitBtn.TabIndex = 10;
            this.exitBtn.TabStop = false;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // dashboardComponent2
            // 
            this.dashboardComponent2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.dashboardComponent2.Location = new System.Drawing.Point(170, 45);
            this.dashboardComponent2.Margin = new System.Windows.Forms.Padding(2);
            this.dashboardComponent2.Name = "dashboardComponent2";
            this.dashboardComponent2.Size = new System.Drawing.Size(1110, 436);
            this.dashboardComponent2.TabIndex = 14;
            // 
            // posUserControl2
            // 
            this.posUserControl2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.posUserControl2.CurrentUsername = null;
            this.posUserControl2.Location = new System.Drawing.Point(171, 45);
            this.posUserControl2.Margin = new System.Windows.Forms.Padding(2);
            this.posUserControl2.Name = "posUserControl2";
            this.posUserControl2.Size = new System.Drawing.Size(1110, 436);
            this.posUserControl2.TabIndex = 13;
            // 
            // productUserControl2
            // 
            this.productUserControl2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.productUserControl2.CurrentUsername = null;
            this.productUserControl2.Location = new System.Drawing.Point(171, 45);
            this.productUserControl2.Name = "productUserControl2";
            this.productUserControl2.Size = new System.Drawing.Size(1110, 436);
            this.productUserControl2.TabIndex = 12;
            // 
            // addUserControl1
            // 
            this.addUserControl1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.addUserControl1.Location = new System.Drawing.Point(170, 45);
            this.addUserControl1.Margin = new System.Windows.Forms.Padding(2);
            this.addUserControl1.Name = "addUserControl1";
            this.addUserControl1.Size = new System.Drawing.Size(1110, 436);
            this.addUserControl1.TabIndex = 11;
            // 
            // dashboardComponent1
            // 
            this.dashboardComponent1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.dashboardComponent1.Location = new System.Drawing.Point(170, 45);
            this.dashboardComponent1.Margin = new System.Windows.Forms.Padding(2);
            this.dashboardComponent1.Name = "dashboardComponent1";
            this.dashboardComponent1.Size = new System.Drawing.Size(1110, 436);
            this.dashboardComponent1.TabIndex = 14;
            // 
            // posUserControl1
            // 
            this.posUserControl1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.posUserControl1.CurrentUsername = null;
            this.posUserControl1.Location = new System.Drawing.Point(170, 45);
            this.posUserControl1.Margin = new System.Windows.Forms.Padding(2);
            this.posUserControl1.Name = "posUserControl1";
            this.posUserControl1.Size = new System.Drawing.Size(1110, 436);
            this.posUserControl1.TabIndex = 13;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.ClientSize = new System.Drawing.Size(1291, 499);
            this.Controls.Add(this.dashboardComponent2);
            this.Controls.Add(this.posUserControl2);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.productUserControl2);
            this.Controls.Add(this.addUserControl1);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coffee Shope Management System";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddProd;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnCashier;
        private System.Windows.Forms.PictureBox exitBtn;
        private POSUserControl posUserControl1;
        private DashboardComponent dashboardComponent1;
        private System.Windows.Forms.Label lblDateTime;
        private AddUserControl addUserControl1;
        private ProductUserControl productUserControl2;
        private POSUserControl posUserControl2;
        private DashboardComponent dashboardComponent2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnLogHistory;
    }
}