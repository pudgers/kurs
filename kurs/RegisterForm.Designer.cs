namespace kurs
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblHeader; // Шапка

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label(); // Шапка

            // Форма
            this.Text = "Регистрация";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ClientSize = new System.Drawing.Size(350, 300); // Увеличена высота формы

            // Шапка
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHeader.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblHeader.Location = new System.Drawing.Point(20, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(240, 26);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Интернет-Провайдер";

            // Метки
            this.lblFirstName.Text = "Имя:";
            this.lblFirstName.Location = new System.Drawing.Point(20, 60); // Сдвинуто вниз для шапки
            this.lblLastName.Text = "Фамилия:";
            this.lblLastName.Location = new System.Drawing.Point(20, 90); // Сдвинуто вниз для шапки
            this.lblLogin.Text = "Логин:";
            this.lblLogin.Location = new System.Drawing.Point(20, 120); // Сдвинуто вниз для шапки
            this.lblPassword.Text = "Пароль:";
            this.lblPassword.Location = new System.Drawing.Point(20, 150); // Сдвинуто вниз для шапки
            this.lblEmail.Text = "Почта (необязательно):";
            this.lblEmail.Location = new System.Drawing.Point(20, 180); // Сдвинуто вниз для шапки

            // Поля ввода
            this.txtFirstName.Location = new System.Drawing.Point(150, 60); // Сдвинуто вниз для шапки
            this.txtLastName.Location = new System.Drawing.Point(150, 90); // Сдвинуто вниз для шапки
            this.txtLogin.Location = new System.Drawing.Point(150, 120); // Сдвинуто вниз для шапки
            this.txtPassword.Location = new System.Drawing.Point(150, 150); // Сдвинуто вниз для шапки
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtEmail.Location = new System.Drawing.Point(150, 180); // Сдвинуто вниз для шапки

            // Кнопка регистрации
            this.btnRegister.Text = "Зарегистрироваться";
            this.btnRegister.Location = new System.Drawing.Point(100, 220); // Сдвинуто вниз для шапки
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // Добавление элементов на форму
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnRegister);
        }
    }
}