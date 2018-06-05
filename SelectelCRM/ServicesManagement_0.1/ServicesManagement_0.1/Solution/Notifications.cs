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
    //Форма "Уведомления"
    public partial class Notifications : Form
    {
        public Notifications()
        {
            InitializeComponent();
        }

        //Номер договора
        static public string contractNum;
        //Email
        static public string email;
        //Дескриптор формы
        static public Notifications form;
        //Номер строкаи таблицы с уведомлениями
        static public int notification_row;

        //Удаление выбранного уведомления
        private void delNoticeBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Подтвердите удаление.", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); //диалоговое окно
            if (result == DialogResult.OK)
            {
                DBConnection.DeleteNotification(notificationsTable.Rows[notification_row].Cells[4].Value.ToString());
                DBConnection.GetNotifications(Form1.requestNum);
                notificationsTable.DataSource = DBConnection.dtNotifications;
                notification_row = 0;

                Form1.form.UpdateNoticesQty();
            }
        }

        //Выполняется при загрузке формы, заполнение таблиц данными из БД, настройка элементов управления
        private void Notifications_Load(object sender, EventArgs e)
        {
            form = this;
            label1.Text = email;
            label4.Text = contractNum;
            DBConnection.GetServicesInContract(Form1.requestNum);
            servicesTable.DataSource = DBConnection.dtServicesInContract;
            servicesTable.Columns[0].Visible = false;
            servicesTable.Columns[1].Visible = false;
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn(); //объект кнопки в колонке таблицы
            btn.HeaderText = "Создать";
            btn.Text = "Уведомление";
            btn.UseColumnTextForButtonValue = true;
            servicesTable.Columns.Add(btn);
            //saleID = servicesTable.Rows[0].Cells[0].Value.ToString();
            DBConnection.GetNotifications(Form1.requestNum);
            notificationsTable.DataSource = DBConnection.dtNotifications;
            notificationsTable.Columns[4].Visible = false;
            notification_row = 0;
                //notificationID = notificationsTable.Rows[0].Cells[4].Value.ToString();
                //days_before = notificationsTable.Rows[0].Cells[1].Value.ToString();
                //text = notificationsTable.Rows[0].Cells[2].Value.ToString();

        }

        //Вызов формы добавления уведомления к услуге по нажатию на кнопку “Уведомление”
        private void servicesTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                AddNotification.saleID = servicesTable.Rows[e.RowIndex].Cells[0].Value.ToString();
                AddNotification addNtfc = new AddNotification();
                addNtfc.Show();
            }
        }

        //Осуществляет получение информации по выбранной строке таблицы с уведомлениями
        private void notificationsTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                notification_row = e.RowIndex;
            }
        }

        //Вызов формы редактирования выбранного уведомления
        private void editNoticeBtn_Click(object sender, EventArgs e)
        {
            if (notificationsTable.Rows.Count == 0)
            {
                MessageBox.Show("Уведомление не выбрано!");
                return;
            }
            EditNotification.notificationID = notificationsTable.Rows[notification_row].Cells[4].Value.ToString();
            EditNotification.days_before = notificationsTable.Rows[notification_row].Cells[1].Value.ToString();
            EditNotification.text = notificationsTable.Rows[notification_row].Cells[2].Value.ToString();
            EditNotification.status = notificationsTable.Rows[notification_row].Cells[3].Value.ToString();
            EditNotification edtNtfc = new EditNotification();
            edtNtfc.Show();
        }
    }
}
