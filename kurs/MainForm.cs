using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kurs
{
    public partial class MainForm : Form
    {
        private string connectionString = "Data Source=DEPRESSEDK1D;Initial Catalog=Demo_2025;Integrated Security=True;Encrypt=False";

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Получаем логин и пароль из текстовых полей
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Проверяем, что логин и пароль не пустые
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Используем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT UserID, UserRole FROM Users WHERE UserLogin = @login AND UserPassword = @password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);

                        // Отладочный вывод для проверки вводимых данных
                        Console.WriteLine($"Login: {login}, Password: {password}");
                        Console.WriteLine($"Query: {query}");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Получаем UserID и UserRole
                                int userId = reader.GetInt32(0); // UserID
                                string role = reader.GetString(1); // UserRole

                                // Выбираем форму в зависимости от роли
                                Form nextForm;
                                if (role == "Admin")
                                {
                                    nextForm = new AdminForm();
                                }
                                else
                                {
                                    nextForm = new UserForm(userId); // Передаем userId в конструктор
                                }

                                // Показываем следующую форму и скрываем текущую
                                nextForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"Ошибка SQL: {sqlEx.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Открываем форму регистрации
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Логика, выполняемая при загрузке формы


        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void txtLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}