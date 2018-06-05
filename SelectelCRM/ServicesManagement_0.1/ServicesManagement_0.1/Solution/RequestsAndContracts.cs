using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Diagnostics;
using Ex = Microsoft.Office.Interop.Excel;



namespace Solution
{
    //Форма "Заявки и договора"
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static public int row_request; //строка в таблице с заявками/договорами
        static public string requestNum; //номер заявки
        static public string clientNum; //номер клиента
        static public string contractNum = "еще не оформлен"; //номер договора
        static public string saleID; //идентификатор услуги в договоре (продажи)
        static public string serviceID; //идентификатор услуги
        static public string serviceDateStart; //Дата начала действия услуги
        static public string serviceDateEnd; //Дата окончания действия услуги
        static public string serviceDateStart2; //Дата начала действия услуги (дополнительно)
        static public string serviceDateEnd2; //Дата окончания действия услуги (дополнительно)
        static public string docNum; //номер документа
        static public string docType; //тип документа
        static public string docStartDate; //дата создания документа
        static public string email; //email клиента
        static public string date_request; //дата заявки
        static public string date_contract; //дата договора
        static public string client_num; //номер клиента (дополнительно)
        static public string manager_num; //номер менеджера

        static public Form1 form; //дескриптор формы

        //Данные для подключения к удаленному серверу SRV1
        string host = "206.189.10.249";
        string username = "root";
        string password = "joo0shaij";
        string destination = @"/root";
        int port = 22;

        //Переменная, содержащая ошибку
        static public string ErrorMessage = "";

        //Завершение процесса Microsoft Office Word
        static public void CloseWord()
        {
            Process[] processes = Process.GetProcessesByName("WINWORD");
            foreach (Process proc in processes)
            {
                proc.Kill();
            }
        }

        //Обновление в Label числа email уведомлений в заявке
        public void UpdateNoticesQty()
        {
            DBConnection.msCommand.CommandText = "SELECT COUNT(CASE WHEN EXTRACT(DAY FROM s.date_end) - n.days_before <= Now() THEN c.email END) FROM notifications n, clients c, sales s, services_in_request sir, requests_and_contracts rc WHERE n.sale_id = s.id AND s.num_service_in_request = sir.num_service_in_request AND sir.request_num = rc.request_num AND c.id = rc.client_num AND rc.request_num = '" + requestNum + "';";
            object result = DBConnection.msCommand.ExecuteScalar(); //результат запроса
            string notifications_qty = result.ToString(); //число уведомлений
            notifications.Text = String.Format("Email уведомления ({0})", notifications_qty);
        }

        //Цветовая визуализация данных в таблице с заявками/договорами по статусу
        public void PaintGrid()
        {
            requestsAndContractsTable.Columns[8].Visible = false;
            requestsAndContractsTable.Columns[9].Visible = false;
            requestsAndContractsTable.Columns[11].Visible = false;
            for (int i = 0; i < requestsAndContractsTable.Rows.Count; i++)
            {
                switch (requestsAndContractsTable.Rows[i].Cells[7].Value.ToString())
                {
                    case "Новый":
                        requestsAndContractsTable.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#86ffb9");
                        /*for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            dataGridView1.Rows[i]
                        */
                        break;
                    case "В обработке":
                        requestsAndContractsTable.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffd686");
                        /*for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            dataGridView1.Rows[i]
                        */
                        break;
                    case "Действует":
                        requestsAndContractsTable.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#dddddd");
                        /*for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            dataGridView1.Rows[i]
                        */
                        break;
                    case "Завершен":
                        requestsAndContractsTable.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                        /*for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            dataGridView1.Rows[i]
                        */
                        break;
                }
            }
        }

        //Получение и сохранение информации о выбранной строке в таблице с заявками/договорами
        static public void ReadRequestsTableRow(int row_idx)
        {
            // idx = form.dataGridView1.CurrentCell.RowIndex;
            requestNum = form.requestsAndContractsTable.Rows[row_idx].Cells[0].Value.ToString(); //номер заявки
            clientNum = form.requestsAndContractsTable.Rows[row_idx].Cells[8].Value.ToString(); //номер клиента
            email = form.requestsAndContractsTable.Rows[row_idx].Cells[11].Value.ToString(); //email
            date_request = form.requestsAndContractsTable.Rows[row_idx].Cells[5].Value.ToString(); //дата заявки
            date_contract = form.requestsAndContractsTable.Rows[row_idx].Cells[6].Value.ToString(); //дата договора
            client_num = form.requestsAndContractsTable.Rows[row_idx].Cells[8].Value.ToString(); //номер клиента (дополнительно)
            manager_num = form.requestsAndContractsTable.Rows[row_idx].Cells[9].Value.ToString(); //номер менеджера
        }

        //Получение и сохранение информации о выбранной строке в таблице с услугами в заявке
        static public void ReadServicesInRequestTableRow(int row_idx)
        {
            //int idx = form.dataGridView2.CurrentCell.RowIndex;
            serviceID = form.servicesInRequestTable.Rows[row_idx].Cells[0].Value.ToString(); //идентификатор услуги
        }

        //Получение и сохранение информации о выбранной строке в таблице с услугами в договоре
        static public void ReadServicesInContractTableRow(int row_idx)
        {
            //int idx = form.dataGridView3.CurrentCell.RowIndex;
            serviceDateStart = form.servicesInContractTable.Rows[row_idx].Cells[4].Value.ToString(); //дата начала действия услуги
            serviceDateEnd = form.servicesInContractTable.Rows[row_idx].Cells[5].Value.ToString(); //дата окончания действия услуги
            saleID = form.servicesInContractTable.Rows[row_idx].Cells[0].Value.ToString(); //идентификатор услуги в договоре (продажи)
        }

        //Получение и сохранение информации о выбранной строке в таблице с документами в заявке
        static public void ReadDocsTableRow(int row_idx)
        {
            //int idx = form.dataGridView4.CurrentCell.RowIndex;
            docNum = form.docsTable.Rows[row_idx].Cells[0].Value.ToString(); //номер документа
            docType = form.docsTable.Rows[row_idx].Cells[1].Value.ToString(); //тип документа
            docStartDate = form.docsTable.Rows[row_idx].Cells[2].Value.ToString(); //дата создания документа
        }

        //Выполняется при загрузке формы, заполнение всех таблиц из БД
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                form = this;
                row_request = 0;
                reconnectBtn.Font = new Font(reconnectBtn.Font.Name, 10, FontStyle.Regular | FontStyle.Underline);

                DBConnection.Connect();
                DBConnection.GetRequests();
                requestsAndContractsTable.DataSource = DBConnection.dtRequests;
                requestsAndContractsTable.Rows[0].Selected = true;
                if (DBConnection.dtRequests.Rows.Count > 0)
                {
                    PaintGrid();
                    ReadRequestsTableRow(0);
                    if (requestsAndContractsTable.Rows[0].Cells[1].Value.ToString() != "")
                    {
                        contractNum = requestsAndContractsTable.Rows[0].Cells[1].Value.ToString();
                        newContractBtn.Enabled = false;
                    }
                    else
                    {
                        contractNum = "еще не оформлен";
                        newContractBtn.Enabled = true;
                    }
                    label2.Text = "Заявка " + requestNum + ", договор " + contractNum;
                }

                servicesInRequestTable.Columns.Clear();
                servicesInRequestTable.DataSource = null;
                DBConnection.GetServicesInRequest(requestNum);
                servicesInRequestTable.DataSource = DBConnection.dtServicesInRequest;
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                servicesInRequestTable.Columns.Add(btn);
                btn.HeaderText = "Действие";
                btn.Text = "+ в договор";
                btn.UseColumnTextForButtonValue = true;

                if (DBConnection.dtServicesInRequest.Rows.Count > 0)
                {
                    ReadServicesInRequestTableRow(0);
                }

                DBConnection.GetClients();
                client.DataSource = DBConnection.dtClients;
                client.DisplayMember = "Название";
                client.ValueMember = "ID";
                DBConnection.GetManagers();
                manager.DataSource = DBConnection.dtManagers;
                manager.DisplayMember = "ФИО";
                manager.ValueMember = "ID";

                DBConnection.GetServicesInContract(requestNum);
                servicesInContractTable.DataSource = DBConnection.dtServicesInContract;
                if (DBConnection.dtServicesInContract.Rows.Count > 0)
                {
                    ReadServicesInContractTableRow(0);
                }

                DBConnection.GetDocs(requestNum);
                docsTable.DataSource = DBConnection.dtDocs;
                if (DBConnection.dtDocs.Rows.Count > 0)
                {
                    ReadDocsTableRow(0);
                }

                //Закоментировать?
                //backgroundWorker1.DoWork += backgroundWorker1_DoWork;
                //backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
                UpdateNoticesQty();

                loadingPanel.Visible = false;
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Заполнение таблиц с услугами в заявке, услугами в договоре, документами в соответствии с выбранной заявкой/договором
        public void UpdateRelatedData(int row_idx)
        {
            if (requestsAndContractsTable.Rows.Count > 0)
            {
                servicesInRequestTable.Columns.Clear();
                servicesInRequestTable.DataSource = null;
                DBConnection.GetServicesInRequest(requestNum);
                servicesInRequestTable.DataSource = DBConnection.dtServicesInRequest;
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn(); //объект кнопки в колонке таблицы
                servicesInRequestTable.Columns.Add(btn);
                btn.HeaderText = "Действие";
                btn.Text = "+ в договор";
                btn.UseColumnTextForButtonValue = true;
                if (DBConnection.dtServicesInRequest.Rows.Count > 0)
                {
                    ReadServicesInRequestTableRow(0);
                }

                DBConnection.GetServicesInContract(requestNum);
                servicesInContractTable.DataSource = DBConnection.dtServicesInContract;
                if (DBConnection.dtServicesInContract.Rows.Count > 0)
                {
                    ReadServicesInContractTableRow(0);
                }

                DBConnection.GetDocs(requestNum);
                docsTable.DataSource = DBConnection.dtDocs;
                if (DBConnection.dtDocs.Rows.Count > 0)
                {
                    ReadDocsTableRow(0);
                    /*if (DBConnection.dtDocs.Rows[0].ItemArray[3].ToString() != "")
                    {
                        button10.Text = "Просмотр в Word";
                    }*/
                }

                if (requestsAndContractsTable.Rows[row_idx].Cells[1].Value.ToString() != "")
                {
                    contractNum = requestsAndContractsTable.Rows[row_idx].Cells[1].Value.ToString();
                    newContractBtn.Enabled = false;
                }
                else
                {
                    contractNum = "еще не оформлен";
                    newContractBtn.Enabled = true;
                }
                label2.Text = "Заявка " + requestNum + ", договор " + contractNum;
                UpdateNoticesQty();

            }

        }

        //Осуществляет получение информации по выбранной строке таблицы с заявками и договорами и обновление информации в зависимых таблицах
        private void requestsAndContractsTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            if (e.RowIndex >= 0 && DBConnection.dtRequests.Rows.Count > 0)
            {
                //row_request = e.RowIndex;
                requestsAndContractsTable.Rows[e.RowIndex].Selected = true;
                ReadRequestsTableRow(e.RowIndex);
                UpdateRelatedData(e.RowIndex);
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Вызов формы создания новой заявки
        private void newRequestBtn_Click(object sender, EventArgs e)
        {
            AddRequest addrqstForm = new AddRequest(); //экземпляр формы добавления заявок
            addrqstForm.Show();
        }

        //Вызов формы добавления услуги в заявку
        private void addServiceToRequestBtn_Click(object sender, EventArgs e)
        {
            AddServiceToRequest addsrvctorqstForm = new AddServiceToRequest(); //экземпляр форм добавления услуг в заявки
            addsrvctorqstForm.Show();

        }

        //Формирование договора по выбранной заявке
        private void newContractBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            loadingPanel.Visible = true;
            this.Enabled = false;

            DBConnection.msCommand.CommandText = "SELECT cl.country_code, c.name FROM clients cl, cities c WHERE cl.city_code = c.id AND cl.id =  '" + clientNum + "';";
            MySqlDataReader reader = DBConnection.msCommand.ExecuteReader(); //результаты запроса
            reader.Read();
            string country_code = reader[0].ToString(); //код страны
            string city_name = reader[1].ToString(); //название города
            reader.Close();
            DBConnection.msCommand.CommandText = "SELECT COUNT(*) FROM requests_and_contracts WHERE contract_num IS NOT NULL;";
            object result = DBConnection.msCommand.ExecuteScalar(); //результат запроса
            string prefix = ""; //префикс к числовым данным < 10
            int month = DateTime.Now.Month; //текущий месяц
            if (month < 10)
            {
                prefix = "0";
            }
            string year = DateTime.Now.Year.ToString().Substring(2, 2); //Текущий год
            string contract_num = String.Format("{0}{1}-{2}{3}{4}ЮР", country_code, city_name.Substring(0, 3), (Convert.ToInt32(result) + 1).ToString(), prefix + month.ToString(), year); //номер договора

            DBConnection.NewContract(contract_num);
            DBConnection.NewDoc(requestNum, "Договор об оказании услуг", DateTime.Now.ToString("yyyy-MM-dd"));
            DBConnection.NewDoc(requestNum, "Приложение к договору", DateTime.Now.ToString("yyyy-MM-dd"));
            newContractBtn.Enabled = false;
            this.Enabled = true;
            this.OnLoad(null);
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Trash
        private void label2_Click(object sender, EventArgs e)
        {

        }

        //Получение информации по выбранной строке в таблице с услугами в заявке. Перенос услуг из заявок в договоры по нажатию кнопки "+ в договор"
        private void servicesInRequestTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            { 
            if (e.RowIndex >= 0)
            {
                ReadServicesInRequestTableRow(e.RowIndex);
                AddServiceToContract.serviceNum = servicesInRequestTable.Rows[e.RowIndex].Cells[0].Value.ToString();
                AddServiceToContract.serviceDateStart = servicesInRequestTable.Rows[e.RowIndex].Cells[5].Value.ToString();
                AddServiceToContract.serviceDateEnd = servicesInRequestTable.Rows[e.RowIndex].Cells[6].Value.ToString();
                AddServiceToContract.price = servicesInRequestTable.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (e.ColumnIndex == 8)
                {
                    if (newContractBtn.Enabled)
                    {
                        MessageBox.Show("Сначала сформируйте договор!");
                        return;
                    }
                    serviceDateStart2 = servicesInRequestTable.Rows[e.RowIndex].Cells[5].Value.ToString();
                    serviceDateEnd2 = servicesInRequestTable.Rows[e.RowIndex].Cells[6].Value.ToString();
                    DialogResult result = MessageBox.Show("При изменении услуг в договоре, связанные с ними документы будут созданы заново. Продолжить?", "Возможная потеря изменений", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        DBConnection.GetDocs(Form1.requestNum);
                        if (DBConnection.dtDocs.Rows.Count > 0)
                        {
                            if (DBConnection.dtDocs.Rows[0].ItemArray[3].ToString() != "")
                            {
                                MessageBox.Show("Данный договор подписан и в него больше нельзя вносить изменения.");
                                return;
                            }
                            else
                            {
                                AddServiceToContract addsrvctocntrctForm = new AddServiceToContract();
                                addsrvctocntrctForm.Show();
                                foreach (DataRow row in DBConnection.dtDocs.AsEnumerable())
                                {
                                    string doc_type = ".xls";
                                    if (row[1].ToString() == "Договор об оказании услуг")
                                    {
                                        doc_type = ".doc";
                                    }
                                    string file_name = Application.StartupPath + @"\" + row[0].ToString() + doc_type;
                                    if (File.Exists(file_name))
                                    {
                                        File.Delete(file_name);
                                    }
                                }
                            }
                        }
                        DBConnection.GetServicesInContract(Form1.requestNum);
                        servicesInContractTable.DataSource = DBConnection.dtServicesInContract;
                        if (servicesInContractTable.Rows.Count > 0)
                        {
                            ReadServicesInContractTableRow(0);
                        }
                    }
                }
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Поиск заявок/договоров по номеру заявки
        private void searchNumberRequestBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            numberContract.Text = "";
            bool flag = false; //флаг успеха
            for (int i = 0; i < requestsAndContractsTable.RowCount; i++)
            {
                if (requestsAndContractsTable.Rows[i].Cells[0].Value.ToString() == numberRequest.Text)
                {
                    requestsAndContractsTable.CurrentCell = requestsAndContractsTable[0, i];
                    requestsAndContractsTable.Rows[i].Selected = true;
                    ReadRequestsTableRow(i);
                    flag = true;
                    ReadRequestsTableRow(0);
                    UpdateRelatedData(0);
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

        //Поиск заявок/договоров по номеру договора
        private void searchNumberContractBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            numberRequest.Text = "";
            bool flag = false; //флаг успеха
            for (int i = 0; i < requestsAndContractsTable.RowCount; i++)
            {
                if (requestsAndContractsTable.Rows[i].Cells[1].Value.ToString() == numberContract.Text)
                {
                    requestsAndContractsTable.CurrentCell = requestsAndContractsTable[1, i];
                    requestsAndContractsTable.Rows[i].Selected = true;
                    ReadRequestsTableRow(i);
                    //MessageBox.Show("Success!");
                    flag = true;
                    ReadRequestsTableRow(0);
                    UpdateRelatedData(0);
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

        //Отбор заявок/договоров по дате заявки
        private void filterRequestDateBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            numberRequest.Text = "";
            numberContract.Text = "";
            DBConnection.FilterRequestDate(dateRequest1.Value.ToString("yyyy-MM-dd"), dateRequest2.Value.ToString("yyyy-MM-dd"));
            requestsAndContractsTable.DataSource = DBConnection.dtRequests;
            PaintGrid();
            if (DBConnection.dtRequests.Rows.Count > 0)
            {
                ReadRequestsTableRow(0);
            }
            UpdateRelatedData(0);
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Отбор заявок/договоров по дате договора
        private void filterContractDateBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            numberRequest.Text = "";
            numberContract.Text = "";
            DBConnection.FilterContractDate(dateContract1.Value.ToString("yyyy-MM-dd"), dateContract2.Value.ToString("yyyy-MM-dd")); 
            requestsAndContractsTable.DataSource = DBConnection.dtRequests;
            PaintGrid();
            if (DBConnection.dtRequests.Rows.Count > 0)
            {
                ReadRequestsTableRow(0);
            }
            UpdateRelatedData(0);
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Отбор заявок/договоров по клиенту
        private void filterClientBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            numberRequest.Text = "";
            numberContract.Text = "";
            DBConnection.FilterClient(client.SelectedValue.ToString());
            requestsAndContractsTable.DataSource = DBConnection.dtRequests;
            PaintGrid();
            if (DBConnection.dtRequests.Rows.Count > 0)
            {
                ReadRequestsTableRow(0);
            }
            UpdateRelatedData(0);
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Trash
        private void button11_Click(object sender, EventArgs e)
        {

        }

        //Удаление выбранной услуги из заявки
        private void delServiceFromRequestBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (servicesInRequestTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для удаления!");
                return;
            }
            DialogResult result = MessageBox.Show("При изменении услуг в договоре, связанные с ними документы будут созданы заново. Продолжить?", "Возможная потеря изменений", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); //диалоговое окно
            if (result == DialogResult.OK)
            {
                DBConnection.DeleteServiceInRequest(serviceID);
                DBConnection.GetServicesInRequest(requestNum);
                servicesInRequestTable.DataSource = DBConnection.dtServicesInRequest;
                if (DBConnection.dtServicesInRequest.Rows.Count > 0)
                {
                    Form1.ReadServicesInRequestTableRow(0);
                }
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Вызов формы редактирования заявки/договора
        private void editRequestContractBtn_Click(object sender, EventArgs e)
        {
            try
            {
            if (requestsAndContractsTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для редактирования!");
                return;
            }
            EditRequest.date_request = date_request;
            EditRequest.date_contract = date_contract;
            EditRequest.client_num = client_num;
            EditRequest.manager_num = manager_num;
            EditRequest edtrqst = new EditRequest(); //экземлпяр формы редактирования заявок/договоров
            edtrqst.Show();
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Удаление выбранной услуги из договора
        private void delRequestContractBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (requestsAndContractsTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для удаления!");
                return;
            }
            loadingPanel.Visible = true;
            this.Enabled = false;
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить заявку/договор?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); //диалоговое окно
            if (result == DialogResult.OK)
            {
                    if (DBConnection.DeleteRequest(requestNum))
                    {
                        DBConnection.GetRequests();
                        //dataGridView1.DataSource = DBConnection.dtRequests;
                        //PaintGrid();
                        //Form1.ReadRequestsTableRow(0);
                        this.OnLoad(null);
                    }
            }
            loadingPanel.Visible = false;
            this.Enabled = true;
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Экспорт документа в PDF формат
        private void exportBtn_Click(object sender, EventArgs e)
        {
            try
            {
                CloseWord();
                Excel.Close();
                if (servicesInContractTable.RowCount == 0)
                {
                    MessageBox.Show("Сначала добавьте услуги в договор!");
                    return;
                }
                saveFileDialog1.ShowDialog();

                if (docType == "Договор об оказании услуг")
                {
                    if (docsTable.Rows[0].Cells[3].Value.ToString() == "")
                    {
                        form.Enabled = false;
                        string file_name = docNum + ".doc"; //имя файла
                        if (File.Exists(Application.StartupPath + @"\" + file_name))
                        {
                            loadingPanel.Visible = true;
                            exportDocToPDF.RunWorkerAsync();
                        }
                        else
                        {
                            loadingPanel.Visible = true;
                            createDocAndexportToPDF.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        string file_name = docNum + ".doc"; //имя файла
                        form.Enabled = false;
                        loadingPanel.Visible = true;
                        sftp.DownloadSFTPFile(host, username, password, file_name, port);
                        exportDocToPDF.RunWorkerAsync();
                    }
                }
                else if (docType == "Приложение к договору")
                {
                    if (docsTable.Rows[0].Cells[3].Value.ToString() == "")
                    {
                        form.Enabled = false;
                        string file_name = docNum + ".xls"; //имя файла
                        if (File.Exists(Application.StartupPath + @"\" + file_name))
                        {
                            loadingPanel.Visible = true;
                            exportExcelToPDF.RunWorkerAsync();
                        }
                        else
                        {
                            loadingPanel.Visible = true;
                            createAttachAndExportToPDF.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        string file_name = docNum + ".xls"; //имя файла
                        form.Enabled = false;
                        loadingPanel.Visible = true;
                        sftp.DownloadSFTPFile(host, username, password, file_name, port);
                        exportExcelToPDF.RunWorkerAsync();
                    }
                }
                else if (docType.Contains("Счет-фактура"))
                {
                    if (docsTable.Rows[0].Cells[3].Value.ToString() == "")
                    {
                        form.Enabled = false;
                        string file_name = docNum + ".xls"; //имя файла
                        if (File.Exists(Application.StartupPath + @"\" + file_name))
                        {
                            loadingPanel.Visible = true;
                            exportExcelToPDF.RunWorkerAsync();
                        }
                        else
                        {
                            loadingPanel.Visible = true;
                            createInvoiceAndExportToPDF.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        string file_name = docNum + ".xls"; //имя файла
                        form.Enabled = false;
                        loadingPanel.Visible = true;
                        sftp.DownloadSFTPFile(host, username, password, file_name, port);
                        exportExcelToPDF.RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }

        }

        //Вывод документа на печать
        private void printBtn_Click(object sender, EventArgs e)
        {
            try
            {
                CloseWord();
                Excel.Close();
                if (servicesInContractTable.RowCount == 0)
                {
                    MessageBox.Show("Сначала добавьте услуги в договор!");
                    return;
                }
                if (docType == "Договор об оказании услуг")
                {
                    if (docsTable.Rows[0].Cells[3].Value.ToString() == "")
                    {
                        form.Enabled = false;
                        string file_name = docNum + ".doc"; //имя файла
                        if (File.Exists(Application.StartupPath + @"\" + file_name))
                        {
                            loadingPanel.Visible = true;
                            printDocFile.RunWorkerAsync();
                        }
                        else
                        {
                            loadingPanel.Visible = true;
                            CreateDocAndPrint.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        string file_name = docNum + ".doc";
                        form.Enabled = false;
                        loadingPanel.Visible = true;
                        sftp.DownloadSFTPFile(host, username, password, file_name, port);
                        printDocFile.RunWorkerAsync();
                    }
                }
                else if (docType == "Приложение к договору")
                {
                    if (docsTable.Rows[0].Cells[3].Value.ToString() == "")
                    {
                        form.Enabled = false;
                        string file_name = docNum + ".xls"; //имя файла
                        if (File.Exists(Application.StartupPath + @"\" + file_name))
                        {
                            loadingPanel.Visible = true;
                            printExcelFile.RunWorkerAsync();
                        }
                        else
                        {
                            //MessageBox.Show("Hello");
                            loadingPanel.Visible = true;
                            createAttachAndPrint.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        string file_name = docNum + ".xls"; //имя файла
                        form.Enabled = false;
                        loadingPanel.Visible = true;
                        sftp.DownloadSFTPFile(host, username, password, file_name, port);
                        printExcelFile.RunWorkerAsync();
                    }
                }
                else if (docType.Contains("Счет-фактура"))
                {
                    if (docsTable.Rows[0].Cells[3].Value.ToString() == "")
                    {
                        form.Enabled = false;
                        string file_name = docNum + ".xls"; //имя файла
                        if (File.Exists(Application.StartupPath + @"\" + file_name))
                        {
                            loadingPanel.Visible = true;
                            printExcelFile.RunWorkerAsync();
                        }
                        else
                        {
                            loadingPanel.Visible = true;
                            createInvoiceAndPrint.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        string file_name = docNum + ".xls"; //имя файла
                        form.Enabled = false;
                        loadingPanel.Visible = true;
                        sftp.DownloadSFTPFile(host, username, password, file_name, port);
                        printExcelFile.RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Возврат на форму авторизации
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization.form.Show();
        }

        //Trash
        private void label5_Click(object sender, EventArgs e)
        {

        }

        //Trash
        private void label8_Click(object sender, EventArgs e)
        {

        }

        //Trash
        private void label7_Click(object sender, EventArgs e)
        {

        }

        //Trash
        private void label19_Click(object sender, EventArgs e)
        {
            try
            { 
            requestsAndContractsTable.DataSource = null;
            servicesInRequestTable.DataSource = null;
            servicesInContractTable.DataSource = null;
            DBConnection.GetRequests();
            requestsAndContractsTable.DataSource = DBConnection.dtRequests;
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Пункт меню "Аналитика"
        private void analyticsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Stats stts = new Stats(); //экземпляр формы аналитики
            stts.Show();
        }

        //Пункт меню "Справочники"
        private void refsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Refs rfs = new Refs(); //экземпляр формы справочники
            rfs.Show();
        }

        private void filterManagerBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            numberRequest.Text = "";
            numberContract.Text = "";
            DBConnection.FilterManager(manager.SelectedValue.ToString());
            requestsAndContractsTable.DataSource = DBConnection.dtRequests;
            PaintGrid();
            if (DBConnection.dtRequests.Rows.Count > 0)
            {
                ReadRequestsTableRow(0);
            }
            UpdateRelatedData(0);
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Пункт меню "Клиенты"
        private void clientsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Clients clnts = new Clients();
            clnts.Show();
        }

        //Создание договора в Word и экспорт в PDF
        private void createDocAndexportToPDF_DoWork(object sender, DoWorkEventArgs e)
        {
            ErrorMessage = "";
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".doc";

                if (DBConnection.dtServicesInContract.Rows.Count == 0)
                {
                    MessageBox.Show("Сначала добавьте услуги в договор!");
                    return;
                }
                var app = new word.Application();
                app.Visible = false;
                var doc = app.Documents.Open(Application.StartupPath + @"\contract.docx");
                saveDocContractFile(doc, file_name);
                doc.SaveAs(saveFileDialog1.FileName + ".pdf", word.WdSaveFormat.wdFormatPDF);
                object doNotSaveChanges = word.WdSaveOptions.wdDoNotSaveChanges;
                doc.Close(doNotSaveChanges);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }

        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void createDocAndexportToPDF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Trash
        private void label22_Click(object sender, EventArgs e)
        {
            try
            { 
            sftp.DownloadSFTPFile(host, username, password, "14.doc", port);
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Кнопка открытия документа в Word/Excel
        private void openDocBtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                CloseWord();
                Excel.Close();
                if (servicesInContractTable.RowCount == 0)
                {
                    MessageBox.Show("Сначала добавьте услуги в договор!");
                    return;
                }
                if (docType == "Договор об оказании услуг")
                {
                    if (docsTable.Rows[0].Cells[3].Value.ToString() == "")
                    {
                        form.Enabled = false;
                        string file_name = docNum + ".doc"; //имя файла
                        if (File.Exists(Application.StartupPath + @"\" + file_name))
                        {
                            //MessageBox.Show("Yes4");
                            loadingPanel.Visible = true;
                            openDocFile.RunWorkerAsync();
                        }
                        else
                        {
                            loadingPanel.Visible = true;
                            createDocFile.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        string file_name = docNum + ".doc"; //имя файла
                        form.Enabled = false;
                        loadingPanel.Visible = true;
                        sftp.DownloadSFTPFile(host, username, password, file_name, port);
                        openDocFile.RunWorkerAsync();
                    }
                }
                else if (docType == "Приложение к договору")
                {
                    if (docsTable.Rows[0].Cells[3].Value.ToString() == "")
                    {
                        form.Enabled = false;
                        string file_name = docNum + ".xls"; //имя файла
                        if (File.Exists(Application.StartupPath + @"\" + file_name))
                        {
                            loadingPanel.Visible = true;
                            openExcelFile.RunWorkerAsync();
                        }
                        else
                        {
                            loadingPanel.Visible = true;
                            createAttachFile.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        string file_name = docNum + ".xls"; //имя файла
                        form.Enabled = false;
                        loadingPanel.Visible = true;
                        sftp.DownloadSFTPFile(host, username, password, file_name, port);
                        openExcelFile.RunWorkerAsync();
                    }
                }
                else if (docType.Contains("Счет-фактура"))
                {
                    if (docsTable.Rows[0].Cells[3].Value.ToString() == "")
                    {
                        form.Enabled = false;
                        string file_name = docNum + ".xls"; //имя файла
                        if (File.Exists(Application.StartupPath + @"\" + file_name))
                        {
                            //MessageBox.Show("Im here");
                            loadingPanel.Visible = true;
                            openExcelFile.RunWorkerAsync();
                        }
                        else
                        {
                            loadingPanel.Visible = true;
                            createInvoiceFile.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        string file_name = docNum + ".xls"; //имя файла
                        form.Enabled = false;
                        loadingPanel.Visible = true;
                        sftp.DownloadSFTPFile(host, username, password, file_name, port);
                        openExcelFile.RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }

        }

        //Заполнение Word шаблона договора данными из БД
        private void saveDocContractFile(word.Document doc, string file_name)
        {
            DBConnection.msCommand.CommandText = "SELECT SUM(s.cost) FROM sales s, services srv, services_in_request sir WHERE sir.request_num = '" + Form1.requestNum + "' AND s.num_service_in_request = sir.num_service_in_request AND srv.id = sir.service_num;";
            object result = DBConnection.msCommand.ExecuteScalar(); //результат запроса
            DBConnection.msCommand.CommandText = "SELECT c.id as `ID`, c.name as `Название`, c.contact_person as `Контактное лицо`, c.phone as `Телефон`, co.name as `Страна`, ci.name as `Город`, c.adress as `Адрес`, c.email as `E-mail`, c.bank_account as `Банк. счет`, c.INN as `ИНН`, c.country_code, c.city_code FROM clients c, countries co, cities ci WHERE c.country_code = co.id AND c.city_code = ci.id AND c.id = '" + clientNum + "';";
            MySqlDataReader reader = DBConnection.msCommand.ExecuteReader(); //объект для чтения запроса с несколькими полями
            reader.Read();
            doc.Activate();
            doc.Bookmarks["num_doc"].Range.Text = contractNum;
            doc.Bookmarks["date"].Range.Text = date_contract.Substring(0, 10);
            doc.Bookmarks["contact_person"].Range.Text = reader[2].ToString();
            doc.Bookmarks["cost"].Range.Text = result.ToString();
            doc.Bookmarks["name"].Range.Text = reader[1].ToString();
            doc.Bookmarks["contact_person2"].Range.Text = reader[2].ToString();
            doc.Bookmarks["phone"].Range.Text = reader[3].ToString();
            doc.Bookmarks["country"].Range.Text = reader[4].ToString();
            doc.Bookmarks["city"].Range.Text = reader[5].ToString();
            doc.Bookmarks["adress"].Range.Text = reader[6].ToString();
            doc.Bookmarks["email"].Range.Text = reader[7].ToString();
            doc.Bookmarks["bank_account"].Range.Text = reader[8].ToString();
            doc.Bookmarks["inn"].Range.Text = reader[9].ToString();

            doc.Saved = false;
            doc.SaveAs(file_name, word.WdSaveFormat.wdFormatDocument);
            reader.Close();

        }

        //Заполнение Excel шаблона приложения к договору данными из БД
        private void saveExcelAttachFile(string file_name)
        {
            Excel.workSheet.Cells[1, 5].Value = contractNum;
            DBConnection.GetServicesInContract(requestNum);
            int count = 0; //счетчик
            foreach (DataRow row in DBConnection.dtServicesInContract.AsEnumerable())
            {
                Excel.workSheet.Cells[8 + count, 1].Value = (count + 1).ToString();
                Excel.workSheet.Cells[8 + count, 2].Value = row["Название услуги"].ToString();
                Excel.workSheet.Cells[8 + count, 3].Value = row["Дата начала"].ToString();
                Excel.workSheet.Cells[8 + count, 4].Value = row["Дата окончания"].ToString();
                Excel.workSheet.Cells[8 + count, 5].Value = row["Стоимость"].ToString();
                count += 1;
            }
            var cells = Excel.workSheet.get_Range("A9", "E" + (7 + DBConnection.dtServicesInContract.Rows.Count).ToString()); //диапазон ячеек рабочей таблицы
            cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние вертикальные
            cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние горизонтальные            
            cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // верхняя внешняя
            cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // правая внешняя
            cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // левая внешняя
            cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


            Excel.workSheet.Cells[9 + count + 1, 2].Value = DateTime.Now.ToString("yyyy-MM-dd").Substring(0, 10);
            Excel.workSheet.Cells[9 + count + 1, 3].Value = "Подпись ответственного лица ___________________________";

            Excel.workBook.Saved = false;
            Excel.workBook.SaveAs(file_name);
        }

        //Заполнение Excel шаблона счета-фактуры данными из БД
        private void saveExcelInvoiceFile(string file_name, string date)
        {
            Excel.workSheet.Cells[1, 5].Value = docNum.ToString();
            DBConnection.msCommand.CommandText = "SELECT c.id as `ID`, c.name as `Название`, c.contact_person as `Контактное лицо`, c.phone as `Телефон`, co.name as `Страна`, ci.name as `Город`, c.adress as `Адрес`, c.email as `E-mail`, c.bank_account as `Банк. счет`, c.INN as `ИНН`, c.country_code, c.city_code FROM clients c, countries co, cities ci WHERE c.country_code = co.id AND c.city_code = ci.id AND c.id = '" + clientNum + "';";
            MySqlDataReader reader = DBConnection.msCommand.ExecuteReader(); //объект для чтения запроса с несколькими полями
            reader.Read();
            Excel.workSheet.Cells[11, 1].Value = "Коммерческое предприятие \"" + reader[1].ToString() + "\"";
            Excel.workSheet.Cells[12, 1].Value = String.Format("Контактное лицо: {0}, Телефон: {1}, Страна: {2}, Город: {3}, Адрес: {4}, Электронная почта: {5}, Банковский счет: {6}, ИНН: {7}", reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString());
            reader.Close();

            Excel.workSheet.Cells[2, 7].Value = date.Substring(0,10);

            DBConnection.GetServicesInContractByMonth(requestNum, Convert.ToDateTime(date).ToString("yyyy-MM-dd"));//Convert.ToDateTime(docStartDate).ToString("yyyy-MM-dd")); //Convert.ToDateTime(docStartDate).Month.ToString(), Convert.ToDateTime(docStartDate).Year.ToString()   Convert.ToDateTime(docStartDate).ToString("yyyy-MM-dd")
            int count = 0; //счетчик
            double sumWithoutNDS = 0; //сумма без НДС
            double NDS = 0; //НДС
            double sumWithNDS = 0; //Сумма с НДС
            foreach (DataRow row in DBConnection.dtServicesInContractByMonth.AsEnumerable())
            {
                double sum = 0; //сумма
                float days = 0; //количество дней
                int day_start = 1; //первый день услуги в месяце
                int day_end = DateTime.DaysInMonth(Convert.ToDateTime(date).Year, Convert.ToDateTime(date).Month); //последний день услуги в месяце
                if (Convert.ToDateTime(row["Дата начала"]).Month == Convert.ToDateTime(date).Month)
                {
                    day_start = Convert.ToDateTime(row["Дата начала"]).Day;
                }
                if (Convert.ToDateTime(row["Дата окончания"]).Month == Convert.ToDateTime(date).Month)
                {
                    day_end = Convert.ToDateTime(row["Дата окончания"]).Day;
                }
                days = (float)(day_end - day_start + 1) / (float)DateTime.DaysInMonth(Convert.ToDateTime(date).Year, Convert.ToDateTime(date).Month);

                sum = Convert.ToDouble(row["cnt"]) * Convert.ToDouble(days) * Convert.ToDouble(row["Стоимость"]);

                Excel.workSheet.Cells[18 + count, 1].Value = (count + 1).ToString();
                Excel.workSheet.Cells[18 + count, 2].Value = row["Название услуги"].ToString();
                Excel.workSheet.Cells[18 + count, 3].Value = "шт.";
                Excel.workSheet.Cells[18 + count, 4].Value = row["cnt"].ToString();
                Excel.workSheet.Cells[18 + count, 5].Value = Convert.ToDouble(row["Стоимость"]).ToString("0.##");
                Excel.workSheet.Cells[18 + count, 6].Value = sum.ToString("0.##");
                sumWithoutNDS = sumWithoutNDS + sum;
                Excel.workSheet.Cells[18 + count, 7].Value = "18";
                Excel.workSheet.Cells[18 + count, 8].Value = (sum * 0.18).ToString("0.##");
                NDS = NDS + sum * 0.18;
                Excel.workSheet.Cells[18 + count, 9].Value = (sum + sum * 0.18).ToString("0.##");
                sumWithNDS = sumWithNDS + sum + sum * 0.18;
                count += 1;
            }
            var cells = Excel.workSheet.get_Range("A17", "I" + (17 + DBConnection.dtServicesInContractByMonth.Rows.Count).ToString()); //диапазон ячеек рабочей таблицы
            cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние вертикальные
            cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние горизонтальные            
            cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // верхняя внешняя
            cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // правая внешняя
            cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // левая внешняя
            cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            Excel.workSheet.Cells[18 + count + 1, 5].Value = "Итого (руб.):";
            Excel.workSheet.Cells[18 + count + 1, 6].Value = sumWithoutNDS.ToString("0.##");
            Excel.workSheet.Cells[18 + count + 1, 8].Value = NDS.ToString("0.##");
            Excel.workSheet.Cells[18 + count + 1, 9].Value = sumWithNDS.ToString("0.##");

            Excel.workSheet.Cells[19 + count + 2, 6].Value = "Подпись ответственного лица ________________________";

            Excel.workBook.Saved = false;
            Excel.workBook.SaveAs(file_name);
        }

        //Создание и открытие договора в Word
        private void createDocFile_DoWork(object sender, DoWorkEventArgs e)
        {
            ErrorMessage = "";
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".doc"; //имя файла

                var app = new word.Application(); //объект приложения Word
                app.Visible = false;
                var doc = app.Documents.Open(Application.StartupPath + @"\contract.docx"); //объект документа Word
                saveDocContractFile(doc, file_name);
                object doNotSaveChanges = word.WdSaveOptions.wdDoNotSaveChanges; //Параметр "не сохранять изменения"
                doc.Close(doNotSaveChanges);

                doc = app.Documents.Open(file_name);
                app.Visible = true;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void createDocFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Получение информации по выбранной строке в таблице с документами
        private void docsTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            { 
            if (e.RowIndex >= 0)
            {
                ReadDocsTableRow(e.RowIndex);
            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Открытие файла договора в Word
        private void openDocFile_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".doc"; //имя файла
                var app = new word.Application(); //объект приложения Word 
                app.Visible = true;
                var doc = app.Documents.Open(file_name); //объект документа Word
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }
        }

        //Создание договора в Word и вывод его на печать
        private void CreateDocAndPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".doc"; //имя файла

                if (DBConnection.dtServicesInContract.Rows.Count == 0)
                {
                    MessageBox.Show("Сначала добавьте услуги в договор!");
                    return;
                }
                var app = new word.Application(); //объект приложения Word 
                app.Visible = false;
                var doc = app.Documents.Open(Application.StartupPath + @"\contract.docx"); //объект документа Word
                saveDocContractFile(doc, file_name);
                app.Dialogs[word.WdWordDialog.wdDialogFilePrint].Show();
                object doNotSaveChanges = word.WdSaveOptions.wdDoNotSaveChanges; //Параметр "не сохранять изменения"
                doc.Close(doNotSaveChanges);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void CreateDocAndPrint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Кнопка подписания договора
        private void signAndSaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnection.GetDocs(requestNum);
                if (DBConnection.dtDocs.Rows.Count <= 2)
                {
                    MessageBox.Show("Ошибка! Проверьте, что в договор добавлены услуги.");
                    return;
                }
                if (docsTable.Rows.Count > 0)
                {
                    if (docsTable.Rows[0].Cells[3].Value.ToString() != "")
                    {
                        MessageBox.Show("Данный договор уже подписан!");
                        return;
                    }
                }
                loadingPanel.Visible = true;
                this.Enabled = false;

                signContractAndSaveDocs.RunWorkerAsync();

                DBConnection.msCommand.CommandText = "SELECT MAX(s.date_end) FROM sales s, services_in_request sir WHERE s.num_service_in_request = sir.num_service_in_request AND sir.request_num = '" + requestNum + "';";
                object result = DBConnection.msCommand.ExecuteScalar(); //результат запроса
                DBConnection.SetContractDateEnd(requestNum, result.ToString());
                
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void openDocFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Создание и открытие приложения к договору в Excel
        private void createAttachFile_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".xls"; //имя файла

                if (DBConnection.dtServicesInContract.Rows.Count == 0)
                {
                    MessageBox.Show("Сначала добавьте услуги в договор!");
                    return;
                }
                Excel.Open(Application.StartupPath + @"\attach.xlsx", false);
                saveExcelAttachFile(file_name);
                Excel.workBook.Close();

                Excel.Open(file_name, true);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }

        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void createAttachFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Открытие документа в Excel
        private void openExcelFile_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".xls"; //имя файла
                Excel.Open(file_name, true);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void openExcelFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Создание и открытие счета-фактуры в Excel
        private void createInvoiceFile_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".xls"; //имя файла

                if (DBConnection.dtServicesInContract.Rows.Count == 0)
                {
                    MessageBox.Show("Сначала добавьте услуги в договор!");
                    return;
                }
                Excel.Open(Application.StartupPath + @"\invoice.xlsx", false);
                saveExcelInvoiceFile(file_name, docStartDate);
                Excel.workBook.Close();

                Excel.Open(file_name, true);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void createInvoiceFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Создание счета-фактуры в Excel и экспорт в PDF
        private void createInvoiceAndExportToPDF_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".xls"; //имя файла

                if (DBConnection.dtServicesInContract.Rows.Count == 0)
                {
                    MessageBox.Show("Сначала добавьте услуги в договор!");
                    return;
                }
                Excel.Open(Application.StartupPath + @"\invoice.xlsx", false);
                saveExcelInvoiceFile(file_name, docStartDate);
                Excel.workSheet.PageSetup.Zoom = 70;
                Excel.workSheet.PageSetup.Orientation = Ex.XlPageOrientation.xlLandscape;
                Excel.workBook.ExportAsFixedFormat(Ex.XlFixedFormatType.xlTypePDF, saveFileDialog1.FileName);
                Excel.workBook.Close();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }
        }

        //Экспорт договора в PDF
        private void exportDocToPDF_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".doc"; //имя файла
                var app = new word.Application(); //объект приложения Word
                app.Visible = false;
                var doc = app.Documents.Open(file_name); //объект документа Word
                doc.SaveAs(saveFileDialog1.FileName, word.WdSaveFormat.wdFormatPDF);
                doc.Close();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void exportDocToPDF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Создание приложения к договору в Excel и экспорт в PDF
        private void createAttachAndExportToPDF_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".xls"; //имя файла

                if (DBConnection.dtServicesInContract.Rows.Count == 0)
                {
                    MessageBox.Show("Сначала добавьте услуги в договор!");
                    return;
                }
                Excel.Open(Application.StartupPath + @"\attach.xlsx", false);
                saveExcelAttachFile(file_name);
                Excel.workSheet.PageSetup.Zoom = 70;
                Excel.workSheet.PageSetup.Orientation = Ex.XlPageOrientation.xlLandscape;
                Excel.workBook.ExportAsFixedFormat(Ex.XlFixedFormatType.xlTypePDF, saveFileDialog1.FileName);
                Excel.workBook.Close();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }

        }

        //Экспорт Excel файла в PDF
        private void exportExcelToPDF_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".xls"; //имя файла
                Excel.Open(file_name, false);
                Excel.workSheet.PageSetup.Zoom = 70;
                Excel.workSheet.PageSetup.Orientation = Ex.XlPageOrientation.xlLandscape;
                Excel.workBook.ExportAsFixedFormat(Ex.XlFixedFormatType.xlTypePDF, saveFileDialog1.FileName);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void createInvoiceAndExportToPDF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void createAttachAndExportToPDF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void exportExcelToPDF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Вывод договора на печать
        private void printDocFile_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".doc"; //имя файла
                var app = new word.Application(); //объект приложения Word
                app.Visible = false;
                var doc = app.Documents.Open(file_name); //объект документа Word
                app.Dialogs[word.WdWordDialog.wdDialogFilePrint].Show();
                doc.Close();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }
        }

        //Создание приложения к договору и вывод на печать
        private void createAttachAndPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".xls"; //имя файла

                if (DBConnection.dtServicesInContract.Rows.Count == 0)
                {
                    MessageBox.Show("Сначала добавьте услуги в договор!");
                    return;
                }
                Excel.Open(Application.StartupPath + @"\attach.xlsx", true);
                saveExcelAttachFile(file_name);

                Excel.workSheet.PageSetup.Zoom = 70;
                Excel.workSheet.PageSetup.Orientation = Ex.XlPageOrientation.xlLandscape;
                Excel.workSheet.Cells.PrintOutEx(Type.Missing, Type.Missing, Type.Missing, true);

                Excel.workBook.Close();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void createAttachAndPrint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Вывод Excel файла на печать
        private void printExcelFile_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".xls"; //имя файла
                Excel.Open(file_name, true);
                Excel.workSheet.PageSetup.Zoom = 70;
                Excel.workSheet.PageSetup.Orientation = Ex.XlPageOrientation.xlLandscape;
                Excel.workSheet.Cells.PrintOutEx(Type.Missing, Type.Missing, Type.Missing, true);
                Excel.workBook.Close();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }
        }

        //Создание счета-фактуры и вывод на печать
        private void createInvoiceAndPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string file_name = Application.StartupPath + @"\" + docNum + ".xls"; //имя файла

                if (DBConnection.dtServicesInContract.Rows.Count == 0)
                {
                    MessageBox.Show("Сначала добавьте услуги в договор!");
                    return;
                }
                Excel.Open(Application.StartupPath + @"\invoice.xlsx", true);
                saveExcelInvoiceFile(file_name, docStartDate);
                Excel.workSheet.PageSetup.Zoom = 70;
                Excel.workSheet.PageSetup.Orientation = Ex.XlPageOrientation.xlLandscape;
                Excel.workSheet.Cells.PrintOutEx(Type.Missing, Type.Missing, Type.Missing, true);
                Excel.workBook.Close();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch
            {
                ErrorMessage = "Непредвиденная ошибка";
            }
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void printDocFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void printExcelFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void createInvoiceAndPrint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
        }

        //Получение информации по выбранной строке в таблице с услугами в договоре
        private void servicesInContractTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ReadServicesInContractTableRow(e.RowIndex);
            }
        }

        //Удаление услуги из договора, обновление связанныз документов
        private void delServiceFromContractBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (servicesInContractTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет записей для удаления!");
                return;
            }
            DialogResult result = MessageBox.Show("При изменении услуг в договоре, связанные с ними документы будут созданы заново. Продолжить?", "Возможная потеря изменений", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); //диалоговое окно
            if (result == DialogResult.OK)
            {
                DBConnection.GetDocs(Form1.requestNum);
                if (DBConnection.dtDocs.Rows.Count > 0)
                {
                    if (DBConnection.dtDocs.Rows[0].ItemArray[3].ToString() != "")
                    {
                        MessageBox.Show("Данный договор подписан и в него больше нельзя вносить изменения.");
                        return;
                    }
                    else
                    {
                        DBConnection.DeleteSale(saleID);
                        DBConnection.GetServicesInContract(requestNum);
                        servicesInContractTable.DataSource = DBConnection.dtServicesInContract;
                        if (servicesInContractTable.Rows.Count > 0)
                        {
                            Form1.ReadServicesInContractTableRow(0);
                        }
                        foreach (DataRow row in DBConnection.dtDocs.AsEnumerable())
                        {
                            string doc_type = ".xls"; //формат документа
                            if (row[1].ToString() == "Договор об оказании услуг")
                            {
                                doc_type = ".doc";
                            }
                            string file_name = Application.StartupPath + @"\" + row[0].ToString() + doc_type;
                            if (File.Exists(file_name))
                            {
                                File.Delete(file_name);
                            }
                        }
                    }
                }

                DBConnection.msCommand.CommandText = "DELETE FROM docs WHERE name != 'Договор об оказании услуг' AND name != 'Приложение к договору' AND request_num = '" + requestNum + "';";
                DBConnection.msCommand.ExecuteNonQuery();
                DBConnection.GetServicesInContract(requestNum);

                foreach (DataRow row in DBConnection.dtServicesInContract.AsEnumerable())
                {

                    int start_month = Convert.ToDateTime(row[4]).Month; //месяц начала действия услуги в году
                    for (int i = Convert.ToDateTime(row[4]).Year; i <= Convert.ToDateTime(row[5]).Year; i++)
                    {
                        //MessageBox.Show(i.ToString());
                        int end_month = 12; //месяц окончания действия услуги в году
                        if (i == Convert.ToDateTime(row[5]).Year)
                        {
                            end_month = Convert.ToDateTime(row[5]).Month;
                        }

                        for (int j = start_month; j <= end_month; j++)
                        {
                            int end_day = DateTime.DaysInMonth(i, j); //последний день услуги
                            if (j == end_month && i == Convert.ToDateTime(row[5]).Year)
                            {
                                end_day = Convert.ToDateTime(row[5]).Day;
                            }
                            string month_name = "Не задан"; //название месяца
                            switch (j)
                            {
                                case 1:
                                    month_name = "Январь";
                                    break;
                                case 2:
                                    month_name = "Февраль";
                                    break;
                                case 3:
                                    month_name = "Март";
                                    break;
                                case 4:
                                    month_name = "Апрель";
                                    break;
                                case 5:
                                    month_name = "Май";
                                    break;
                                case 6:
                                    month_name = "Июнь";
                                    break;
                                case 7:
                                    month_name = "Июль";
                                    break;
                                case 8:
                                    month_name = "Август";
                                    break;
                                case 9:
                                    month_name = "Сентябрь";
                                    break;
                                case 10:
                                    month_name = "Октябрь";
                                    break;
                                case 11:
                                    month_name = "Ноябрь";
                                    break;
                                case 12:
                                    month_name = "Декабрь";
                                    break;
                            }
                            start_month = 1;
                            DBConnection.msCommand.CommandText = "SELECT id FROM docs WHERE name = 'Счет-фактура за " + month_name + "' AND request_num = '" + Form1.requestNum + "' AND EXTRACT(YEAR FROM date_create) = '" + i + "';";
                            object data = DBConnection.msCommand.ExecuteScalar();
                            if (data != null)
                            {
                                continue;
                            }
                            DBConnection.NewDoc(Form1.requestNum, "Счет-фактура за " + month_name, String.Format("{0}-{1}-{2}", i, j, end_day));
                            DBConnection.GetDocs(Form1.requestNum);
                            Form1.form.docsTable.DataSource = DBConnection.dtDocs;
                            Form1.ReadDocsTableRow(0);

                        }
                    }
                }
                DBConnection.GetServicesInContract(Form1.requestNum);
                form.servicesInContractTable.DataSource = DBConnection.dtServicesInContract;
                if (DBConnection.dtServicesInContract.Rows.Count > 0)
                {
                    ReadServicesInContractTableRow(0);
                }
                DBConnection.GetDocs(Form1.requestNum);
                form.docsTable.DataSource = DBConnection.dtDocs;
                if (DBConnection.dtDocs.Rows.Count > 0)
                {
                    ReadDocsTableRow(0);
                }

            }
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Подписание договора и отправка всех связанных документов на сервер
        private void signContractAndSaveDocs_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                foreach (DataRow row in DBConnection.dtDocs.AsEnumerable())
                {
                    CloseWord();
                    Excel.Close();
                    string file_name = ""; //имя файла

                    if (row[1].ToString() == "Договор об оказании услуг")
                    {
                        file_name = Application.StartupPath + @"\" + row[0].ToString() + ".doc";
                        if (!File.Exists(file_name))
                        {
                            var app = new word.Application(); //объект приложения Word
                            app.Visible = false;
                            var doc = app.Documents.Open(Application.StartupPath + @"\contract.docx"); //объект документа Word
                            saveDocContractFile(doc, file_name);
                            object doNotSaveChanges = word.WdSaveOptions.wdDoNotSaveChanges; //параметр "не сохранять изменения"
                            doc.Close(doNotSaveChanges);
                        }
                    }
                    else if (row[1].ToString() == "Приложение к договору")
                    {
                        file_name = Application.StartupPath + @"\" + row[0].ToString() + ".xls";
                        if (!File.Exists(file_name))
                        {
                            Excel.Open(Application.StartupPath + @"\attach.xlsx", false);
                            saveExcelAttachFile(file_name);
                            Excel.workBook.Close();
                        }
                    }
                    else
                    {
                        file_name = Application.StartupPath + @"\" + row[0].ToString() + ".xls";
                        if (!File.Exists(file_name))
                        {
                            Excel.Open(Application.StartupPath + @"\invoice.xlsx", false);
                            saveExcelInvoiceFile(file_name, row[2].ToString());
                            Excel.workBook.Close();
                        }
                    }
                    
                    sftp.UploadSFTPFile(host, username, password, file_name, destination, port);
                    
                    DBConnection.SignDoc(row[0].ToString());
                    DBConnection.ChangeStatus(requestNum, "Действует");
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                ErrorMessage = ex.ToString();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }

        }

        //Разблокировка формы, вывод сообщений об ошибках
        private void signContractAndSaveDocs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !string.IsNullOrEmpty(ErrorMessage))
            {
                //if (!exceptPanel.Visible)
                exceptPanel.Visible = true;
                richTextBox1.Text = ErrorMessage;
            }
            loadingPanel.Visible = false;
            form.Enabled = true;
            DBConnection.GetDocs(requestNum);
            docsTable.DataSource = DBConnection.dtDocs;
            DBConnection.GetServicesInContract(requestNum);
            servicesInContractTable.DataSource = DBConnection.dtServicesInContract;
        }

        //Trash
        private void button24_Click(object sender, EventArgs e)
        {

        }

        //Возврат на форму авторизации
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseWord();
            Excel.Close();
            Authorization.form.Show();
        }

        //Отбор заявок и договоров по статусу
        private void filterStatusBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            numberRequest.Text = "";
            numberContract.Text = "";
            DBConnection.FilterStatus(status.Text);
            requestsAndContractsTable.DataSource = DBConnection.dtRequests;
            PaintGrid();
            if (DBConnection.dtRequests.Rows.Count > 0)
            {
                ReadRequestsTableRow(0);
            }
            UpdateRelatedData(0);
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        private void filtersPnl_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void filtersLbl_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }


        //Раскрытие панели с инструментами поиска и отбора
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (filterPanel.Location.Y <= 220)
                {
                    timer1.Enabled = false;
                }
                if (requestsAndContractsTable.Height > 212)
                {
                    requestsAndContractsTable.Height -= 3;
                }
                filterPanel.Top -= 3;
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Кнопка скрытия панели с инструментами поиска и отбора
        private void hideLabel_Click(object sender, EventArgs e)
        {
            numberRequest.Text = "";
            numberContract.Text = "";
            timer2.Enabled = true;
            
        }

        //Скрытие панели с инструментами поиска и отбора
        private void timer2_Tick(object sender, EventArgs e)
        { 
            try
            { 
            if (filterPanel.Top >= 406)
            {
                timer2.Enabled = false;
            }
            if (requestsAndContractsTable.Height < 355)
            {
                requestsAndContractsTable.Height += 3;
            }
            filterPanel.Top += 3;
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Отмена действующих фильтров в таблице с заявками и договорами
        private void filterCancelBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            numberRequest.Text = "";
            numberContract.Text = "";
            loadingPanel.Visible = true;
            this.Enabled = false;
            DBConnection.GetRequests();
            requestsAndContractsTable.DataSource = DBConnection.dtRequests;
            PaintGrid();
            if (DBConnection.dtRequests.Rows.Count > 0)
            {
                ReadRequestsTableRow(0);
            }
            UpdateRelatedData(0);
            loadingPanel.Visible = false;
            this.Enabled = true;
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }

        }

        //Пункт меню «Менеджеры»
        private void managersBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Managers mngrs = new Managers(); //экземпляр формы "Менеджеры"
            mngrs.Show();
        }

        //Пункт меню «Услуги»
        private void servicesBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Services srvcs = new Services(); //экземпляр формы "Услуги"
            srvcs.Show();
        }

        //Обновление таблицы с заявками/договорами и связанных с ней таблиц
        private void refreshBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            DBConnection.GetRequests();
            requestsAndContractsTable.DataSource = DBConnection.dtRequests;
            PaintGrid();
            if (DBConnection.dtRequests.Rows.Count > 0)
            {
                ReadRequestsTableRow(0);
            }
            UpdateRelatedData(0);
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }
        }

        //Цветовая визуализация таблицы после сортировки
        private void requestsAndContractsTable_Sorted(object sender, EventArgs e)
        {
            PaintGrid();
        }

        //Вызов формы настройки Email уведомлений
        private void notifications_Click(object sender, EventArgs e)
        {
            try
            { 
            DBConnection.GetDocs(requestNum);
            if (DBConnection.dtDocs.Rows.Count > 2)
            {
                if (docsTable.Rows[0].Cells[3].Value.ToString() != "")
                {
                    Notifications.contractNum = contractNum;
                    Notifications.email = email;
                    Notifications ntfc = new Notifications();
                    ntfc.Show();
                    return;
                }
            }
            MessageBox.Show("Настраивать уведомления можно только для подписанных договоров!");
            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }

        }

        //Смена стиля надписи при наведении
        private void notifications_MouseMove(object sender, MouseEventArgs e)
        {
            notifications.Font = new Font(notifications.Font.Name, 12, FontStyle.Bold | FontStyle.Underline);
        }

        //Возврат исходного стиля надписи
        private void notifications_MouseLeave(object sender, EventArgs e)
        {
            notifications.Font = new Font(notifications.Font.Name, 12, FontStyle.Regular | FontStyle.Underline);
        }

        private void backgroundWorker17_DoWork(object sender, DoWorkEventArgs e)
        {
            PaintGrid();
        }

        //Закрытие панели ошибок
        private void label22_Click_2(object sender, EventArgs e)
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
        private void logoutPnl_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization.form.Show();
        }

        //Переподключение к базе данных
        private void reconnectBtn_Click_1(object sender, EventArgs e)
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
