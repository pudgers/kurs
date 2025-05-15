using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using QRCoder;
using System.Drawing;

namespace kurs
{
    public partial class UserForm : Form
    {
        private string connectionString = "Data Source=DEPRESSEDK1D;Initial Catalog=Demo_2025;Integrated Security=True;Encrypt=False";
        private int userId;

        String GetUserName(int userId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                var cmd = new SqlCommand("SELECT CONCAT(FirstName,' ',LastName) from Users Where USERID = @userId", connection);
                cmd.Parameters.AddWithValue("userId", userId);
                return cmd.ExecuteScalar()?.ToString();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }


        public UserForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadServices();
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

        // Оформление заказа
        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите услугу!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int serviceId = Convert.ToInt32(dgvServices.SelectedRows[0].Cells["ServiceID"].Value);
            string serviceName = dgvServices.SelectedRows[0].Cells["ServiceName"].Value.ToString();
            string serviceDesc = dgvServices.SelectedRows[0].Cells["Description"].Value.ToString();
            decimal price = Convert.ToDecimal(dgvServices.SelectedRows[0].Cells["Price"].Value);
            string orderDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Сохраняем выбранную услугу в таблицу SelectedServices
                    string insertQuery = "INSERT INTO SelectedServices (UserID, ServiceID, SelectionDate) VALUES (@UserID, @ServiceID, @SelectionDate)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@UserID", userId);
                        insertCommand.Parameters.AddWithValue("@ServiceID", serviceId);
                        insertCommand.Parameters.AddWithValue("@SelectionDate", DateTime.Now);
                        insertCommand.ExecuteNonQuery();
                    }

                    // Оформление заказа
                    string orderQuery = "INSERT INTO Orders (UserID, ServiceID, Status, OrderDate) VALUES (@UserID, @ServiceID, 'Pending', @OrderDate)";
                    using (SqlCommand orderCommand = new SqlCommand(orderQuery, connection))
                    {
                        orderCommand.Parameters.AddWithValue("@UserID", userId);
                        orderCommand.Parameters.AddWithValue("@ServiceID", serviceId);
                        orderCommand.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        orderCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Заказ оформлен!\nУслуга: {serviceName}\nСумма: {price} руб.", "Чек", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GenerateReportV2(serviceName,price,orderDate,serviceId.ToString(), serviceDesc);
                    // GenerateReceipt(serviceName, price, orderDate); // Генерация и сохранение чека
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка заказа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FindAndReplace(Microsoft.Office.Interop.Word.Application doc, object findText, object replaceWithText)
        {
            //options
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            //execute find and replace
            doc.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }

        public void GenerateReportV2(string serviceName, decimal price, string orderDate,string id = "N/A",string description = "")
        {
            try
            {
                object fileName = Path.Combine(System.Windows.Forms.Application.StartupPath, "template.docx");
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application { Visible = false };
                Microsoft.Office.Interop.Word.Document aDoc = wordApp.Documents.Open(fileName, ReadOnly: false, Visible: false);
                aDoc.Activate();



                FindAndReplace(wordApp, "{id}", id);
                FindAndReplace(wordApp, "{bank}", "ООО Рога и Копыта");
                FindAndReplace(wordApp, "{schet}", Tools.RandomSchet());
                FindAndReplace(wordApp, "{bik}", Tools.RandomSchet());
                FindAndReplace(wordApp, "{id}", id);
                FindAndReplace(wordApp, "{date}", orderDate);
                FindAndReplace(wordApp, "{serviceName}", serviceName);
                FindAndReplace(wordApp, "{servicePrice}", Math.Round(price, 2));
                FindAndReplace(wordApp, "{serviceDesc}", description);
                FindAndReplace(wordApp, "{summPropisi}", RuDateAndMoneyConverter.CurrencyToTxtFull(Convert.ToDouble(price), true));



                var saveDialog = new SaveFileDialog()
                {
                    Filter = ".docx|Word",
                    Title = "Сохранение счёта"
                };
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    aDoc.SaveAs2(saveDialog.FileName);

                }
                aDoc.Close();
                MessageBox.Show("Файл создан!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       
        }


        // Генерация и сохранение чека в формате Word с QR-кодом
        private void GenerateReceipt(string serviceName, decimal price, string orderDate)
        {
            try
            {
                // Создаем новый экземпляр Word
                Word.Application wordApp = new Word.Application();
                Word.Document doc = wordApp.Documents.Add();

                // Заголовок организации
                Word.Paragraph header = doc.Paragraphs.Add();
                header.Range.Text = "ООО \"ИНТЕРНЕТ-СЕРВИС\"\nАдрес: ул. Технологическая, д. 5, офис 3\nИНН: 1234567890   ОГРН: 0987654321\n";
                header.Range.Font.Size = 12;
                header.Range.Font.Bold = 1;
                header.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                header.Range.InsertParagraphAfter();

                // Линия разделения
                Word.Paragraph line = doc.Paragraphs.Add();
                line.Range.Text = "________________________________________________________________________________________";
                line.Range.Font.Size = 10;
                line.Range.InsertParagraphAfter();

                // Название квитанции
                Word.Paragraph title = doc.Paragraphs.Add();
                title.Range.Text = "КВИТАНЦИЯ ОБ ОПЛАТЕ";
                title.Range.Font.Size = 16;
                title.Range.Font.Bold = 1;
                title.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                title.Range.InsertParagraphAfter();

                // Основная информация
                Word.Paragraph info = doc.Paragraphs.Add();
                info.Range.Text = $"Дата: {orderDate}\nУслуга: {serviceName}\nСумма к оплате: {price} руб.";
                info.Range.Font.Size = 12;
                info.Range.InsertParagraphAfter();

                // Таблица с информацией
                Word.Table table = doc.Tables.Add(doc.Paragraphs[doc.Paragraphs.Count].Range, 4, 2);
                table.Borders.Enable = 1;
                table.Cell(1, 1).Range.Text = "№ заказа";
                table.Cell(1, 2).Range.Text = "000" + new Random().Next(1000, 9999);
                table.Cell(2, 1).Range.Text = "Услуга";
                table.Cell(2, 2).Range.Text = serviceName;
                table.Cell(3, 1).Range.Text = "Сумма";
                table.Cell(3, 2).Range.Text = price.ToString("C");
                table.Cell(4, 1).Range.Text = "Дата заказа";
                table.Cell(4, 2).Range.Text = orderDate;

                // Генерация QR-кода
                string qrText = $"Услуга: {serviceName}\nСумма: {price} руб.\nДата: {orderDate}";
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(5);

                // Сохранение QR-кода в файл
                string qrCodePath = Path.Combine(Path.GetTempPath(), "qrcode.png");
                qrCodeImage.Save(qrCodePath);

                // Вставка QR-кода в документ
                doc.Paragraphs.Add();
                Word.InlineShape qrShape = doc.InlineShapes.AddPicture(qrCodePath);
                qrShape.Width = 100;
                qrShape.Height = 100;
                qrShape.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                // Подписи
                Word.Paragraph signatures = doc.Paragraphs.Add();
                signatures.Range.Text = "\nКлиент: _______________   Администратор: _______________";
                signatures.Range.InsertParagraphAfter();

                // Место для печати
                Word.Paragraph stamp = doc.Paragraphs.Add();
                stamp.Range.Text = "\nМ.П. (Место для печати)";
                stamp.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                stamp.Range.InsertParagraphAfter();

                // Сохранение документа
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Квитанция.docx");
                doc.SaveAs2(path);
                doc.Close();
                wordApp.Quit();

                // Удаление временного файла QR-кода
                File.Delete(qrCodePath);

                MessageBox.Show($"Чек сохранен на рабочем столе: {path}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка генерации чека: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Просмотр истории заказов
        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            LoadOrderHistory();
        }

        // Загрузка истории заказов
        private void LoadOrderHistory()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            Orders.OrderID, 
                            Services.ServiceName, 
                            Orders.OrderDate, 
                            Orders.Status 
                        FROM 
                            Orders 
                        INNER JOIN 
                            Services ON Orders.ServiceID = Services.ServiceID 
                        WHERE 
                            Orders.UserID = @UserID";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@UserID", userId);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Отображение данных в DataGridView
                    dgvOrderHistory.DataSource = dataTable;
                    dgvOrderHistory.Visible = true; // Показать DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки истории заказов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Поиск услуг
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Введите текст для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            ServiceID, 
                            ServiceName, 
                            Description, 
                            Price 
                        FROM 
                            Services 
                        WHERE 
                            ServiceName LIKE @SearchText";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Отображение результатов поиска
                    dgvServices.DataSource = dataTable;

                    // Логирование поискового запроса
                    string logQuery = "INSERT INTO UserSearchLog (UserID, SearchText, SearchDate) VALUES (@UserID, @SearchText, @SearchDate)";
                    using (SqlCommand logCommand = new SqlCommand(logQuery, connection))
                    {
                        logCommand.Parameters.AddWithValue("@UserID", userId);
                        logCommand.Parameters.AddWithValue("@SearchText", searchText);
                        logCommand.Parameters.AddWithValue("@SearchDate", DateTime.Now);
                        logCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Выход из системы
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var frm = new ChangePasswordForm(userId,connectionString);
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AiChatForm aiChatForm = new AiChatForm(GetUserName(userId),"Оптима 100");
            aiChatForm.Show();
        }
    }
}