using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kurs
{
    public partial class AdminForm : Form
    {
        // Строка подключения к базе данных
        private string connectionString = "Data Source=DEPRESSEDK1D;Initial Catalog=Demo_2025;Integrated Security=True;Encrypt=False";

        // Конструктор формы
        public AdminForm()
        {
            InitializeComponent();
            LoadOrders();    // Загрузка заказов при запуске формы
            LoadRequests();  // Загрузка запросов пользователей при запуске формы
            LoadServices();  // Загрузка услуг при запуске формы
        }

        // Загрузка заказов в DataGridView
        private void LoadOrders()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT Orders.OrderID, Users.UserLogin, Services.ServiceName, Orders.OrderDate, Orders.Status 
                        FROM Orders 
                        INNER JOIN Users ON Orders.UserID = Users.UserID 
                        INNER JOIN Services ON Orders.ServiceID = Services.ServiceID";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvOrders.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Загрузка запросов пользователей в DataGridView
        private void LoadRequests()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM UserSearchLog ORDER BY SearchDate DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvRequests.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки запросов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Загрузка услуг в DataGridView
        private void LoadServices()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ServiceID, ServiceName, Description, Price FROM Services";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvServices.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки услуг: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Добавление новой услуги
        private void btnAddService_Click(object sender, EventArgs e)
        {
            string serviceName = txtServiceName.Text.Trim();
            string description = txtDescription.Text.Trim();
            decimal price;

            // Проверка корректности введенных данных
            if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(description) || !decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Заполните все поля корректно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (price <= 0)
            {
                MessageBox.Show("Цена должна быть больше нуля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Services (ServiceName, Description, Price) VALUES (@ServiceName, @Description, @Price)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ServiceName", serviceName);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@Price", price);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Услуга добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadServices(); // Обновляем таблицу услуг
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления услуги: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Редактирование услуги
        private void btnEditService_Click(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите услугу для редактирования!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int serviceId = Convert.ToInt32(dgvServices.SelectedRows[0].Cells["ServiceID"].Value);
            string serviceName = txtServiceName.Text.Trim();
            string description = txtDescription.Text.Trim();
            decimal price;

            // Проверка корректности введенных данных
            if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(description) || !decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Заполните все поля корректно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (price <= 0)
            {
                MessageBox.Show("Цена должна быть больше нуля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Services SET ServiceName = @ServiceName, Description = @Description, Price = @Price WHERE ServiceID = @ServiceID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ServiceName", serviceName);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@ServiceID", serviceId);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Услуга обновлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadServices(); // Обновляем таблицу услуг
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления услуги: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Удаление услуги
        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите услугу для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int serviceId = Convert.ToInt32(dgvServices.SelectedRows[0].Cells["ServiceID"].Value);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Services WHERE ServiceID = @ServiceID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ServiceID", serviceId);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Услуга удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadServices(); // Обновляем таблицу услуг
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления услуги: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Выход из системы
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрываем текущую форму
            MainForm mainForm = new MainForm(); // Открываем главную форму
            mainForm.Show();
        }
    }
}