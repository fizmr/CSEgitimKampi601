using Npgsql;
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
    public partial class FrmCustomer: Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }
        string ConnectionString = "Server=localhost;port=5432;Database=CustomerDb;user ID=postgres;Password=1327";
        void GetAllCustomers()
        {
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            string query = "SELECT * FROM Customers";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
       
        
        private void btnList_Click(object sender, EventArgs e)
        {
            GetAllCustomers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string customerSurname = txtCustomerSurname.Text;
            string customerCity = txtCustomerCity.Text;
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            string query = "INSERT INTO Customers (CustomerName,CustomerSurname,CustomerCity) VALUES (@customerName,@customerSurname,@customerCity)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerName", customerName);
            command.Parameters.AddWithValue("@customerSurname", customerSurname);
            command.Parameters.AddWithValue("@customerCity", customerCity);
            command.ExecuteNonQuery();
            MessageBox.Show("Müşteri Başarıyla Eklendi!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            GetAllCustomers();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCustomerID.Text);
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            string query = "DELETE FROM Customers WHERE CustomerID=@id";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Müşteri Başarıyla Silindi!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            GetAllCustomers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string customerSurname = txtCustomerSurname.Text;
            string customerCity = txtCustomerCity.Text;
            int id = Convert.ToInt32(txtCustomerID.Text);
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            string query = "UPDATE Customers SET CustomerName=@customerName,CustomerSurname=@customerSurname,CustomerCity=@customerCity WHERE CustomerID=@id";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerName", customerName);
            command.Parameters.AddWithValue("@customerSurname", customerSurname);
            command.Parameters.AddWithValue("@customerCity", customerCity);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Müşteri Başarıyla Güncellendi!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            GetAllCustomers();
        }

        private void btnGetbyID_Click(object sender, EventArgs e)
        {
            string customerID = txtCustomerID.Text;
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            string query = "SELECT * FROM Customers WHERE CustomerID=@customerID";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerID", customerID);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataGridView1.DataSource = dataTable;
            connection.Close();
            GetAllCustomers();

        }
    }
}
