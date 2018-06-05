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
    //Форма добавления группы услуг
    public partial class AddServiceGroup : Form
    {
        public AddServiceGroup()
        {
            InitializeComponent();
        }

        //Добавление новой группы услуг в БД, обновление данных в связанных таблицах
        private void addServicesGroup_Click(object sender, EventArgs e)
        {
            try
            { 
            if (name.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DBConnection.NewServiceGroup(name.Text);
            DBConnection.GetServicesGroups();
            Services.formDescriptor.groupsTable.DataSource = DBConnection.dtServicesGroups;
            Services.ReadGroupsTableRow(0);
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
