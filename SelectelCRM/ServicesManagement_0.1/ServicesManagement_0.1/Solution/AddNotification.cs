using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Solution
{
    //Форма добавления нового уведомления
    public partial class AddNotification : Form
    {
        public AddNotification()
        {
            InitializeComponent();
        }

        static public string saleID;

        private void AddNotification_Load(object sender, EventArgs e)
        {

        }

        //Добавление нового уведомления к услуге в БД
        private void addNotice_Click(object sender, EventArgs e)
        {
            try
            { 
            if (daysBefore.Text == "" || text.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DBConnection.NewNotification(saleID, daysBefore.Text, text.Text);
            DBConnection.GetNotifications(Form1.requestNum);
            Notifications.form.notificationsTable.DataSource = DBConnection.dtNotifications;
            Notifications.notification_row = 0;

            Form1.form.UpdateNoticesQty();
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Ограничение ввода буквенных символов
        private void daysBefore_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
                return;
            else e.Handled = true;
        }
    }
}
