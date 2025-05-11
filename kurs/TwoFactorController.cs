using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs
{
   public class TwoFactorController
    {

        string connectionString;

        public TwoFactorController(string connectionString)
        {
            this.connectionString = connectionString;
        }




        public bool GetIs2faStatus(int userId)
        {
            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT COALESCE(u.Is2FA,0) as 'is2fa' from [dbo].Users u WHERE u.UserID = @userId", connection);
                cmd.Parameters.AddWithValue("userId", userId);
                var data = Convert.ToInt32(cmd.ExecuteScalar());
                if (data == 0)
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }


        public string GetSecretKey(int userId)
        {
            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT u.secretKey as 'secretKey' from [dbo].Users u WHERE u.UserID = @userId", connection);
                cmd.Parameters.AddWithValue("userId", userId);
                var data = cmd.ExecuteScalar();
                return data?.ToString()?.Trim();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        public bool SetF2AStatus(int userId, bool is2faEnabled, string secretKey)
        {
            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                var cmd = new SqlCommand("UPDATE [dbo].[Users] \r\nset Is2FA = @fa2,secretKey = @key \r\nWHERE UserID = @id", connection);
                cmd.Parameters.AddWithValue("id", userId);
                cmd.Parameters.AddWithValue("key", secretKey);
                cmd.Parameters.AddWithValue("fa2", is2faEnabled);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

    }
}
