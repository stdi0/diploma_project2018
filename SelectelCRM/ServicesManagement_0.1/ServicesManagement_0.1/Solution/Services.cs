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
    //Форма "Услуги"
    public partial class Services : Form
    {
        public Services()
        {
            InitializeComponent();
        }

        //Дескриптор формы
        static public Services formDescriptor;
        //Идентификатор услуги
        static public string serviceID;
        //Название услуги
        static public string name;
        //Стоимость
        static public string cost;
        //Информация об услуге
        static public string info;
        //Идентификатор группы услуг
        static public string serviceGroupID;
        //Название группы услуг
        static public string serviceGroupName;

        //Получение и сохранение информации о выбранной строке в таблице с группами услуг
        static public void ReadGroupsTableRow(int row_idx)
        {
            serviceGroupID = formDescriptor.groupsTable.Rows[row_idx].Cells[0].Value.ToString();
            serviceGroupName = formDescriptor.groupsTable.Rows[row_idx].Cells[1].Value.ToString(); ;
        }

        //Получение и сохранение информации о выбранной строке в таблице с услугами
        static public void ReadServicesTableRow(int row_idx)
        {
            serviceID = formDescriptor.servicesTable.Rows[row_idx].Cells[0].Value.ToString();
            name = formDescriptor.servicesTable.Rows[row_idx].Cells[2].Value.ToString();
            info = formDescriptor.servicesTable.Rows[row_idx].Cells[3].Value.ToString();
            cost = formDescriptor.servicesTable.Rows[row_idx].Cells[4].Value.ToString();
        }

        //Выполняется при загрузке формы, заполнение таблиц данными из БД, настройка элементов управления
        private void Services_Load(object sender, EventArgs e)
        {
            try
            {
                formDescriptor = this;
                reconnectBtn.Font = new Font(reconnectBtn.Font.Name, 10, FontStyle.Regular | FontStyle.Underline);
                servicesTable.RowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f1f2f6");
                servicesTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                groupsTable.RowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f1f2f6");
                groupsTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                DBConnection.GetServicesGroups();
                groupsTable.DataSource = DBConnection.dtServicesGroups;
                if (groupsTable.Rows.Count > 0)
                {
                    groupsTable.Rows[0].Selected = true;
                    ReadGroupsTableRow(0);
                }
                DBConnection.GetServicesInGroup(serviceGroupID);
                servicesTable.DataSource = DBConnection.dtServicesInGroup;
                if (servicesTable.Rows.Count > 0)
                {
                    servicesTable.Rows[0].Selected = true;
                    ReadServicesTableRow(0);
                }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Вызов формы добавления услуги
        private void addServiceBtn_Click(object sender, EventArgs e)
        {
            AddService addsrvc = new AddService(); //экземпляр формы добавления услуги
            addsrvc.Show();
        }

        //Осуществляет получение информации по выбранной строке таблицы с услугами
        private void servicesTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            { 
            if (e.RowIndex >= 0)
            {
                servicesTable.Rows[e.RowIndex].Selected = true;
                ReadServicesTableRow(e.RowIndex);
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Вызов формы редактирования выбранной услуги
        private void editServiceBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (servicesTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для редактирования!");
                return;
            }
            EditService edtsrvc = new EditService(); //экземпляр формы редактирования услуги
            edtsrvc.Show();
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Вызов формы добавления новой группы услуг
        private void addGroupBtn_Click(object sender, EventArgs e)
        {
            AddServiceGroup addsrvcgrp = new AddServiceGroup(); //экземпляр формы добавления группы услуг
            addsrvcgrp.Show();
        }

        //Вызов формы редактирования выбранной группы услуг
        private void editGroupBtn_Click(object sender, EventArgs e)
        {
            try
            { 
                if (groupsTable.Rows.Count == 0)
                {
                    MessageBox.Show("Нет записей для редактирования!");
                    return;
                }
                EditServiceGroup edtsrvcgrp = new EditServiceGroup(); //экземпляр формы редактирования группы услуг
                edtsrvcgrp.Show();
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Удаление выбранной группы услуг
        private void delGroupBtn_Click(object sender, EventArgs e)
        {
            try
            {
            if (groupsTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для удаления!");
                return;
            }
            DialogResult result = MessageBox.Show("Подтвердите удаление.", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); //диалоговое окно
            if (result == DialogResult.OK)
            {
                DBConnection.DeleteServiceGroup(serviceGroupID);
                DBConnection.GetServicesGroups();
                groupsTable.DataSource = DBConnection.dtServicesGroups;
                if (DBConnection.dtServicesGroups.Rows.Count > 0)
                {
                    ReadGroupsTableRow(0);
                }
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Удаление выбранной услуги
        private void delServiceBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (servicesTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для удаления!");
                return;
            }
            DialogResult result = MessageBox.Show("Подтвердите удаление.", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); //диалоговое окно
            if (result == DialogResult.OK)
            {
                DBConnection.DeleteService(serviceID);
                DBConnection.GetServicesInGroup(serviceGroupID);
                servicesTable.DataSource = DBConnection.dtServicesInGroup;
                if (DBConnection.dtServicesInGroup.Rows.Count > 0)
                {
                    ReadServicesTableRow(0);
                }
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Пункт меню “Заявки и договоры”
        private void requestsAndContractsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1.form.Show();
        }

        //Пункт меню “Клиенты”
        private void clientsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Clients clnts = new Clients(); //экземпляр формы "Клиенты"
            clnts.Show();
        }

        //Пункт меню “Менеджеры”
        private void managersBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Managers mngrs = new Managers(); //экземпляр формы "Клиенты"
            mngrs.Show();
        }

        //Пункт меню “Аналитика”
        private void analyticsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Stats stts = new Stats(); //экземпляр формы "Клиенты"
            stts.Show();
        }

        //Пункт меню “Справочники”
        private void refsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Refs rfs = new Refs(); //экземпляр формы "Клиенты"
            rfs.Show();
        }

        //Осуществляет получение информации по выбранной строке таблицы с услугами, заполнение данными зависимых таблиц
        private void groupsTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    groupsTable.Rows[e.RowIndex].Selected = true;
                    ReadGroupsTableRow(e.RowIndex);
                    DBConnection.GetServicesInGroup(serviceGroupID);
                    servicesTable.DataSource = DBConnection.dtServicesInGroup;
                    if (servicesTable.Rows.Count > 0)
                    {
                        ReadServicesTableRow(0);
                    }
                }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Закрытие панели ошибок
        private void label22_Click(object sender, EventArgs e)
        {
            exceptPanel.Visible = false;
        }

        //Возврат на форму авторизации
        private void logoutLbl_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization.form.Show();
        }

        //Возврат на форму авторизации
        private void logoutPanel_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization.form.Show();
        }

        //Возврат на форму авторизации
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization.form.Show();
        }

        //Возврат на форму авторизации
        private void Services_FormClosed(object sender, FormClosedEventArgs e)
        {
            Authorization.form.Show();
        }

        //Переподключение к базе данных
        private void reconnectBtn_Click_1(object sender, EventArgs e)
        {
            if (DBConnection.Connect())
            {
                MessageBox.Show("Успешно!");
            }
        }

        //Смена стиля надписи при наведении
        private void reconnectBtn_MouseMove(object sender, MouseEventArgs e)
        {
            reconnectBtn.Font = new Font(reconnectBtn.Font.Name, 10, FontStyle.Bold | FontStyle.Underline);
        }

        //Возврат исходного стиля надписи
        private void reconnectBtn_MouseLeave(object sender, EventArgs e)
        {
            reconnectBtn.Font = new Font(reconnectBtn.Font.Name, 10, FontStyle.Regular | FontStyle.Underline);
        }

    }
}
