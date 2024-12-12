using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace Inventory
{
    internal class CashierOrdersClass
    {
        readonly SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-DVKO92VB\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;");

        public int CustomerID {  get; set; }
        public string ProductID {  get; set; }
        public string ProductName{  get; set; }
        public string ProductCategory {  get; set; }
        public int Quantity {  get; set; }

        public string Price {  get; set; }

        public List<CashierOrdersClass> OrderListData()
        {
            List<CashierOrdersClass> listData = new List<CashierOrdersClass>();
            
            if (Connection.State == ConnectionState.Closed)
            {
                try
                {
                    Connection.Open();

                    int CustID = 0;
                    string selectCustomerData = "SELECT MAX(CustomerID) FROM Orders";

                    using (SqlCommand getCustomerData = new SqlCommand(selectCustomerData, Connection))
                    {
                        object result = getCustomerData.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            int temp = Convert.ToInt32(result);

                            if (temp == 0)
                            {
                                CustID = 1;
                            }
                            else
                            {
                                CustID = temp;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error ID");
                        }

                        string selectData = "SELECT * FROM Orders WHERE CustomerID = @custID";

                        using (SqlCommand cmd = new SqlCommand(selectData, Connection))
                        {
                            cmd.Parameters.AddWithValue("@custID",CustID);

                            SqlDataReader read = cmd.ExecuteReader();

                            while (read.Read())
                            {
                                CashierOrdersClass coData = new CashierOrdersClass();

                                coData.CustomerID = (int)read["CustomerID"];
                                coData.ProductID = read["ProductID"].ToString();
                                coData.ProductName = read["ProductName"].ToString();
                                coData.ProductCategory = read["Category"].ToString();
                                coData.Quantity = (int)read["OrderQuantity"];
                                coData.Price = read["Price"].ToString();

                                listData.Add(coData);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error", ex.Message);
                }
                finally { Connection.Close(); } 
            }
            return listData;
        }

    }
}
