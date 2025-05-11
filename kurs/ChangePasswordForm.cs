using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurs
{
    public partial class ChangePasswordForm: Form
    {
        private int userId;
        private string connectionString;
        public ChangePasswordForm(int userId,string connectionString)
        {
            InitializeComponent();
            this.userId = userId;
            this.connectionString = connectionString;

        }
        private (String login,String name, String surName) GetInfoFromDBForCurrentUser()
        {
            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                DataTable tempData = new DataTable();
                var cmd = new SqlCommand("SELECT [UserID]\r\n      ,[FirstName]\r\n      ,[LastName]\r\n      ,[Email]\r\n      ,[UserLogin]\r\n      ,[UserPassword]\r\n      ,[UserRole]\r\n  FROM [Demo_2025].[dbo].[Users] d  WHERE d.UserID = @id ",connection);
                cmd.Parameters.AddWithValue("id", userId);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(tempData);

                return (tempData.Rows[0]["UserLogin"].ToString(), tempData.Rows[0]["FirstName"].ToString(), tempData.Rows[0]["LastName"].ToString());


                
            }catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return ("", "", "");
        }


        private void UpdateUI()
        {
            var data = GetInfoFromDBForCurrentUser();
            nameLbl.Text = data.name;
            surnameLbl.Text = data.surName;
            loginLbl.Text = data.login;
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            UpdateUI();

        }


        private void ChangePasswordForUser(int userId, string password)
        {
            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                var cmd = new SqlCommand("update [dbo].[Users] set UserPassword = @password where UserID = @userId", connection);
                cmd.Parameters.AddWithValue("password", password);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
        }

private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(password1Tb.Text) && !String.IsNullOrWhiteSpace(password2Tb.Text) && password1Tb.Text == password2Tb.Text)
            {
                ChangePasswordForUser(userId, password1Tb.Text);
                MessageBox.Show("Пароль сменен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information) ;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(this,"Введены неправильные данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
