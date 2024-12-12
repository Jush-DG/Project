using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    internal class CashierOrderForm
    {
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-DVKO92VB\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;");

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public int ProductPrice { get; set; }

        public List<CashierOrderForm> ShowMenu()
        {
            List<CashierOrderForm> listData = new List<CashierOrderForm>();

            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string selectData = "SELECT ProductID, ProductName, Category, Price, Description FROM ProductTable WHERE ArchiveStatus = 'Active'";
                using (SqlCommand cmd = new SqlCommand(selectData, Connection))
                {
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        CashierOrderForm productData = new CashierOrderForm();


                        productData.ProductID = Convert.ToInt32(sqlDataReader["ProductID"]);
                        productData.ProductName = sqlDataReader["ProductName"].ToString();
                        productData.ProductCategory = sqlDataReader["Category"].ToString();
                        productData.ProductPrice = Convert.ToInt32(sqlDataReader["Price"]);
                        

                        listData.Add(productData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving products: " + ex.Message);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }

            return listData;
        }

        
        }
    }


