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
    //Форма "Клиенты"
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }

        //Дескриптор формы
        static public Clients formDescriptor;
        //Идентификатор клиента
        static public string clientID;
        //Название клиента
        static public string name;
        //Контактное лицое
        static public string contact_person;
        //Телефон
        static public string phone;
        //Код страны
        static public string countryID;
        //Код города
        static public string cityID;
        //Адрес
        static public string adress;
        //Email
        static public string email;
        //Счет в банке
        static public string bankAccount;
        //ИНН
        static public string INN;

        //Получение и сохранение информации о выбранной строке в таблице с клиентами
        static public void ReadClientsTableRow(int row_idx)
        {
            clientID = formDescriptor.clientsTable.Rows[row_idx].Cells[0].Value.ToString();
            name = formDescriptor.clientsTable.Rows[row_idx].Cells[1].Value.ToString();
            contact_person = formDescriptor.clientsTable.Rows[row_idx].Cells[2].Value.ToString();
            phone = formDescriptor.clientsTable.Rows[row_idx].Cells[3].Value.ToString();
            adress = formDescriptor.clientsTable.Rows[row_idx].Cells[6].Value.ToString();
            email = formDescriptor.clientsTable.Rows[row_idx].Cells[7].Value.ToString();
            bankAccount = formDescriptor.clientsTable.Rows[row_idx].Cells[8].Value.ToString();
            INN = formDescriptor.clientsTable.Rows[row_idx].Cells[9].Value.ToString();
            countryID = formDescriptor.clientsTable.Rows[row_idx].Cells[10].Value.ToString();
            cityID = formDescriptor.clientsTable.Rows[row_idx].Cells[11].Value.ToString();
        }

        //Выполняется при загрузке формы, заполнение таблицы данными из БД, настройка элементов управления
        private void Clients_Load(object sender, EventArgs e)
        {
            try
            { 
            reconnectBtn.Font = new Font(reconnectBtn.Font.Name, 10, FontStyle.Regular | FontStyle.Underline);
            formDescriptor = this;
            clientsTable.RowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f1f2f6");
            clientsTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            DBConnection.GetClients();
            clientsTable.DataSource = DBConnection.dtClients;
            clientsTable.CurrentCell = clientsTable[0, 0];
            clientsTable.Rows[0].Selected = true;
            clientsTable.Columns[10].Visible = false;
            clientsTable.Columns[11].Visible = false;
            if (DBConnection.dtClients.Rows.Count > 0)
            {
                ReadClientsTableRow(0);
            }

            DBConnection.GetCountries();
            country.DataSource = DBConnection.dtCountries;
            country.DisplayMember = "Название";
            country.ValueMember = "ID";
            DBConnection.GetCities();
            city.DataSource = DBConnection.dtCities;
            city.DisplayMember = "Название";
            city.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Вызов формы добавления нового клиента
        private void addClientBtn_Click(object sender, EventArgs e)
        {
            AddClient addclntForm = new AddClient(); //экземпляр формы добавления клиента
            addclntForm.Show();
        }

        //Вызов формы редактирования клиента
        private void editClientBtn_Click(object sender, EventArgs e)
        {
            if (clientsTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для редактирования!");
                return;
            }
            EditClient edtclntForm = new EditClient(); //экземпляр формы редактирования клиента
            edtclntForm.Show();
        }

        //Осуществляет получение информации по выбранной строке таблицы с клиентами
        private void clientsTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    clientsTable.Rows[e.RowIndex].Selected = true;
                    ReadClientsTableRow(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Поиск клиентов по названию
        private void searchNameBtn_Click(object sender, EventArgs e)
        {
            try
            {
            DataRow[] dtRows = DBConnection.dtClients.Select("Название LIKE '*" + clientName.Text + "*'"); //запись результатов запроса поиска
            if (dtRows.Count() == 0)
            {
                MessageBox.Show("Запись не найдена!");
                return;
            }
            bool flag = false; //флаг успеха
            for (int i = 0; i < clientsTable.RowCount; i++)
            {
                if (clientsTable.Rows[i].Cells[0].Value.ToString() == dtRows[0].ItemArray[0].ToString())
                {
                    clientsTable.CurrentCell = clientsTable[0, i];
                    clientsTable.Rows[i].Selected = true;
                    //MessageBox.Show("Success!");
                    flag = true;

                    ReadClientsTableRow(i);

                    return;
                }
            }
            if (!flag)
            {
                MessageBox.Show("Запись не найдена!");
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Отбор клиентов по стране
        private void filterCountryBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            clientName.Text = "";
            DBConnection.FilterCountry(country.SelectedValue.ToString());
            clientsTable.DataSource = DBConnection.dtClients;
            if (DBConnection.dtClients.Rows.Count > 0)
            {
                ReadClientsTableRow(0);
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Отбор клиентов по городу
        private void filterCityBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            clientName.Text = "";
            clientsTable.DataSource = null;
            DBConnection.FilterCity(city.SelectedValue.ToString());
            clientsTable.DataSource = DBConnection.dtClients;
            if (DBConnection.dtClients.Rows.Count > 0)
            {
                ReadClientsTableRow(0);
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Удаление выбранного клиента
        private void delClientBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (clientsTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для удаления!");
                return;
            }
            clientsTable.DataSource = null;
            DBConnection.DeleteClient(clientID);
            DBConnection.GetClients();
            clientsTable.DataSource = DBConnection.dtClients;
            if (DBConnection.dtClients.Rows.Count > 0)
            {
                ReadClientsTableRow(0);
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            try
            {
            clientsTable.DataSource = null;
            DBConnection.GetClients();
            clientsTable.DataSource = DBConnection.dtClients;
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Пункт меню “Аналитика”
        private void analyticsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Stats stts = new Stats(); //экземпляр формы аналитики
            stts.Show();
            
        }

        //Пункт меню “Заявки и договора”
        private void requestsAndContractsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1.form.Show();
           
        }

        //Пункт меню "Клиенты"
        private void clientsBtn_Click(object sender, EventArgs e)
        {
            //Managers mngrs = new Managers();
            //mngrs.Show();
        }

        //Пункт меню “Менеджеры”
        private void managersBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Managers mngrs = new Managers(); //экземпляр формы "Менеджеры"
            mngrs.Show();
        }

        //Пункт меню “Услуги”
        private void servicesBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Services srvcs = new Services(); //экземпляр формы "Услуги"
            srvcs.Show();
        }

        //Пункт меню “Справочники”
        private void refsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Refs rfs = new Refs(); //экземпляр формы "Справочники"
            rfs.Show();
        }

        //Отмена действующих фильтров в таблице с клиентами
        private void filterCancelBtn_Click_1(object sender, EventArgs e)
        {
            try
            { 
            clientName.Text = "";
            clientsTable.DataSource = null;
            DBConnection.GetClients();
            clientsTable.DataSource = DBConnection.dtClients;
            if (DBConnection.dtClients.Rows.Count > 0)
            {
                ReadClientsTableRow(0);
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
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
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization.form.Show();
        }

        //Возврат на форму авторизации
        private void Clients_FormClosed(object sender, FormClosedEventArgs e)
        {
            Authorization.form.Show();
        }

        //Переподключение к базе данных
        private void reconnectBtn_Click(object sender, EventArgs e)
        {
            if (DBConnection.Connect())
            {
                MessageBox.Show("Успешно!");
            }
        }

        //Смена стиля надписи при наведение
        private void reconnectBtn_MouseMove(object sender, MouseEventArgs e)
        {
            reconnectBtn.Font = new Font(reconnectBtn.Font.Name, 10, FontStyle.Bold | FontStyle.Underline);
        }

        //Возврат исходного стиля надписи
        private void reconnectBtn_MouseLeave(object sender, EventArgs e)
        {
            reconnectBtn.Font = new Font(reconnectBtn.Font.Name, 10, FontStyle.Regular | FontStyle.Underline);
        }

        //Закрытие панели с ошибками
        private void label22_Click(object sender, EventArgs e)
        {
            exceptPanel.Visible = false;
        }
    }
}
