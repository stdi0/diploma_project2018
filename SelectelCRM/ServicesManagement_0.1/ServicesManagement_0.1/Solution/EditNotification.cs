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
    //Форма редактирования уведомления
    public partial class EditNotification : Form
    {
        public EditNotification()
        {
            InitializeComponent();
        }

        //Идентификатор уведомления
        static public string notificationID;
        //Количество оставшихся дней до завершения действия услуги
        static public string days_before;
        //Текст уведомления
        static public string text;
        //Статус уведомления
        static public string status;

        //Происходит при загрузке формы, настройка элементов управления
        private void EditNotification_Load(object sender, EventArgs e)
        {
            try
            { 
            daysBefore.Text = days_before;
            textNotice.Text = text;
            switch (status)
            {
                case "Новое":
                    noticeStatus.SelectedIndex = 0;
                    break;
                case "Отправлено":
                    noticeStatus.SelectedIndex = 1;
                    break;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Редактирование уведомления, обновление данных в связанных таблицах
        private void saveNoticeBtn_Click(object sender, EventArgs e)
        {
            try
            {
            if (daysBefore.Text == "" || textNotice.Text == "" || noticeStatus.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DBConnection.EditNotification(notificationID, daysBefore.Text, textNotice.Text, noticeStatus.Text);
            DBConnection.GetNotifications(Form1.requestNum);
            Notifications.form.notificationsTable.DataSource = DBConnection.dtNotifications;
            Notifications.notification_row = 0;

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
