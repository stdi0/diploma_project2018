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
    //Форма добавления нового менеджера
    public partial class AddManager : Form
    {
        public AddManager()
        {
            InitializeComponent();
        }

        private void AddManager_Load(object sender, EventArgs e)
        {
            
        }

        //Добавление нового менеджера в БД, обновление данных в связанных таблицах
        private void addManagerBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (fullName.Text == "" || adress.Text == "" || phone.Text == "" || login.Text == "" || password.Text == "")
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }
                DBConnection.NewManager(fullName.Text, adress.Text, phone.Text, dateBirth.Value.ToString("yyyy-MM-dd"), dateStartWork.Value.ToString("yyyy-MM-dd"), login.Text, password.Text);
                try
                {
                    DBConnection.GetManagers();
                    Managers.formDescriptor.managersTable.DataSource = DBConnection.dtManagers;
                    Managers.ReadManagersTableRow(0);
                }
                catch { }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
