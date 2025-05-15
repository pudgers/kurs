
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace kurs
{

    public partial class AiChatForm: Form
    {
        String UserName { get; set; }
        String UserTarif { get; set; }

        OllamaController controller = new OllamaController();

        bool IsFirstRequest = true;
        public AiChatForm(string name, string tarif)
        {
            InitializeComponent();
            this.UserName = name;
            this.UserTarif = tarif;
        }

        private void AiChatForm_Load(object sender, EventArgs e)
        {
        


        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.button1.Text = "ЗАГРУЗКА...";
            this.button1.Enabled = false;

            var messageText = symptomsTb.Text;
            symptomsTb.Text = "";
            if (IsFirstRequest)
            {
                var data = await controller.SendPrompt(messageText, UserName, "Оптика 100 мб/c");
                this.richTextBox1.Clear();
                richTextBox1.AppendText ( "<< " + messageText + Environment.NewLine + Environment.NewLine,Color.Black);
                richTextBox1.AppendText(">>" + data + Environment.NewLine + Environment.NewLine, Color.Blue);
                IsFirstRequest = false;
            }
            else
            {
                richTextBox1.AppendText("<< " + messageText + Environment.NewLine + Environment.NewLine, Color.Black);
                var text = await controller.SendMessage(messageText);
                richTextBox1.AppendText(">> " + text + Environment.NewLine + Environment.NewLine, Color.Blue);
    
            }
      
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            this.richTextBox1.ScrollToCaret();

            this.button1.Text = "Отправить";
            this.button1.Enabled = true;

        }

        private void symptomsTb_KeyDown(object sender, KeyEventArgs e)
        {
            
 
        }

        private void symptomsTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
