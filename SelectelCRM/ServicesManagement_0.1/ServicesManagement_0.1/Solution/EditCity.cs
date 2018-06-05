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
    //Форма редактирования города в справочнике
    public partial class EditCity : Form
    {
        public EditCity()
        {
            InitializeComponent();
        }

        //Код города
        static public string cityID;
        //Название города
        static public string cityName;

        //Происходит при загрузке формы, настройка элементов управления
        private void EditCity_Load(object sender, EventArgs e)
        {
            try
            { 
            cityCode.Text = cityID;
            name.Text = cityName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Редактирование города, обновление данных в связанных таблицах
        private void editCityBtn_Click(object sender, EventArgs e)
        {
            try
            {
            if (name.Text == "" || cityCode.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DBConnection.EditCity(cityID, cityName, cityCode.Text, name.Text);
            DBConnection.GetCities();
            Refs.formDescriptor.citiesTable.DataSource = DBConnection.dtCities;
            Refs.ReadCityTableRow(0);
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
