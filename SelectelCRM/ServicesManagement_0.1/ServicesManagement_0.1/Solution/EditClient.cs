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
    //Форма редактирования клиента
    public partial class EditClient : Form
    {
        public EditClient()
        {
            InitializeComponent();
        }

        //Происходит при загрузке формы, настройка элементов управления
        private void EditClient_Load(object sender, EventArgs e)
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

            name.Text = Clients.name;
            contactPerson.Text = Clients.contact_person;
            phone.Text = Clients.phone;
            country.SelectedValue = Clients.countryID;
            city.SelectedValue = Clients.cityID;
            adress.Text = Clients.adress;
            email.Text = Clients.email;
            bankAccount.Text = Clients.bankAccount;
            INN.Text = Clients.INN;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        //Редактирование клиента, обновление данных в связанных таблицах
        private void saveClientBtn_Click(object sender, EventArgs e)
        {
            try
            {
            if (name.Text == "" || contactPerson.Text == "" || phone.Text == "" || adress.Text == "" || email.Text == "" || bankAccount.Text == "" || INN.Text == "" || country.Text == "" || city.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DBConnection.EditClient(Clients.clientID, name.Text, contactPerson.Text, phone.Text, country.SelectedValue.ToString(), city.SelectedValue.ToString(), adress.Text, email.Text, bankAccount.Text, INN.Text);
            DBConnection.GetClients();
            Clients.formDescriptor.clientsTable.DataSource = DBConnection.dtClients;
            Clients.ReadClientsTableRow(0);
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
