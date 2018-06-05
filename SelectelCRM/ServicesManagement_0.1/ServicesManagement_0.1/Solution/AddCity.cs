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
    //Форма добавления нового города
    public partial class AddCity : Form
    {
        public AddCity()
        {
            InitializeComponent();
        }

        //Добавление нового города в справочник, обновление данных в связанных таблицах
        private void addCityBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (cityCode.Text == "" || name.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DBConnection.NewCity(cityCode.Text, name.Text);
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

        private void AddCity_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
