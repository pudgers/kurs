namespace kurs
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.dgvRequests = new System.Windows.Forms.DataGridView();
            this.dgvServices = new System.Windows.Forms.DataGridView();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.btnAddService = new System.Windows.Forms.Button();
            this.btnEditService = new System.Windows.Forms.Button();
            this.btnDeleteService = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblOrders = new System.Windows.Forms.Label();
            this.lblRequests = new System.Windows.Forms.Label();
            this.lblServices = new System.Windows.Forms.Label();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label(); // Шапка

            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).BeginInit();
            this.SuspendLayout();

            // dgvOrders (Таблица заказов)
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(12, 60); // Сдвинуто вниз для шапки
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.Size = new System.Drawing.Size(600, 150);
            this.dgvOrders.TabIndex = 0;

            // dgvRequests (Таблица запросов пользователей)
            this.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequests.Location = new System.Drawing.Point(12, 240); // Сдвинуто вниз для шапки
            this.dgvRequests.Name = "dgvRequests";
            this.dgvRequests.Size = new System.Drawing.Size(600, 150);
            this.dgvRequests.TabIndex = 1;

            // dgvServices (Таблица услуг)
            this.dgvServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServices.Location = new System.Drawing.Point(12, 410); // Сдвинуто вниз для шапки
            this.dgvServices.Name = "dgvServices";
            this.dgvServices.Size = new System.Drawing.Size(600, 150);
            this.dgvServices.TabIndex = 2;

            // txtServiceName (Поле ввода названия услуги)
            this.txtServiceName.Location = new System.Drawing.Point(650, 80); // Сдвинуто вниз для шапки
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(120, 20);
            this.txtServiceName.TabIndex = 3;

            // txtDescription (Поле ввода описания услуги)
            this.txtDescription.Location = new System.Drawing.Point(650, 120); // Сдвинуто вниз для шапки
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(120, 20);
            this.txtDescription.TabIndex = 4;

            // txtPrice (Поле ввода цены услуги)
            this.txtPrice.Location = new System.Drawing.Point(650, 160); // Сдвинуто вниз для шапки
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(120, 20);
            this.txtPrice.TabIndex = 5;

            // btnAddService (Кнопка добавления услуги)
            this.btnAddService.Location = new System.Drawing.Point(650, 200); // Сдвинуто вниз для шапки
            this.btnAddService.Name = "btnAddService";
            this.btnAddService.Size = new System.Drawing.Size(120, 30);
            this.btnAddService.TabIndex = 6;
            this.btnAddService.Text = "Добавить услугу";
            this.btnAddService.UseVisualStyleBackColor = true;
            this.btnAddService.Click += new System.EventHandler(this.btnAddService_Click);

            // btnEditService (Кнопка редактирования услуги)
            this.btnEditService.Location = new System.Drawing.Point(650, 240); // Сдвинуто вниз для шапки
            this.btnEditService.Name = "btnEditService";
            this.btnEditService.Size = new System.Drawing.Size(120, 30);
            this.btnEditService.TabIndex = 7;
            this.btnEditService.Text = "Изменить услугу";
            this.btnEditService.UseVisualStyleBackColor = true;
            this.btnEditService.Click += new System.EventHandler(this.btnEditService_Click);

            // btnDeleteService (Кнопка удаления услуги)
            this.btnDeleteService.Location = new System.Drawing.Point(650, 280); // Сдвинуто вниз для шапки
            this.btnDeleteService.Name = "btnDeleteService";
            this.btnDeleteService.Size = new System.Drawing.Size(120, 30);
            this.btnDeleteService.TabIndex = 8;
            this.btnDeleteService.Text = "Удалить услугу";
            this.btnDeleteService.UseVisualStyleBackColor = true;
            this.btnDeleteService.Click += new System.EventHandler(this.btnDeleteService_Click);

            // btnLogout (Кнопка выхода)
            this.btnLogout.Location = new System.Drawing.Point(650, 380); // Сдвинуто вниз для шапки
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(120, 30);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "Выход";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // lblOrders (Метка "Заказы")
            this.lblOrders.AutoSize = true;
            this.lblOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblOrders.Location = new System.Drawing.Point(12, 40); // Сдвинуто вниз для шапки
            this.lblOrders.Name = "lblOrders";
            this.lblOrders.Size = new System.Drawing.Size(65, 17);
            this.lblOrders.TabIndex = 10;
            this.lblOrders.Text = "Заказы";

            // lblRequests (Метка "Запросы пользователей")
            this.lblRequests.AutoSize = true;
            this.lblRequests.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblRequests.Location = new System.Drawing.Point(12, 220); // Сдвинуто вниз для шапки
            this.lblRequests.Name = "lblRequests";
            this.lblRequests.Size = new System.Drawing.Size(190, 17);
            this.lblRequests.TabIndex = 11;
            this.lblRequests.Text = "Запросы пользователей";

            // lblServices (Метка "Услуги")
            this.lblServices.AutoSize = true;
            this.lblServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblServices.Location = new System.Drawing.Point(12, 390); // Сдвинуто вниз для шапки
            this.lblServices.Name = "lblServices";
            this.lblServices.Size = new System.Drawing.Size(65, 17);
            this.lblServices.TabIndex = 12;
            this.lblServices.Text = "Услуги";

            // lblServiceName (Метка названия услуги)
            this.lblServiceName.AutoSize = true;
            this.lblServiceName.Location = new System.Drawing.Point(650, 60); // Сдвинуто вниз для шапки
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(90, 13);
            this.lblServiceName.TabIndex = 13;
            this.lblServiceName.Text = "Название услуги";

            // lblDescription (Метка описания услуги)
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(650, 100); // Сдвинуто вниз для шапки
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(57, 13);
            this.lblDescription.TabIndex = 14;
            this.lblDescription.Text = "Описание";

            // lblPrice (Метка цены услуги)
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(650, 140); // Сдвинуто вниз для шапки
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(33, 13);
            this.lblPrice.TabIndex = 15;
            this.lblPrice.Text = "Цена";

            // lblHeader (Шапка)
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHeader.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblHeader.Location = new System.Drawing.Point(12, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(240, 26);
            this.lblHeader.TabIndex = 16;
            this.lblHeader.Text = "Интернет-Провайдер";

            // AdminForm
            this.ClientSize = new System.Drawing.Size(800, 600); // Увеличена высота формы
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblServiceName);
            this.Controls.Add(this.lblServices);
            this.Controls.Add(this.lblRequests);
            this.Controls.Add(this.lblOrders);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnDeleteService);
            this.Controls.Add(this.btnEditService);
            this.Controls.Add(this.btnAddService);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtServiceName);
            this.Controls.Add(this.dgvServices);
            this.Controls.Add(this.dgvRequests);
            this.Controls.Add(this.dgvOrders);
            this.Name = "AdminForm";
            this.Text = "Панель администратора";

            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.DataGridView dgvRequests;
        private System.Windows.Forms.DataGridView dgvServices;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.Button btnEditService;
        private System.Windows.Forms.Button btnDeleteService;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblOrders;
        private System.Windows.Forms.Label lblRequests;
        private System.Windows.Forms.Label lblServices;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblHeader; // Шапка
    }
}