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
    //Форма добавления новой страны в справочник
    public partial class AddContry : Form
    {
        public AddContry()
        {
            InitializeComponent();
        }

        //Добавление новой страны в справочник, обновление данных в связанных таблицах
        private void addCountryBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (countryCode.Text == "" || name.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DBConnection.NewCountry(countryCode.Text, name.Text);
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
