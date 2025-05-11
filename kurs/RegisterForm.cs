using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kurs
{
    public partial class RegisterForm : Form
    {
        private string connectionString = "Data Source=DEPRESSEDK1D;Initial Catalog=Demo_2025;Integrated Security=True;Encrypt=False";

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Получаем данные из текстовых полей
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim(); // Почта необязательна

            // Проверка на заполненность обязательных полей
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Проверяем, существует ли уже пользователь с таким логином
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE UserLogin = @login";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@login", login);
                        int userExists = (int)checkCommand.ExecuteScalar();

                        if (userExists > 0)
                        {
                            MessageBox.Show("Такой логин уже используется!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // SQL-запрос для вставки нового пользователя
                    string insertQuery = "INSERT INTO Users (FirstName, LastName, UserLogin, UserPassword, Email, UserRole) " +
                                         "VALUES (@firstName, @lastName, @login, @password, @email, 'User')";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", firstName);
                        command.Parameters.AddWithValue("@lastName", lastName);
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
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
    }
}
