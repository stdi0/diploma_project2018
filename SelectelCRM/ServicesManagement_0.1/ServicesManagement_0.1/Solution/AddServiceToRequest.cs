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
    //Форма добавления услуги в заявку
    public partial class AddServiceToRequest : Form
    {
        public AddServiceToRequest()
        {
            InitializeComponent();
        }

        public float cost; //стоимость услуги

        //Добавление новой услуги в заявку, обновление данных в связанных таблицах
        private void addServiceToRequestBtn_Click(object sender, EventArgs e)
        {
            try
            { 
            if (service.Text == "" || servicesGroup.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            if (dateEnd.Value <= dateStart.Value)
            {
                MessageBox.Show("Дата окончания не может быть меньше или равна дате начала!");
                return;
            }
            DBConnection.NewServiceInRequest(service.SelectedValue.ToString(), dateStart.Value.ToString("yyyy-MM-dd"), dateEnd.Value.ToString("yyyy-MM-dd"), cost.ToString().Replace(',','.'));
            DBConnection.GetServicesInRequest(Form1.requestNum);
            Form1.form.servicesInRequestTable.DataSource = DBConnection.dtServicesInRequest;
            Form1.ReadServicesInRequestTableRow(0);
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Происходит при загрузке формы, настройка элементов управления
        private void AddServiceToRequest_Load(object sender, EventArgs e)
        {
            try
            {
                DBConnection.GetServices();
                DBConnection.GetServicesGroups();
                servicesGroup.DataSource = DBConnection.dtServicesGroups;
                servicesGroup.DisplayMember = "Наименование";
                servicesGroup.ValueMember = "ID";

                DBConnection.GetServicesInGroup(servicesGroup.SelectedValue.ToString());
                service.DataSource = DBConnection.dtServicesInGroup;
                service.DisplayMember = "Название";
                service.ValueMember = "id";

                dateStart.Value = Convert.ToDateTime("2018-12-19");
                dateEnd.Value = Convert.ToDateTime("2019-01-19");
                calculate_cost_and_display();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); } 
        }

        //Расчет итоговой стоимость услуги и вывод в Label
        private void calculate_cost_and_display()
        {
            try
            {
                DataRow[] dtRow = DBConnection.dtServices.Select("id = '" + service.SelectedValue.ToString() + "'");
                //TimeSpan period = dateTimePicker2.Value.Date - dateTimePicker1.Value.Date;
                //int date_start = 1;
                double months = 0; //количество месяцев
                int start = dateStart.Value.Day - 1; //день начала действия услуги в месяце
                int start_month = dateStart.Value.Month; //месяц начала действия услуги в году
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
                cost = Convert.ToInt32(dtRow[0]["cost"]) * (float)months;
                label3.Text = String.Format("{0:0.##} мес. x {1} руб. = {2:0.##} руб.", months, dtRow[0]["cost"], cost);
            }
            catch { }
        }

        //Вызов процедуры расчета и вывода итоговой стоимости
        private void service_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculate_cost_and_display();
        }

        ////Вызов процедуры расчета и вывода итоговой стоимости
        private void dateStart_ValueChanged(object sender, EventArgs e)
        {
            calculate_cost_and_display();
        }

        //Вызов процедуры расчета и вывода итоговой стоимости
        private void dateEnd_ValueChanged(object sender, EventArgs e)
        {
            calculate_cost_and_display();
        }

        //Вызов процедуры расчета и вывода итоговой стоимости
        private void servicesGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (servicesGroup.SelectedValue != null)
                {
                    DBConnection.GetServicesInGroup(servicesGroup.SelectedValue.ToString());
                    service.DataSource = DBConnection.dtServicesInGroup;
                    service.DisplayMember = "Название";
                    service.ValueMember = "id";
                }
                calculate_cost_and_display();
            }
            catch { }
        }
    }
}
