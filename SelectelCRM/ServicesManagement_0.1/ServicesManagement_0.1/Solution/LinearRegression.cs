using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Solution
{
    //Вычисление параметров линейной регрессии
    class LinearRegression
    {
        //Значения независимой переменной (предиктора)
        static public List<float> x;
        //Значения зависимой (предсказуемой) переменной
        static public List<float> y;
        //Среднее x, среднее y, ковариация
        static public float mean_x, mean_y, covariation;
        //Стандартные отклонения, коэффициент корреляции, коэффициенты линейной регрессии 
        static public double std_deviation_x, std_deviation_y, correlation, slope, intercept;

        //Вычисление линейных коэффициентов регрессии
        static public void calculate_regression(List<float> _x, List<float> _y)
        {
            x = _x;
            y = _y;
            std_deviation();
            get_correlation();
            slope = correlation * (std_deviation_y / std_deviation_x);
            intercept = mean_y - slope * mean_x;

        }

        //Расчет значения несмещенной дисперсии
        static private double unbiased_variance(List<float> n, float mean)
        {
            double sum = 0; //вспомогательная переменная для расчетов
            foreach (float i in n)
            {
                sum += Math.Pow(i - mean, 2);
            }
            return sum / (n.Count()); // -1
        }
        
        //Расчет стандартного отклонения для x и y
        static public void std_deviation()
        {
            mean_x = (float)x.Sum() / x.Count();
            mean_y = (float)y.Sum() / y.Count();
            std_deviation_x = Math.Sqrt(unbiased_variance(x, mean_x));
            std_deviation_y = Math.Sqrt(unbiased_variance(y, mean_y));
        }

        //Вычисление значений ковариации и коэффициента корреляции
        static public void get_correlation()
        {
            covariation = (float)x.Zip(y, (a, b) => (a - mean_x) * (b - mean_y)).Sum() / x.Count();
            correlation = covariation / (std_deviation_x * std_deviation_y);
        }
    }
}
