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
    //форма редактирования группы услуг
    public partial class EditServiceGroup : Form
    {
        public EditServiceGroup()
        {
            InitializeComponent();
        }

        //Происходит при загрузке формы, настройка элементов управления
        private void EditServiceGroup_Load(object sender, EventArgs e)
        {
            try
            { 
            name.Text = Services.serviceGroupName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Редактирование группы услуг, обновление данных в связанных таблицах
        private void saveServicesGroupBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (name.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DBConnection.EditServiceGroup(Services.serviceGroupID, name.Text);
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
