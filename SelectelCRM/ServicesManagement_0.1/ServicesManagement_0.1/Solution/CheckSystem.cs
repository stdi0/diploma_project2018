using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using word = Microsoft.Office.Interop.Word;

namespace Solution
{
    public partial class CheckSystem : Form
    {
        public CheckSystem()
        {
            InitializeComponent();
        }

        public bool CheckDBConnection()
        {
            if (DBConnection.Connect())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckWord()
        {
            try
            {
                var app = new word.Application();
                app.Visible = false;
                var doc = app.Documents.Open(Application.StartupPath + @"\contract.docx");
                doc.Close();
                return true;
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
                return false;
            }
        }

        public bool CheckExcel()
        {
            try
            {
                Excel.Open(Application.StartupPath + @"\invoice.xlsx", false);
                Excel.workBook.Close();
                return true;
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
                return false;
            }

        }

        public void Checker()
        {
            pictureBox2.Refresh();
            label2.Text = "Проверяем соединение с удаленной базой данных...";
            System.Threading.Thread.Sleep(5000);
            // MessageBox.Show("Yes1");
            if (!CheckDBConnection())
            {
                label2.Text = " Неудача, не можем соединиться с базой данных :(";
                return;
            }
            //MessageBox.Show("Yes2");
            System.Threading.Thread.Sleep(5000);
            label2.Text = "Проверяем работу с Word...";
            if (!CheckWord())
            {
                label2.Text = "Что-то не так с Word...";
                return;
            }
            System.Threading.Thread.Sleep(5000);
            //MessageBox.Show("Yes3");
            label2.Text = "Проверяем работу с Excel...";
            if (!CheckWord())
            {
                label2.Text = "Что-то не так с Excel...";
                return;
            }
            System.Threading.Thread.Sleep(5000);
            label2.Text = "Все готово, начинаем!";
            System.Threading.Thread.Sleep(3000);
        }

        private void CheckSystem_Load(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {
            exceptPanel.Visible = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void CheckSystem_Shown(object sender, EventArgs e)
        {
            Checker();
        }

        private void pictureBox2_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            
        }
    }
}
