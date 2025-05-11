using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InternetProviderApp
{
    public partial class OrderHistoryForm : Form
    {
        private int userId;
        private string connectionString = "your_connection_string_here"; // Укажите вашу строку подключения

        public OrderHistoryForm(int userId)
        {
            InitializeComponent(); // Вызов дизайнера формы
            this.userId = userId;
            LoadOrderHistory();
        }

        private void LoadOrderHistory()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT OrderID, ServiceName, Price, OrderDate, Status FROM Orders WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dgvOrderHistory.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки истории заказов: " + ex.Message);
            }
        }
    }
}
