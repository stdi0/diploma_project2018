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
    //Форма редактирования заявок/договоров
    public partial class EditRequest : Form
    {
        public EditRequest()
        {
            InitializeComponent();
        }

        //Дата заявки
        static public string date_request;
        //Дата договора
        static public string date_contract;
        //Номер клиента
        static public string client_num;
        //Номер менеджера
        static public string manager_num;

        private void button2_Click(object sender, EventArgs e)
        {
            AddClient addclntForm = new AddClient(); //экземпляр формы добавления клиента
            addclntForm.Show();
        }

        //Редактирование заявки/договора, обновление данных в связанных таблицах
        private void saveRequestBtn_Click(object sender, EventArgs e)
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
            DBConnection.EditRequest(Form1.requestNum, client.SelectedValue.ToString(), manager.SelectedValue.ToString(), dateRequest.Value.ToString("yyyy-MM-dd"), dateContract.Value.ToString("yyyy-MM-dd"));
            DBConnection.GetRequests();
            Form1.form.requestsAndContractsTable.DataSource = DBConnection.dtRequests;
            Form1.form.PaintGrid();
            Form1.ReadRequestsTableRow(0);
            Form1.form.UpdateRelatedData(0);
            this.Enabled = true;
            Form1.form.Enabled = true;
            loadingPanel.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        //Происходит при загрузке формы, настройка элементов управления
        private void EditRequest_Load(object sender, EventArgs e)
        {

            try
            { 
            loadingPanel.Visible = false;
            DBConnection.GetManagers();
            manager.DataSource = DBConnection.dtManagers;
            manager.DisplayMember = "ФИО";
            manager.ValueMember = "ID";
            DBConnection.GetClients();
            client.DataSource = DBConnection.dtClients;
            client.DisplayMember = "Название";
            client.ValueMember = "ID";
            dateRequest.Value = Convert.ToDateTime(date_request);
            dateContract.Value = Convert.ToDateTime(date_contract);
            client.SelectedValue = client_num;
            manager.SelectedValue = manager_num;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
