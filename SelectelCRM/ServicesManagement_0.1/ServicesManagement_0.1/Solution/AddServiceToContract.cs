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
    //Форма добавления услуги в договор
    public partial class AddServiceToContract : Form
    {
        public AddServiceToContract()
        {
            InitializeComponent();
        }

        static public string serviceNum; //номер услуги
        static public string serviceDateStart; //дата начала действия услуги
        static public string serviceDateEnd; //дата окончания действия услуги
        static public string price; //цена услуги

        public float cost; //стоимость услуги

        //Расчет итоговой стоимость услуги и вывод в Label
        private void calculate_cost_and_display()
        {
            try
            {
                double months = 0; //количесво месяцев
                int start = dateStart.Value.Day - 1; //день начала действия услуги 
                int start_month = dateStart.Value.Month; //месяц начала действия услуги
                int days; //количество дней
                for (int i = dateStart.Value.Year; i <= dateEnd.Value.Year; i++)
                {
                    int end_month = 12; //последний месяц действия услуги в году
                    if (i == dateEnd.Value.Year)
                    {
                        end_month = dateEnd.Value.Month;
                    }
                    for (int j = start_month; j <= end_month; j++)
                    {
                        if (j == dateEnd.Value.Month && i == dateEnd.Value.Year)
                        {
                            days = dateEnd.Value.Day - start;
                        }
                        else
                        {
                            days = DateTime.DaysInMonth(i, j) - start;
                        }
                        start = 0;
                        float result = (float)days / (float)DateTime.DaysInMonth(i, j); //количество месяцев
                        months += result;
                    }
                    start_month = 1;
                }
                cost = Convert.ToSingle(price) * (float)months;
                label2.Text = String.Format("{0:0.##} мес. x {1} руб. = {2:0.##} руб.", (float)months, price, cost);
            }
            catch { }
        }

        //Происходит при загрузке формы, настройка элементов управления
        private void AddServiceToContract_Load(object sender, EventArgs e)
        {
            try
            {
                dateStart.Value = Convert.ToDateTime(serviceDateStart);
                dateEnd.Value = Convert.ToDateTime(serviceDateEnd);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            calculate_cost_and_display();

        }

        //Добавление услуги в договор, создание связанных документов
        private void addServiceToContractBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (dateEnd.Value <= dateStart.Value)
            {
                MessageBox.Show("Дата окончания не может быть меньше или равна дате начала!");
                return;
            }
            DBConnection.NewServiceInContract(serviceNum, cost.ToString().Replace(',','.'), dateStart.Value.ToString("yyyy-MM-dd"), dateEnd.Value.ToString("yyyy-MM-dd"));
            
            int start_month = dateStart.Value.Month; //месяц начала действия услуги в году
            for (int i = dateStart.Value.Year; i <= dateEnd.Value.Year; i++)
            {
                int end_month = 12; //последний месяц действия услуги в году
                if (i == dateEnd.Value.Year)
                {
                    end_month = dateEnd.Value.Month;
                }

                for (int j = start_month; j <= end_month; j++)
                {
                    int end_day = DateTime.DaysInMonth(i, j); //последний день предоставления услуги в месяце
                    if (j == end_month && i == dateEnd.Value.Year)
                    {
                        end_day = dateEnd.Value.Day;
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
                    object data = DBConnection.msCommand.ExecuteScalar(); //результат запрос
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
            DBConnection.GetServicesInContract(Form1.requestNum);
            Form1.form.servicesInContractTable.DataSource = DBConnection.dtServicesInContract;
            if (DBConnection.dtServicesInContract.Rows.Count > 0)
            {
                Form1.ReadServicesInContractTableRow(0);
            }
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Вызов процедуры расчета и вывода итоговой стоимости
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            calculate_cost_and_display();
        }

        //Вызов процедуры расчета и вывода итоговой стоимости
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            calculate_cost_and_display();
        }
    }
}
