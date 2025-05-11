using OtpNet;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurs
{
    public partial class OtpSettings: Form
    {
        private int userId;
        private string connectionString;
        Totp Totp;
        string secretKey;
        public OtpSettings(int userId,string connectionString)
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

            TwoFactorController twoFactorController = new TwoFactorController(connectionString);
            fa2Cb.Checked = twoFactorController.GetIs2faStatus(userId);



            var data = GetInfoFromDBForCurrentUser();
            nameLbl.Text = data.name;
            surnameLbl.Text = data.surName;
            loginLbl.Text = data.login;

            if (fa2Cb.Checked)
            {
                secretKey = twoFactorController.GetSecretKey(userId);
            }
            else
            {
                secretKey = Tools.RandomString(16);
            }


             Totp = new Totp(Base32Encoding.ToBytes(secretKey));

  
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            var uriString = new OtpUri(OtpType.Totp, secretKey, "alice@google.com", "ACME Co").ToString();


            QRCodeData qrCodeData = qrGenerator.CreateQrCode(uriString,QRCodeGenerator.ECCLevel.Default);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(5);
            qrCodePb.Image = qrCodeImage;


        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            UpdateUI();

        }




private void button1_Click(object sender, EventArgs e)
        {
            if (fa2Cb.Checked)
            {
                var facontroller = new TwoFactorController(connectionString);
                facontroller.SetF2AStatus(userId, fa2Cb.Checked, secretKey);
            }
            else
            {
                var facontroller = new TwoFactorController(connectionString);
                facontroller.SetF2AStatus(userId, false, "");
            }
            MessageBox.Show("Настройки сохранены!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
