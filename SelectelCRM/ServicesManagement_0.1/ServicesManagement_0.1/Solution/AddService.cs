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
    //Форма добавления новой услуги
    public partial class AddService : Form
    {
        public AddService()
        {
            InitializeComponent();
        }

        private void AddService_Load(object sender, EventArgs e)
        {
            
        }

        //Добавление новой услуги в БД, обновление данных в связанных таблицах
        private void addServiceBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (name.Text == "" || cost.Text == "" || info.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DBConnection.NewServiceInGroup(Services.serviceGroupID, name.Text, info.Text, cost.Text);
            DBConnection.GetServicesInGroup(Services.serviceGroupID);
            Services.formDescriptor.servicesTable.DataSource = DBConnection.dtServicesInGroup;
            Services.ReadServicesTableRow(0);
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Ограничение ввода буквенных символов
        private void cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
                return;
            else e.Handled = true;
        }
    }
}
