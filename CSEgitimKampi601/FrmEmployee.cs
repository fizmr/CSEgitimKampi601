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
    public partial class FrmEmployee: Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }
        string conneactionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;password=1327";
        void EmployeeList()
        {
            var connection = new NpgsqlConnection(conneactionString);
            connection.Open();
            string query = "Select * from Employees";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        void DepartmetList()
        {
            var connection = new NpgsqlConnection(conneactionString);
            connection.Open();
            string query = "Select * from Departments";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            comboDepartment.DisplayMember = "DepartmentName";
            comboDepartment.ValueMember = "DepartmentId";
            comboDepartment.DataSource = dataTable;
            connection.Close();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            EmployeeList();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {           
            DepartmetList();
            EmployeeList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text;
            string employeeSurname = txtEmployeeSurname.Text;
            decimal employeeSalary = decimal.Parse(txtEmployeeSalary.Text);
            int departmentId = Convert.ToInt32(comboDepartment.SelectedValue);
            var connection = new NpgsqlConnection(conneactionString);
            connection.Open();
            string query = "Insert into Employees (EmployeeName, EmployeeSurname, EmployeeSalary, DepartmentId) values (@employeeName, @employeeSurname, @employeeSalary, @departmentId)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeeName", employeeName);
            command.Parameters.AddWithValue("@employeeSurname", employeeSurname);
            command.Parameters.AddWithValue("@employeeSalary", employeeSalary);
            command.Parameters.AddWithValue("@departmentId", departmentId);
            command.ExecuteNonQuery();
            MessageBox.Show("Çalışan Başarıyla Eklendi!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
            connection.Close();
            EmployeeList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtEmployeeId.Text);
            var connection = new NpgsqlConnection(conneactionString);
            connection.Open();
            string query = "Delete from Employees where EmployeeId = @id";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Çalışan Başarıyla Silindi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            EmployeeList();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text;
            string employeeSurname = txtEmployeeSurname.Text;
            decimal employeeSalary = decimal.Parse(txtEmployeeSalary.Text);
            int departmentId = Convert.ToInt32(comboDepartment.SelectedValue);
            int id = Convert.ToInt32(txtEmployeeId.Text);
            var connection = new NpgsqlConnection(conneactionString);
            connection.Open();
            string query = "Update Employees set EmployeeName = @employeeName, EmployeeSurname = @employeeSurname, EmployeeSalary = @employeeSalary, DepartmentId = @departmentId where EmployeeId = @id";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeeName", employeeName);
            command.Parameters.AddWithValue("@employeeSurname", employeeSurname);
            command.Parameters.AddWithValue("@employeeSalary", employeeSalary);
            command.Parameters.AddWithValue("@departmentId", departmentId);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Çalışan Başarıyla Güncellendi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            EmployeeList();
        }
    }
}
