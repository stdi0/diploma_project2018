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
    //Форма редактирования услуги
    public partial class EditService : Form
    {
        public EditService()
        {
            InitializeComponent();
        }

        //Происходит при загрузке формы, настройка элементов управления
        private void EditService_Load(object sender, EventArgs e)
        {
            name.Text = Services.name;
            cost.Text = Services.cost;
            info.Text = Services.info;
        }

        //Редактирование услуги, обновление данных в связанных таблицах
        private void saveServiceBtn_Click(object sender, EventArgs e)
        {
            try
            {
            if (name.Text == "" || cost.Text == "" || info.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DBConnection.EditServiceInGroup(Services.serviceID, name.Text, info.Text, cost.Text);
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
