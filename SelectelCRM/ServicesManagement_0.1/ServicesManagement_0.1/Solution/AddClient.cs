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
    //Форма добавления нового клиента
    public partial class AddClient : Form
    {
        public AddClient()
        {
            InitializeComponent();
        }

        //Происходит при загрузке формы, настройка элементов управления
        private void AddClient_Load(object sender, EventArgs e)
        {
            try
            {
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
                MessageBox.Show(ex.ToString());
            }
        }

        //Добавление нового клиента в справочник, обновление данных в связанных таблицах
        private void addClientBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (name.Text == "" || contactPerson.Text == "" || phone.Text == "" || adress.Text == "" || email.Text == "" || bankAccount.Text == "" || INN.Text == "" || country.Text == "" || city.Text == "")
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }
                DBConnection.NewClient(name.Text, contactPerson.Text, phone.Text, country.SelectedValue.ToString(), city.SelectedValue.ToString(), adress.Text, email.Text, bankAccount.Text, INN.Text);
                DBConnection.GetClients();
                try
                {
                    Clients.formDescriptor.clientsTable.DataSource = DBConnection.dtClients;
                    Clients.ReadClientsTableRow(0);
                }
                catch { }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Ограничение ввода буквенных символов
        private void bankAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
                return;
            else e.Handled = true;
        }

        //Ограничение ввода буквенных символов
        private void INN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
                return;
            else e.Handled = true;
        }
    }
}
