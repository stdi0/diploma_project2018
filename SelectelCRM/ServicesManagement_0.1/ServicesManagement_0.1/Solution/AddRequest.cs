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
    //Форма добавления новой заявки
    public partial class AddRequest : Form
    {
        public AddRequest()
        {
            InitializeComponent();
        }

        //Происходит при загрузке формы, настройка элементов управления
        private void AddRequest_Load(object sender, EventArgs e)
        {
            try
            { 
            loadingPanel.Visible = false;

            DBConnection.GetClients();
            client.DataSource = DBConnection.dtClients;
            client.DisplayMember = "Название";
            client.ValueMember = "ID";

            DBConnection.GetManagers();
            manager.DataSource = DBConnection.dtManagers;
            manager.DisplayMember = "ФИО";
            manager.ValueMember = "ID";

            status.Text = "Новый";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Добавление новой заявки в БД, обновление данных в связанных таблицах
        private void addRequestBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (client.Text == "" || manager.Text == "")
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }
                loadingPanel.Visible = true;
                this.Enabled = false;
                Form1.form.Enabled = false;
                DBConnection.NewRequest(client.SelectedValue.ToString(), manager.SelectedValue.ToString(), dateCreate.Value.ToString("yyyy-MM-dd"), status.Text);
                DBConnection.GetRequests();
                Form1.form.requestsAndContractsTable.DataSource = DBConnection.dtRequests;
                Form1.form.PaintGrid();
                Form1.ReadRequestsTableRow(0);
                Form1.form.newContractBtn.Enabled = true;
                Form1.form.UpdateRelatedData(0);
                loadingPanel.Visible = false;
                Form1.form.Enabled = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Вызов формы создания нового клиента
        private void newClient_Click(object sender, EventArgs e)
        {
            AddClient addclntForm = new AddClient();
            addclntForm.Show();
        }
    }
}
