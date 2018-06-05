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
    //Форма редактирования менеджера
    public partial class EditManager : Form
    {
        public EditManager()
        {
            InitializeComponent();
        }

        //Происходит при загрузке формы, настройка элементов управления
        private void EditManager_Load(object sender, EventArgs e)
        {
            try
            { 
            fullName.Text = Managers.full_name;
            adress.Text = Managers.adress;
            phone.Text = Managers.phone;
            dateBirth.Value = Convert.ToDateTime(Managers.date_of_birth);
            dateStartWork.Value = Convert.ToDateTime(Managers.date_start_work);
            login.Text = Managers.login;
            password.Text = Managers.password;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Редактирование менеджера, обновление данных в связанных таблицах
        private void saveManagerBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (fullName.Text == "" || adress.Text == "" || phone.Text == "" || login.Text == "" || password.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DBConnection.EditManager(Managers.managerID, fullName.Text, adress.Text, phone.Text, dateBirth.Value.ToString("yyyy-MM-dd"), dateStartWork.Value.ToString("yyyy-MM-dd"), login.Text, password.Text);
            DBConnection.GetManagers();
            Managers.formDescriptor.managersTable.DataSource = DBConnection.dtManagers;
            Managers.ReadManagersTableRow(0);
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
