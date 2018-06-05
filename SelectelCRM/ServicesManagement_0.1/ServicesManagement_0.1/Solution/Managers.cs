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
    //Форма "Менеджеры"
    public partial class Managers : Form
    {
        public Managers()
        {
            InitializeComponent();
        }

        //Дескриптор формы
        static public Managers formDescriptor;
        //Идентификатор менеджера
        static public string managerID;
        //ФИО менеджера
        static public string full_name;
        //Адрес
        static public string adress;
        //Телефон
        static public string phone;
        //Дата рождения
        static public string date_of_birth;
        //Дата приема на работу
        static public string date_start_work;
        //Логин
        static public string login;
        //Пароль
        static public string password;

        //Получение и сохранение информации о выбранной строке в таблице с менеджерами
        static public void ReadManagersTableRow(int row_idx)
        {
            managerID = formDescriptor.managersTable.Rows[row_idx].Cells[0].Value.ToString();
            full_name = formDescriptor.managersTable.Rows[row_idx].Cells[1].Value.ToString();
            adress = formDescriptor.managersTable.Rows[row_idx].Cells[2].Value.ToString();
            phone = formDescriptor.managersTable.Rows[row_idx].Cells[3].Value.ToString();
            date_of_birth = formDescriptor.managersTable.Rows[row_idx].Cells[4].Value.ToString();
            date_start_work = formDescriptor.managersTable.Rows[row_idx].Cells[5].Value.ToString();
            login = formDescriptor.managersTable.Rows[row_idx].Cells[6].Value.ToString();
            password = formDescriptor.managersTable.Rows[row_idx].Cells[7].Value.ToString();
        }

        //Выполняется при загрузке формы, заполнение таблицы данными из БД, настройка элементов управления
        private void Managers_Load(object sender, EventArgs e)
        {
            try
            { 
                formDescriptor = this;
                reconnectBtn.Font = new Font(reconnectBtn.Font.Name, 10, FontStyle.Regular | FontStyle.Underline);
                managersTable.RowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f1f2f6");
                managersTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                DBConnection.GetManagers();
                managersTable.DataSource = DBConnection.dtManagers;
                //managersTable.CurrentCell = managersTable[0, 0];
                managersTable.Rows[0].Selected = true;
                ReadManagersTableRow(0);
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Вызов формы добавления нового менеджера
        private void addManagerBtn_Click(object sender, EventArgs e)
        {
            AddManager addmngr = new AddManager(); //экземпляр формы добавления менеджера
            addmngr.Show();
        }

        //Осуществляет получение информации по выбранной строке таблицы с менеджерами
        private void managersTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    //managersTable.CurrentCell = managersTable[0, e.RowIndex];
                    managersTable.Rows[e.RowIndex].Selected = true;
                    ReadManagersTableRow(managersTable.CurrentCell.RowIndex);
                }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Вызов формы редактирования менеджера
        private void editManagerBtn_Click(object sender, EventArgs e)
        {

            if (managersTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для редактирования!");
                return;
            }
            EditManager edtmngr = new EditManager(); //экемпляр формы редактирования менеджера
            edtmngr.Show();
        }

        //Поиск менеджера по ФИО
        private void searchFullNameBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            DataRow[] dtRows = DBConnection.dtManagers.Select("ФИО LIKE '*" + fullName.Text + "*'"); //строка с результатом поиска
            if (dtRows.Count() == 0)
            {
                MessageBox.Show("Запись не найдена!");
                return;
            }
            bool flag = false; //флаг успеха
            for (int i = 0; i < managersTable.RowCount; i++)
            {
                if (managersTable.Rows[i].Cells[0].Value.ToString() == dtRows[0].ItemArray[0].ToString())
                {
                    managersTable.CurrentCell = managersTable[0, i];
                    managersTable.Rows[i].Selected = true;
                    ReadManagersTableRow(i);
                    flag = true;
                    ReadManagersTableRow(0);
                    return;
                }
            }
            if (!flag)
            {
                MessageBox.Show("Запись не найдена!");
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Отбор менеджеров по дате начала работы
        private void filterStartWorkDateBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            fullName.Text = "";
            managersTable.DataSource = null;
            DBConnection.FilterStartWorkDate(date1.Value.ToString("yyyy-MM-dd"), date2.Value.ToString("yyyy-MM-dd"));
            managersTable.DataSource = DBConnection.dtManagers;
            if (DBConnection.dtManagers.Rows.Count > 0)
            {
                ReadManagersTableRow(0);
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Отмена действующих фильтров в таблице с менеджерами
        private void filterCancelBtn_Click(object sender, EventArgs e)
        {
            try
            {
            fullName.Text = "";
            managersTable.DataSource = null;
            DBConnection.GetManagers();
            managersTable.DataSource = DBConnection.dtManagers;
            if (DBConnection.dtManagers.Rows.Count > 0)
            {
                ReadManagersTableRow(0);
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Удаление выбранного менеджера
        private void delManagerBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (managersTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для удаления!");
                return;
            }
            managersTable.DataSource = null;
            DBConnection.DeleteManager(managerID);
            DBConnection.GetManagers();
            managersTable.DataSource = DBConnection.dtManagers;
            if (DBConnection.dtManagers.Rows.Count > 0)
            {
                ReadManagersTableRow(0);
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Пункт меню “Заявки и договоры”
        private void requestsAndContractsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1.form.Show();
        }

        //Пункт меню “Клиенты”
        private void clientsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Clients clnts = new Clients(); //экземпляр формы "Клиенты"
            clnts.Show();
        }

        //Пункт меню “Аналитика”
        private void AnalyticsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Stats stts = new Stats(); //экземпляр формы "Аналитика"
            stts.Show();
        }

        //Пункт меню “Услуги”
        private void servicesBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Services srvcs = new Services(); //экземпляр формы "Услуги"
            srvcs.Show();
        }

        //Пункт меню “Справочники”
        private void refsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Refs rfs = new Refs(); //экземпляр формы "Справочники"
            rfs.Show();
        }

        //Закрытие панели ошибок
        private void label22_Click(object sender, EventArgs e)
        {
            exceptPanel.Visible = false;
        }

        //Возврат на форму авторизации
        private void logoutLbl_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization.form.Show();
        }

        //Возврат на форму авторизации
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization.form.Show();
        }

        //Возврат на форму авторизации
        private void Managers_FormClosed(object sender, FormClosedEventArgs e)
        {
            Authorization.form.Show();
        }

        //Возврат на форму авторизации
        private void logoutPanel_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization.form.Show();
        }

        //Переподключение к базе данных
        private void reconnectBtn_Click(object sender, EventArgs e)
        {
            if (DBConnection.Connect())
            {
                MessageBox.Show("Успешно!");
            }
        }

        //Смена стиля надписи при наведении
        private void reconnectBtn_MouseMove(object sender, MouseEventArgs e)
        {
            reconnectBtn.Font = new Font(reconnectBtn.Font.Name, 10, FontStyle.Bold | FontStyle.Underline);
        }

        //Возврат исходного стиля надписи
        private void reconnectBtn_MouseLeave(object sender, EventArgs e)
        {
            reconnectBtn.Font = new Font(reconnectBtn.Font.Name, 10, FontStyle.Regular | FontStyle.Underline);
        }
    }
}
