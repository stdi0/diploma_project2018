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
    //Форма "Аналитика"
    public partial class Stats : Form
    {
        public Stats()
        {
            InitializeComponent();
        }

        //периоды (месяцы) продаж
        public List<float> x = new List<float>();
        //объемы продаж
        public List<float> y = new List<float>();
        //коэффициент сезонности
        public List<double> err = new List<double>();

        //Составление графика прогноза продаж на N месяцев
        public void prediction(double intercept, double slope, List<double> err, int num_months)
        {
            chart1.Series[1].Points.Clear();
            chart1.Series[1].Points.Dispose();
            chart1.Series[1].Points.AddXY(x[x.Count() - 1], y[y.Count() - 1]);

            for (int i = 1; i <= num_months; i++)
            {
                double value = (intercept + slope * (i + x.Count())) * err[i - 1]; //предсказанное значение
                chart1.Series[1].Points.AddXY(i + x.Count(), value);
            }

        }

        //Построение линии тренда
        public void trend_line(double intercept, double slope, int end_x)
        {
            chart1.Series[2].Points.Clear();
            chart1.Series[2].Points.Dispose();
            chart1.Series[2].Points.AddXY(1, intercept + slope * 1);
            chart1.Series[2].Points.AddXY(end_x, intercept + slope * end_x);
        }

        //Построение графика продаж
        public void sales_chart(List<float> x, List<float>y)
        {
            for (int i = 0; i < x.Count(); i++)
            {
                chart1.Series[0].Points.AddXY(x[i], y[i]);
            }
            chart1.Series[0].Name = "Продажи за год";
            chart1.Series[0].BorderWidth = 3;
        }

        //Происходит при загрузке формы, первичное построение графика продаж, линии тренда, прогноза
        private void Stats_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    chart1.Series[0].Points.Clear();
                    chart1.Series[0].Points.Dispose();
                    chart1.Series[1].Points.Clear();
                    chart1.Series[1].Points.Dispose();
                    chart1.Series[2].Points.Clear();
                    chart1.Series[2].Points.Dispose();
                        }
                catch { }
                DBConnection.GetSellingsYears();
                salesYears.DataSource = DBConnection.dtSellingsYears;
                salesYears.DisplayMember = "year";
                salesYears.ValueMember = "year";

                DBConnection.GetSellings();
                reconnectBtn.Font = new Font(reconnectBtn.Font.Name, 10, FontStyle.Regular | FontStyle.Underline);

                x.Clear();
                y.Clear();
                foreach (DataRow row in DBConnection.dtSellings.AsEnumerable())
                {
                    x.Add(Convert.ToSingle(row[0]));
                    y.Add(Convert.ToSingle(row[1]));
                }
                LinearRegression.calculate_regression(x, y);

                label20.Text = LinearRegression.mean_x.ToString("0.##");
                label21.Text = LinearRegression.mean_y.ToString("0.##");
                label22.Text = LinearRegression.std_deviation_x.ToString("0.##");
                label23.Text = LinearRegression.std_deviation_y.ToString("0.##");
                label2.Text = LinearRegression.covariation.ToString("0.##");
                label4.Text = LinearRegression.correlation.ToString("0.##");

                for (int i = 0; i < 12; i++)
                {
                    try
                    {
                        err.Add(y[i] / (y.Sum() / y.Count()));
                    }
                    catch
                    {
                        err.Add(1);
                    }
                }

                sales_chart(x, y);

                chart1.Series.Add("Прогноз");
                chart1.Series[1].BorderWidth = 3;
                chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                prediction(LinearRegression.intercept, LinearRegression.slope, err, 6);

                chart1.Series.Add("Линия тренда");
                chart1.Series[2].BorderWidth = 3;
                chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                int end_x = chart1.Series[0].Points.Count + chart1.Series[1].Points.Count;
                trend_line(LinearRegression.intercept, LinearRegression.slope, end_x);

                chart1.ChartAreas["ChartArea1"].AxisX.Title = "Номер периода";
                chart1.ChartAreas["ChartArea1"].AxisY.Title = "Сумма";

            }
            catch (Exception ex)
            {
                exceptPanel.Visible = true;
                richTextBox1.Text = ex.ToString();
            }

        }

        //Расчет новых параметров регрессии и построение нового прогноза вместе с линией тренда
        private void predictionMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    chart1.Series[0].Points.Clear();
                    chart1.Series[0].Points.Dispose();
                    chart1.Series[1].Points.Clear();
                    chart1.Series[1].Points.Dispose();
                    chart1.Series[2].Points.Clear();
                    chart1.Series[2].Points.Dispose();
                }
                catch { }
                DBConnection.GetSellings();
                x.Clear();
                y.Clear();
                foreach (DataRow row in DBConnection.dtSellings.AsEnumerable())
                {
                    x.Add(Convert.ToSingle(row[0]));
                    y.Add(Convert.ToSingle(row[1]));
                }         
                LinearRegression.calculate_regression(x, y);
                label20.Text = LinearRegression.mean_x.ToString("0.##");
                label21.Text = LinearRegression.mean_y.ToString("0.##");
                label22.Text = LinearRegression.std_deviation_x.ToString("0.##");
                label23.Text = LinearRegression.std_deviation_y.ToString("0.##");
                label2.Text = LinearRegression.covariation.ToString("0.##");
                label4.Text = LinearRegression.correlation.ToString("0.##");
                for (int i = 0; i < 12; i++)
                {
                    try
                    {
                        err.Add(y[i] / (y.Sum() / y.Count()));
                    }
                    catch
                    {
                        err.Add(1);
                    }
                }
                sales_chart(x, y);

                prediction(LinearRegression.intercept, LinearRegression.slope, err, Convert.ToInt32(predictionMonths.Text));
                int end_x = chart1.Series[0].Points.Count + Convert.ToInt32(predictionMonths.Text); //последняя точка линии тренда
                trend_line(LinearRegression.intercept, LinearRegression.slope, end_x);
                chart1.Invalidate();
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

        //Пункт меню “Менеджеры”
        private void managersBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Managers mngrs = new Managers(); //экземпляр формы "Менеджеры"
            mngrs.Show();
            
        }

        //Пункт меню “Услуги”
        private void servicesBtn_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void label3_Click(object sender, EventArgs e)
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
        private void logoutPanel_Click(object sender, EventArgs e)
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
        private void Stats_FormClosed(object sender, FormClosedEventArgs e)
        {
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

        //Расчет новых параметров регрессии и построение нового графика продаж вместе с линией тренда
        private void displaySalesGraph_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    chart1.Series[0].Points.Clear();
                    chart1.Series[0].Points.Dispose();
                    chart1.Series[1].Points.Clear();
                    chart1.Series[1].Points.Dispose();
                    chart1.Series[2].Points.Clear();
                    chart1.Series[2].Points.Dispose();
                }
                catch { }
                DBConnection.GetSellingsByYear(salesYears.Text);
                x.Clear();
                y.Clear();
                foreach (DataRow row in DBConnection.dtSellingsByYear.AsEnumerable())
                {
                    x.Add(Convert.ToSingle(row[0]));
                    y.Add(Convert.ToSingle(row[1]));
                }
                sales_chart(x, y);

                LinearRegression.calculate_regression(x, y);

                label20.Text = LinearRegression.mean_x.ToString("0.##");
                label21.Text = LinearRegression.mean_y.ToString("0.##");
                label22.Text = LinearRegression.std_deviation_x.ToString("0.##");
                label23.Text = LinearRegression.std_deviation_y.ToString("0.##");
                label2.Text = LinearRegression.covariation.ToString("0.##");
                label4.Text = LinearRegression.correlation.ToString("0.##");

                int end_x = Convert.ToInt32(x.Max()); //конечная точка линии тренда
                trend_line(LinearRegression.intercept, LinearRegression.slope, end_x);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
