using CSEgitimKampi601.Entities;
using CSEgitimKampi601.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSEgitimKampi601
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CustomerOperations customerOperations = new CustomerOperations();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var Customer = new Customer
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerBalance = Convert.ToDecimal(txtCustomerBalance.Text),
                CustomerShoppingCount = Convert.ToInt32(txtCustomerTotalShopPrice.Text)
            };
            customerOperations.AddCustomer(Customer);
            MessageBox.Show("Müşteri Başarıyla Eklendi!","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Information );
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            List<Customer> customers = customerOperations.GetAllCustomer();
            dataGridView1.DataSource = customers;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string customerID = txtCustomerID.Text;
            customerOperations.DeleteCustomer(customerID);
            MessageBox.Show("Müşteri Başarıyla Silindi!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string customerID = txtCustomerID.Text;
            var Customer = new Customer
            {
                CustomerID = customerID,
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerBalance = Convert.ToDecimal(txtCustomerBalance.Text),
                CustomerShoppingCount = Convert.ToInt32(txtCustomerTotalShopPrice.Text)
            };
            customerOperations.UpdateCustomer(Customer);
            MessageBox.Show("Müşteri Başarıyla Güncellendi!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGetbyID_Click(object sender, EventArgs e)
        {
            string customerID = txtCustomerID.Text;
            Customer customers = customerOperations.GetCustomerByID(customerID);
            dataGridView1.DataSource = new List<Customer> {customers};

        }
    }
}
