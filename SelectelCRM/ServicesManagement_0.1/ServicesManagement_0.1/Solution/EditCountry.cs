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
    //Форма редактирования страны в справочнике
    public partial class EditCountry : Form
    {
        public EditCountry()
        {
            InitializeComponent();
        }

        //Код страны
        static public string countryID;
        //Название страны
        static public string countryName;

        //Происходит при загрузке формы, настройка элементов управления
        private void EditCountry_Load(object sender, EventArgs e)
        {
            try
            { 
            countryCode.Text = countryID;
            name.Text = countryName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Редактирование страны, обновление данных в связанных таблицах
        private void saveCountryBtn_Click(object sender, EventArgs e)
        {
            try
            {
            if (countryCode.Text == "" || name.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DBConnection.EditCountry(countryID, countryName, countryCode.Text, name.Text);
            DBConnection.GetCountries();
            Refs.formDescriptor.countriesTable.DataSource = DBConnection.dtCountries;
            Refs.ReadCountryTableRow(0);
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
