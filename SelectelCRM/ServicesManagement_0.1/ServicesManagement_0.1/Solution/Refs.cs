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
    //Форма "Справочники"
    public partial class Refs : Form
    {
        public Refs()
        {
            InitializeComponent();
        }
        //Дескриптор формы
        static public Refs formDescriptor;
        //Код страны
        static public string countryID;
        //Название страны
        static public string countryName;
        //Код города
        static public string cityID;
        //Название города
        static public string cityName;

        //Получение и сохранение информации о выбранной строке в таблице со странами
        static public void ReadCountryTableRow(int row_idx)
        {
            countryID = formDescriptor.countriesTable.Rows[row_idx].Cells[0].Value.ToString();
            countryName = formDescriptor.countriesTable.Rows[row_idx].Cells[1].Value.ToString();
        }

        //Получение и сохранение информации о выбранной строке в таблице с городами
        static public void ReadCityTableRow(int row_idx)
        {
            cityID = formDescriptor.citiesTable.Rows[row_idx].Cells[0].Value.ToString();
            cityName = formDescriptor.citiesTable.Rows[row_idx].Cells[1].Value.ToString();
        }

        //Выполняется при загрузке формы, заполнение таблиц данными из БД, настройка элементов управления
        private void Refs_Load(object sender, EventArgs e)
        {
            try
            {
                reconnectBtn.Font = new Font(reconnectBtn.Font.Name, 10, FontStyle.Regular | FontStyle.Underline);
                DBConnection.GetCountries();
                countriesTable.DataSource = DBConnection.dtCountries;
                DBConnection.GetCities();
                citiesTable.DataSource = DBConnection.dtCities;

                countriesTable.CurrentCell = countriesTable[0, 0];
                countriesTable.Rows[0].Selected = true;

                citiesTable.CurrentCell = citiesTable[0, 0];
                citiesTable.Rows[0].Selected = true;

                formDescriptor = this;

                if (countriesTable.Rows.Count > 0)
                {
                    ReadCountryTableRow(0);
                }
                if (citiesTable.Rows.Count > 0)
                {
                    ReadCityTableRow(0);
                }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Вызов формы добавления новой страны
        private void addCountryBtn_Click(object sender, EventArgs e)
        {
            AddContry addcntry = new AddContry(); //экземпляр формы добавления страны в справочник
            addcntry.Show();
        }

        //Вызов формы редактирования страны
        private void editCountryBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (countriesTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для редактирования!");
                return;
            }
            EditCountry.countryID = countryID;
            EditCountry.countryName = countryName;
            EditCountry edtcntry = new EditCountry();
            edtcntry.Show();
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Осуществляет получение информации по выбранной строке таблицы со странами
        private void countriesTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            { 
            if (e.RowIndex >= 0)
            {
                countriesTable.Rows[e.RowIndex].Selected = true;
                ReadCountryTableRow(e.RowIndex);
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Вызов формы добавления нового города
        private void addCityBtn_Click(object sender, EventArgs e)
        {
            AddCity addcty = new AddCity(); //экземпляр формы добавления города
            addcty.Show();
        }

        //Вызов формы редактирования города
        private void editCityBtn_Click(object sender, EventArgs e)
        {
            try
            { 
                if (citiesTable.Rows.Count == 0)
                {
                    MessageBox.Show("Нет записей для редактирования!");
                    return;
                }
                EditCity.cityID = cityID;
                EditCity.cityName = cityName;
                EditCity edtcty = new EditCity(); //экземпляр формы редактирования города
                edtcty.Show();
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Осуществляет получение информации по выбранной строке таблицы с городами
        private void citiesTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            { 
            if (e.RowIndex >= 0)
            {
                citiesTable.Rows[e.RowIndex].Selected = true;
                ReadCityTableRow(e.RowIndex);
            }
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
            Stats stts = new Stats(); //экземпляр формы "Аналитика"
            stts.Show();
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
            Clients clnts = new Clients(); //экземпляр формы "Клиента"
            clnts.Show();
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

        //Удаление выбранной страны
        private void delCountryBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (countriesTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для удаления!");
                return;
            }
            DialogResult result = MessageBox.Show("Подтвердите удаление.", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                DBConnection.DeleteCountry(countryID);
                DBConnection.GetCountries();
                countriesTable.DataSource = DBConnection.dtCountries;
                if (DBConnection.dtCountries.Rows.Count > 0)
                {
                    ReadCountryTableRow(0);
                }
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Удаление выбранного города
        private void delCityBtn_Click(object sender, EventArgs e)
        {
            try
            {
            if (citiesTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для удаления!");
                return;
            }
            DialogResult result = MessageBox.Show("Подтвердите удаление.", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                DBConnection.DeleteCity(cityID);
                DBConnection.GetCities();
                citiesTable.DataSource = DBConnection.dtCities;
                if (DBConnection.dtCities.Rows.Count > 0)
                {
                    ReadCityTableRow(0);
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
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization.form.Show();
        }

        //Возврат на форму авторизации
        private void Refs_FormClosed(object sender, FormClosedEventArgs e)
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
